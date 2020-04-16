using System.Collections.Generic;
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
            return LandscapeDrawer.GetDrawables();
        }
    }
}