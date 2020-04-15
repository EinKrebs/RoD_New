using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskOfDeduction.Domain
{
    public class Game
    {
        public int CurrentLevelIndex { get; set; }
        public List<Level> Levels { get; }
        public Level currentLevel => Levels[CurrentLevelIndex];
        public Player Player { get; private set; }
        public int Height { get; }
        public int Width { get; }
        public float G { get; } = 10;
        public float OneTick { get; } = 0.25f;

        public Game(int width, int height)
        {
            Width = width;
            Height = height;
            Levels = new List<Level>();
        }

        public void InitializePlayer(float x, float y, int width, int height)
        {
            Player = new Player(x, y, width, height, this);
        }

        public void AddLevel(Level level)
        {
            Levels.Add(level);
        }
    }
}
