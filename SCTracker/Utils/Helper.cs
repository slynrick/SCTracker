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

        public static byte[] HexToBytes(this String s)
        {
            uint num = uint.Parse(s, System.Globalization.NumberStyles.AllowHexSpecifier);

            return BitConverter.GetBytes(num);
        }

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

        public static String GenerateTimeStampFormat(this DateTime DT)
        {
            return "[" + DT.ToString("yyyy-MM-dd HH:mm:ss.ffff") + "]";
        }

        public static String CreateLogMessage(this String LogMessage, MessageClass Class)
        {
            String preffix = DateTime.Now.GenerateTimeStampFormat() + Class.GenerateString() + ": ";
            return preffix + LogMessage;
        }
    }
}
