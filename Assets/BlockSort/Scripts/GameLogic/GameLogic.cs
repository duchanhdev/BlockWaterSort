namespace BlockSort.GameLogic
{
    public class GameLogic
    {
        private static GameLogic instance;

        //Dictionary<string, PlayerInfo> playerInfoList;
        private PlayerInfo playerInfo;

        private Game game;
        private PlayerInfoSO playerInfoSO;

        public GameLogic()
        {
            instance = this;
            //this.playerInfoList = new Dictionary<string, PlayerInfo>();
            //this.ReadFileNewPlayerInfoList();
            game = null;
            playerInfoSO = DataStream.GetInstance().GetPlayerInfoSO();
            /*
            if (playerInfoSO.GetCurLevel() == 0)
            {
                this.playerInfo = new PlayerInfo("Player");
                this.SavePlayerInfo();
            }
            this.playerInfo = new PlayerInfo(playerInfoSO.GetId(),playerInfoSO.GetPlayerName(),playerInfoSO.GetCurLevel(), playerInfoSO.GetMaxLevel());
            */
            playerInfo = new PlayerInfo("Player");
            playerInfo.Load();
            StartGame();
        }

        public static GameLogic GetInstance()
        {
            if (instance == null)
            {
                instance = new GameLogic();
            }

            return instance;
        }

        public Game StartGame()
        {
            game = new Game(playerInfo);
            return game;
        }

        public Game GetGame()
        {
            if (game == null)
            {
                game = new Game(playerInfo);
            }

            return game;
        }

        public void SavePlayerInfo()
        {
            playerInfo.Save();
            /*
            playerInfoSO.SetId(playerInfo.GetId());
            playerInfoSO.SetName(playerInfo.GetName());
            playerInfoSO.SetCurLevel(playerInfo.GetCurLevel());
            playerInfoSO.SetMaxLevel(playerInfo.GetMaxLevel());
            */
        }

        public PlayerInfo GetPlayerInfo()
        {
            return playerInfo;
        }

        /*
        public PlayerInfo GetPlayerInfoById(string playerInfoId)
        {
            if (!this.playerInfoList.ContainsKey(playerInfoId))
            {
                return null;
            }

            return this.playerInfoList[playerInfoId];

        }
        */
        /*
        public PlayerInfo AddNewPlayer(string name)
        {
            PlayerInfo playerInfo = new PlayerInfo(name);
            this.playerInfoList.Add(playerInfo.GetId(), playerInfo);
            this.SaveFileNewPlayerInfo(playerInfo);
            return playerInfo;
        }

        */

        /*
        public Game LoadGame(string playerInfoId)
        {

            PlayerInfo playerInfo = this.GetPlayerInfoById(playerInfoId);
            if (playerInfo == null)
            {
                return null;
            }
            this.game = new Game(playerInfo);
            return this.game;
        }

        */

        // Start Game Is LoadGame without Player

        /*
        public Game StartGame()
        {
            if (this.playerInfoList.Count == 0)
            {
                this.AddNewPlayer("Player");
            }
            PlayerInfo playerInfo = (PlayerInfo)this.GetListPlayerInfo()[0];
            if (playerInfo == null)
            {
                return null;
            }
            this.game = new Game(playerInfo);
            return this.game;

        }
        */

        /*
        public ArrayList GetListPlayerInfo()
        {
            ArrayList list = new ArrayList();
            foreach (KeyValuePair<string, PlayerInfo> item in this.playerInfoList)
            {
                list.Add(item.Value);
            }
            return list;
        }
        */

        /*
        public void Main()
        {
            Debug.Log("Hello GameLogic");
        }
        */
        /*
        public void ReadFileNewPlayerInfoList()
        {
            string path = Path.Combine("Assets", "BlockSort", "data", "playerInfoList.txt");
            //string path = "Assets\\BlockSort\\data\\playerInfoList.txt";
            string[] s = File.ReadAllLines(path);
            for(int i=0; i < s.Length/3; i++)
            {
                PlayerInfo playerInfo = new PlayerInfo(s[i * 3], s[i * 3 + 1], int.Parse(s[i * 3 + 2]));
                this.playerInfoList.Add(playerInfo.GetId(),playerInfo);
            }
        }
        */

        /*
        public void SaveFileNewPlayerInfo(PlayerInfo playerInfo)
        {
            string path = Path.Combine("Assets", "BlockSort", "data", "playerInfoList.txt");
            //string path = "Assets\\BlockSort\\data\\playerInfoList.txt";
            string[] s = new string[3];
            s[0] = playerInfo.GetId();
            s[1] = playerInfo.GetName();
            s[2] = playerInfo.GetLevel().ToString();
            File.AppendAllLines(path, s);
        }

        */
        /*
        public void SaveFileAllPlayerInfo()
        {
            string path = Path.Combine("Assets", "BlockSort", "data", "playerInfoList.txt");
            //string path = "Assets\\BlockSort\\data\\playerInfoList.txt";
            string[] s = new string[(this.playerInfoList.Count) * 3];
            int index = 0;
            foreach (KeyValuePair<string, PlayerInfo> item in this.playerInfoList)
            {
                PlayerInfo playerInfo = item.Value;
                s[index * 3] = playerInfo.GetId();
                s[index * 3 + 1] = playerInfo.GetName();
                s[index * 3 + 2] = playerInfo.GetLevel().ToString();

            }
            File.WriteAllLines(path, s);
        }

        */

        /*
        public void LogPlayerInfoList()
        {
            Debug.Log("Print List Player Info --------------");
            foreach (KeyValuePair<string, PlayerInfo> element in this.playerInfoList)
            {
                PlayerInfo playerInfo = element.Value;
                Debug.Log(playerInfo.GetId());
                Debug.Log(playerInfo.GetName());
                Debug.Log(playerInfo.GetLevel());
            }
            Debug.Log("End Print List Player Info --------------");
        }
        */
    }
}
