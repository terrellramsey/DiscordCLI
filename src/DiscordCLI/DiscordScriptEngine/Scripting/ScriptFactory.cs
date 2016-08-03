using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using RestSharp.Extensions;

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
        private Dictionary<string, IVariable> Variables { get; set; }
        public void LoadScript(string path) {
            var stippedPath = path.TrimEnd('"').TrimStart('"');
            var util = new StringUtils();
            var parser = new CommandParser();
            if (!File.Exists(stippedPath)) {
                Logger.Log(string.Format(MessageStore.FileNotFound, stippedPath));
                return;
            }
            if (Path.GetExtension(path) != ".dscript") {
                Logger.Log(string.Format(MessageStore.FileFormatNotValid, stippedPath));
                return;
            }
            var script = File.ReadAllLines(stippedPath);
            Commands = new List<DCommand>();
            foreach (var line in script) {
                if (line.StartsWith("//")) continue;
                if (line.StartsWith("svar") || line.StartsWith("ivar") || line.StartsWith("arr(")) {
                    //TODO: Cast value to value.
                    var vari = LoadVariable(line);
                    if (vari != null) {
                        //TODO: Fix this
                        Variables.Add("", vari);
                    }
                    continue;
                }
                var cmd = new DCommand {
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
                var engine = new ScriptEngine(Global.client);
                engine.Run(cmd);
            }
        }
        public IVariable LoadVariable(string input) {
            var replacedText = string.Empty;
            IVariable thing = null;
            if (input.StartsWith("svar")) {
                //TODO: Check for null...
                var removeType = input.RemoveFirst("svar ");
                var parts = removeType.Split(' ');
                thing = new DString(parts[0], parts[1]);

            }
            else if (input.StartsWith("ivar")) {
                var removeType = input.RemoveFirst("ivar ");
                var parts = removeType.Split(' ');
                int outInt;
                if (int.TryParse(parts[1], out outInt)) {
                    thing = new DInt(parts[0], outInt);
                }
                else {
                    throw new FormatException("The value is not an integer");
                }
            }
            else if (input.StartsWith("arr(") && input.EndsWith(")")) {
                //TODO: Fix the stuff here!
                var value1 = input.RemoveFirst("arr(");
                var value = value1.TrimEnd(')');
                var parts = value.Split(' ');
                var array = parts[1].Split(',');
                if (array.Length != 0) {
                    thing = new DArray(parts[0], array);
                }
            }
            return thing;
        }
    }
}
