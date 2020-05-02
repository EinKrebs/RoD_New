﻿using System;
using System.Drawing;
using System.Linq;

namespace RiskOfDeduction.Domain
{
    public class Tank : IMovable, IActive
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public int Width { get; } = 100;
        public int Height { get; } = 50;
        public float VelocityX { get; } = 1;
        public float VelocityY { get; private set; }
        public float G { get; } = 10;
        public Direction Direction { get; private set; }
        public int Hp { get; private set; } = 10;

        private int Tick = 40;
        private int ShotSize { get; } = 18;
        private Game Game { get; set; }


        public Tank(int x, int y, Direction direction, Game game)
        {
            X = x;
            Y = y;
            Direction = direction;
            Game = game;
        }
        
        public bool DiesInColliding(IGameObject other)
        {
            if (other is Shot shot)
            {
                if (shot.sender == ShotSender.Player)
                {
                    Hp--;
                }
            }

            return Hp <= 0;
        }

        public void Shoot()
        {
            var x = Direction == Direction.Left ? X - ShotSize : X + Width;
            var y = Y + 20;
            var angle = Direction == Direction.Left ? Math.PI : 0;
            var shot = new Shot(x, y, angle, ShotSize, Game, ShotSender.Tank);
        }

        public void Update()
        {
            var directionToPlayer = Game.Player.X + Game.Player.Width / 2 - (X + Width / 2) < 0 
                ? Direction.Left 
                : Direction.Right;
            if (Y - Game.Player.Height <= Game.Player.Y
                && Game.Player.Y <= Y + Height
                && Tick == 0
                && directionToPlayer == Direction)
            {
                Shoot();
                Tick = 40;
            } 
            Tick = Math.Max(Tick - 1, 0);
            var oldX = X;
            X += VelocityX * (Direction == Direction.Left ? -1 : 1);
            var checkRectangle = new RectangleF(
                X + (Direction == Direction.Left ? -Width / 2 : Width),
                Y + 0.01f,
                Width / 2,
                Height);
            if (!Game.Objects.Any(gameObject => checkRectangle.IntersectsWith(gameObject.GetRect())))
            {
                X = oldX;
                Direction = 1 - Direction;
            }

            checkRectangle = new RectangleF(X, Y + 0.01f, Width, Height);
            if (!Game.Objects.Any(gameObject => checkRectangle.IntersectsWith(gameObject.GetRect())))
            {
                Y += VelocityY;
                VelocityY += G;
            }
            else
            {
                VelocityY = 0;
            }
        }
    }
}