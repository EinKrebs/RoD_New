using System.Collections.Generic;
using System.Linq;

namespace RiskOfDeduction.Domain
{
    public class Scene : IModel
    {
        public Ground LandScape { get; }
        private HashSet<Shot> Shots { get; } = new HashSet<Shot>();
        private HashSet<Turret> Turrets { get; } = new HashSet<Turret>();

        public IEnumerable<IGameObject> Objects => ((IEnumerable<IGameObject>) LandScape).Concat(Shots).Concat(Turrets);

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
            var blocks = new List<Block>();
            for (var i = 0; i < map.Length; i++)
            {
                for (var j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == '#')
                    {
                        blocks.Add(new Block(j * blockSize, i * blockSize, blockSize, blockSize));
                    }
                    else if (map[i][j] == 'T')
                    {
                        Turrets.Add(new Turret(j * blockSize, i * blockSize, game));
                        j++; 
                    }
                }
            }

            LandScape = new Ground(blocks);
        }

        public static Scene BuildSceneFromStringArray(string[] map, int blockSize, Game game)
        {
            return new Scene(map, blockSize, game);
        }

        public void Update() 
        {
            foreach (var activeObject in GetActives())
            {
                activeObject.Update();
            }
        }

        public void Remove(IGameObject gameObject)
        {
            if (gameObject is Shot shot)
            {
                Shots.Remove(shot);
            }

            if (gameObject is Turret turret)
            {
                Turrets.Remove(turret);
            }
        }

        public void AddShot(Shot shot)
        {
            Shots.Add(shot);
        }

        public IEnumerable<IActive> GetActives()
        {
            return Shots.Concat((IEnumerable<IActive>) Turrets);
        }
    }
}