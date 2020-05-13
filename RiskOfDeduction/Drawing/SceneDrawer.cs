using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class SceneDrawer : IDrawer
    {
        private Scene Scene { get; }
        
        private GroundDrawer LandscapeDrawer { get; }
        private TankDrawer TankDrawer { get; } = new TankDrawer();
        private TurretDrawer TurretDrawer { get; } = new TurretDrawer();
        private ShotDrawer ShotDrawer { get; } = new ShotDrawer();

        public SceneDrawer(Scene scene)
        {
            Scene = scene;
            LandscapeDrawer = new GroundDrawer(scene.LandScape);
        }

        public IEnumerable<Drawable> GetDrawables()
        {
            return LandscapeDrawer
                .GetDrawables()
                .Concat(Scene
                    .GetActives()
                    .Select(active =>
                    {
                        switch (active.GetType().Name)
                        {
                            case "Shot":
                                return ShotDrawer.GetDrawable(active as Shot);
                            case "Turret":
                                return TurretDrawer.GetDrawable(active as Turret);
                            case "Tank":
                                return TankDrawer.GetDrawable(active as Tank); 
                            default:
                                throw new ArgumentException("Unknown type");
                        }
                    }));
        }
    }
}