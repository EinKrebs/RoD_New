using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;

namespace RiskOfDeduction.Domain
{
    public class Ground : IGameObject
    {
        public float X { get; }
        public float Y { get; }
        public int Width { get; }
        public int Height { get; }
        public bool DiesInColliding(IGameObject other)
        {
            return false;
        }
        
        private HashSet<Block> groundBlocks { get; }
        public StaticObject[,] Objects { get; }
        public int BlockSize { get; }

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
            X = 0;
            Y = 0;
            Width = map[0].Length * blockSize;
            Height = map.Length * blockSize;
            groundBlocks = new HashSet<Block>();
            BlockSize = blockSize;
            Objects = new StaticObject[map[0].Length, map.Length];
            for (var i = 0; i < map.Length; i++)
            {
                for (var j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == '#')
                    {
                        groundBlocks.Add(new Block(j * blockSize, i * blockSize, blockSize, blockSize));
                    }

                    Objects[j, i] = map[i][j] == '#' ? StaticObject.Block : StaticObject.Nothing;
                }
            }
        }

        public bool IntersectsWith(RectangleF obj)
        {
            var left = (int) ((obj.Left + 0.05f) / BlockSize);
            var right = (int) ((obj.Right - 0.05f) / BlockSize);
            var top = (int) ((obj.Top + 0.05) / BlockSize);
            var bottom = (int) ((obj.Bottom - 0.05) / BlockSize);
            for (var i = left; i <= right; i++)
            {
                for (var j = top; j <= bottom; j++)
                {
                    if (Objects[i, j] == StaticObject.Block)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static Ground BuildGroundFromStringArray(string[] map, int blockSize)
        {
            return new Ground(map, blockSize);
        }
    }
}