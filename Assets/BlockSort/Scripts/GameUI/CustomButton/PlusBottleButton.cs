using BlockSort.GameLogic;
using GoogleMobileAds.Common;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlockSort.GameUI.CustomButton
{
    [RequireComponent(typeof(Button))]
    public sealed class PlusBottleButton : RewardAdButton
    {
        [SerializeField]
        [CanBeNull]
        private TextMeshProUGUI _plusBottleCountText;

        [SerializeField]
        private GameObject _hasPlusBottleCountState;

        [SerializeField]
        private GameObject _hasNoPlusBottleCountState;

        //TODO: Workaround to update count and state when PlusBottleCount changed, should use delegate instead
        private float _timer;

        private static Game CurrentGame => GameLogic.GameLogic.GetInstance().GetGame();

        private void Update()
        {
            if (_timer >= 0.5)
            {
                _timer = 0;
                Refresh();
            }
            else
            {
                _timer += Time.deltaTime;
            }
        }

        protected override void Init()
        {
            base.Init();
            Refresh();
        }

        private void Refresh()
        {
            UpdatePlusBottleCountText();
            UpdateState();
        }

        private void UpdateState()
        {
            _hasPlusBottleCountState.gameObject.SetActive(CurrentGame.CanPlusBottle);
            _hasNoPlusBottleCountState.gameObject.SetActive(!CurrentGame.CanPlusBottle);
        }

        private void UpdatePlusBottleCountText()
        {
            if (_plusBottleCountText == null)
            {
                return;
            }

            _plusBottleCountText.text = $"{CurrentGame.PlusBottleCount}";
        }

        protected override void ProcessGameLogicAfterAdClosed()
        {
            base.ProcessGameLogicAfterAdClosed();
            MobileAdsEventExecutor.ExecuteInUpdate(() => { gameUIManager.AddNewBottle(); });
        }

        protected override bool ShouldShowAds()
        {
            return !CurrentGame.CanPlusBottle;
        }

        protected override void OnAfterAdClosed()
        {
            base.OnAfterAdClosed();
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                GameLogic.GameLogic.GetInstance().GetGame().ResetPlusBottleCount();
                UpdatePlusBottleCountText();
                UpdateState();
            });
        }
    }
}
