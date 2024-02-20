using System;

namespace BlockSort.GameLogic
{
    public class PlayerInfo
    {
        private int curLevel;
        private string id;
        private int maxLevel;
        private string name;

        public PlayerInfo(string name)
        {
            this.name = name;
            id = Guid.NewGuid().ToString();
            curLevel = 1;
            maxLevel = 1;
        }

        public PlayerInfo(string id, string name, int curLevel, int maxLevel)
        {
            this.name = name;
            this.id = id;
            this.curLevel = curLevel;
            this.maxLevel = maxLevel;
        }

        public string GetId()
        {
            return id;
        }

        public string GetName()
        {
            return name;
        }

        public int GetMaxLevel()
        {
            return maxLevel;
        }

        public int GetCurLevel()
        {
            return curLevel;
        }

        public void UpdateCurLevel(int level)
        {
            curLevel = level;
            GameLogic.GetInstance().SavePlayerInfo();
        }

        public bool UpdateMaxLevel(int level)
        {
            if (maxLevel < level)
            {
                maxLevel = level;
                GameLogic.GetInstance().SavePlayerInfo();
                return true;
            }

            return false;
        }

        public void Save()
        {
            var playerInfoSO = DataStream.GetInstance().GetPlayerInfoSO();
            playerInfoSO.SetId(id);
            playerInfoSO.SetName(name);
            playerInfoSO.SetCurLevel(curLevel);
            playerInfoSO.SetMaxLevel(maxLevel);
            ES3.Save("PlayerInfoSO", playerInfoSO);
        }

        public void Load()
        {
            if (!ES3.KeyExists("PlayerInfoSO"))
            {
                Save();
            }

            var playerInfoSO = DataStream.GetInstance().GetPlayerInfoSO();
            var dataPlayerInfoSO = ES3.Load<PlayerInfoSO>("PlayerInfoSO");
            id = dataPlayerInfoSO.GetId();
            name = dataPlayerInfoSO.GetPlayerName();
            curLevel = dataPlayerInfoSO.GetCurLevel();
            maxLevel = dataPlayerInfoSO.GetCurLevel();
            playerInfoSO.SetId(id);
            playerInfoSO.SetName(name);
            playerInfoSO.SetCurLevel(curLevel);
            playerInfoSO.SetMaxLevel(maxLevel);
        }
    }
}
