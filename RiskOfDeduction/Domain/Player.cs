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
        public float VelocityX { get; } = 10f;
        public float VelocityY { get; private set; }
        public float G { get; } = 10f;
        
        private Game game { get; }
        private float oneTick { get; } = 0.25f;
        private float jumpInitialVelocity { get; } = -25;

        public Player(float x, float y, int width, int height, Game game)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            this.game = game;
        }

        public void MoveTo(Direction direction)
        {
            var oldX = X;
            switch (direction)
            {
                case Direction.Left:
                    X -= VelocityX * oneTick;
                    break;
                case Direction.Right:
                    X += VelocityX * oneTick;
                    break;
            }

            if (game.currentLevel.CurrentScene.LandScape.IsThereAnyIntersection(new RectangleF(X, Y, Width, Height)))
            {
                X = oldX;
            }

            if (X > game.Width)
            {
                if (game.currentLevel.NextScene())
                {
                    X = 0;
                }
                else
                {
                    X = oldX;
                }
            }
            else if (X + Width < 0)
            {
                if (game.currentLevel.PreviousScene())
                {
                    X = game.Width;
                }
                else
                {
                    X = oldX;
                }
            }
        }

        public void Jump()
        {
            VelocityY = jumpInitialVelocity;
        }

        public void UpdateYPos()
        {
            var oldY = Y;
            Y += VelocityY * oneTick;
            if (game.currentLevel.CurrentScene.LandScape.IsThereAnyIntersection(new RectangleF(X, Y, Width, Height)) ||
                Y + Height > game.Height)
            {
                Y = oldY;
                VelocityY = 0;
            }
            VelocityY += G * oneTick;
        }
    }
}
