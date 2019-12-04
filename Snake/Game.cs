using System;
using System.Collections.Generic;
using System.Drawing;

namespace Snake
{
    internal class Game
    {
        private static readonly Random _random = new Random();

        public static Size Size = new Size(40, 20);

        public static string SnakeHeadCharacter = "-";
        public static ConsoleColor SnakeHeadForeground = ConsoleColor.White;
        public static ConsoleColor SnakeHeadBackground = ConsoleColor.White;

        public static string SnakeTailCharacter = "-";
        public static ConsoleColor SnakeTailForeground = ConsoleColor.Gray;
        public static ConsoleColor SnakeTailBackground = ConsoleColor.Gray;

        public static string CanvasCharacter = "-";
        public static ConsoleColor CanvasForeground = ConsoleColor.Green;
        public static ConsoleColor CanvasBackground = ConsoleColor.Green;

        public static string BorderCharacter = "-";
        public static ConsoleColor BorderForeground = ConsoleColor.Red;
        public static ConsoleColor BorderBackground = ConsoleColor.Red;

        public static string FruitCharacter = "*";
        public static ConsoleColor FruitForeground = ConsoleColor.Black;
        public static ConsoleColor FruitBackground = ConsoleColor.Green;

        public static Point Head;
        public static Point Fruit;
        public static bool GameOver;
        public static List<Point> Tail;
        public static Direction Direction;
        public static int BorderLeft { get; } = 0;
        public static int BorderTop { get; } = 0;
        public static int BorderRight => Size.Width - 1;
        public static int BorderBottom => Size.Height - 1;
        public static int CanvasWidth => Size.Width - 2;
        public static int CanvasHeight => Size.Height - 2;
        public static int ScoreboardLeft => Game.Size.Width + 3;
        public static string UserName { get; internal set; }

        public static void Reset()
        {
            GameOver = false;
            Tail = new List<Point>();
            Direction = Direction.Stop;
            Head = GenerateRandomPoint();
            Fruit = GenerateRandomPoint();
        }

        public static Point GenerateRandomPoint()
        {
            var x = _random.Next(1, Size.Width - 1);
            var y = _random.Next(1, Size.Height - 1);
            var point = new Point(x, y);

            if (Tail.Contains(point) || Head == point)
            {
                return GenerateRandomPoint();
            }
            else
            {
                return point;
            }
        }

        public static void SetDirection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    {
                        Direction = Direction.Up;
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        Direction = Direction.Down;
                        break;
                    }
                case ConsoleKey.LeftArrow:
                    {
                        Direction = Direction.Left;
                        break;
                    }
                case ConsoleKey.RightArrow:
                    {
                        Direction = Direction.Right;
                        break;
                    }
                case ConsoleKey.Escape:
                    {
                        Direction = Direction.Stop;
                        break;
                    }
            }
        }
    }
}
