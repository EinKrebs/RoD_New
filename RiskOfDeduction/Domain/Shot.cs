using System.ComponentModel.Design;
using NUnit.Framework.Constraints;
using static System.Math;

namespace RiskOfDeduction.Domain
{
    public class Shot : IMovable, IActive
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public int Width { get; }
        public int Height { get; }
        public float VelocityX { get; }
        public float VelocityY { get; }
        public float G { get; } = 10f;
        public double Angle { get; }
        public ShotSender sender { get;  }

        private float Speed { get; } = 20f;
        private Game Game { get; }

        public bool DiesInColliding(IGameObject other)
        {
            if (sender == ShotSender.Player && other is Player
                || (sender == ShotSender.Tank || sender == ShotSender.Turret) && (other is Turret || other is Tank))
            {
                return false;
            }

            return !(other is Shot);
        }

        public Shot(float x, float y, double angle, int size, Game game, ShotSender sender)
        {
            VelocityX = (float)Cos(angle) * Speed;
            VelocityY = (float)Sin(angle) * Speed;
            Angle = angle;
            X = x;
            Y = y;
            Width = size;
            Height = size;
            Game = game;
            Game.CurrentLevel.CurrentScene.AddShot(this);
            this.sender = sender;
        }

        public void Update()
        {
            X += VelocityX;
            Y += VelocityY;
        }
    }
}