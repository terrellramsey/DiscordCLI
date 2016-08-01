using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DScriptEngine {
    public class ScriptFactory {
        private List<DCommand> _commands { get; set; }
        public void LoadScript(string path) {
            var util = new StringUtils();
            var parser = new CommandParser();
            if (!File.Exists(path)) {
                Logger.Log(string.Format(MessageStore.FileNotFound, path));
                return;
            }
            if (Path.GetExtension(path) != "dscript") {
                Logger.Log(string.Format(MessageStore.FileFormatNotValid, path));
                return;
            }
            var script = File.ReadAllLines(path);
            _commands = new List<DCommand>();
            foreach (string line in script) {
                if (!line.StartsWith("//")) {
                    var cmd = new DCommand();
                    cmd.Command = util.GetCommandFromString(line.Split(' ')[0]);
                    cmd.Parameters = parser.GetCommandParameters(line);
                    _commands.Add(cmd);
                }
            }
            if(!_commands.Any()) {
                Logger.Log(string.Format(MessageStore.EmptyScriptFile, path), LogLevel.Error);
            }
            Logger.Log($"Loaded {_commands.Count} commands");
            RunScript();
        }
        public void RunScript() {
            if(_commands == null) {
                return;
            }
            foreach(var cmd in _commands) {
                Logger.Log(string.Format(MessageStore.RunningCommand, cmd.Command));
                var engine = new ScriptEngine(new Discord.DiscordClient());
                engine.Run(cmd);
            }
        }
    }
}
