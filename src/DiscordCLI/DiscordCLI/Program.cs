using ConsoleUtils;
using DScriptEngine;
using DScriptEngine.Syntax;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordCLI {
    class Program {
        static void Main(string[] args) {
            InitConsole();
            if (args.Count() != 0) {
                if(args[0] == "-script") {
                    var factory = new ScriptFactory();
                    factory.LoadScript(args[1]);
                }
            }
            var connector = new DiscordConnector();
            var thr = new Thread(connector.Start) { IsBackground = true };
            thr.Start();
            while (Global.DClient == null)
            {
                Thread.Sleep(1000);
            }

            HandleCliInput();

        }
        internal static void InitConsole() {
            ConsoleFrame frame = new ConsoleFrame();
            var Title = "DiscordCLI - Command line interface for Discord (C) SharpDevs 2016";
            var UserInfo = "Total servers: {0} | Admin count: {0} |";
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.ForegroundColor = SyntaxSettings.ConsoleKeyColor;
            Console.Title = Title;
            ConsoleWriter.WriteCenter(Title);
            ConsoleWriter.WriteCenter(UserInfo);
            Config conf = new Config();
            conf.LoadConfig();
            //frame.Frame(Console.WindowWidth, Console.WindowHeight, new Point { X = 10, Y = 10 }, ConsoleColor.White);
        }
        static void HandleCliInput() {
            Console.Write(SyntaxSettings.ConsoleKey);
            var input = ConsoleHinter.ReadHintedLine(SyntaxCommand.AutoCompleteCommands, command => command);
            if (!string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input)) {
                //  Console.WriteLine("Input: " + input);
                //var handler = new DScriptEngine.;
                //handler.Process(input);
                var engine = new DScriptEngine.ScriptEngine(Global.DClient);
                engine.Run(input);
            }
            //Always on the bottom to return to script!
            HandleCliInput();
        }

    }
}
