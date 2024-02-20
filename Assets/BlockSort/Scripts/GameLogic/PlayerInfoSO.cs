using UnityEngine;

namespace BlockSort.GameLogic
{
    [CreateAssetMenu(fileName = "PlayerInfo", menuName = "Data/PlayerInfo")]
    public class PlayerInfoSO : ScriptableObject
    {
        [SerializeField]
        private string id;

        [SerializeField]
        private string playerName;

        [SerializeField]
        private int maxLevel;

        [SerializeField]
        private int curLevel;

        private void Awake()
        {
        }

        public string GetId()
        {
            return id;
        }

        public string GetPlayerName()
        {
            return playerName;
        }

        public int GetMaxLevel()
        {
            return maxLevel;
        }

        public int GetCurLevel()
        {
            return curLevel;
        }

        public void SetCurLevel(int level)
        {
            curLevel = level;
        }

        public void SetMaxLevel(int level)
        {
            maxLevel = level;
        }

        public void SetId(string id)
        {
            this.id = id;
        }

        public void SetName(string name)
        {
            playerName = name;
        }
    }
}
