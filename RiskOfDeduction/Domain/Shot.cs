using static System.Math;

namespace RiskOfDeduction.Domain
{
    public class Shot : IMovable
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public int Width { get; } = 20;
        public int Height { get; } = 20;
        public float VelocityX { get; }
        public float VelocityY { get; }
        public float G { get; } = 10f;
        private float Speed { get; } = 50f;
        private Game Game { get; }

        public Shot(float x, float y, double angle, Game game)
        {
            VelocityX = (float)Cos(angle) * Speed;
            VelocityY = (float) Sin(angle) * Speed;
            X = x;
            Y = y;
            Game = game;
            Game.CurrentLevel.CurrentScene.Shots.Add(this);
        }

        public void Move()
        {
            X += VelocityX;
            Y += VelocityY;
        }
    }
}