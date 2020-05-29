﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Windows.Forms;

namespace RiskOfDeduction.Domain
{
    public class Level : IModel
    {

        public int CurrentSceneIndex { get; private set; }
        public IReadOnlyList<Scene> Scenes => scenes;
        public Scene CurrentScene => scenes[CurrentSceneIndex];
        public IEnumerable<IGameObject> Objects => CurrentScene.Objects;
        public string Name { get; set; } = "None";
        public string LevelStyle { get; set; }

        private Game Game { get; set; }
        private List<Scene> scenes { get; set; }
        private List<string> ToRefresh { get; set; }
        private int BlocksPerScene { get; set; }

        public Level(string[] map, int sceneLength, int blockSize, Game game)
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
                scenesFromStringArray.Add(Scene.BuildSceneFromStringArray(currentScene.ToArray(), blockSize, game, this));
            }

            scenes = scenesFromStringArray;
            ToRefresh = map.ToList();
            BlocksPerScene = sceneLength;
            Game = game;
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

        public static Level GenerateLevelFromStringArray(string[] map, int sceneLength, int blockSize, Game game)
        {
            return new Level(map, sceneLength, blockSize, game);
        }

        public bool AreColliding(IGameObject first, IGameObject second)
        {
            return CurrentScene.AreColliding(first, second);
        }

        public void Update()
        {
            CurrentScene.Update();
        }

        public void Remove(IGameObject gameObject)
        {
            CurrentScene.Remove(gameObject);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetGame(Game game)
        {
            Game = game;
        }

        public static Level FromFile(string filePath, Game game)
        {
            var sr = new StreamReader(filePath);
            var levelName = sr.ReadLine();
            var style = sr.ReadLine();
            var blocksPerScene = int.Parse(sr.ReadLine());
            var textLevel = new List<string>();
            while (true)
            {
                var str = sr.ReadLine();
                if (str == null)
                {
                    break;
                }
                textLevel.Add(str);
            }

            game.BlockSize = game.Width / blocksPerScene;
            var level = GenerateLevelFromStringArray(textLevel.ToArray(),
                blocksPerScene * game.BlockSize,
                game.BlockSize,
                game);
            level.SetGame(game);
            level.SetName(levelName);
            level.LevelStyle = style;

            return level;
        }

        public Level Refresh()
        {
            Game.BlockSize = Game.Width / BlocksPerScene;
            var level = GenerateLevelFromStringArray(ToRefresh.ToArray(),
                BlocksPerScene * Game.BlockSize,
                Game.BlockSize,
                Game);
            level.SetName(Name);
            level.LevelStyle = LevelStyle;
            return level;
        }
    }
}