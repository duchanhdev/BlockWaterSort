using FirebaseHelper.Runtime;
using GoogleMobileAds.Common;
using UnityEngine;
using UnityEngine.UI;

namespace BlockSort.GameUI.CustomButton
{
    [RequireComponent(typeof(Button))]
    public sealed class PreButton : RewardAdButton
    {
        protected override void ProcessGameLogicAfterAdClosed()
        {
            base.ProcessGameLogicAfterAdClosed();
            MobileAdsEventExecutor.ExecuteInUpdate(() => { gameUIManager.PreGameLevel(); });
            AnalyticsController.LogLevelStart(GameLogic.GameLogic.GetInstance().GetGame().GetLevel(), "{0}");
        }
    }
}
