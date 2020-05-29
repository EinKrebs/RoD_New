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
        public bool IsPaused { get; private set; }
        public GameState CurrentState { get; private set; } = GameState.MainMenu;
        public int BlockSize { get; set; } = 50;

        public event Action<GameState> GameStateChanged;

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
            Player.Update();
            CurrentLevel.Update();
            var currentObjects = Objects.ToList();
            currentObjects
                .Where(gameObject => !IsValid(gameObject, currentObjects))
                .ToList()
                .ForEach(Remove);
            
        }

        public void Remove(IGameObject gameObject)
        {
            CurrentLevel.Remove(gameObject);
        }

        public void Over(bool success)
        {
            if (!success)
            {
                CurrentState = GameState.GameOver;
                GameStateChanged?.Invoke(CurrentState);
            }
            Running = success;
        }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Play()
        {
            CurrentState = GameState.Playing;
            IsPaused = false;
            GameStateChanged?.Invoke(CurrentState);
        }

        public void ToMainMenu()
        {
            CurrentState = GameState.MainMenu;
            GameStateChanged?.Invoke(CurrentState);
        }

        public void ChoosingLevel()
        {
            IsPaused = true;
            CurrentState = GameState.ChoosingLevel;
            GameStateChanged?.Invoke(CurrentState);
        }

        private bool IsValid(IGameObject gameObject, List<IGameObject> objects)
        {
            if (!(-0.05 <= gameObject.X
                  && gameObject.X + gameObject.Width < Width + 0.05
                  && -0.05 <= gameObject.Y
                  && gameObject.Y + gameObject.Height < Height + 0.05))
            {
                return false;
            }
            if (objects.Any(otherObject => !otherObject.Equals(gameObject)
                                           && AreColliding(gameObject, otherObject)
                                           && gameObject.DiesInColliding(otherObject)))
            {
                return false;
            }

            return true;
        }
    }
}
