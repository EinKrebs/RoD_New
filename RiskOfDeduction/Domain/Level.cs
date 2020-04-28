using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Windows.Forms;

namespace RiskOfDeduction.Domain
{
    public class Level : IModel
    {
        private List<Scene> scenes;

        public int CurrentSceneIndex { get; private set; }
        public IReadOnlyList<Scene> Scenes => scenes;
        public Scene CurrentScene => scenes[CurrentSceneIndex];
        public IEnumerable<IGameObject> Objects => CurrentScene.Objects;

        public Level()
        {
            scenes = new List<Scene>();
        }

        public Level(IEnumerable<Scene> scenes)
        {
            this.scenes = new List<Scene>(scenes);
        }

        public Level(string[] map, int sceneLength, int blockSize)
        {
            if (sceneLength % blockSize != 0)
            {
                throw new ArgumentException("SceneLength should be divisible by the blockSize");
            }

            sceneLength /= blockSize;

            if (map.Any(s => s.Length != map[0].Length || s.Length % sceneLength != 0))
            {
                throw new ArgumentException("Strings are with different length or aren't divisible by sceneLength");
            }

            var scenesFromStringArray = new List<Scene>();
            for (var i = 0; i < map[0].Length / sceneLength; i++)
            {
                var currentScene = new List<string>();
                foreach (var s in map)
                {
                    currentScene.Add(s.Substring(i * sceneLength, sceneLength));
                }
                scenesFromStringArray.Add(Scene.BuildSceneFromStringArray(currentScene.ToArray(), blockSize));
            }

            scenes = scenesFromStringArray;
        }

        public bool NextScene()
        {
            if (CurrentSceneIndex + 1 >= scenes.Count) return false;
            CurrentSceneIndex++;
            return true;

        }

        public bool PreviousScene()
        {
            if (CurrentSceneIndex - 1 < 0) return false;
            CurrentSceneIndex--;
            return true;

        }

        public static Level GenerateLevelFromStringArray(string[] map, int sceneLength, int blockSize)
        {
            return new Level(map, sceneLength, blockSize);
        }

        public void Update()
        {
            CurrentScene.Update();
        }

        public void Remove(IGameObject gameObject)
        {
            CurrentScene.Remove(gameObject);
        }
    }
}