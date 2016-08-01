using ConsoleUtils;
using DScriptEngine;
using DScriptEngine.Syntax;
using System;
using System.Linq;
using System.Threading;

namespace DiscordCLI {
    class Program {
        static void Main(string[] args) {
            InitConsole();
            var connector = new DiscordConnector();
            var thr = new Thread(connector.Start) { IsBackground = true };
            thr.Start();
            while (Global.DClient == null) {
                Thread.Sleep(1000);
            }
            if (args.Count() != 0) {
                if (args[0] == "-script") {
                    var factory = new ScriptFactory(Global.DClient);
                    factory.LoadScript(args[1]);
                }
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
          //  frame.Frame(Console.WindowWidth, Console.WindowHeight, new Point { X = 10, Y = 100 }, ConsoleColor.White);
        }
        static void HandleCliInput() {
            Console.Write(SyntaxSettings.ConsoleKey);
            var input = ConsoleHinter.ReadHintedLine(SyntaxCommand.AutoCompleteCommands, command => command);
            if (!string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input)) {
                var engine = new ScriptEngine(Global.DClient);
                engine.Run(input);
            }
            HandleCliInput();
        }

    }
}
