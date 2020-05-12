using System.Collections.Generic;
using System.Linq;

namespace RiskOfDeduction.Domain
{
    public class Scene : IModel
    {
        public Ground LandScape { get; }
        private HashSet<IActive> Actives { get; } = new HashSet<IActive>();
        private List<IActive> ToAdd { get; set; } = new List<IActive>();
        public IEnumerable<IGameObject> Objects => Actives.Append((IGameObject)LandScape);
        private Game Game { get; }

        public Scene()
        {
            LandScape = new Ground();
        }

        public Scene(Ground ground)
        {
            LandScape = ground;
        }

        public Scene(string[] map, int blockSize, Game game)
        {
            Game = game;
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

        public static Scene BuildSceneFromStringArray(string[] map, int blockSize, Game game)
        {
            return new Scene(map, blockSize, game);
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
            
            ToAdd = new List<IActive>();
        }

        public void Remove(IGameObject gameObject)
        {
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
        }

        public IEnumerable<IActive> GetActives() => Actives;
    }
}