using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class SceneDrawer : IDrawer
    {
        public IGameObject MainItem => null;
        public int DrawingPriority { get; }

        private Scene Scene { get; }
        private List<IDrawer> Drawers { get; }

        public SceneDrawer(Scene scene)
        {
            Scene = scene;
            Drawers = scene.Objects.Select(DrawerFromGameObj).ToList();
            scene.ObjAdded += obj => Drawers.Add(DrawerFromGameObj(obj));
            scene.ObjRemoved += obj => Drawers.RemoveAll(drawer => drawer.MainItem == obj);
        }

        public void DrawItem(Graphics g)
        {
            foreach (var drawer in Drawers.OrderBy(drawer => drawer.DrawingPriority))
            {
                drawer.DrawItem(g);
            }
        }

        private IDrawer DrawerFromGameObj(IGameObject obj)
        {
            switch (obj)
            {
                case Tank tank:
                    return new TankDrawer(tank);
                case Turret turret:
                    return new TurretDrawer(turret);
                case Shot shot:
                    return new ShotDrawer(shot);
                case Ground ground:
                    return new GroundDrawer(ground);
                case Portal portal:
                    return new PortalDrawer(portal);
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}