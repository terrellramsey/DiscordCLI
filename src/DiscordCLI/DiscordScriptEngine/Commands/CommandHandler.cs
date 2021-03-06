﻿using DScriptEngine.Syntax;
using System;
using System.ComponentModel;
using DScriptEngine.Enums;
using NLog;
using static DScriptEngine.Commands;

namespace DScriptEngine {
    internal class CommandHandler {
        public void Process(string input) {
            var args = input.Split(' ');
            var utils = new StringUtils();
            var parser = new CommandParser();
            var com = new DCommand();
            var cmd = utils.GetCommandFromString(args[0]);
            if (cmd == Command.Error) {
                //Logger.Log($"{args[0]} not found. Use Show-Help for commands", LogLevel.Error);
                Global.logger.Log(NLog.LogLevel.Error, $"{args[0]} not found. Use Show-Help for commands");
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
            //Logger.OnLoggedMessage += (sender, e) => {
            //    if (e.Result.ExecutedCommand == Command.UseServer && e.Result.Result == Result.Ok) {
            //        SyntaxSettings.ConsoleKey = SyntaxSettings.ConsoleOriginalKey + $"({e.Result.Message}) > ";
            //    }
            //    Logger.LogResult(e.Result);
            //};
            var result = new DResult {ExecutedCommand = com.Command};
            Global.logger.Log(NLog.LogLevel.Info, string.Format(MessageStore.RunningCommand, com.Command));
            switch (com.Command) {
                case Command.UseServer:
                    dHandler.UseServer(com);
                    break;
                case Command.CreateServer:
                    result = dHandler.CreateServer(com);
                    break;
                case Command.ShowHelp:
                    result = dHandler.ShowHelp(com);
                    break;
                case Command.ShowServer:
                    result = dHandler.ShowServer(com);
                    break;
                case Command.RemoveServer:
                    result = dHandler.RemoveServer(com);
                    break;
                case Command.EditServer:
                    result = dHandler.EditServer(com);
                    break;
                case Command.SaveServer:
                    result = dHandler.SaveServer(com);
                    break;
                case Command.CreateInvite:
                    result = dHandler.CreateInvite();
                    break;
                case Command.ShowText:
                    result = dHandler.ShowText(com);
                    break;
                case Command.CreateText:
                    result = dHandler.CreateText(com);
                    break;
                case Command.RemoveText:
                    result = dHandler.RemoveText(com);
                    break;
                case Command.EditText:
                    result = dHandler.EditText(com);
                    break;
                case Command.SaveText:
                    result = dHandler.SaveText(com);
                    break;
                case Command.ShowVoice:
                    result = dHandler.ShowVoice(com);
                    break;
                case Command.CreateVoice:
                    result = dHandler.CreateVoice(com);
                    break;
                case Command.RemoveVoice:
                    result = dHandler.RemoveVoice(com);
                    break;
                case Command.EditVoice:
                    result = dHandler.EditVoice(com);
                    break;
                case Command.SaveVoice:
                    result = dHandler.SaveVoice(com);
                    break;
                case Command.ShowUsers:
                    result = dHandler.ShowUsers(com);
                    break;
                case Command.BanUser:
                    result = dHandler.BanUser(com);
                    break;
                case Command.KickUser:
                    result = dHandler.KickUser(com);
                    break;
                case Command.SaveUsers:
                    result = dHandler.SaveUsers(com);
                    break;
                case Command.ShowRoles:
                    result = dHandler.ShowRoles(com);
                    break;
                case Command.CreateRole:
                    result = dHandler.CreateRole(com);
                    break;
                case Command.RemoveRole:
                    result = dHandler.RemoveRole(com);
                    break;
                case Command.EditRole:
                    result = dHandler.EditRole(com);
                    break;
                case Command.SaveRole:
                    result = dHandler.SaveRole(com);
                    break;
                default:
                    Global.logger.Log(NLog.LogLevel.Error, string.Format(MessageStore.CommandNotFound, com.Command));
                    break;
            }
            //Logger.LogResult(result);
            Global.logger.Log(NLog.LogLevel.Info, result.Message);
        }
    }


}
