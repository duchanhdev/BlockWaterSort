using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlockSort.GameLogic
{
    [Serializable]
    public class GameStatus
    {
        private const string className = "GameStatus";
        public static int maxTube = 12;

        [SerializeField]
        private int numTube;

        [SerializeField]
        private List<Tube> tubes;

        public GameStatus(int level)
        {
            SetStatusByLevel(level);
        }

        public GameStatus()
        {
        }

        public GameStatus(GameStatus orther)
        {
            numTube = orther.numTube;
            tubes = new List<Tube>();
            for (var i = 0; i < numTube; i++)
            {
                tubes.Add(new Tube(orther.tubes[i]));
            }
        }

        private string[] ReadFile(int level)
        {
            var numEmptyTube = DataStream.GetInstance().GetlevelSO(level).GetNumEmptyTube();
            var tubes = DataStream.GetInstance().GetlevelSO(level).GetTubes();

            var s = new string[tubes.Length + 1];

            s[0] = "#" + numEmptyTube;
            for (var i = 0; i < tubes.Length; i++)
            {
                s[i + 1] = tubes[i];
            }

            return s;
        }

        private int GetNumTubeFromFile(string[] s)
        {
            var numEmptyTube = 0;
            var numTube = 0;

            for (var i = 0; i < s.Length; i++)
            {
                if (s[i].Length > 0)
                {
                    if (s[i][0] == '#')
                    {
                        numEmptyTube = int.Parse(s[i].Substring(1));
                    }
                    else
                    {
                        numTube += 1;
                    }
                }
            }

            numTube += numEmptyTube;
            return numTube;
        }

        private void SetStatusByLevel(int level)
        {
            var s = ReadFile(level);

            numTube = GetNumTubeFromFile(s);
            tubes = new List<Tube>();

            var indexTube = 0;
            for (var i = 0; i < s.Length; i++)
            {
                if (s[i].Length > 0)
                {
                    if (s[i][0] != '#')
                    {
                        tubes.Add(new Tube(s[i]));
                        indexTube++;
                    }
                }
            }

            while (indexTube < numTube)
            {
                tubes.Add(new Tube(""));
                indexTube++;
            }

            //this.numTube = 3;
            //this.tubes = new ShopItem[this.numTube];
            //this.tubes[0] = new ShopItem("yyvv");
            //this.tubes[1] = new ShopItem("vvyy");
            //this.tubes[2] = new ShopItem("");
        }

        public GameStatus Clone()
        {
            return new GameStatus(this);
        }

        public bool MoveBlock(int indexTube1, int indexTube2)
        {
            var tube1 = GetTubeByIndex(indexTube1);
            var tube2 = GetTubeByIndex(indexTube2);
            if (tube1 == null || tube2 == null || tube1.IsEmptyOrFullATypeBlock() || tube2.IsFullBlock())
            {
                return false;
            }

            if (tube2.GetNumBlock() == 0 || tube1.GetTopBlock().Equals(tube2.GetTopBlock()))
            {
                var block = tube1.PopBlock();
                tube2.AddBlock(block);
                MoveBlock(indexTube1, indexTube2);
                return true;
            }

            return false;
        }

        public bool AddNewTube()
        {
            if (numTube >= maxTube)
            {
                return false;
            }

            tubes.Add(new Tube(""));
            numTube++;
            return true;
        }

        public bool IsComplete()
        {
            for (var i = 0; i < numTube; i++)
            {
                if (!tubes[i].IsEmptyOrFullATypeBlock())
                {
                    return false;
                }
            }

            return true;
        }

        public List<Tube> GetTubes()
        {
            return tubes;
        }

        public Tube GetTubeByIndex(int index)
        {
            if (index >= numTube)
            {
                return null;
            }

            return tubes[index];
        }

        public void MoveToLevel(int level)
        {
            SetStatusByLevel(level);
        }

        public void LogStatus()
        {
            Debug.Log("NumTube: " + numTube);
            for (var i = 0; i < numTube; i++)
            {
                Debug.Log("ShopItem " + i + ": ");
                tubes[i].LogStatus();
            }

            Debug.Log("");
        }

        public string ConvertTubesToString()
        {
            var s = "";
            for (var i = 0; i < tubes.Count; i++)
            {
                s += tubes[i].ConvertBlocksToString();
            }

            return s;
        }

        public int GetNumTube()
        {
            return numTube;
        }

        public void Save()
        {
            try
            {
                ES3.Save(className, this);
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
            }
        }

        public void Load()
        {
            if (!ES3.KeyExists(className))
            {
                Save();
            }

            try
            {
                var gameStatus = (GameStatus)ES3.Load(className);
                tubes = gameStatus.tubes;
                numTube = gameStatus.numTube;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
            }
        }
    }
}
