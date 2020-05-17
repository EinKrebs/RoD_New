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
        public int DrawingPriority { get; }

        private Game Game { get; }

        public GameDrawer(Game game)
        {
            Game = game;
        }

        public void DrawItem(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}