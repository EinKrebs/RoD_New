using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public static class HpBarDrawer
    {
        public static int HpHeight = 10;
        public static int Padding = 5;
        public static void DrawHp<T>(T obj, Graphics g) where T : IHp, IGameObject
        {
            var objRect = obj.GetRect();
            var hpRect = new RectangleF(objRect.X, objRect.Y - HpHeight - Padding, obj.Width, HpHeight);
            g.FillRectangle(Brushes.HotPink, hpRect);

            var curHpRect = new RectangleF(hpRect.X, hpRect.Y, hpRect.Width * ((float)obj.Hp / obj.MaxHP), hpRect.Height);
            g.FillRectangle(Brushes.Aquamarine, curHpRect);
        }
    }
}