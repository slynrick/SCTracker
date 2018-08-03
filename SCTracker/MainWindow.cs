using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using SCTracker.Utils;
using Neo;
using Neo.VM;
using Neo.Cryptography;
using System.Numerics;

namespace SCTracker
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string OpenAVMDlg()
        {
            OpenFileDialog FileDlg = new OpenFileDialog
            {
                Filter = "AVM file (*.avm)|*.avm",
                FilterIndex = 1,
                Multiselect = false,
                RestoreDirectory = true
            };

            if (FileDlg.ShowDialog() == DialogResult.OK)
            {
                return FileDlg.FileName;
            }

            return "";
        }

        private void ReadAVM(String filename, Boolean IsBin)
        {
            if (filename.Length == 0)
                return;
            SCReader x = new SCReader(filename, IsBin);
            ScriptText.Text = x.ReadAllScript();
            OpenedFile.Text = x.ToString();

            CheckOpcodesMenuItem.Enabled = true;
        }

        private void binaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadAVM(OpenAVMDlg(), true);
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadAVM(OpenAVMDlg(), false);
        }

        private void ScriptText_TextChanged(object sender, EventArgs e)
        {
            if (ScriptText.TextLength % 2 != 0)
                TrackerOutputText.Text += "Not Hex String (Inappropriate Length)\n".CreateLogMessage(Utils.Helper.MessageClass.Error);
            if (!ScriptText.Text.IsHex())
                TrackerOutputText.Text += "Not Hex String (Inappropriate Characters)\n".CreateLogMessage(Utils.Helper.MessageClass.Error);
        }

        private void HelpMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SCTracker Beta version 0.1", "Help",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CheckOpcodesMenuItem_Click(object sender, EventArgs e)
        {
            // set it reaonly to not change the script
            ScriptText.ReadOnly = true;
            ParameterTable.ClearSelection();

            new Thread(() => {
                this.Invoke((MethodInvoker)delegate ()
                {
                    TrackerOutputText.Text += ($"Running CheckOpcodes with Thread {Thread.CurrentThread.ManagedThreadId.ToString()} \n").CreateLogMessage(Utils.Helper.MessageClass.Information);

                    var engine = new ExecutionEngine(null, Crypto.Default);
                    engine.LoadScript(ScriptText.Text.HexToBytes()); // loads avm

                    string scripthash = engine.CurrentContext.ScriptHash.ToHexString().ToUpper();
                    TrackerOutputText.Text += ($"Contract Scripthash: 0x{scripthash} \n").CreateLogMessage(Utils.Helper.MessageClass.Information);


                    using (ScriptBuilder sb = new ScriptBuilder()) // loading parameter, from the last to the first
                    {
                        for (int i = ParameterTable.Rows.Count - 2; i >= 0; --i)
                        {
                            string type = ParameterTable.Rows[i].Cells[0].Value.ToString();
                            string value = ParameterTable.Rows[i].Cells[1].Value.ToString();
                            switch (type)
                            {
                                case "byte[]":
                                    sb.EmitPush(value.HexToBytes());
                                    break;
                                case "BigInteger":
                                    sb.EmitPush(BigInteger.Parse(value));
                                    break;
                                case "bool":
                                    sb.EmitPush(Convert.ToBoolean(value));
                                    break;
                                case "string":
                                    sb.EmitPush(value);
                                    break;
                                default:
                                    break;
                            }
                            TrackerOutputText.Text += ($"[Contract:0x{scripthash}] Add parameter {type} = {value}  \n").CreateLogMessage(Utils.Helper.MessageClass.Information);
                        }
                        engine.LoadScript(sb.ToArray());
                        TrackerOutputText.Text += ($"[Contract:0x{scripthash}] Added parameters as hex = {sb.ToArray().ToHexString()}  \n").CreateLogMessage(Utils.Helper.MessageClass.Information);
                    }

                    engine.Execute(); // start execution

                    string state = engine.State == VMState.HALT ? "OK" : "FAIL";
                    string stateMessage = $"[Contract:0x{scripthash}] Result = {state} \n";
                    Utils.Helper.MessageClass mClass = Utils.Helper.MessageClass.Information;
                    string result = "";
                    if ( engine.State == VMState.HALT )
                    {
                        result = engine.EvaluationStack.Peek().ToParameter().ToString();
                        mClass = Utils.Helper.MessageClass.Information;
                    }
                    else if (engine.State == VMState.FAULT )
                    {
                        int pos = engine.InvocationStack.Peek().InstructionPointer * 2 - 2;
                        ScriptText.SelectionStart = pos;
                        ScriptText.SelectionLength = 2;
                        ScriptText.SelectionColor = Color.Red;

                        result = engine.InvocationStack.Peek().NextInstruction.ToString() + " at position " + engine.InvocationStack.Peek().InstructionPointer;
                        mClass = Utils.Helper.MessageClass.Error;
                    }
                    else
                    {
                        int pos = engine.InvocationStack.Peek().InstructionPointer * 2 - 2;
                        ScriptText.SelectionStart = pos;
                        ScriptText.SelectionLength = 2;
                        ScriptText.SelectionColor = Color.DarkGoldenrod;

                        result = engine.InvocationStack.Peek().NextInstruction.ToString() + " at position " + engine.InvocationStack.Peek().InstructionPointer;
                        mClass = Utils.Helper.MessageClass.Warning;
                    }
                    TrackerOutputText.Text += ($"[Contract:0x{scripthash}] Result = {result} \n").CreateLogMessage(mClass);
                    TrackerOutputText.Text += (stateMessage).CreateLogMessage(mClass);
                    TrackerOutputText.Text += "CheckOpcodes Finishes \n".CreateLogMessage(Utils.Helper.MessageClass.Information);
                });
            }).Start();

            // put it back to be changed
            ScriptText.ReadOnly = false;
        }

    }
}
