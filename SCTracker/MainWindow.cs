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
using System.Globalization;

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
                TrackerOutputText.CreateLogMessage("Not Hex String (Inappropriate Length)\n", Utils.Helper.MessageClass.Error);
            if (!ScriptText.Text.IsHex())
                TrackerOutputText.CreateLogMessage("Not Hex String (Inappropriate Characters)\n", Utils.Helper.MessageClass.Error);
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
            ParameterTable.Rows[0].Selected = true;

            new Thread(() => {
                this.Invoke((MethodInvoker)delegate ()
                {
                    TrackerOutputText.CreateLogMessage(($"Running CheckOpcodes with Thread {Thread.CurrentThread.ManagedThreadId.ToString()} \n"), Utils.Helper.MessageClass.Information);

                    var engine = new ExecutionEngine(null, Crypto.Default);
                    engine.LoadScript(ScriptText.Text.HexToBytes()); // loads avm

                    string scripthash = engine.CurrentContext.ScriptHash.ToHexString();
                    TrackerOutputText.CreateLogMessage(($"Contract Scripthash: 0x{scripthash} \n"), Utils.Helper.MessageClass.Information);


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
                            TrackerOutputText.CreateLogMessage(($"[Contract:0x{scripthash}] Add parameter {type} = {value}  \n"), Utils.Helper.MessageClass.Information);
                        }
                        engine.LoadScript(sb.ToArray());
                        TrackerOutputText.CreateLogMessage(($"[Contract:0x{scripthash}] Added parameters as hex = {sb.ToArray().ToHexString()}  \n"), Utils.Helper.MessageClass.Information);
                    }

                    engine.Execute(); // start execution

                    string state = engine.State == VMState.HALT ? "OK" : "FAIL";
                    string stateMessage = $"[Contract:0x{scripthash}] Result = {state} \n";
                    Utils.Helper.MessageClass mClass = Utils.Helper.MessageClass.Information;
                    OpCode currentOpCode = OpCode.NOP;
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

                        currentOpCode = (OpCode)Convert.ToByte(ScriptText.Text.Substring(pos, 2), 16);
                        mClass = Utils.Helper.MessageClass.Error;

                        result = currentOpCode.ToString() + " at position " + pos / 2 + "\n";
                        result += "\tTracking: \n";
                        for (int i = 0; i <= pos; i += 2)
                        {
                            OpCode PreviousOpCode = (OpCode)Convert.ToByte(ScriptText.Text.Substring(i, 2), 16);
                            result += "\t\t" + ScriptText.Text.Substring(i, 2) + " -> " + PreviousOpCode.ToString();
                            if (PreviousOpCode >= OpCode.PUSHBYTES1 && 
                                PreviousOpCode <= OpCode.PUSHBYTES75)
                            {
                                int initialPos = i + 2;
                                int len = (int)PreviousOpCode * 2;
                                result += ScriptText.Text.Substring(initialPos, len).HexToBytes().ToString();
                                i += len;
                            }
                            else if( PreviousOpCode >= OpCode.JMP &&
                                     PreviousOpCode <= OpCode.JMPIFNOT )
                            {
                                int initialPos = i + 2;
                                result += ScriptText.Text.Substring(initialPos, 4).HexToBytes().ToString();
                                int offset = (int)BigInteger.Parse(ScriptText.Text.Substring(initialPos, 4), NumberStyles.AllowHexSpecifier);
                                i = i + (offset * 2) - (3 * 2);
                            }
                            else if (PreviousOpCode == OpCode.CALL)
                            {
                                int initialPos = i + 2;
                                result += ScriptText.Text.Substring(initialPos, 4).HexToBytes().ToString();
                                i += 2 * 2;
                            }
                            else if (PreviousOpCode == OpCode.APPCALL || PreviousOpCode == OpCode.TAILCALL)
                            {
                                int initialPos = i + 2;
                                result += ScriptText.Text.Substring(initialPos, 60).HexToBytes().ToString();
                                i += 30 * 2;
                            }
                            else if (PreviousOpCode == OpCode.SYSCALL)
                            {
                                int initialPos = i + 2;
                                result += ScriptText.Text.Substring(initialPos, 504).HexToBytes().ToString();
                                i += 252 * 2;
                            }
                            
                            result += "\n";
                        }
                    }
                    else
                    {
                        int pos = engine.InvocationStack.Peek().InstructionPointer * 2 - 2;
                        ScriptText.SelectionStart = pos;
                        ScriptText.SelectionLength = 2;
                        ScriptText.SelectionColor = Color.DarkGoldenrod;

                        currentOpCode = (OpCode)Convert.ToByte(ScriptText.Text.Substring(pos, 2), 16);
                        result = currentOpCode.ToString() + " at position " + pos/2;
                        mClass = Utils.Helper.MessageClass.Warning;
                    }
                    TrackerOutputText.CreateLogMessage(($"[Contract:0x{scripthash}] Result = {result} \n"), mClass);
                    TrackerOutputText.CreateLogMessage(stateMessage, mClass);
                    TrackerOutputText.CreateLogMessage("CheckOpcodes Finishes \n", Utils.Helper.MessageClass.Information);
                });
            }).Start();

            // put it back to be changed
            ScriptText.ReadOnly = false;
        }

    }
}
