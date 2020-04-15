using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RiskOfDeduction
{
    public class Player : IGameObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Height => Image.Height;
        public int Width => Image.Width;
        public double Angle { get; private set; }
        public Image Image { get; private set; }
        public int DrawingPriority { get; }
        private Action action { get; set; }
        private Game game;
        private readonly int speed = 3;

        public Player(int x, int y, double angle, string image, int drawingPriority, Game game)
        {
            X = x;
            Y = y;
            Angle = angle;
            Image = Image.FromFile(image);
            DrawingPriority = drawingPriority;
            this.game = game;
        }

        public Action Act()
        {
            return action;
        }

        public bool DiesInConflict(IGameObject gameObject)
        {
            return false;
        }

        public void Update(Action action)
        {
            X = action.X;
            Y = action.Y;
            Angle = action.Angle;
            Image = action.Image;
        }
    }
}
