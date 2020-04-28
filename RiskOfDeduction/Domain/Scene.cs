using System.Collections.Generic;
using System.Linq;

namespace RiskOfDeduction.Domain
{
    public class Scene : IModel
    {
        public Ground LandScape { get; }
        public HashSet<Shot> Shots { get; set; } = new HashSet<Shot>();

        public IEnumerable<IGameObject> Objects => ((IEnumerable<IGameObject>) LandScape).Concat(Shots);

        public Scene()
        {
            LandScape = new Ground();
            Shots = new HashSet<Shot>();
        }

        public Scene(Ground ground)
        {
            LandScape = ground;
            Shots = new HashSet<Shot>();
        }

        public Scene(string[] map, int blockSize)
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
                }
            }

            LandScape = new Ground(blocks);
        }

        public static Scene BuildSceneFromStringArray(string[] map, int blockSize)
        {
            return new Scene(map, blockSize);
        }

        public void Update() 
        {
            foreach (var shot in Shots)
            {
                shot.Move();
            }
        }

        public void Remove(IGameObject gameObject)
        {
            if (gameObject is Shot shot)
            {
                Shots.Remove(shot);
            }
        }
    }
}