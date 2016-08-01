using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUtils {
    public static class ConsoleWriter {
        public static void WriteCenter(string input) {
            Console.SetCursorPosition((Console.WindowWidth - input.Length) / 2, Console.CursorTop);
            Console.WriteLine(input);
        }

        public static void WriteError(string input) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(input);
            Console.ResetColor();
        }
    }
}
