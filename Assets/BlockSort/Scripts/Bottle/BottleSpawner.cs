using System;
using System.Collections;
using BlockSort.GameLogic;
using BlockSort.GameUI;
using JetBrains.Annotations;
using UnityEngine;

namespace BlockSort.Bottle
{
    public sealed class BottleSpawner : MonoBehaviour
    {
        [SerializeField]
        private Bottle[] bottles;

        [SerializeField]
        private GameUIManager gameUIManager;

        [SerializeField]
        private LineRenderer _linePouringWaterRenderer;

        private int _firstBottleIndex;

        private GameStatus gameStatus;

        private int secondBottleIndex;

        private Game game => GameLogic.GameLogic.GetInstance().GetGame();

        private void Start()
        {
            gameStatus = game.GetCurGameStatus();
            foreach (var bottle in bottles)
            {
                bottle.SetLinePouringWaterRenderer(_linePouringWaterRenderer);
            }

            DrawBottle();
        }

        private void ChooseBottle(int bottleIndex)
        {
            Debug.Log("Choose BottleClass: ");
            var result = game.GetTubeSelector().ChooseTube(bottleIndex);
            switch (result)
            {
                case Config.Config.CHOOSE_FIRST_TUBE_SUCCESS:
                    bottles[bottleIndex].Select();
                    _firstBottleIndex = bottleIndex;
                    break;
                case Config.Config.CHOOSE_FIRST_TUBE_FAIL:
                    break;
                case Config.Config.CHOOSE_SECOND_TUBE_SUCCESS:

                    DrawBottle();
                    if (game.IsComplete())
                    {
                        gameUIManager.ShowVictoryLayer();
                    }
                    break;
                case Config.Config.CHOOSE_SECOND_TUBE_FAIL:
                    DrawBottle();
                    break;
                case Config.Config.DELETE_FIRST_TUBE_SUCCESS:
                    bottles[bottleIndex].Deselect();
                    break;
            }
        }

        public void DrawBottle()
        {
            gameStatus = game.GetCurGameStatus();
            var numBottle = gameStatus.GetNumTube();

            for (var i = 0; i < bottles.Length; i++)
            {
                var bottle = bottles[i];

                bottle.OnInteract.RemoveListener(ChooseBottle);
                if (i < numBottle)
                {
                    bottle.SetColorsWithTubeLogic(i, gameStatus.GetTubeByIndex(i));
                    bottle.Deselect();
                    bottle.OnInteract.AddListener(ChooseBottle);
                    bottle.gameObject.SetActive(true);
                    continue;
                }

                bottle.gameObject.SetActive(false);
            }
        }
    }
}
