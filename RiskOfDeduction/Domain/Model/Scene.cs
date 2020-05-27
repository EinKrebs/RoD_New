using System;
using System.Collections.Generic;
using System.Linq;

namespace RiskOfDeduction.Domain
{
    public class Scene : IModel
    {
        public IEnumerable<IGameObject> Objects => Actives.Append((IGameObject)LandScape);
        public IEnumerable<IActive> GetActives() => Actives;
        public Ground LandScape { get; }

        public Level Level { get; }

        public event Action<IGameObject> ObjAdded;
        public event Action<IGameObject> ObjRemoved;

        private HashSet<IActive> Actives { get; } = new HashSet<IActive>();
        private List<IActive> ToAdd { get; set; } = new List<IActive>();
        private Game Game { get; }

        public Scene()
        {
            LandScape = new Ground();
        }

        public Scene(Ground ground)
        {
            LandScape = ground;
        }

        public Scene(string[] map, int blockSize, Game game, Level level)
        {
            Game = game;
            Level = level;
            var blocks = new List<Block>();
            for (var i = 0; i < map.Length; i++)
            {
                for (var j = 0; j < map[i].Length; j++)
                {
                    switch (map[i][j])
                    {
                        case '#':
                            blocks.Add(new Block(j * blockSize, i * blockSize, blockSize, blockSize));
                            break;
                        case 'T':
                            Actives.Add(new Turret(j * blockSize, i * blockSize, game));
                            break;
                        case 'M':
                            Actives.Add(new Tank(
                                j * blockSize,
                                i * blockSize,
                                map[i][j + 1] == 'L' ? Direction.Left : Direction.Right,
                                game));
                            j++;
                            break;
                    }
                }
            }
            LandScape = new Ground(map, blockSize);
        }

        public static Scene BuildSceneFromStringArray(string[] map, int blockSize, Game game, Level level)
        {
            return new Scene(map, blockSize, game, level);
        }

        public bool AreColliding(IGameObject first, IGameObject second)
        {
            if (first is Ground firstGround)
            {
                return firstGround.IntersectsWith(second.GetRect());
            }

            if (second is Ground secondGround)
            {
                return secondGround.IntersectsWith(first.GetRect());
            }

            return first.GetRect().IntersectsWith(second.GetRect());
        }

        public void Update() 
        {
            foreach (var active in Actives)
            {
                active.Update();
            }

            foreach (var active in ToAdd)
            {
                active.Update();
                Actives.Add(active);
            }
            
            ToAdd.Clear();
        }

        public void Remove(IGameObject gameObject)
        {
            ObjRemoved(gameObject);

            if (gameObject is Player)
            {
                Game.Over(false);
            }
            if (gameObject is IActive active)
            {
                Actives.Remove(active);
            }
        }

        public void AddShot(Shot shot)
        {
            ToAdd.Add(shot);
            ObjAdded(shot);
        }
    }
}