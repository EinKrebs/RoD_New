using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RiskOfDeduction.Domain
{
    public class Player : IMovable, IActive
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public int Width { get; }
        public int Height { get; }
        public float VelocityX { get; } = 20f;
        public float VelocityY { get; private set; }
        public float G { get; } = 20f;
        private Direction Direction { get; set; }
        private Game Game { get; }
        private float OneTick { get; } = 0.25f;
        private float JumpInitialVelocity { get; } = -60;
        private int Hp { get; set; } = 10;

        public Player(float x, float y, int width, int height, Game game)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Game = game;
        }

        public bool DiesInColliding(IGameObject other)
        {
            if (!(other is Shot)) return false;
            if (Hp == 0) return true;
            Hp--;
            return false;
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
                
            for (var i = 0; i < 10; i++)
            {
                var mid = (right + left) / 2;
                if (Game.Objects.Any(gameObject => !gameObject.Equals(this) && Game.AreColliding(this, gameObject)))
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

            if (X > oldX)
            {
                Direction = Direction.Right;
            }
            else if (X < oldX)
            {
                Direction = Direction.Left;
            }

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

        public void Shoot()
        {
            var initX = Direction == Direction.Left ? X : X + Width;
            var initY = Y + Height / 2 - 10;
            var angle = Math.Atan2(
                Game.Crosshair.Y - initY + Game.Crosshair.Height / 2 - 4,
                Game.Crosshair.X - initX + Game.Crosshair.Width / 2 - 4);
            var direction = -Math.PI / 2 < angle && angle < Math.PI / 2 ? Direction.Right : Direction.Left;
            //if (direction == Direction)
            //{
            var shot = new Shot(initX, initY, angle, 8, Game);
            //}
        }

        public void Update()
        {
            var oldY = Y;
            Y += VelocityY * OneTick;
            if (Game.Objects.Any(gameObject => gameObject != this 
                                               && !(gameObject is Shot) 
                                               && Game.AreColliding(this, gameObject)) 
                || Y + Height > Game.Height
                || Y < 0)
            {
                Y = oldY;
                VelocityY = 0;
            }
            VelocityY += G * OneTick;
        }
    }
}
