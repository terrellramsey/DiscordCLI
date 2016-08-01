using System;
using Discord;

namespace DScriptEngine {
    public class ScriptEngine {
        public ScriptEngine(DiscordClient client) {
            if (client == null) {
                throw new NullReferenceException("DiscordClient can not be null");
            }
            if (client.GatewaySocket.State == ConnectionState.Disconnected) {
                throw new ServerException("DiscordClient not connected");
            }
            Global.client = client;
        }
        #region Server Management
        public void Run(string input) {
            var handler = new CommandHandler();
            handler.Process(input);
        }
        internal void Run(DCommand command) {
            var handler = new CommandHandler();
            handler.RunCommand(command);
        }
        #endregion
    }
}
