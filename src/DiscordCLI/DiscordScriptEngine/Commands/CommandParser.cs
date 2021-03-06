﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DScriptEngine {
    public class CommandParser {
        //TODO: Escape - inside of "
        public Dictionary<CommandParameter, string> GetCommandParameters(string input, Dictionary<string, IVariable> variables = null) {
            var paramDict = new Dictionary<CommandParameter, string>();
            var utils = new StringUtils();
            var cmd = input.Split(' ')[0];
            var rawInput = input;
            var rawParam = rawInput.RemoveFirst(cmd);
            var param = rawParam.Split('-');
            for (var i = 1; i < param.Length; i++) {
                var par = param[i].Split('"');
                var parameter = utils.GetCommandParameter(par[0]);
                if (parameter == CommandParameter.Unknown)
                {
                    Global.logger.Log(NLog.LogLevel.Fatal, "Error getting the parameter");
                    //throw new Exception("Error getting the parameter");
                    break;
                }
                paramDict.Add(utils.GetCommandParameter(par[0]),
                    variables == null ? par[1] : CheckForVariable(par[1], variables));
            }
            //string command = input.Split(' ')[0];
            return paramDict;
        }

        private string CheckForVariable(string input, Dictionary<string, IVariable> variables) {
            //TODO: Match and replace using regex. This is just for testing.
            var words = input.Split(' ');
            foreach (var word in words) {
                if (variables.ContainsKey(word)) {
                    input = input.Replace(word, variables.FirstOrDefault(t => t.Key == word).Value.GetValue().ToString());
                    if (input.Contains('"'))
                    {
                        input = input.RemoveFirst("\"").TrimEnd('"');
                    }
                }
            }
            return input;
        }
    }
}
