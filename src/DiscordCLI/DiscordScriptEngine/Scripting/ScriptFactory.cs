using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Discord;

namespace DScriptEngine {
    public class ScriptFactory {

        public ScriptFactory(DiscordClient client) {
            if (client == null) {
                throw new NullReferenceException(MessageStore.ClientNotSet);
            }
            if (client.GatewaySocket.State == ConnectionState.Disconnected) {
                throw new ServerException(MessageStore.ClientNotConnected);
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
                if (line.StartsWith("svar") || line.StartsWith("ivar") || line.StartsWith("arr") || line.StartsWith("lvar")) {
                    if (Variables == null) { Variables = new Dictionary<string, IVariable>(); }
                    //TODO: Cast value to value.
                    var vari = LoadVariable(line);
                    if (vari != null) {
                        var name = vari.GetName();
                        var variType = vari.GetType();
                        if (Variables.ContainsKey(name)) {
                            throw new Exception(string.Format(MessageStore.VariableDefined, name));
                        }
                        Variables.Add(vari.GetName(), vari);
                        Logger.Log(string.Format(MessageStore.LoadedVariable, name, variType));
                    }
                    continue;
                }
                var cmd = new DCommand {
                    Command = util.GetCommandFromString(line.Split(' ')[0]),
                    Parameters = parser.GetCommandParameters(line, Variables)
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
            else if (input.StartsWith("arr")) {
                //TODO: Fix the stuff here!
                var value = input.RemoveFirst("arr ");
                var parts = value.Split(' ');
                var rawarray = parts[1].RemoveFirst("(").TrimEnd(')');
                var array = rawarray.Split(',');
                //var parts = value.Split(' ');
                //var value = value1.TrimEnd(')');
                if (array.Length != 0) {
                    thing = new DArray(parts[0], array);
                }
            }
            else if(input.StartsWith("lvar")) {
                var removeType = input.RemoveFirst("lvar ");
                var parts = removeType.Split(' ');
                ulong outInt;
                if (ulong.TryParse(parts[1], out outInt)) {
                    thing = new DULong(parts[0], outInt);
                }
                else {
                    throw new FormatException("The value is not an of type ulong");
                }
            }
            return thing;
        }
    }
}
