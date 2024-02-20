using FirebaseHelper.Runtime;
using GoogleMobileAds.Common;
using UnityEngine;
using UnityEngine.UI;

namespace BlockSort.GameUI.CustomButton
{
    [RequireComponent(typeof(Button))]
    public sealed class NextButton : RewardAdButton
    {
        protected override void ProcessGameLogicAfterAdClosed()
        {
            base.ProcessGameLogicAfterAdClosed();
            MobileAdsEventExecutor.ExecuteInUpdate(() => { gameUIManager.NextGameLevel(); });
            AnalyticsController.LogLevelStart(GameLogic.GameLogic.GetInstance().GetGame().GetLevel(), "{0}");
        }
    }
}
