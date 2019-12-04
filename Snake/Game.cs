using System;
using System.Collections.Generic;
using System.Drawing;

namespace Snake
{
    internal static class Game
    {
        private static readonly Random _random = new Random();

        public static int Score = 0;
        public static Point Head;
        public static Point Fruit;
        public static bool GameOver;
        public readonly static List<Point> Tail = new List<Point>();
        public static Direction Direction;
        public static string UserName;

        public static void Reset()
        {
            Score = 0;
            Tail.Clear();
            GameOver = false;
            Direction = Direction.Stop;

            // reset the snake location
            Head = GenerateRandomPoint();

            // reset the fruit location
            Fruit = GenerateRandomPoint();
        }

        public static Point GenerateRandomPoint()
        {
            var x = _random.Next(1, Settings.Size.Width - 1);
            var y = _random.Next(1, Settings.Size.Height - 1);
            var point = new Point(x, y);

            // if inside the snake, generate again
            return (Tail.Contains(point) || Head == point)
                ? GenerateRandomPoint()
                : point;
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
