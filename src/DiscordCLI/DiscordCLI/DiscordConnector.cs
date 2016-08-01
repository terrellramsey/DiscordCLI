using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordCLI {
    public class DiscordConnector {
        private DiscordClient _client;

        public void Start() {
            _client = new DiscordClient();
                      
            //_client.MessageReceived += async (s, e) =>
            //{
            // //   if (!e.Message.IsAuthor)
            //       await e.Channel.SendMessage(e.Message.Text);
            //};

          _client.ExecuteAndWait(async () => {
                await _client.Connect(Config.Username, Config.Password);
                Global.DClient = _client;
                Console.WriteLine("Connected");
            });
            //_client.ExecuteAndWait(async () => {
            //   var adminServers =  await _client.Servers.Where(t => t.GetUser(_client.CurrentUser.Id).ServerPermissions.ManageServer);
            //});
        }

    }
}
