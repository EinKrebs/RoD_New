using System.Collections.Generic;

namespace RiskOfDeduction.Domain
{
    public class Scene
    {
        public Ground LandScape { get; }

        public Scene()
        {
            LandScape = new Ground();
        }

        public Scene(Ground ground)
        {
            LandScape = ground;
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
    }
}