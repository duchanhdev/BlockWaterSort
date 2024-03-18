namespace BlockSort.GameLogic
{
    public class GameLogic
    {
        private static GameLogic instance;
        private PlayerInfo playerInfo;

        private Game game;
        private PlayerInfoSO playerInfoSO;

        public GameLogic()
        {
            instance = this;
            game = null;
            playerInfoSO = DataStream.GetInstance().GetPlayerInfoSO();
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
        }

        public PlayerInfo GetPlayerInfo()
        {
            return playerInfo;
        }
        
    }
}
