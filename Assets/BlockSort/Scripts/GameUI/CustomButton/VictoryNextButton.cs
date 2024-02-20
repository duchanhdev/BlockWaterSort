using FirebaseHelper.Runtime;
using GoogleMobileAds.Common;
using UnityEngine;
using UnityEngine.UI;

namespace BlockSort.GameUI.CustomButton
{
    [RequireComponent(typeof(Button))]
    public sealed class VictoryNextButton : RewardAdButton
    {
        protected override void ProcessGameLogicAfterAdClosed()
        {
            base.ProcessGameLogicAfterAdClosed();

            AnalyticsController.LogLevelSuccess(GameLogic.GameLogic.GetInstance().GetGame().GetLevel());
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                gameUIManager.HideVictoryLayer();
                gameUIManager.CompleteLevel();
            });
            AnalyticsController.LogLevelStart(GameLogic.GameLogic.GetInstance().GetGame().GetLevel(), "{0}");
        }

        protected override bool ShouldShowAds()
        {
            return false;

            var level = GameLogic.GameLogic.GetInstance().GetGame().GetLevel();
            return level % 3 == 0;
        }
    }
}
