using System;
using static DScriptEngine.Commands;

namespace DScriptEngine {
    public class StringUtils {
        public Command GetCommandFromString(string input) {
            Command cmd = Command.Error;
            try {
                cmd = (Command)Enum.Parse(typeof(Command), string.Concat(input.Split('-')));
                if (!Enum.IsDefined(typeof(Command), cmd)) {
                    cmd = Command.Error;
                }
            }
            catch (ArgumentException ex) {
                cmd = Command.Error;
                Global.logger.Log(NLog.LogLevel.Fatal, ex.Message);
                //Logger.Log(ex.Message, LogLevel.Error);
            }
            return cmd;
        }
        public ServerRegion GetRegionFromString(string region) {
            ServerRegion reg = ServerRegion.UNKNOWN;
            try {
                reg = (ServerRegion)Enum.Parse(typeof(ServerRegion), region);
                if (!Enum.IsDefined(typeof(ServerRegion), reg)) {
                    reg = ServerRegion.UNKNOWN;
                }
            }
            catch (ArgumentException ex) {
                reg = ServerRegion.UNKNOWN;
                Global.logger.Log(NLog.LogLevel.Fatal, ex.Message);
                //Logger.Log(ex.Message, LogLevel.Error);
            }
            return reg;
        }

        public CommandParameter GetCommandParameter(string name) {
            CommandParameter param = CommandParameter.Unknown;
            try {
                param = (CommandParameter)Enum.Parse(typeof(CommandParameter), name);
                if (!Enum.IsDefined(typeof(CommandParameter), param)) {
                    param = CommandParameter.Unknown;
                }
            }
            catch (ArgumentException ex) {
                param = CommandParameter.Unknown;
                //Logger.Log(ex.Message, LogLevel.Error);
                Global.logger.Log(NLog.LogLevel.Fatal, ex.Message);
            }
            return param;
        }
    }
}