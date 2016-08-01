using Discord;
using DScriptEngine.Enums;
using System.IO;
using System.Linq;

namespace DScriptEngine {
    internal class DiscordHandler {
        #region Definers

        #endregion
        #region Connection
        public void Connect(DiscordClient Client) {
            Global.client = Client;
        }
        public void Connect(string Username, string Password) {
            Global.client.Connect(Username, Password);
        }
        public void Connect(string Token) {
            Global.client.Connect(Token);
        }
        #endregion
        public DResult UseServer(DCommand cmd) {
            var result = new DResult();
            if (!cmd.Parameters.ContainsKey(CommandParameter.Name) && !cmd.Parameters.ContainsKey(CommandParameter.Id)) {
                result.Result = Result.Error;
                result.Message = string.Format(MessageStore.MissingParameter, "(Name) or (Id)");
                return result;
            }
            if (cmd.Parameters.ContainsKey(CommandParameter.Name)) {
                var name = cmd.Parameters[CommandParameter.Name];
                var server = Global.client.FindServers(name).FirstOrDefault();
                if (server == null) {
                    result.Result = Result.Error;
                    result.Message = string.Format(MessageStore.ServerNotFound, name);
                    return result;
                }
                result.Result = Result.Ok;
                result.Message = server.Name;
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
                result.Result = Result.Ok;
                result.Message = server.Name;
            }

            // var server = Global.client.FindServers()
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
                if (ext != "png" && ext != "jpeg") {
                    result.Result = Result.Error;
                    result.Message = MessageStore.WrongImageType;
                    return result;
                }
                image = new FileStream(cmd.Parameters[CommandParameter.Image], FileMode.Open);
            }
            var regionPart = utils.GetRegionFromString(cmd.Parameters[CommandParameter.Region]);
            if (regionPart != ServerRegion.UNKNOWN) {
                result.Result = Result.Error;
                result.Message = MessageStore.ServerRegionNotFound;
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
                }
            });
            image?.Close();
            return result;
        }
        public DResult CreateTextChannel(DCommand cmd) {
            var result = new DResult { ExecutedCommand = cmd.Command };
            Global.client.ExecuteAndWait(async () => {
                if (Global.activeServer == null) {
                    result.Result = Result.Error;
                    result.Message = MessageStore.NoServerSelected;
                }
                var channel = await Global.activeServer.CreateChannel("", ChannelType.Text);
                if (channel != null) {
                    result.Result = Result.Ok;
                    result.Message = string.Format(MessageStore.TextChannelCreated, channel.Name, channel.Server.Name);
                }
            });
            return result;
        }
    }
}
