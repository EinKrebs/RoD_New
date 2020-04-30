using static System.Math;

namespace RiskOfDeduction.Domain
{
    public class Shot : IMovable, IActive
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public int Width { get; }
        public int Height { get; }
        public bool DiesInColliding(IGameObject other)
        {
            return true;
        }

        public float VelocityX { get; }
        public float VelocityY { get; }
        public float G { get; } = 10f;
        private float Speed { get; } = 50f;
        private Game Game { get; }
        public double Angle { get; }

        public Shot(float x, float y, double angle, int size, Game game)
        {
            VelocityX = (float)Cos(angle) * Speed;
            VelocityY = (float) Sin(angle) * Speed;
            Angle = angle;
            X = x;
            Y = y;
            Width = size;
            Height = size;
            Game = game;
            Game.CurrentLevel.CurrentScene.AddShot(this);
        }

        public void Update()
        {
            X += VelocityX;
            Y += VelocityY;
        }
    }
}