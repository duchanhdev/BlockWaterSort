using BlockSort.Bottle;
using BlockSort.GameLogic;
using UnityEngine;

namespace BlockSort.GameUI
{
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField]
        private Menu menu;

        [SerializeField]
        private LevelText levelText;

        [SerializeField]
        private BottleSpawner bottleSpawner;

        [SerializeField]
        private GameObject victoryLayer;

        [SerializeField]
        private UIParticle_Confetti UIParticlePrefab;

        [SerializeField]
        private GameObject particleParent;

        private Game _game;
        private GameLogic.GameLogic _gameLogic;
        private UIParticle_Confetti _uiParticle;

        private void Awake()
        {
            _uiParticle = Instantiate(UIParticlePrefab, particleParent.transform);
        }

        private void Start()
        {
            _gameLogic = GameLogic.GameLogic.GetInstance();
            _game = _gameLogic.GetGame();
            HideVictoryLayer();
        }

        public void ShowVictoryLayer()
        {
            victoryLayer.SetActive(true);
            _uiParticle.PlayAnimation();
        }

        public void HideVictoryLayer()
        {
            victoryLayer.SetActive(false);
        }

        public void ShowMenu()
        {
            menu.gameObject.SetActive(true);
        }

        private void HideMenu()
        {
            menu.gameObject.SetActive(false);
        }

        public void ResetGameLevel()
        {
            _game.ResetLevel();
            DrawBottleGridAgain();
        }

        public void BackGameStatus()
        {
            if (_game.UndoStep() == null)
            {
                return;
            }

            DrawBottleGridAgain();
        }

        public void AddNewBottle()
        {
            if (!_game.AddNewTube())
            {
                return;
            }

            DrawBottleGridAgain();
        }

        public void CompleteLevel()
        {
            if (_game.CompleteLevel() == null)
            {
                return;
            }

            levelText.UpdateText(_game.GetLevel());
            DrawBottleGridAgain();
        }

        public void NextGameLevel()
        {
            HideMenu();
            if (_game.MoveToNextLevel() == null)
            {
                return;
            }

            levelText.UpdateText(_game.GetLevel());
            DrawBottleGridAgain();
        }

        public void PreGameLevel()
        {
            HideMenu();
            if (_game.MoveToPreLevel() == null)
            {
                return;
            }

            levelText.UpdateText(_game.GetLevel());
            DrawBottleGridAgain();
        }

        private void DrawBottleGridAgain()
        {
            _game.GetTubeSelector().ResetChoose();
            bottleSpawner.DrawBottle();
        }
    }
}
