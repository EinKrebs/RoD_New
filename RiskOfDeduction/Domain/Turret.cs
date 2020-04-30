﻿using System;

namespace RiskOfDeduction.Domain
{
    public class Turret : IActive, IMovable
    {
        public float X { get; set; }
        public float Y { get; }
        public int Width { get; } = 100;
        public int Height { get; } = 50;
        private int Timer { get; set; }
        private int Hp { get; set; } = 4;
        public Direction Direction { get; private set; }
        public bool Firing { get; private set; }
        private Game Game { get; }
        private float Center { get; }
        public float VelocityX { get; } = 0;
        public float VelocityY { get; } = 0;
        public float G { get; } = 10;
        public bool DiesInColliding(IGameObject other)
        {
            if (!(other is Shot)) return false;
            Hp--;
            return Hp == 0;
        }

        public Turret(int x, int y, Game game)
        {
            Center = x;
            Y = y;
            Game = game;
            Timer = 16;
            Update();
        }

        private void Shoot()
        {
            var initX = Direction == Direction.Left ? X - 20 : X + Width;
            var initY = Y + 15;
            var angle = Direction == Direction.Left ? Math.PI : 0;
            var shot = new Shot(initX, initY, angle, 20, Game);
        }

        public void Update()
        {
            Firing = false;
            Direction = Game.Player.X < Center ? Direction.Left : Direction.Right;
            X = Direction == Direction.Left ? Center - Width / 2 : Center;
            if (Timer == 0)
            {
                Shoot();
                Firing = true;
            }
            Timer = Timer > 0 ? Timer - 1 : 10;
        }
    }
}