using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DScriptEngine.Commands;

namespace DScriptEngine {
    internal class DCommand : ICommand {

        private Command _command { get; set; }
        private Dictionary<CommandParameter, string> _param { get; set; }

        public DCommand(Command cmd, Dictionary<CommandParameter, string> param) {
            _command = cmd;
            _param = param;
        }
        public DCommand() {
          
        }
        public Command Command
        {
            get {
                return _command;
            }
            set { _command = value; }
        }

        public Dictionary<CommandParameter, string> Parameters {
            get {
                return _param;
            }
            set { _param = value; }
        }
       
    }
}
