using DScriptEngine.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DScriptEngine {
    public class Loggerr {
        public static EventHandler<OnLoggedMessage> OnLoggedMessage;


        private static LogOutput output = LogOutput.Console;
        public enum LogOutput {
            Console,
            String
        }
        public static string Log(string message, LogLevel level = LogLevel.Info) {
            if (output == LogOutput.Console) {
                Console.WriteLine(GetLogFormat(message, level));
            }
            else {
                return GetLogFormat(message, level);
            }
            return string.Empty;
        }
        internal static string GetLogFormat(string message, LogLevel level) {
            var toReturn = $"[{DateTime.Now.ToString(CultureInfo.InvariantCulture)}]" + "[{0}]: {1}";
            switch (level) {
                case LogLevel.Info:
                    toReturn = string.Format(toReturn, "Info", message);
                    break;
                case LogLevel.Warning:
                    toReturn = string.Format(toReturn, "Warning", message);
                    break;
                case LogLevel.Error:
                    toReturn = string.Format(toReturn, "Error", message);
                    break;
            }
            return toReturn;
        }

        internal static void LogResult(DResult result) {
            if (result.Result == Result.Error) {
                Log(result.Message, LogLevel.Error);
               // OnLoggedMessage.Emit(null, new OnLoggedMessage(result));
            }
            else {
                Log(result.Message, LogLevel.Info);
            }
        }
    }
}
