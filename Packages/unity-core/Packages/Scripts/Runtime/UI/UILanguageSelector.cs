using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Egitech.Core.Runtime.UI
{
    public sealed class UILanguageSelector : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField]
        private Locale _englishLocale = default!;

        [Header("Children")]
        [SerializeField]
        private Button _vietnameseButton = default!;

        [SerializeField]
        private Button _englishButton = default!;

        public UnityEvent<Locale> OnSelectLocale;

        private void OnEnable()
        {
            _vietnameseButton.onClick.AddListener(OnVietnameseButtonClick);
            _englishButton.onClick.AddListener(OnEnglishButtonClick);
        }

        private void OnDisable()
        {
            _vietnameseButton.onClick.RemoveListener(OnVietnameseButtonClick);
            _englishButton.onClick.RemoveListener(OnEnglishButtonClick);
        }

        private void OnEnglishButtonClick()
        {
            _englishButton.interactable = false;
            _vietnameseButton.interactable = true;
            OnSelectLocale.Invoke(_englishLocale);
        }

        private void OnVietnameseButtonClick()
        {
            _vietnameseButton.interactable = false;
            _englishButton.interactable = true;
            OnSelectLocale.Invoke(_englishLocale);
        }

        public void SetupLocale(Locale locale)
        {
            if (locale == _englishLocale)
            {
                OnEnglishButtonClick();
                return;
            }

            OnVietnameseButtonClick();
        }
    }
}