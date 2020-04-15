using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;
using System.Windows.Forms;

namespace RiskOfDeduction
{
    public class Ground : IGameObject
    {
        public int X { get; }
        public int Y { get; }
        public int Height => Image.Height;
        public int Width => Image.Width;
        public double Angle { get; }
        public Image Image { get; }
        public int DrawingPriority { get; }
        private Action action { get; set; }

        public Ground(int x, int y, double angle, string image)
        {
            X = x;
            Y = y;
            Angle = angle;
            Image = Image.FromFile(image);
            DrawingPriority = 0;
            action = new Action(x, y, angle, Image);
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
            return;
        }

        public static List<Ground> FillRectangle(int x, int y, int height, int width, string image)
        {
            var n = height / 32;
            var m = width / 32;
            var result = new List<Ground>();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result.Add(new Ground(x + 32 * i, y + 32 * j, 0, image));
                }
            }

            return result;
        }
    }
}