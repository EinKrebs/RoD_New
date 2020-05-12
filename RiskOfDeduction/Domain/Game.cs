using System;
using System.Collections.Generic;
using System.Linq;

namespace RiskOfDeduction.Domain
{
    public class Game : IModel
    {
        public int CurrentLevelIndex { get; set; }
        public List<Level> Levels { get; }
        public Level CurrentLevel => Levels[CurrentLevelIndex];
        public Player Player { get; private set; }
        public Crosshair Crosshair { get; }
        public int Height { get; }
        public int Width { get; }
        public float G { get; } = 10;
        public float OneTick { get; } = 0.25f;
        public bool Running { get; private set; }
        public IEnumerable<IGameObject> Objects => CurrentLevel.Objects.Append(Player);

        public Game(int width, int height)
        {
            Width = width;
            Height = height;
            Levels = new List<Level>();
            Crosshair = new Crosshair();
            Running = true;
        }

        public void InitializePlayer(float x, float y, int width, int height)
        {
            Player = new Player(x, y, width, height, this);
        }

        public void AddLevel(Level level)
        {
            Levels.Add(level);
        }

        public bool AreColliding(IGameObject first, IGameObject second)
        {
            return CurrentLevel.AreColliding(first, second);
        }

        public void Update()
        {
            var currentObjects = Objects.ToList();
            var levelUpdate = (Action) CurrentLevel.Update;
            var levelUpdateResult = levelUpdate.BeginInvoke(null, null);
            var collisions = (Func<List<IGameObject>, List<IGameObject>>) 
                (objects => objects
                    .Where(gameObject => !IsValid(gameObject, objects))
                    .ToList());
            var collisionsResult = collisions.BeginInvoke(currentObjects, null, null);
            Player.Update();
            levelUpdate.EndInvoke(levelUpdateResult);
            collisions.EndInvoke(collisionsResult).ForEach(Remove);
        }

        public void Remove(IGameObject gameObject)
        {
            CurrentLevel.Remove(gameObject);
        }

        public void Over(bool success)
        {
            Running = success;
        }

        private bool IsValid(IGameObject gameObject, List<IGameObject> objects)
        {
            if (objects.Any(otherObject => !otherObject.Equals(gameObject)
                                           && AreColliding(gameObject, otherObject)
                                           && gameObject.DiesInColliding(otherObject)))
            {
                return false;
            }

            return 0 <= gameObject.X
                   && gameObject.X + gameObject.Width < Width
                   && 0 <= gameObject.Y
                   && gameObject.Y + gameObject.Height < Height;
        }
    }
}
