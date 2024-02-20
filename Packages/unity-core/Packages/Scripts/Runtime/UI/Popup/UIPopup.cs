using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;

namespace Egitech.Core.Runtime.UI.Popup
{
    public sealed class UIPopup : MonoBehaviour
    {
        [Foldout("References")]
        [SerializeField]
        private LocalizeStringEvent _titleLocalizeStringEvent = default!;

        [Foldout("References")]
        [SerializeField]
        private TextMeshProUGUI _descriptionText = default!;

        [Foldout("References")]
        [SerializeField]
        private LocalizeStringEvent _descriptionLocalizeStringEvent = default!;

        [Foldout("References")]
        [SerializeField]
        private UIGenericButton _positiveButton = default!;

        [Foldout("References")]
        [SerializeField]
        private UIGenericButton _negativeButton = default!;

        private UniTaskCompletionSource<bool> _taskCompletion = new();

        public UniTask<bool> WaitUntilSelected => _taskCompletion.Task;

        private void Start()
        {
            _taskCompletion = new UniTaskCompletionSource<bool>();
        }

        private void OnDisable()
        {
            _negativeButton.Clicked.RemoveListener(CancelButtonClicked);
            _positiveButton.Clicked.RemoveListener(ConfirmButtonClicked);
            _taskCompletion.TrySetResult(false);
        }

        public event UnityAction<bool> OnSelected = delegate { };

        public void SetPopup(PopupType popupType, string? descriptionText = null)
        {
            popupType.UpdatePopup(_titleLocalizeStringEvent, _descriptionLocalizeStringEvent,
                _negativeButton, _positiveButton,
                out var isConfirmation);

            if (!string.IsNullOrEmpty(descriptionText))
            {
                _descriptionText.text = descriptionText;
            }

            if (isConfirmation) // needs two button : Is a decision 
            {
                _negativeButton.Clicked.AddListener(CancelButtonClicked);
                _positiveButton.Clicked.AddListener(ConfirmButtonClicked);
            }
            else // needs only one button : Is an information 
            {
                _positiveButton.Clicked.AddListener(ConfirmButtonClicked);
            }
        }

        private void ConfirmButtonClicked()
        {
            _taskCompletion.TrySetResult(true);
            OnSelected.Invoke(true);
        }

        private void CancelButtonClicked()
        {
            _taskCompletion.TrySetResult(false);
            OnSelected.Invoke(false);
        }

#if UNITY_EDITOR

        [Foldout("Editor-only")]
        [SerializeField]
        private PopupType? _popupType;

        [Foldout("Editor-only")]
        [SerializeField]
        private string? _description;

        [Button]
        public void TestPopupType()
        {
            if (_popupType != null)
            {
                SetPopup(_popupType, _description);
            }
        }

#endif
    }
}