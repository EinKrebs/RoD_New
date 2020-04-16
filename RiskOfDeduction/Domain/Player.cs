using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RiskOfDeduction.Domain
{
    public class Player : IMovable
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public int Width { get; }
        public int Height { get; }
        public float VelocityX { get; } = 20f;
        public float VelocityY { get; private set; }
        public float G { get; } = 10f;
        public Direction Direction { get; private set; }
        
        private Game Game { get; }
        private float OneTick { get; } = 0.25f;
        private float JumpInitialVelocity { get; } = -40;

        public Player(float x, float y, int width, int height, Game game)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Game = game;
        }

        public void MoveTo(Direction direction)
        {
            var oldX = X;
            switch (direction)
            {
                case Direction.Left:
                    X -= VelocityX * OneTick;
                    break;
                case Direction.Right:
                    X += VelocityX * OneTick;
                    break;
            }

            var left = Math.Min(X, oldX);
            var right = Math.Max(X, oldX);
                
            for (int i = 0; i < 10; i++)
            {
                var mid = (right + left) / 2;
                if (Game.CurrentLevel.CurrentScene.LandScape.IsThereAnyIntersection(
                    new RectangleF(mid, Y, Height,Width)))
                {
                    if (X < oldX)
                    {
                        left = mid;
                    }
                    else
                    {
                        right = mid;
                    }
                }
                else
                {
                    if (X < oldX)
                    {
                        right = mid;
                    }
                    else
                    {
                        left = mid;
                    }
                }
            }

            X = (float)Math.Round(X < oldX ? right : left);

            if (X > Game.Width)
            {
                X = Game.CurrentLevel.NextScene() ? X = 0 : oldX;
            }
            else if (X + Width < 0)
            {
                X = Game.CurrentLevel.PreviousScene() ? Game.Width : X = oldX;
            }
        }

        public void Jump()
        {
            if (Game.CurrentLevel.CurrentScene.LandScape.IsThereAnyIntersection(new RectangleF(X, Y + 1, Width, Height)))
            {
                VelocityY = JumpInitialVelocity;
            }
        }

        public void UpdateYPos()
        {
            var oldY = Y;
            Y += VelocityY * OneTick;
            if (Game.CurrentLevel.CurrentScene.LandScape.IsThereAnyIntersection(new RectangleF(X, Y, Width, Height)) ||
                Y + Height > Game.Height)
            {
                Y = oldY;
                VelocityY = 0;
            }
            VelocityY += G * OneTick;
        }
    }
}
