using System;
using System.Drawing;

namespace Snake
{
    internal class Settings
    {
        public static Size Size = new Size(40, 20);

        public static string SnakeHeadCharacter = "-";
        public static ConsoleColor SnakeHeadForeground = ConsoleColor.White;
        public static ConsoleColor SnakeHeadBackground = ConsoleColor.White;

        public static string SnakeTailCharacter = "-";
        public static ConsoleColor SnakeTailForeground = ConsoleColor.Gray;
        public static ConsoleColor SnakeTailBackground = ConsoleColor.Gray;

        public static string CanvasCharacter = "-";
        public static ConsoleColor CanvasForeground = ConsoleColor.DarkBlue;
        public static ConsoleColor CanvasBackground = ConsoleColor.DarkBlue;

        public static string BorderCharacter = "-";
        public static ConsoleColor BorderForeground = ConsoleColor.DarkGreen;
        public static ConsoleColor BorderBackground = ConsoleColor.DarkGreen;

        public static string FruitCharacter = "&";
        public static ConsoleColor FruitForeground = ConsoleColor.Yellow;
        public static ConsoleColor FruitBackground = ConsoleColor.DarkBlue;

        public static int BorderLeft { get; } = 0;
        public static int BorderTop { get; } = 0;
        public static int BorderRight => Size.Width - 1;
        public static int BorderBottom => Size.Height - 1;
        public static int CanvasWidth => Size.Width - 2;
        public static int CanvasHeight => Size.Height - 2;
        public static int ScoreboardLeft => Size.Width + 3;
    }
}
