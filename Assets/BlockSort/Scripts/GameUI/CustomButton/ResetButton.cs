using GoogleMobileAds.Common;
using UnityEngine;
using UnityEngine.UI;

namespace BlockSort.GameUI.CustomButton
{
    [RequireComponent(typeof(Button))]
    public sealed class ResetButton : RewardAdButton
    {
        protected override void ProcessGameLogicAfterAdClosed()
        {
            base.ProcessGameLogicAfterAdClosed();
            MobileAdsEventExecutor.ExecuteInUpdate(() => { gameUIManager.ResetGameLevel(); });
        }

        protected override bool ShouldShowAds()
        {
            return false;
        }
    }
}
