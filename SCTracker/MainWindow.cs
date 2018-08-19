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
using System.Numerics;
using System.Globalization;

using SCTracker.Utils;
using SCTracker.Engines;

using Neo;
using Neo.VM;
using Neo.Cryptography;

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

            ExecutionEngineMenuItem.Enabled = true;
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

            TrackerOutputText.CreateLogMessage(($"Running CheckOpcodes with Thread {Thread.CurrentThread.ManagedThreadId.ToString()} \n"), Utils.Helper.MessageClass.Information);

            var engine = new SCTrackerExecutionEngine(TrackerOutputText, null, Crypto.Default);
            engine.SCTrackerLoadScript(ScriptText.Text.HexToBytes()); // loads avm

            ScriptBuilder sb = ParameterTable.GetScriptBuilder(TrackerOutputText, engine);
            engine.LoadScript(sb.ToArray());
            TrackerOutputText.CreateLogMessage(($"[Contract:0x{engine.GetScriptHash()}] Added parameters as hex = {sb.ToArray().ToHexString()}  \n"), Utils.Helper.MessageClass.Information);

            TrackerOutputText.CreateLogMessage("\tTracking:\n", Utils.Helper.MessageClass.Information);
            engine.SCTrackerExecute(); // start execution

            string state = engine.State == VMState.HALT ? "OK" : "FAIL";
            string stateMessage = $"[Contract:0x{engine.GetScriptHash()}] Result = {state} \n";
            Utils.Helper.MessageClass mClass = Utils.Helper.MessageClass.Information;
            OpCode currentOpCode = OpCode.NOP;
            string result = "No result";
            if (engine.State == VMState.HALT)
            {
                result = engine.ResultStack.Peek().ToParameter().ToString();
                mClass = Utils.Helper.MessageClass.Success;
            }
            else if (engine.State == VMState.FAULT)
            {
                int pos = engine.InvocationStack.Peek().InstructionPointer * 2 - 2;
                ScriptText.SelectionStart = pos;
                ScriptText.SelectionLength = 2;
                ScriptText.SelectionColor = Color.Red;
                mClass = Utils.Helper.MessageClass.Error;
            }
            else
            {
                int pos = engine.InvocationStack.Peek().InstructionPointer * 2 - 2;
                ScriptText.SelectionStart = pos;
                ScriptText.SelectionLength = 2;
                ScriptText.SelectionColor = Color.DarkGoldenrod;

                currentOpCode = (OpCode)Convert.ToByte(ScriptText.Text.Substring(pos, 2), 16);
                result = currentOpCode.ToString() + " at position " + pos / 2;
                mClass = Utils.Helper.MessageClass.Warning;
            }
            TrackerOutputText.CreateLogMessage(($"[Contract:0x{engine.GetScriptHash()}] Result = {result} \n"), mClass);
            TrackerOutputText.CreateLogMessage(stateMessage, mClass);
            TrackerOutputText.CreateLogMessage("CheckOpcodes Finishes \n", Utils.Helper.MessageClass.Information);

            // put it back to be changed
            ScriptText.ReadOnly = false;
        }

    }
}
