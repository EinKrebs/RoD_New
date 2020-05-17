using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class TurretDrawer : IDrawer
    {
        public int DrawingPriority { get; }

        private static Image TurretImage { get; } = Images.TurretLeft;
        private Turret Turret { get; }

        public TurretDrawer(Turret turret)
        {
            Turret = turret;
        }

        public void DrawItem(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}