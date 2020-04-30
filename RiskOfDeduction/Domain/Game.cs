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
        public IEnumerable<IGameObject> Objects => CurrentLevel.Objects;

        public Game(int width, int height)
        {
            Width = width;
            Height = height;
            Levels = new List<Level>();
            Crosshair = new Crosshair();
        }

        public void InitializePlayer(float x, float y, int width, int height)
        {
            Player = new Player(x, y, width, height, this);
        }

        public void AddLevel(Level level)
        {
            Levels.Add(level);
        }
        
        public static bool AreColliding(IGameObject first, IGameObject second)
        {
            var firstRectangle = first.GetRect();
            var secondRectangle = second.GetRect();
            return firstRectangle.IntersectsWith(secondRectangle);
        }

        private bool IsValid(IGameObject gameObject)
        {
            if (Objects.Any(otherObject => otherObject != gameObject 
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

        public void Update()
        {
            Player.Update();
            CurrentLevel.Update();
            Objects.Where(gameObject => !IsValid(gameObject)).ToList().ForEach(Remove);
        }

        public void Remove(IGameObject gameObject)
        {
            CurrentLevel.Remove(gameObject);
        }
    }
}
