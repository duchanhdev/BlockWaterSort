using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Egitech.Utils.Runtime.UI.Toggle
{
    public sealed class UIToggle : MonoBehaviour
    {
        [Foldout("Config")]
        [SerializeField]
        private UIToggleState[] _states = default!;

        [Foldout("Config")]
        [SerializeField]
        private UIToggleState _initState = default!;

        [Foldout("References")]
        [SerializeField]
        private Image _toggleButtonImage = default!;

        [Foldout("References")]
        [SerializeField]
        private LocalizeStringEvent _toggleButtonLocalizedStringEvent = default!;

        [Foldout("References")]
        [SerializeField]
        private LocalizeStringEvent _chatModeLabelLocalizedStringEvent = default!;

        private int _currentStateIndex;

        public UIToggleState CurrentState { get; private set; } = default!;

        private void OnEnable()
        {
            for (var i = 0; i < _states.Length; i++)
            {
                if (_states[i] != _initState)
                {
                    continue;
                }

                _currentStateIndex = i;
                break;
            }

            CurrentState = _initState;
            Refresh();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_states.Length == 0)
            {
                Debug.LogError($"{_states} cannot be empty");
            }
        }
#endif

        public void NextState()
        {
            if (_states.Length <= 1)
            {
                return;
            }

            if (_currentStateIndex == _states.Length - 1)
            {
                _currentStateIndex = 0;
            }
            else
            {
                _currentStateIndex++;
            }

            CurrentState = _states[_currentStateIndex];

            Refresh();
        }

        private void Refresh()
        {
            _toggleButtonImage.sprite = CurrentState.ToggleButtonImage;

            _toggleButtonLocalizedStringEvent.StringReference = CurrentState.Label;
            _toggleButtonLocalizedStringEvent.RefreshString();

            _chatModeLabelLocalizedStringEvent.StringReference = CurrentState.ChatModeLabel;
            _chatModeLabelLocalizedStringEvent.RefreshString();
        }
    }
}