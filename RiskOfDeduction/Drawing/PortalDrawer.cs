using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class PortalDrawer : IDrawer
    {
        public IGameObject MainItem => Portal;
        public int DrawingPriority { get; } = 10;
        private Portal Portal { get; }
        private Image Image = Images.Portal;

        public PortalDrawer(Portal portal)
        {
            Portal = portal;
        }
        
        public void DrawItem(Graphics g)
        {
            g.DrawImage(Image, Portal.X, Portal.Y, Portal.Width, Portal.Height);
        }
    }
}