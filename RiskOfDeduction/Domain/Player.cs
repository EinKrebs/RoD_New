using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RiskOfDeduction.Domain
{
    public class Player : IMovable, IActive, IHp
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public int Width { get; }
        public int Height { get; }
        public float VelocityX { get; } = 35f;
        public float VelocityY { get; private set; }
        public float G { get; } = 20f;
        public Direction Direction { get; private set; }
        public int Hp { get; private set; } = 10;
        public int MaxHP { get; } = 10;

        public bool InJump =>
            Game != null
            && !Game.CurrentLevel.CurrentScene.LandScape.IntersectsWith(new RectangleF(X + 0.05f, Y + 0.5f,
                Width - 0.05f, Height));
        public bool IsMoving { get; private set; }

        private int Timer { get; set; } = 0;
        private Game Game { get; }
        private float OneTick { get; } = 0.25f;
        private float JumpInitialVelocity { get; } = -60;

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
            if (!(other is Shot) || (other as Shot).Sender == ShotSender.Player) return false;
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
                
            for (var i = 0; i < 2; i++)
            {
                var mid = (right + left) / 2;
                X = mid;
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
                IsMoving = true;
            }
            else if (X < oldX)
            {
                Direction = Direction.Left;
                IsMoving = true;
            }
            else
            {
                if (Math.Abs(oldX - X) < 0.5f)
                {
                    IsMoving = false;
                }
            }

            if (X + Width > Game.Width - 1)
            {
                X = Game.CurrentLevel.NextScene() ? 0 : oldX;
            }
            else if (X < 1)
            {
                X = Game.CurrentLevel.PreviousScene() ? Game.Width - Width : oldX;
            }
        }

        public void Jump()
        {
            if (Game.CurrentLevel.CurrentScene.LandScape.IntersectsWith(new RectangleF(X, Y + 1, Width, Height)))
            {
                VelocityY = JumpInitialVelocity;
            }
        }

        public void Shoot()
        {
            if (Timer > 0) return;
            Timer = 5;
            var initX = Direction == Direction.Left ? X : X + Width;
            var initY = Y + Height / 2 - 10;
            var angle = Math.Atan2(
                Game.Crosshair.Y - initY + Game.Crosshair.Height / 2 - 4,
                Game.Crosshair.X - initX + Game.Crosshair.Width / 2 - 4);
            var direction = -Math.PI / 2 < angle && angle < Math.PI / 2 ? Direction.Right : Direction.Left;
            //if (direction == Direction)
            //{
            var shot = new Shot(initX, initY, angle, 15, Game, ShotSender.Player);
            //}
        }

        public void Update()
        {
            Timer = Math.Max(Timer - 1, 0);
            var oldY = Y;
            Y += VelocityY * OneTick;
            var newY = Y;
            if (Y < 0)
            {
                Y = 0;
            }

            if (Y > Game.Height - Height)
            {
                Y = Game.Height - Height;
            }
            
            var left = oldY;
            var right = Y;
            
            for (var i = 0; i < 10; i++)
            {
                var mid = (left + right) / 2;
                Y = mid;
                if (Game.Objects.Any(gameObject => gameObject != this
                                                   && !(gameObject is Shot)
                                                   && Game.AreColliding(this, gameObject)))
                {
                    right = mid;
                }
                else
                {
                    left = mid;
                }
            }

            Y = (float) Math.Round(right);
            VelocityY = Math.Abs(newY - Y) < 1 ? VelocityY : 0;
            VelocityY += G * OneTick;
        }

        public void Stop()
        {
            IsMoving = false;
        }
    }
}