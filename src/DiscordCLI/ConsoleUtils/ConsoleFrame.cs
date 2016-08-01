using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUtils {
    public  class ConsoleFrame {
        private int hWidth;
        private int hHieght;
        private Point hLocation;
        private ConsoleColor hBorderColor;

        public void Frame(int width, int hieght, Point location, ConsoleColor borderColor) {
            hWidth = width;
            hHieght = hieght;
            hLocation = location;
            hBorderColor = borderColor;
            Darw();
        }

        public Point Location {
            get { return hLocation; }
            set { hLocation = value; }
        }

        public int Width {
            get { return hWidth; }
            set { hWidth = value; }
        }

        public int Hieght {
            get { return hHieght; }
            set { hHieght = value; }
        }

        public ConsoleColor BorderColor {
            get { return hBorderColor; }
            set { hBorderColor = value; }
        }

        public void Darw() {
            string s = "╔";
            string space = "";
            string temp = "";
            for (int i = 0; i < Width; i++) {
                space += " ";
                s += "═";
            }

            for (int j = 0; j < Location.X; j++)
                temp += " ";

            s += "╗" + "\n";

            for (int i = 0; i < Hieght; i++)
                s += temp + "║" + space + "║" + "\n";

            s += temp + "╚";
            for (int i = 0; i < Width; i++)
                s += "═";

            s += "╝" + "\n";

            Console.ForegroundColor = BorderColor;
            Console.CursorTop = hLocation.Y;
            Console.CursorLeft = hLocation.X;
            Console.Write(s);
            Console.ResetColor();
        }
    }
}
