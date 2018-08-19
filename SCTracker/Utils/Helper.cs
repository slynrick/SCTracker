using Neo;
using Neo.VM;
using SCTracker.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCTracker.Utils
{
    internal static class Helper
    {
        public enum MessageClass
        {
            Information,
            Warning,
            Error,
            Success
        };

       
        public static bool IsHex(this String s)
        {
            return Regex.IsMatch(s, @"\A\b[0-9a-fA-F]+\b\Z");
        }

        public static String GenerateString(this MessageClass Class)
        {
            switch (Class)
            {
                case MessageClass.Information:
                    return "[INFO]";
                case MessageClass.Warning:
                    return "[WARN]";
                case MessageClass.Error:
                    return "[ERRO]";
                case MessageClass.Success:
                    return "[SUCC]";
                default:
                    return "[UNKW]";
            }
        }

        public static System.Drawing.Color GetMessageClassColor(this MessageClass Class)
        {
            switch (Class)
            {
                case MessageClass.Information:
                    return System.Drawing.Color.DarkBlue;
                case MessageClass.Warning:
                    return System.Drawing.Color.DarkGoldenrod;
                case MessageClass.Error:
                    return System.Drawing.Color.Red;
                case MessageClass.Success:
                    return System.Drawing.Color.ForestGreen;
                default:
                    return System.Drawing.Color.DarkGray;
            }
        }

        public static String GenerateTimeStampFormat(this DateTime DT)
        {
            return "[" + DT.ToString("yyyy-MM-dd HH:mm:ss.ffff") + "]";
        }

        public static void CreateLogMessage(this RichTextBox box, String LogMessage, MessageClass Class)
        {
            String preffix = DateTime.Now.GenerateTimeStampFormat() + Class.GenerateString() + ": ";
            LogMessage = preffix + LogMessage;

            box.SelectionStart = box.Text.Length;
            box.SelectionColor = Class.GetMessageClassColor();
            box.AppendText(LogMessage);
            box.SelectionColor = box.ForeColor;
        }

        public static ScriptBuilder GetScriptBuilder(this DataGridView ParameterTable, RichTextBox Logger, SCTrackerExecutionEngine engine)
        {
            ScriptBuilder sb = new ScriptBuilder();
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
                Logger.CreateLogMessage(($"[Contract:0x{engine.GetScriptHash()}] Add parameter {type} = {value}  \n"), Utils.Helper.MessageClass.Information);
            }
            return sb;
        }
    }
}
