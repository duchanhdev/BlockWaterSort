using UnityEngine;
using UnityEngine.Serialization;

namespace FirebaseHelper.Runtime
{
    [CreateAssetMenu(fileName = "Ads_ID", menuName = "Admob/Config/Ads ID")]
    public class AdsID : ScriptableObject
    {
        private const string BANNER_ADS_ID_UNITY_ANDROID_AND_DEV = "ca-app-pub-3940256099942544/6300978111";
        private const string BANNER_ADS_ID_UNITY_IPHONE_AND_DEV = "ca-app-pub-3940256099942544/2934735716";
        private const string INTERSTITIAL_ADS_ID_UNITY_ANDROID_DEV = "ca-app-pub-3940256099942544/1033173712";
        private const string INTERSTITIAL_ADS_ID_UNITY_IPHONE_DEV = "ca-app-pub-3940256099942544/4411468910";
        private const string REWARDED_ADS_ID_UNITY_ANDROID_AND_DEV = "ca-app-pub-3940256099942544/5224354917";
        private const string REWARDED_ADS_ID_UNITY_IPHONE_AND_DEV = "ca-app-pub-3940256099942544/1712485313";
        private const string APPOPEN_ADS_ID_UNITY_ANDROID_DEV = "ca-app-pub-3940256099942544/3419835294";
        private const string APPOPEN_ADS_ID_UNITY_IPHONE_DEV = "ca-app-pub-3940256099942544/5662855259";

        [Header("Banner ID")]
        [SerializeField]
        private string _bannerAdsIdUnityEditor = "unused";

        [SerializeField]
        private string _bannerAdsIdUnityAndroidAndDev = BANNER_ADS_ID_UNITY_ANDROID_AND_DEV;

        [SerializeField]
        private string _bannerAdsIdUnityAndroidAndProd = "";

        [SerializeField]
        private string _bannerAdsIdUnityIphoneAndDev = BANNER_ADS_ID_UNITY_IPHONE_AND_DEV;

        [SerializeField]
        private string _bannerAdsIdUnityIphoneAndProd = "";

        [SerializeField]
        private string _bannerAdsIdUnityOther = "unexpected_platform";

        [Header("Intertitial ID")]
        [SerializeField]
        private string _interstitialAdsIdUnityEditor = "unused";

        [FormerlySerializedAs("_interstitialAdsIdUnityAndroid")]
        [SerializeField]
        private string _interstitialAdsIdUnityAndroidDev = INTERSTITIAL_ADS_ID_UNITY_ANDROID_DEV;

        [SerializeField]
        private string _interstitialAdsIdUnityAndroidProd = "";

        [FormerlySerializedAs("_interstitialAdsIdUnityIphone")]
        [SerializeField]
        private string _interstitialAdsIdUnityIphoneDev = INTERSTITIAL_ADS_ID_UNITY_IPHONE_DEV;

        [SerializeField]
        private string _interstitialAdsIdUnityIphoneProd = "";

        [SerializeField]
        private string _interstitialAdsIdUnityOther = "unexpected_platform";

        [Header("Rewarded ID")]
        [SerializeField]
        private string _rewardedAdsIdUnityEditor = "unused";

        [SerializeField]
        private string _rewardedAdsIdUnityAndroidAndDev = REWARDED_ADS_ID_UNITY_ANDROID_AND_DEV;

        [SerializeField]
        private string _rewardedAdsIdUnityAndroidAndProd = "";

        [SerializeField]
        private string _rewardedAdsIdUnityIphoneAndDev = REWARDED_ADS_ID_UNITY_IPHONE_AND_DEV;

        [SerializeField]
        private string _rewardedAdsIdUnityIphoneAndProd = "";

        [SerializeField]
        private string _rewardedAdsIdUnityOther = "unexpected_platform";

        [Header("Appopen ID")]
        [SerializeField]
        private string _appopenAdsIdUnityEditor = "unused";

        [FormerlySerializedAs("_appopenAdsIdUnityAndroid")]
        [SerializeField]
        private string _appopenAdsIdUnityAndroidDev = APPOPEN_ADS_ID_UNITY_ANDROID_DEV;

        [SerializeField]
        private string _appopenAdsIdUnityAndroidProd = "";

        [FormerlySerializedAs("_appopenAdsIdUnityIphone")]
        [SerializeField]
        private string _appopenAdsIdUnityIphoneDev = APPOPEN_ADS_ID_UNITY_IPHONE_DEV;

        [SerializeField]
        private string _appopenAdsIdUnityIphoneProd = "";

        [SerializeField]
        private string _appopenAdsIdUnityOther = "unexpected_platform";

        public string GetBannerAdsId()
        {
#if UNITY_EDITOR
            return _bannerAdsIdUnityEditor;
#elif UNITY_ANDROID && DEV
            return _bannerAdsIdUnityAndroidAndDev;
#elif UNITY_ANDROID && PROD
            if (string.IsNullOrEmpty(_bannerAdsIdUnityAndroidAndProd))
            {
                return _bannerAdsIdUnityAndroidAndDev;
            }
            return _bannerAdsIdUnityAndroidAndProd;
#elif UNITY_IPHONE && DEV
            return _bannerAdsIdUnityIphoneAndDev;
#elif UNITY_IPHONE && PROD
            if (string.IsNullOrEmpty(_bannerAdsIdUnityIphoneAndProd))
            {
                return _bannerAdsIdUnityIphoneAndDev;
            }
            return _bannerAdsIdUnityIphoneAndProd;
#else
            return _bannerAdsIdUnityOther;
#endif
        }

        public string GetInterstitialAdsId()
        {
#if UNITY_EDITOR
            return _interstitialAdsIdUnityEditor;
#elif UNITY_ANDROID && DEV
            return _interstitialAdsIdUnityAndroidDev;
#elif UNITY_ANDROID && PROD
            if (string.IsNullOrEmpty(_interstitialAdsIdUnityAndroidProd))
            {
                return _interstitialAdsIdUnityAndroidDev;
            }
            return _interstitialAdsIdUnityAndroidProd;
#elif UNITY_IOS && DEV
            return _interstitialAdsIdUnityIphoneDev;
#elif UNITY_IOS && PROD
            if (string.IsNullOrEmpty(_interstitialAdsIdUnityIphoneProd))
            {
                return _interstitialAdsIdUnityIphoneDev;
            }
            return _interstitialAdsIdUnityIphoneProd;
#else
            return _interstitialAdsIdUnityOther;
#endif
        }

        public string GetRewardedAdsId()
        {
#if UNITY_EDITOR
            return _rewardedAdsIdUnityEditor;
#elif UNITY_ANDROID && DEV
            return _rewardedAdsIdUnityAndroidAndDev;
#elif UNITY_ANDROID && PROD
            if (string.IsNullOrEmpty(_rewardedAdsIdUnityAndroidAndProd)) 
            {
                return _rewardedAdsIdUnityAndroidAndDev;
            }
            return _rewardedAdsIdUnityAndroidAndProd;
#elif UNITY_IPHONE && DEV
            return _rewardedAdsIdUnityIphoneAndDev;
#elif UNITY_IPHONE && PROD
            if (string.IsNullOrEmpty(_rewardedAdsIdUnityIphoneAndProd))
            {
                return _rewardedAdsIdUnityIphoneAndDev;
            }
            return _rewardedAdsIdUnityIphoneAndProd;
#else
            return _rewardedAdsIdUnityOther;
#endif
        }

        public string GetAppopenAdsId()
        {
#if UNITY_EDITOR
            return _appopenAdsIdUnityEditor;
#elif UNITY_ANDROID && DEV
            return _appopenAdsIdUnityAndroidDev;
#elif UNITY_ANDROID && PROD
            if (string.IsNullOrEmpty(_appopenAdsIdUnityAndroidProd))
            {
                return _appopenAdsIdUnityAndroidDev;
            }
            return _appopenAdsIdUnityAndroidProd;
#elif UNITY_IOS && DEV
            return _appopenAdsIdUnityIphoneDev;
#elif UNITY_IOS && PROD
            if (string.IsNullOrEmpty(_appopenAdsIdUnityIphoneProd))
            {
                return _appopenAdsIdUnityIphoneDev;
            }
            return _appopenAdsIdUnityIphoneProd;
#else
            return _appopenAdsIdUnityOther;
#endif
        }

#if UNITY_EDITOR
        [ContextMenu("Reset Ads Id Dev")]
        private void ResetAdsIdDev()
        {
            _bannerAdsIdUnityAndroidAndDev = BANNER_ADS_ID_UNITY_ANDROID_AND_DEV;
            _bannerAdsIdUnityIphoneAndDev = BANNER_ADS_ID_UNITY_IPHONE_AND_DEV;
            _interstitialAdsIdUnityAndroidDev = INTERSTITIAL_ADS_ID_UNITY_ANDROID_DEV;
            _interstitialAdsIdUnityIphoneDev = INTERSTITIAL_ADS_ID_UNITY_IPHONE_DEV;
            _rewardedAdsIdUnityAndroidAndDev = REWARDED_ADS_ID_UNITY_ANDROID_AND_DEV;
            _rewardedAdsIdUnityIphoneAndDev = REWARDED_ADS_ID_UNITY_IPHONE_AND_DEV;
            _appopenAdsIdUnityAndroidDev = APPOPEN_ADS_ID_UNITY_ANDROID_DEV;
            _appopenAdsIdUnityIphoneDev = APPOPEN_ADS_ID_UNITY_IPHONE_DEV;
        }
#endif
    }
}