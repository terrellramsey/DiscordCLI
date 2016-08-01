using Config.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordCLI {
    class Config {
        ConfigFile conf = new ConfigFile("", "conf");
        public static string Username { get; set; }
        public static string Password { get; set; }
        public void LoadConfig() {
            if(!File.Exists("conf.cfg")) {
                //Login
                conf.AddConfig("username", "YourUsernameHere");
                conf.AddConfig("password", "YourPasswordHere");
                conf.AddComment("username", "Login info");
                conf.Write();
                //
                Console.WriteLine("Please fill out the config file to continue");
                Console.ReadLine();
                return;
                //Exit Here
            }
            conf.Load();
            Username = conf.GetConfig("username");
            Password = conf.GetConfig("password");

        }
    }
}
