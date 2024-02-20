using UnityEngine;

namespace FirebaseHelper.Runtime.Event.Ad
{
    [CreateAssetMenu(fileName = "EV_FB_Analytic_InterstitialAds", menuName = "Firebase/Analytic/Ads/Interstitial")]
    public class AnalyticInterstitialAdsEvents : AnalyticAdsEvents
    {
        protected override AnalyticAdLogData AdLogData => new AnalyticAdLogData(_adsID.GetInterstitialAdsId(), AdType.Interstitial);
    }
}