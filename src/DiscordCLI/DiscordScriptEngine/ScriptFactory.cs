using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace DScriptEngine {
    public class ScriptFactory {

        public ScriptFactory(DiscordClient client) {
            if (client == null) {
                throw new NullReferenceException("DiscordClient can not be null");
            }
            if (client.GatewaySocket.State == ConnectionState.Disconnected) {
                throw new ServerException("DiscordClient not connected");
            }
            Global.client = client;
        }
        private List<DCommand> Commands { get; set; }
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
            Commands = new List<DCommand>();
            foreach (var line in script) {
                if (line.StartsWith("//")) continue;
                var cmd = new DCommand
                {
                    Command = util.GetCommandFromString(line.Split(' ')[0]),
                    Parameters = parser.GetCommandParameters(line)
                };
                Commands.Add(cmd);
            }
            if (!Commands.Any()) {
                Logger.Log(string.Format(MessageStore.EmptyScriptFile, path), LogLevel.Error);
            }
            Logger.Log($"Loaded {Commands.Count} commands");
            RunScript();
        }
        public void RunScript() {
            if (Commands == null) {
                return;
            }
            foreach (var cmd in Commands) {
                Logger.Log(string.Format(MessageStore.RunningCommand, cmd.Command));
                var engine = new ScriptEngine(null);
                engine.Run(cmd);
            }
        }
    }
}
