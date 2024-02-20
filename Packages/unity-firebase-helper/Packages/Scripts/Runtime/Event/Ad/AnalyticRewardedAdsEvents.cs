using UnityEngine;

namespace FirebaseHelper.Runtime.Event.Ad
{
    [CreateAssetMenu(fileName = "EV_FB_Analytic_RewardedAds", menuName = "Firebase/Analytic/Ads/Rewarded")]
    public class AnalyticRewardedAdsEvents : AnalyticAdsEvents
    {
        protected override AnalyticAdLogData AdLogData => new AnalyticAdLogData(_adsID.GetRewardedAdsId(), AdType.Rewarded);
    }
}