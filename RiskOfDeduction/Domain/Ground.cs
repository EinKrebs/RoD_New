using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;

namespace RiskOfDeduction.Domain
{
    public class Ground : IEnumerable<Block>
    {
        private HashSet<Block> groundBlocks { get; }

        public Ground()
        {
            groundBlocks = new HashSet<Block>();
        }

        public Ground(IEnumerable<Block> blocks)
        {
            groundBlocks = new HashSet<Block>(blocks);
        }

        public Ground(string[] map, int blockSize)
        {
            groundBlocks = new HashSet<Block>();
            for (var i = 0; i < map.Length; i++)
            {
                for (var j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == '#')
                    {
                        groundBlocks.Add(new Block(j * blockSize, i * blockSize, blockSize, blockSize));
                    }
                }
            }
        }

        public void AddBlock(Block newBlock)
        {
            groundBlocks.Add(newBlock);
        }

        public bool IsThereAnyIntersection(RectangleF obj)
        {
            return groundBlocks.Any(block =>
            {
                var blockRectangle = new RectangleF(block.X, block.Y, block.Width, block.Height);
                return blockRectangle.IntersectsWith(obj);
            });
        }

        public static Ground BuildGroundFromStringArray(string[] map, int blockSize)
        {
            return new Ground(map, blockSize);
        }

        public IEnumerator<Block> GetEnumerator()
        {
            return ((IEnumerable<Block>) groundBlocks).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}