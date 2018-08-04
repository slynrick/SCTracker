using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SCTracker.Utils
{
    internal static class Helper
    {
        public enum MessageClass
        {
            Information,
            Warning,
            Error
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
                default:
                    return System.Drawing.Color.DarkGray;
            }
        }

        public static String GenerateTimeStampFormat(this DateTime DT)
        {
            return "[" + DT.ToString("yyyy-MM-dd HH:mm:ss.ffff") + "]";
        }

        public static void CreateLogMessage(this System.Windows.Forms.RichTextBox box, String LogMessage, MessageClass Class)
        {
            String preffix = DateTime.Now.GenerateTimeStampFormat() + Class.GenerateString() + ": ";
            LogMessage = preffix + LogMessage;

            box.SelectionStart = box.Text.Length;
            box.SelectionColor = Class.GetMessageClassColor();
            box.AppendText(LogMessage);
            box.SelectionColor = box.ForeColor;
        }
    }
}
