﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class LevelDrawer : IDrawer
    {
        private Level Level { get; }
        private List<SceneDrawer> Scenes { get; }

        public LevelDrawer(Level level)
        {
            Level = level;
            Scenes = level.Scenes.Select(scene => new SceneDrawer(scene)).ToList();
        }
        
        public IEnumerable<Drawable> GetDrawables()
        {
            return Scenes[Level.CurrentSceneIndex].GetDrawables();
        }
    }
}