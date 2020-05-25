using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class GameDrawer : IDrawer
    {
        public IGameObject MainItem => null;
        public int DrawingPriority { get; }
        public CrosshairDrawer CrosshairDrawer { get; }

        private Game Game { get; }
        private List<LevelDrawer> LevelDrawers { get; }
        private HeroDrawer HeroDrawer { get; }

        public GameDrawer(Game game)
        {
            Game = game;
            LevelDrawers = game.Levels.Select(level => new LevelDrawer(level)).ToList();
            HeroDrawer = new HeroDrawer(game.Player);
            CrosshairDrawer = new CrosshairDrawer(game.Crosshair);
        }

        public void DrawItem(Graphics g)
        {
            LevelDrawers[Game.CurrentLevelIndex].DrawItem(g);
            HeroDrawer.DrawItem(g);
            CrosshairDrawer.DrawItem(g);
        }
    }
}