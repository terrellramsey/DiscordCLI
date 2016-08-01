using DScriptEngine.Syntax;
using System;
using System.ComponentModel;
using DScriptEngine.Enums;
using static DScriptEngine.Commands;

namespace DScriptEngine {
    internal class CommandHandler {
        public void Process(string input) {
            //Rebuild to support scripts
            var args = input.Split(' ');
            var utils = new StringUtils();
            var cHandler = new CommandHandler();
            var parser = new CommandParser();
            var com = new DCommand();
            var cmd = utils.GetCommandFromString(args[0]);
            if (cmd == Command.Error) {
                Console.WriteLine($"{args[0]} not found. Use Show-Help for commands");
                return;
            }
            com.Command = cmd;
            com.Parameters = parser.GetCommandParameters(input);
            Execute(com);
        }

        public void RunCommand(DCommand cmd) {
            Execute(cmd);
        }
        private void Execute(DCommand com) {
            var dHandler = new DiscordHandler();
            Logger.Log(string.Format(MessageStore.RunningCommand, com.Command));
            switch (com.Command) {
                case Command.UseServer:
                    var res = dHandler.UseServer(com);
                    Logger.LogResult(res);
                    if (res.Result == Result.Ok) {
                        SyntaxSettings.ConsoleKey = SyntaxSettings.ConsoleOriginalKey + $"({res.Message}) > ";
                        //SyntaxSettings.ConsoleKey = SyntaxSettings.ConsoleOriginalKey + $"({args[1]}) > ";  
                    }
                    break;
                case Command.CreateServer:
                    var result = dHandler.CreateServer(com);
                    Logger.LogResult(result);
                    break;
                case Command.ShowHelp:
                   
                    break;
                case Command.ShowServer:
                    break;
                case Command.RemoveServer:
                    break;
                case Command.EditServer:
                    break;
                case Command.SaveServer:
                    break;
                case Command.CreateInvite:
                    break;
                case Command.ShowText:
                    break;
                case Command.CreateText:
                    break;
                case Command.RemoveText:
                    break;
                case Command.EditText:
                    break;
                case Command.SaveText:
                    break;
                case Command.ShowVoice:
                    break;
                case Command.CreateVoice:
                    break;
                case Command.RemoveVoice:
                    break;
                case Command.EditVoice:
                    break;
                case Command.SaveVoice:
                    break;
                case Command.ShowUsers:
                    break;
                case Command.BanUser:
                    break;
                case Command.KickUser:
                    break;
                case Command.SaveUsers:
                    break;
                case Command.ShowRoles:
                    break;
                case Command.CreateRole:
                    break;
                case Command.RemoveRole:
                    break;
                case Command.EditRole:
                    break;
                case Command.SaveRole:
                    break;
                case Command.Error:
                    break;
                case Command.Clear:
                    break;
                default:
                    Logger.Log(string.Format(MessageStore.CommandNotFound, com.Command), LogLevel.Error);
                    break;
            }
        }
    }


}
