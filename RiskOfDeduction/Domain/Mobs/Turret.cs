using System;
using System.Linq;

namespace RiskOfDeduction.Domain
{
    public class Turret : IActive, IMovable, IHp
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Direction Direction { get; private set; }
        public bool Firing { get; private set; }
        public float VelocityX { get; } = 0;
        public float VelocityY { get; } = 10;
        public float G { get; } = 10;
        public int Hp { get; private set; } = 4;
        public int MaxHP { get; } = 4;
        public bool Dead => Hp <= 0;

        private int Timer { get; set; }
        private Game Game { get; }
        private float Center { get; }

        public bool DiesInColliding(IGameObject other)
        {
            if (other is Shot shot)
            {
                if (shot.Sender == ShotSender.Player)
                {
                    Hp--;
                }
            }

            return Hp <= 0;
        }

        public Turret(int x, int y, Game game)
        {
            Center = x;
            Y = y;
            Game = game;
            Width = Game.BlockSize * 2;
            Height = Game.BlockSize;
            Timer = 30;
            Update();
        }

        public void Shoot()
        {
            var initX = Direction == Direction.Left ? X - 20 : X + Width;
            var initY = Y + 15;
            var angle = Direction == Direction.Left ? Math.PI : 0;
            var shot = new Shot(initX, initY, angle, 20, Game, ShotSender.Turret);
        }

        public void Update()
        {
            try
            {
                if (!Game.Objects.Any(obj => obj != this && Game.AreColliding(obj, this)))
                {
                    Y += 1;
                }
            }
            catch
            {
            }
            
            Firing = false;
            if (Game.Player == null)
            {
                return;
            }

            Direction = Game.Player.X < Center ? Direction.Left : Direction.Right;
            X = Direction == Direction.Left ? Center - Width / 2 : Center;
            if (Timer == 0)
            {
                Shoot();
                Firing = true;
            }
            Timer = Timer > 0 ? Timer - 1 : 30;
        }
    }
}