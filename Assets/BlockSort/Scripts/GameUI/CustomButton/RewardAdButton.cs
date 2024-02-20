using FirebaseHelper.Runtime;
using GoogleMobileAds.Common;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace BlockSort.GameUI.CustomButton
{
    public class RewardAdButton : MonoBehaviour
    {
        [SerializeField]
        protected GameUIManager gameUIManager;

        [SerializeField]
        [CanBeNull]
        private GameObject _inputBlocker;

        [SerializeField]
        private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(TaskOnClick);
            Init();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _button = GetComponent<Button>();
        }
#endif

        protected virtual void Init()
        {
        }

        private void TaskOnClick()
        {
            BlockInput();
            ProcessGameLogicAfterAdClosed();
            UnblockInput();
            
            /*
            BlockInput();
            if (!ShouldShowAds())
            {
                ProcessGameLogicAfterAdClosed();
                UnblockInput();
                return;
            }

            var googleAdMobController = GoogleAdMobController.Instance;
            if (!googleAdMobController.HasRewardedAd)
            {
                OnAdClosed();
            }

            googleAdMobController.OnAdClosedEvent.AddListener(OnAdClosed);
            googleAdMobController.ShowRewardedAd();
            */
        }

        private void UnblockInput()
        {
            if (_inputBlocker != null)
            {
                _inputBlocker.SetActive(false);
            }
        }

        private void BlockInput()
        {
            if (_inputBlocker != null)
            {
                _inputBlocker.SetActive(true);
            }
        }

        private void OnAdClosed()
        {
            var googleAdMobController = GoogleAdMobController.Instance;
            googleAdMobController.RequestAndLoadRewardedAd();
            googleAdMobController.OnAdClosedEvent.RemoveListener(OnAdClosed);
            MobileAdsEventExecutor.ExecuteInUpdate(UnblockInput);
            ProcessGameLogicAfterAdClosed();
            OnAfterAdClosed();
        }

        protected virtual void OnAfterAdClosed()
        {
        }

        protected virtual void ProcessGameLogicAfterAdClosed()
        {
        }

        protected virtual bool ShouldShowAds()
        {
            return true;
        }
    }
}
