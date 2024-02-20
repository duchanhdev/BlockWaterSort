using NaughtyAttributes;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.Gameplay.GameState
{
    [CreateAssetMenu(fileName = "GameStateSO")]
    public sealed class GameStateSO : DescriptionBaseSO
    {
        [Header("Game states")]
        [SerializeField]
        [ReadOnly]
        private GameState _currentGameState;

        [SerializeField]
        [ReadOnly]
        private GameState _previousGameState;

        [Header("Broadcasting on")]
        [SerializeField]
        private BoolEvent _changeCombatStateEvent = default!;

        public GameState CurrentGameState => _currentGameState;

        public void UpdateGameState(GameState newGameState)
        {
            if (newGameState == CurrentGameState)
            {
                return;
            }

            _changeCombatStateEvent.Raise(newGameState == GameState.Combat);

            _previousGameState = _currentGameState;
            _currentGameState = newGameState;
        }

        public void ResetToPreviousGameState()
        {
            if (_previousGameState == _currentGameState)
            {
                return;
            }

            if (_previousGameState == GameState.Combat)
            {
                _changeCombatStateEvent.Raise(false);
            }
            else if (_currentGameState == GameState.Combat)
            {
                _changeCombatStateEvent.Raise(true);
            }

            (_previousGameState, _currentGameState) = (_currentGameState, _previousGameState);
        }
    }
}