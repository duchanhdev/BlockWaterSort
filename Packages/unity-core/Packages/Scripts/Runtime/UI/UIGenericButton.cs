using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Egitech.Core.Runtime.UI
{
    public sealed class UIGenericButton : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField]
        private LocalizeStringEvent? _buttonText;

        [Header("Reference")]
        [SerializeField]
        private Button _button = default!;

        public UnityEvent Clicked;

        public void SetButton(bool isSelected)
        {
            if (isSelected)
            {
                SelectButton();
            }
        }

        public void SetButton(LocalizedString localizedString, bool isSelected)
        {
            if (_buttonText != null)
            {
                _buttonText.StringReference = localizedString;
            }

            SetButton(isSelected);
        }

        public void SetButton(string tableEntryReference, bool isSelected)
        {
            if (_buttonText != null)
            {
                _buttonText.StringReference.TableEntryReference = tableEntryReference;
            }

            SetButton(isSelected);
        }

        private void SelectButton()
        {
            _button.Select();
        }

        public void Click()
        {
            Clicked.Invoke();
        }
    }
}