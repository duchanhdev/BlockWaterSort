using UnityEngine;

namespace BlockSort.TestGameLogic
{
    public class TestLogic : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            var gameLogic = GameLogic.GameLogic.GetInstance();
            //gameLogic.Main();

            // AddPlayer

            //gameLogic.AddNewPlayer("player1");
            //gameLogic.AddNewPlayer("player3");

            //gameLogic.LogPlayerInfoList();
            //Game game = gameLogic.LoadGame(playerInfo.GetId());
            var game = gameLogic.StartGame();
            game.LogStatus();

            //game.LogStatus();
            Debug.Log("-------------------------------------");
            var sol = game.GetSolution().SolveGameStatus(game.GetCurGameStatus());
            Debug.Log("Solution-------------------");

            //foreach (int[] item in sol)
            //{
            //    Debug.Log("Move " + item[0] + " " + item[1]);
            //}
            for (var i = 0; i < sol.Count; i++)
            {
                var item = sol[i];
                Debug.Log("Move " + item[0] + " " + item[1]);
            }

            Debug.Log("End Solution-------------------");

            for (var i = 0; i < sol.Count; i++)
            {
                var item = sol[i];
                var x = (int)game.GetTubeSelector().ChooseTube(item[0]);
                var y = (int)game.GetTubeSelector().ChooseTube(item[1]);
                //game.MoveBlock(item[0], item[1]);
                Debug.Log("test: " + item[0] + " " + item[1] + " " + x + " " + y);
            }

            //game.MoveBlock(0, 2);
            //game.MoveBlock(0, 2);
            //game.MoveBlock(1, 0);
            //game.MoveBlock(1, 0);
            //game.MoveBlock(1, 2);
            //game.MoveBlock(1, 2);

            //game.UndoStep();
            //game.UndoStep();
            //game.UndoStep();

            //game.ResetLevel();

            game.LogStatus();

            Debug.Log("Is Complete: " + game.IsComplete());
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}
