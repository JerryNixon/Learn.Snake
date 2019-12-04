using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace Snake
{
    public enum Direction { Stop, Up, Down, Left, Right }

    internal class Program
    {
        private static List<Point> _snake = new List<Point>();

        private static void Main(string[] args)
        {
            // get user's name
            Console.WriteLine("What is your name?");
            Game.UserName = Console.ReadLine();

            // start
            Start();
        }

        private static void Start()
        {
            Setup();
            while (Game.GameOver == false)
            {
                Draw();
                Input();
                Logic();
                Score();
                Thread.Sleep(100);
            }
            End();
        }

        private static void Setup()
        {
            // setup console
            Console.Clear();
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;
            Console.Title = "Snake Game";

            // setup canvas
            DrawCanvas();

            // setup game variables
            Game.Reset();

            void DrawCanvas()
            {
                // draw the background
                var border = new string(Game.BorderCharacter[0], Game.Size.Width);
                foreach (var index in Enumerable.Range(0, Game.Size.Height))
                {
                    Draw(border, 0, index, Game.BorderForeground, Game.BorderBackground);
                }

                // draw the foreground
                var canvas = new string(Game.CanvasCharacter[0], Game.CanvasWidth);
                foreach (var index in Enumerable.Range(1, Game.CanvasHeight))
                {
                    Draw(canvas, 1, index, Game.CanvasForeground, Game.CanvasBackground);
                }
            }
        }

        private static void Draw()
        {
            // clear snake
            Erase();

            // remember snake
            _snake.AddRange(new[] { Game.Head }.Union(Game.Tail));

            // draw the snake head
            Draw(Game.SnakeHeadCharacter, Game.Head, Game.SnakeHeadForeground, Game.SnakeHeadBackground);

            // draw the snake tail
            Draw(Game.SnakeTailCharacter, Game.Tail, Game.SnakeTailForeground, Game.SnakeTailBackground);

            // draw the fruit
            Draw(Game.FruitCharacter, Game.Fruit, Game.FruitForeground, Game.FruitBackground);

            void Erase()
            {
                foreach (var point in _snake)
                {
                    Draw(Game.CanvasCharacter, point, Game.CanvasForeground, Game.CanvasBackground);
                }
                _snake.Clear();
            }
        }

        private static void Input()
        {
            if (Console.KeyAvailable)
            {
                // read the key, set the direction
                Game.SetDirection(Console.ReadKey(true).Key);
            }
        }

        private static void Logic()
        {
            // did the head hit the tail?
            if (Game.Tail.Contains(Game.Head))
            {
                // end the game
                Game.GameOver = true;
            }

            // if direction Stop, then do nothing
            if (Game.Direction == Direction.Stop || Game.GameOver)
            {
                // do not proceed
                return;
            }

            // move and grow the tail
            Game.Tail.Insert(0, Game.Head);

            // is head over fruit?
            if (Game.Head == Game.Fruit)
            {
                // make a sound
                Console.Beep(1000, 10);

                // relocate the fruit
                Game.Fruit = Game.GenerateRandomPoint();
            }
            else
            {
                // decrease the tail size
                Game.Tail.Remove(Game.Tail.Last());
            }

            if (Game.Direction == Direction.Up)
            {
                // hit the top border
                if (Game.Head.Y - 1 == Game.BorderTop)
                {
                    // move to bottom edge
                    Game.Head.Y = Game.BorderBottom - 1;
                }
                else
                {
                    // move up one spot
                    Game.Head.Y--;
                }
            }
            else if (Game.Direction == Direction.Down)
            {
                // hit the bottom border
                if (Game.Head.Y + 1 == Game.BorderBottom)
                {
                    // move to top edge
                    Game.Head.Y = Game.BorderTop + 1;
                }
                else
                {
                    // move down one spot
                    Game.Head.Y++;
                }
            }
            else if (Game.Direction == Direction.Left)
            {
                // hit the left border
                if (Game.Head.X - 1 == Game.BorderLeft)
                {
                    // move to right edge
                    Game.Head.X = Game.BorderRight - 1;
                }
                else
                {
                    // move left one spot
                    Game.Head.X--;
                }
            }
            else if (Game.Direction == Direction.Right)
            {
                // hit the right border
                if (Game.Head.X + 1 == Game.BorderRight)
                {
                    // move to left edge
                    Game.Head.X = Game.BorderLeft + 1;
                }
                else
                {
                    // move right one spot
                    Game.Head.X++;
                }
            }
        }

        public static void Score()
        {
            Draw(Game.UserName.ToUpper(), Game.ScoreboardLeft, 3, ConsoleColor.White, ConsoleColor.Black);
            Draw("----------", Game.ScoreboardLeft, 4, ConsoleColor.White, ConsoleColor.Black);
            Draw($"Score: {Game.Tail.Count()}", Game.ScoreboardLeft, 5, ConsoleColor.White, ConsoleColor.Black);
            if (Game.GameOver)
            {
                Draw("GAME OVER", Game.ScoreboardLeft, 6, ConsoleColor.Red, ConsoleColor.White);
                Draw($"Spacebar to play again", Game.ScoreboardLeft, 7, ConsoleColor.Black, ConsoleColor.Gray);
            }
        }

        private static void End()
        {
            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar)
            {
                // do nothing
            }
            Start();
        }

        #region Draw

        private static void Draw(string text, IEnumerable<Point> points, ConsoleColor foreground, ConsoleColor background)
        {
            // loop through all the points passed in
            foreach (var point in points)
            {
                Draw(text, point.X, point.Y, foreground, background);
            }
        }

        private static void Draw(string text, Point point, ConsoleColor foreground, ConsoleColor background)
        {
            // deconstruct point to X & Y
            Draw(text, point.X, point.Y, foreground, background);
        }

        private static void Draw(string text, int x, int y, ConsoleColor foreground, ConsoleColor background)
        {
            // move position
            Console.SetCursorPosition(x, y);

            // change color
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;

            // write
            Console.Write(text);

            // reset colors
            Console.ResetColor();
        }

        #endregion
    }
}
