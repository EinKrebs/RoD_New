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
        public int BlockSize { get; }
        public StaticObject[,] Objects { get; }
        public HashSet<Block> groundBlocks { get; }

        public bool DiesInColliding(IGameObject other)
        {
            return false;
        }

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
            BlockSize = blockSize;
            groundBlocks = new HashSet<Block>();
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
            var left = Math.Max((int) ((obj.Left + 0.05f) / BlockSize), 0);
            var right = Math.Min((int) ((obj.Right - 0.05f) / BlockSize), Objects.GetLength(0) - 1);
            var top = Math.Max((int) ((obj.Top + 0.05) / BlockSize), 0);
            var bottom = Math.Min((int) ((obj.Bottom - 0.05) / BlockSize), Objects.GetLength(1) - 1);
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