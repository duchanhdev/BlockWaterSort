using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace BlockSort.GameLogic
{
    public class Game
    {
        private const int MAX_GAME_STATUS_IN_HISTORY = 5;
        private const int MAX_LEVEL = 100;
        private const string UNDO_COUNT_KEY = "undo_count_key";
        private const string PLUS_BOTTLE_COUNT_KEY = "plus_bottle_count_key";

        private List<GameStatus> _historyGameStatus;
        private readonly int _maxPlusBottleCount = 2;
        private readonly int _maxUndoCount = 3;
        private PlayerInfo _playerInfo;
        private Solution _solution;
        private TubeSelector _tubeSelector;
        private GameStatus _curGameStatus;
        private int _level;

        public Game(PlayerInfo playerInfo, int maxUndoCount = 3, int maxPlusBottleCount = 2)
        {
            _historyGameStatus = new List<GameStatus>();
            _playerInfo = playerInfo;
            _level = playerInfo.GetCurLevel();
            _curGameStatus = new GameStatus(_level);
            _curGameStatus.Load();
            _tubeSelector = new TubeSelector();
            _solution = new Solution();
            _maxUndoCount = maxUndoCount;
            _maxPlusBottleCount = maxPlusBottleCount;
            LoadCurrentUndoCount();
            LoadCurrentPlusBottleCount();

            if (IsComplete())
            {
                CompleteLevel();
            }
        }

        public bool CanUndo => UndoCount > 0 && UndoCount <= _maxUndoCount;

        public int UndoCount { get; private set; }

        public bool CanPlusBottle => PlusBottleCount > 0 && PlusBottleCount <= _maxPlusBottleCount;

        public int PlusBottleCount { get; private set; }

        private void SaveCurrentUndoCount()
        {
            ES3.Save(UNDO_COUNT_KEY, UndoCount);
        }

        private void LoadCurrentUndoCount()
        {
            UndoCount = ES3.Load(UNDO_COUNT_KEY, _maxUndoCount);
        }

        private void SaveCurrentPlusBottleCount()
        {
            ES3.Save(PLUS_BOTTLE_COUNT_KEY, PlusBottleCount);
        }

        private void LoadCurrentPlusBottleCount()
        {
            PlusBottleCount = _maxPlusBottleCount;
            if (ES3.KeyExists(PLUS_BOTTLE_COUNT_KEY))
            {
                PlusBottleCount = ES3.Load(PLUS_BOTTLE_COUNT_KEY, _maxUndoCount);
            }
        }

        public void SaveCurGameStatus()
        {
            _curGameStatus.Save();
        }

        public void AddGameStatusToHistory(GameStatus gameStatus)
        {
            _historyGameStatus.Add(gameStatus);
        }

        public GameStatus? PopLastGameStatus()
        {
            if (_historyGameStatus.Count == 0)
            {
                return null;
            }

            var gameStatus = _historyGameStatus[^1];
            _historyGameStatus.Remove(gameStatus);
            return gameStatus;
        }

        public bool MoveBlock(int indexTube1, int indexTube2)
        {
            var cloneCurGameStatus = _curGameStatus.Clone();
            if (!_curGameStatus.MoveBlock(indexTube1, indexTube2))
            {
                return false;
            }

            AddGameStatusToHistory(cloneCurGameStatus);
            SaveCurGameStatus();
            return true;
        }

        public void ResetPlusBottleCount()
        {
            PlusBottleCount = _maxPlusBottleCount;
        }

        public bool AddNewTube()
        {
            if (!_curGameStatus.AddNewTube())
            {
                return false;
            }

            PlusBottleCount--;
            foreach (var gameStatus in _historyGameStatus)
            {
                gameStatus.AddNewTube();
            }

            SaveCurGameStatus();
            SaveCurrentPlusBottleCount();
            return true;
        }

        public TubeSelector GetTubeSelector()
        {
            return _tubeSelector;
        }

        public void ResetUndoCount()
        {
            UndoCount = _maxUndoCount;
        }

        [CanBeNull]
        public GameStatus UndoStep()
        {
            var gameStatus = PopLastGameStatus();
            if (gameStatus == null)
            {
                return null;
            }

            UndoCount--;
            _curGameStatus = gameStatus;
            SaveCurGameStatus();
            SaveCurrentUndoCount();
            return _curGameStatus;
        }

        public int GetNumTimeBackStatus()
        {
            return _historyGameStatus.Count;
        }

        public bool IsComplete()
        {
            return _curGameStatus.IsComplete();
        }

        public GameStatus GetCurGameStatus()
        {
            return _curGameStatus;
        }

        public GameStatus CompleteLevel()
        {
            _playerInfo.UpdateMaxLevel(_level + 1);
            return MoveToNextLevel();
        }

        public GameStatus MoveToNextLevel()
        {
            if (_level == GameLogic.GetInstance().GetPlayerInfo().GetMaxLevel())
            {
                return null;
            }

            if (_level == MAX_LEVEL)
            {
                return null;
            }

            _level++;
            InitNewLevelData();
            return _curGameStatus;
        }

        private void InitNewLevelData()
        {
            _curGameStatus.MoveToLevel(_level);
            _playerInfo.UpdateCurLevel(_level);
            _playerInfo.UpdateMaxLevel(_level);
            _historyGameStatus.Clear();
            SaveCurGameStatus();
            ResetUndoCount();
            SaveCurrentUndoCount();
            SaveCurrentPlusBottleCount();
        }

        public GameStatus MoveToPreLevel()
        {
            if (_level == 1)
            {
                return null;
            }

            _level--;
            _curGameStatus.MoveToLevel(_level);
            _playerInfo.UpdateCurLevel(_level);
            _historyGameStatus.Clear();
            SaveCurGameStatus();
            ResetUndoCount();
            SaveCurrentUndoCount();
            return _curGameStatus;
        }

        public GameStatus ResetLevel()
        {
            _curGameStatus.MoveToLevel(_level);
            _historyGameStatus.Clear();
            SaveCurGameStatus();
            return _curGameStatus;
        }

        public Solution GetSolution()
        {
            return _solution;
        }

        public void LogStatus()
        {
            Debug.Log("LevelClass: " + _level);
            Debug.Log("Cur status: ");
            _curGameStatus.LogStatus();
            Debug.Log("");
        }

        public int GetLevel()
        {
            return _level;
        }
    }
}
