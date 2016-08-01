using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DScriptEngine {
    public class CommandParser {
        public Dictionary<CommandParameter, string> GetCommandParameters(string input) {
            var paramDict = new Dictionary<CommandParameter, string>();
            var utils = new StringUtils();
            var cmd = input.Split(' ')[0];
            var rawInput = input;
            var rawParam = rawInput.RemoveFirst(cmd);
            var param = rawParam.Split('-');
            for (var i = 1; i < param.Length; i++) {
                var par = param[i].Split('"');
                paramDict.Add(utils.GetCommandParameter(par[0]), par[1]);
            }
            //string command = input.Split(' ')[0];
            return paramDict;
        }
    }
}
