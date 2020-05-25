using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class TurretDrawer : IDrawer
    {
        public IGameObject MainItem => Turret;
        public int DrawingPriority { get; } = 20;

        private static Image TurretImage { get; } = Images.TurretRight;
        private static Image FiringImage { get; } = Images.TurretRightFiring;
        private Turret Turret { get; }

        public TurretDrawer(Turret turret)
        {
            Turret = turret;
        }

        public void DrawItem(Graphics g)
        {
            HpBarDrawer.DrawHp(Turret, g);
            DrawFrameConsideringDirection(g, Turret.Firing ? FiringImage : TurretImage);
        }

        private void DrawFrameConsideringDirection(Graphics g, Image image)
        {
            if (Turret.Direction == Direction.Left)
            {
                image.RotateFlip(RotateFlipType.Rotate180FlipY);
            }

            g.DrawImage(image, new RectangleF(Turret.X, Turret.Y, Turret.Width, Turret.Height));

            if (Turret.Direction == Direction.Left)
            {
                image.RotateFlip(RotateFlipType.Rotate180FlipY);
            }
        }
    }
}