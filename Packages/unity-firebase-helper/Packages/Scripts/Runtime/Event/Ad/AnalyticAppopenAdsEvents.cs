using UnityEngine;

namespace FirebaseHelper.Runtime.Event.Ad
{
    [CreateAssetMenu(fileName = "EV_FB_Analytic_AppopenAds", menuName = "Firebase/Analytic/Ads/Appopen")]
    public class AnalyticAppopenAdsEvents : AnalyticAdsEvents
    {
        protected override AnalyticAdLogData AdLogData => new AnalyticAdLogData(_adsID.GetAppopenAdsId(), AdType.Appopen);
    }
}