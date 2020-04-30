using System;
using System.Collections.Generic;
using System.Linq;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class SceneDrawer : IDrawer
    {
        private Scene Scene { get; }
        
        private GroundDrawer LandscapeDrawer { get; }

        public SceneDrawer(Scene scene)
        {
            Scene = scene;
            LandscapeDrawer = new GroundDrawer(scene.LandScape);
        }

        public IEnumerable<IDrawable> GetDrawables()
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
                                return (IDrawable) new ShotDrawer(active as Shot);
                            case "Turret":
                                return (IDrawable) new TurretDrawer(active as Turret);
                            default:
                                throw new ArgumentException("Unknown type");
                        }
                    }));
        }
    }
}