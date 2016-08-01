using Discord;

namespace DScriptEngine {
    public class ScriptEngine {
        #region Init
        public ScriptEngine(DiscordClient client)
        {
            Global.client = client;
            //   _parameters = GetCommandParameters(Command);
        }
        public ScriptEngine(string Username, string Password, DiscordClient client) {
            // _parameters = GetCommandParameters(Command);
        }
        public ScriptEngine(string Token, DiscordClient client) {
            // _parameters = GetCommandParameters(Command);
        }
        #endregion
        #region Server Management
        public void Run(string input) {
            var handler = new DScriptEngine.CommandHandler();
            handler.Process(input);
        }
        internal void Run(DCommand command) {
            var handler = new CommandHandler();
            handler.RunCommand(command);
        }
        #endregion
    }
}
