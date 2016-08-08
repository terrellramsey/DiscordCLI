using System;
using Discord;
using DScriptEngine.Enums;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DScriptEngine {
    internal class DiscordHandler {
        #region Definers

        
        #endregion

        #region Connection


        #endregion

        #region Server

        public void UseServer(DCommand cmd) {
            var result = new DResult();
            if (!cmd.Parameters.ContainsKey(CommandParameter.Name) && !cmd.Parameters.ContainsKey(CommandParameter.Id)) {
                result.Result = Result.Error;
                result.Message = string.Format(MessageStore.MissingParameter, "(Name) or (Id)");
                //Logger.OnLoggedMessage.Emit(this, new OnLoggedMessage(result));
                Global.logger.Log(NLog.LogLevel.Error, result.Message);

                return;
             //   return result;
            }
            if (cmd.Parameters.ContainsKey(CommandParameter.Name)) {
                var name = cmd.Parameters[CommandParameter.Name];
                var server = Global.client.FindServers(name).FirstOrDefault();
                if (server == null) {
                    result.Result = Result.Error;
                    result.Message = string.Format(MessageStore.ServerNotFound, name);
                    //Logger.OnLoggedMessage.Emit(this, new OnLoggedMessage(result));
                    Global.logger.Log(NLog.LogLevel.Error, result.Message);
                    return;
                    //    return result;
                }
                result.Result = Result.Ok;
                result.Message = server.Name;
                Global.activeServer = server;
                Global.logger.Log(NLog.LogLevel.Info, result.Message);
                //Logger.OnLoggedMessage.Emit(this, new OnLoggedMessage(result));
                return;
                //return result;
            }
            else {
                var id = cmd.Parameters[CommandParameter.Id];
                ulong serverId;
                if (!ulong.TryParse(id, out serverId)) {
                    result.Result = Result.Error;
                    result.Message = string.Format(MessageStore.IdNotValid, id);
                    Global.logger.Log(NLog.LogLevel.Error, result.Message);
                    // Logger.OnLoggedMessage.Emit(this, new OnLoggedMessage(result));
                    //return result;
                    return;
                }
                var server = Global.client.GetServer(serverId);
                if (server == null) {
                    result.Result = Result.Error;
                    result.Message = string.Format(MessageStore.ServerNotFound, id);
                    Global.logger.Log(NLog.LogLevel.Error, result.Message);
                    //Logger.OnLoggedMessage.Emit(this, new OnLoggedMessage(result));
                    return;
                    //return result;
                }
                result.Result = Result.Ok;
                result.Message = server.Name;
                Global.activeServer = server;
                //return result;
                //Logger.OnLoggedMessage.Emit(this, new OnLoggedMessage(result));
                Global.logger.Log(NLog.LogLevel.Info, result.Message);
                return;
                
            }
        }

        public DResult CreateInvite() {
            var result = new DResult();
            if (!ActiveServerSet()) {
                result.Result = Result.Error;
                result.Message = MessageStore.NoServerSelected;
                return result;
            }
            var invite = Global.client.GetServer(Global.activeServer.Id).CreateInvite();
            if (invite == null) {
                result.Result = Result.Error;
                result.Message = MessageStore.FailedToCreateInvite;
                return result;
            }
            result.Result = Result.Ok;
            result.Message = invite.Result.Url;
            return result;
        }

        public DResult CreateServer(DCommand cmd) {
            var result = new DResult();
            var type = ImageType.None;
            var utils = new StringUtils();
            Stream image = null;
            if (!cmd.Parameters.ContainsKey(CommandParameter.Name)) {
                result.Result = Result.Error;
                result.Message = string.Format(MessageStore.MissingParameter, "(Name)");
                return result;
            }
            if (!cmd.Parameters.ContainsKey(CommandParameter.Region)) {
                result.Result = Result.Error;
                result.Message = string.Format(MessageStore.MissingParameter, "(Region)");
                return result;
            }
            if (cmd.Parameters.ContainsKey(CommandParameter.Image)) {
                var path = cmd.Parameters[CommandParameter.Image];
                if (!File.Exists(path)) {
                    result.Result = Result.Error;
                    result.Message = string.Format(MessageStore.FileNotFound, path);
                    return result;
                }
                string ext = Path.GetExtension(path);
                if (ext != ".png" && ext != ".jpeg") {
                    result.Result = Result.Error;
                    result.Message = MessageStore.WrongImageType;
                    return result;
                }
                image = new FileStream(cmd.Parameters[CommandParameter.Image], FileMode.Open);
                var convertedExtention = char.ToUpper(Path.GetExtension(path).Substring(1)[0]) + Path.GetExtension(path).Substring(2);
                type = (ImageType)Enum.Parse(typeof(ImageType), convertedExtention);
            }
            var regionPart = utils.GetRegionFromString(cmd.Parameters[CommandParameter.Region]);
            if (regionPart == ServerRegion.UNKNOWN) {
                result.Result = Result.Error;
                result.Message = "The server region was not found";
                return result;
            }
            var serverRegion = Global.client.Regions.FirstOrDefault(t => t.Name == regionPart.ToString());
            var serverName = cmd.Parameters[CommandParameter.Name];
            Global.client.ExecuteAndWait(async () => {
                var server = await Global.client.CreateServer(serverName, serverRegion, type, image);
                if (server != null) {
                    Global.activeServer = server;
                    result.Result = Result.Ok;
                    result.Message = string.Format(MessageStore.ServerCreated, server.Name, server.Id);
                    image?.Close();
                    return;
                }
                return;
            });
            image?.Close();
            return result;
        }

        public DResult ShowServer(DCommand cmd) {
            var result = new DResult();
            Server server = null;
            if (!cmd.Parameters.ContainsKey(CommandParameter.Name) && !cmd.Parameters.ContainsKey(CommandParameter.Id)) {
                result.Result = Result.Error;
                result.Message = string.Format(MessageStore.MissingParameter, "(Name) or (Id)");
                return result;
            }
            if (cmd.Parameters.ContainsKey(CommandParameter.Name)) {
                var name = cmd.Parameters[CommandParameter.Name];
                server = Global.client.FindServers(name).FirstOrDefault();
                if (server == null) {
                    result.Result = Result.Error;
                    result.Message = string.Format(MessageStore.ServerNotFound, name);
                    return result;
                }
            }
            else {
                var id = cmd.Parameters[CommandParameter.Id];
                ulong serverId;
                if (!ulong.TryParse(id, out serverId)) {
                    result.Result = Result.Error;
                    result.Message = string.Format(MessageStore.IdNotValid, id);
                    return result;
                }
                server = Global.client.GetServer(serverId);
                if (server == null) {
                    result.Result = Result.Error;
                    result.Message = string.Format(MessageStore.ServerNotFound, id);
                    return result;
                }
            }
            if(server == null) {
                result.Result = Result.Error;
                result.Message = MessageStore.ServerNotFound;
            }
            var table = new TableWriter("Name", "Info");
            table.AddRow("Name", server.Name);
            table.AddRow("Id", server.Id);
            table.AddRow("Members", server.Users.Count());
            result.Result = Result.Ok;
            result.Message = Environment.NewLine +  table.WriteToString();
            return result;
        }

        public DResult RemoveServer(DCommand cmd) {
            var result = new DResult();
            if (!cmd.Parameters.ContainsKey(CommandParameter.Id)) {
                result.Result = Result.Error;
                result.Message = string.Format(MessageStore.MissingParameter, "(Id)");
                return result;
            }
            var id = cmd.Parameters[CommandParameter.Id];
            ulong serverId;
            if (!ulong.TryParse(id, out serverId)) {
                result.Result = Result.Error;
                result.Message = string.Format(MessageStore.IdNotValid, id);
                return result;
            }
            var server = Global.client.GetServer(serverId);
            if (!server.IsOwner) {
                result.Result = Result.Error;
                result.Message = MessageStore.NotServerOwner;
                return result;
            }
            Global.client.ExecuteAndWait(async () => {
                await server.Delete();
                result.Result = Result.Ok;
                result.Message = string.Format(MessageStore.ServerDeleted,
                    server.Name);
            });
            return result;
        }

        public DResult EditServer(DCommand cmd) {
            var result = new DResult();
            var type = ImageType.None;
            var utils = new StringUtils();
            Stream image = null;
            string newName = null;
            string sregion = null;

            if (!cmd.Parameters.ContainsKey(CommandParameter.Name) && !cmd.Parameters.ContainsKey(CommandParameter.Id)) {
                result.Result = Result.Error;
                result.Message = string.Format(MessageStore.MissingParameter, "(Name) or (Id)");
                return result;
            }
            if (cmd.Parameters.ContainsKey(CommandParameter.Image)) {
                var path = cmd.Parameters[CommandParameter.Image];
                if (!File.Exists(path)) {
                    result.Result = Result.Error;
                    result.Message = string.Format(MessageStore.FileNotFound, path);
                    return result;
                }
                string ext = Path.GetExtension(path);
                if (ext != ".png" && ext != ".jpeg") {
                    result.Result = Result.Error;
                    result.Message = MessageStore.WrongImageType;
                    return result;
                }
                image = new FileStream(cmd.Parameters[CommandParameter.Image], FileMode.Open);
                type = (ImageType)Enum.Parse(typeof(ImageType), Path.GetExtension(path).Substring(1));
            }
            if (cmd.Parameters.ContainsKey(CommandParameter.NewName)) {
                newName = cmd.Parameters[CommandParameter.NewName];
            }
            if (cmd.Parameters.ContainsKey(CommandParameter.Region)) {
                var regionPart = utils.GetRegionFromString(cmd.Parameters[CommandParameter.Region]);
                if (regionPart == ServerRegion.UNKNOWN) {
                    result.Result = Result.Error;
                    result.Message = "The server region was not found";
                    return result;
                }
                sregion = cmd.Parameters[CommandParameter.Region];
            }
            if (cmd.Parameters.ContainsKey(CommandParameter.Name)) {
                var name = cmd.Parameters[CommandParameter.Name];
                var server = Global.client.FindServers(name).FirstOrDefault();
                if (server == null) {
                    result.Result = Result.Error;
                    result.Message = string.Format(MessageStore.ServerNotFound, name);
                    return result;
                }
                if (newName != string.Empty) {
                    Global.client.ExecuteAndWait(async () => {
                        await server.Edit(newName, sregion, image, type);
                    });
                }
                else {
                    Global.client.ExecuteAndWait(async () => {
                        await server.Edit(name, sregion, image, type);
                    });
                }
                result.Result = Result.Ok;
                result.Message = string.Format(MessageStore.ServerUpdated, server.Name);
                return result;
            }
            else {
                var id = cmd.Parameters[CommandParameter.Id];
                ulong serverId;
                if (!ulong.TryParse(id, out serverId)) {
                    result.Result = Result.Error;
                    result.Message = string.Format(MessageStore.IdNotValid, id);
                    return result;
                }
                var server = Global.client.GetServer(serverId);
                if (server == null) {
                    result.Result = Result.Error;
                    result.Message = string.Format(MessageStore.ServerNotFound, id);
                    return result;
                }
                if (newName != string.Empty) {
                    Global.client.ExecuteAndWait(async () => {
                        await server.Edit(newName, sregion, image, type);
                    });
                }
                else {
                    Global.client.ExecuteAndWait(async () => {
                        await server.Edit(server.Name, sregion, image, type);
                    });
                }
                result.Result = Result.Ok;
                result.Message = string.Format(MessageStore.ServerUpdated, server.Name);
                return result;
            }
        }

        public DResult SaveServer(DCommand com) {
            throw new NotImplementedException();
        }

        #endregion

        #region Text Channels

        internal DResult RemoveText(DCommand com) {
            throw new NotImplementedException();
        }

        internal DResult CreateText(DCommand cmd) {
            var result = new DResult();
            if (!ActiveServerSet()) {
                result.Result = Result.Error;
                result.Message = MessageStore.NoServerSelected;
                return result;
            }
            if (!cmd.Parameters.ContainsKey(CommandParameter.Name)) {
                result.Result = Result.Error;
                result.Message = string.Format(MessageStore.MissingParameter, "(Name)");
                return result;
            }
            var name = cmd.Parameters[CommandParameter.Name];
            Channel channel = null;
            Global.client.ExecuteAndWait(async () => {
                channel = await Global.client.GetServer(Global.activeServer.Id).CreateChannel(name, ChannelType.Text);
            });
            if (channel == null) {
                result.Result = Result.Error;
                result.Message = string.Format(MessageStore.FailedToCreateChannel, name, Global.activeServer.Name);
                return result;
            }
            result.Result = Result.Ok;
            result.Message = string.Format(MessageStore.TextChannelCreated, channel.Name, channel.Server.Name);
            return result;
        }

        internal DResult ShowText(DCommand com) {
            throw new NotImplementedException();
        }

        //public DResult CreateTextChannel(DCommand cmd) {
        //    var result = new DResult { ExecutedCommand = cmd.Command };
        //    if (!cmd.Parameters.ContainsKey(CommandParameter.Name)) {
        //        result.Result = Result.Error;
        //        result.Message = string.Format(MessageStore.MissingParameter, "(Name)");
        //        return result;
        //    }
        //    if (Global.activeServer == null) {
        //        result.Result = Result.Error;
        //        result.Message = MessageStore.NoServerSelected;
        //    }
        //    Global.client.ExecuteAndWait(async () => {
        //        var channel = await Global.activeServer.CreateChannel("", ChannelType.Text);
        //        if (channel != null) {
        //            result.Result = Result.Ok;
        //            result.Message = string.Format(MessageStore.TextChannelCreated, channel.Name, channel.Server.Name);
        //        }
        //    });
        //    return result;
        //}

        public DResult EditText(DCommand com) {
            throw new NotImplementedException();
        }

        public DResult SaveText(DCommand com) {
            throw new NotImplementedException();
        }

        #endregion

        #region Voice Channels

        internal DResult EditVoice(DCommand com) {
            throw new NotImplementedException();
        }

        internal DResult RemoveVoice(DCommand com) {
            throw new NotImplementedException();
        }

        internal DResult CreateVoice(DCommand com) {
            throw new NotImplementedException();
        }

        public DResult SaveVoice(DCommand com) {
            throw new NotImplementedException();
        }

        public DResult ShowVoice(DCommand com) {
            throw new NotImplementedException();
        }

        #endregion

        #region Roles

        public DResult CreateRole(DCommand com) {
            throw new NotImplementedException();
        }

        public DResult EditRole(DCommand com) {
            throw new NotImplementedException();
        }

        public DResult SaveRole(DCommand com) {
            throw new NotImplementedException();
        }

        public DResult RemoveRole(DCommand com) {
            throw new NotImplementedException();
        }

        public DResult ShowRoles(DCommand com) {
            throw new NotImplementedException();
        }

        #endregion Help

        public DResult ShowHelp(DCommand com) {
           throw new NotImplementedException();
        }

        #region Users

        public DResult ShowUsers(DCommand com) {
            throw new NotImplementedException();
        }

        public DResult BanUser(DCommand com) {
            throw new NotImplementedException();
        }



        public DResult KickUser(DCommand com) {
            throw new NotImplementedException();
        }



        public DResult SaveUsers(DCommand com) {
            throw new NotImplementedException();
        }

        #endregion

        #region

        #endregion

        #region Checkers

        private bool ActiveServerSet() {
            return Global.activeServer != null;
        }

        #endregion
    }
}
