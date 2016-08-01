using System;
using static DScriptEngine.Commands;

namespace DScriptEngine {
    public class StringUtils {
        public Command GetCommandFromString(string input) {
            var cmd = (Command)Enum.Parse(typeof(Command), string.Concat(input.Split('-')));
            if (!Enum.IsDefined(typeof(Command), cmd)) {
                cmd = Command.Error;
            }
            return cmd;
        }
        public ServerRegion GetRegionFromString(string region) {
            var reg = (ServerRegion)Enum.Parse(typeof(ServerRegion), region);
            if (!Enum.IsDefined(typeof(ServerRegion), reg)) {
                reg = ServerRegion.UNKNOWN;
            }
            return reg;
        }

        public CommandParameter GetCommandParameter(string name) {
            var param = (CommandParameter)Enum.Parse(typeof(CommandParameter), name);
            if (!Enum.IsDefined(typeof(CommandParameter), param)) {
                param = CommandParameter.Unknown;
            }
            return param;
        }

    }
}