using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace Egitech.Core.Runtime.UI.Popup
{
    [CreateAssetMenu(menuName = "UI/PopupType")]
    public sealed class PopupType : ScriptableObject
    {
        [SerializeField]
        private LocalizedString _title = default!;

        [SerializeField]
        private LocalizedString _description = default!;

        [SerializeField]
        private LocalizedString _positiveButtonLabel = default!;

        [ShowIf("_isConfirmation")]
        [SerializeField]
        private LocalizedString _negativeButtonLabel = default!;

        [SerializeField]
        private bool _isConfirmation;

        public void UpdatePopup(LocalizeStringEvent titleText, LocalizeStringEvent descriptionText,
            UIGenericButton negativeButton, UIGenericButton positiveButton,
            out bool isConfirmation)
        {
            titleText.StringReference = _title;
            descriptionText.StringReference = _description;

            isConfirmation = _isConfirmation;

            if (_isConfirmation)
            {
                positiveButton.gameObject.SetActive(true);
                negativeButton.gameObject.SetActive(true);
            }
            else
            {
                positiveButton.gameObject.SetActive(true);
                negativeButton.gameObject.SetActive(false);
            }

            positiveButton.SetButton(_positiveButtonLabel, true);
            negativeButton.SetButton(_negativeButtonLabel, false);
        }
    }
}