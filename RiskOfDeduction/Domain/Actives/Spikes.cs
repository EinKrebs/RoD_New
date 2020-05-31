using System;

namespace RiskOfDeduction.Domain
{
    public class Spikes : IActive
    {
        public float X { get; }
        public float Y { get; }
        public int Width { get; }
        public int Height { get; }
        public int Timer { get; set; } = 0;
        public int MaxTimer { get; } = 15;

        public Spikes(int x, int y, Game game)
        {
            X = x;
            Y = y;
            Width = game.BlockSize;
            Height = game.BlockSize / 3;
        }
        
        public bool DiesInColliding(IGameObject other)
        {
            return false;
        }

        public void Update()
        {
            Timer = Math.Max(0, Timer - 1);
        }
    }
}