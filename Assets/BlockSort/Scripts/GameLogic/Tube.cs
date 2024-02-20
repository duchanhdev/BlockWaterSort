using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlockSort.GameLogic
{
    [Serializable]
    public class Tube
    {
        private const int maxLength = 4;

        [SerializeField]
        private List<Block> blocks;

        public Tube(string blocksChar)
        {
            blocks = new List<Block>();

            for (var i = 0; i < blocksChar.Length; i++)
            {
                blocks.Add(new Block(blocksChar[i]));
            }
        }

        public Tube(Tube other)
        {
            blocks = new List<Block>();

            for (var i = 0; i < other.blocks.Count; i++)
            {
                blocks.Add(new Block(other.blocks[i]));
            }
        }

        public bool IsEmptyOrFullATypeBlock()
        {
            switch (blocks.Count)
            {
                case 0:
                    return true;
                case < maxLength:
                    return false;
            }

            for (var i = 1; i < blocks.Count; i++)
            {
                if (!blocks[i].Equals(blocks[0]))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsFullBlock()
        {
            return GetNumBlock() >= maxLength;
        }

        public bool AddBlock(Block block)
        {
            if (blocks.Count >= maxLength)
            {
                return false;
            }

            if (blocks.Count > 0 && !GetTopBlock().Equals(block))
            {
                return false;
            }

            blocks.Add(block);
            return true;
        }

        public Block PopBlock()
        {
            if (blocks.Count == 0)
            {
                return null;
            }

            var block = blocks[blocks.Count - 1];
            blocks.Remove(block);
            return block;
        }

        public List<Block> GetBlocks()
        {
            return blocks;
        }

        public Block GetTopBlock()
        {
            if (blocks.Count == 0)
            {
                return null;
            }

            return blocks[blocks.Count - 1];
        }

        public int GetNumBlock()
        {
            return blocks.Count;
        }

        public void LogStatus()
        {
            Debug.Log("NumBlock: " + blocks.Count);

            for (var i = blocks.Count - 1; i >= 0; i--)
            {
                Debug.Log("Block" + i + ": " + blocks[i].GetCharColor());
            }
        }

        public string ConvertBlocksToString()
        {
            var s = "";
            for (var i = 0; i < blocks.Count; i++)
            {
                s += blocks[i].GetCharColor();
            }

            while (s.Length < maxLength)
            {
                s += ' ';
            }

            return s;
        }
    }
}
