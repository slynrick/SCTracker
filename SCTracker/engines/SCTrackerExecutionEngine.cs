using Neo;
using Neo.VM;
using SCTracker.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SCTracker.Engines
{
    public class SCTrackerExecutionEngine : ExecutionEngine
    {
        private RichTextBox Logger = null;

        public SCTrackerExecutionEngine(RichTextBox LogOutput, IScriptContainer container, ICrypto crypto, IScriptTable table = null, InteropService service = null)
            : base(container, crypto, table, service)
        {
            this.Logger = LogOutput;
        }

        public void SCTrackerExecute()
        {
            State &= ~VMState.BREAK;
            while (!State.HasFlag(VMState.HALT) && !State.HasFlag(VMState.FAULT) && !State.HasFlag(VMState.BREAK))
            {
                if (this.CurrentContext.Script.Length > this.CurrentContext.InstructionPointer)
                {
                    String log = "\t\t" + ((byte)this.CurrentContext.NextInstruction).ToString("X2") + " -> " + this.CurrentContext.NextInstruction + "\n";
                    if(State.HasFlag(VMState.FAULT))
                        this.Logger.CreateLogMessage(log, Utils.Helper.MessageClass.Error);
                    else
                        this.Logger.CreateLogMessage(log, Utils.Helper.MessageClass.Information);
                }
                StepInto();
            }
        }

        public ExecutionContext SCTrackerLoadScript(byte[] script, int rvcount = -1)
        {
            ExecutionContext context = this.LoadScript(script, rvcount);
            string scripthash = this.CurrentContext.ScriptHash.ToHexString();
            Logger.CreateLogMessage(($"Contract Scripthash: 0x{scripthash} \n"), Utils.Helper.MessageClass.Information);
            return context;
        }

        public String GetScriptHash() => this.CurrentContext.ScriptHash.ToHexString();

    }
}
