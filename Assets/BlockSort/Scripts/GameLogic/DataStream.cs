using UnityEngine;

namespace BlockSort.GameLogic
{
    public class DataStream : MonoBehaviour
    {
        private static DataStream instance;

        [SerializeField]
        private PlayerInfoSO playerInfoSO;

        [SerializeField]
        private LevelSO[] levelSOs;

        // Use this for initialization
        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public static DataStream GetInstance()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataStream>();
                if (instance == null)
                {
                    var obj = new GameObject();
                    instance = obj.AddComponent<DataStream>();
                }
            }

            return instance;
        }

        public PlayerInfoSO GetPlayerInfoSO()
        {
            return playerInfoSO;
        }

        public LevelSO GetlevelSO(int level)
        {
            return levelSOs[level - 1];
        }
    }
}
