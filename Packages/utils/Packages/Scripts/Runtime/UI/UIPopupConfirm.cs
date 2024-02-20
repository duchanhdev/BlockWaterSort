using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Egitech.Utils.Runtime.UI
{
    public sealed class UIPopupConfirm : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private TextMeshProUGUI _descriptionText = default!;

        [SerializeField]
        private Button _positiveButton = default!;

        [SerializeField]
        private TextMeshProUGUI _positiveButtonText = default!;

        [SerializeField]
        private Button _negativeButton = default!;

        [SerializeField]
        private TextMeshProUGUI _negativeButtonText = default!;

        public UnityAction OnPressedNegativeButton = () => { };

        public UnityAction OnPressedPositiveButton = () => { };

        private void OnEnable()
        {
            _positiveButton.onClick.AddListener(OnPressedPositiveButtonInternal);
            _negativeButton.onClick.AddListener(OnPressedNegativeButtonInternal);
        }

        private void OnDisable()
        {
            _positiveButton.onClick.RemoveListener(OnPressedPositiveButtonInternal);
            _negativeButton.onClick.RemoveListener(OnPressedNegativeButtonInternal);
        }

#if UNITY_EDITOR
        [Button]
        private void RefreshUI()
        {
            SetDescription(_description);
            SetPositiveButton(_positiveButtonLabel);
            SetNegativeButton(_negativeButtonLabel);
        }
#endif

        private void SetNegativeButton(string label)
        {
            _negativeButtonText.text = label;
        }

        private void SetPositiveButton(string label)
        {
            _positiveButtonText.text = label;
        }

        private void SetDescription(string description)
        {
            _descriptionText.text = description;
        }

        private void OnPressedPositiveButtonInternal()
        {
            OnPressedPositiveButton.Invoke();
            gameObject.SetActive(false);
        }

        private void OnPressedNegativeButtonInternal()
        {
            OnPressedNegativeButton.Invoke();
            gameObject.SetActive(false);
        }
#if UNITY_EDITOR
        [Header("Config")]
        [SerializeField]
        private string _description = string.Empty;

        [SerializeField]
        private string _positiveButtonLabel = string.Empty;

        [SerializeField]
        private string _negativeButtonLabel = string.Empty;
#endif
    }
}