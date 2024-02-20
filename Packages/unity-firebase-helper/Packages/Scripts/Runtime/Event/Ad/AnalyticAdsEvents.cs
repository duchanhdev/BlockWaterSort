using System.Collections.Generic;
using Firebase.Analytics;
using UnityEngine;

namespace FirebaseHelper.Runtime.Event.Ad
{
    public abstract class AnalyticAdsEvents : AnalyticEvent
    {
        [SerializeField]
        protected AdsID _adsID;

        [SerializeField]
        private string _adTypeParameterName = "ad_type";

        [SerializeField]
        private string _adLoadSuccessEventName = "ad_load_success";

        [SerializeField]
        private string _adLoadFailedEventName = "ad_load_failed";

        [SerializeField]
        private string _adLoadRetryEventName = "ad_load_retry";

        [SerializeField]
        private string _adShowSuccessEventName = "ad_show_success";

        [SerializeField]
        private string _adShowFailedEventName = "ad_show_failed";

        [SerializeField]
        private string _adIdParameterName = "ad_id";

        [SerializeField]
        private string _adRetryCountParameterName = "retry_count";

        private readonly Dictionary<string, Parameter> _adIdParameters = new Dictionary<string, Parameter>();
        private readonly Dictionary<int, Parameter> _adRetryCountParameters = new Dictionary<int, Parameter>();
        private readonly Dictionary<AnalyticAdLogData, Parameter[]> _cacheParameters = new Dictionary<AnalyticAdLogData, Parameter[]>();
        private Dictionary<AdType, Parameter> _adTypeParameters = new Dictionary<AdType, Parameter>();

        protected abstract AnalyticAdLogData AdLogData { get; }

        private void OnEnable()
        {
            InitAdTypeParameters();
        }

        private void InitAdTypeParameters()
        {
            _adTypeParameters = new Dictionary<AdType, Parameter>
            {
                {
                    AdType.Appopen, new Parameter(_adTypeParameterName, "app_open")
                },
                {
                    AdType.Banner, new Parameter(_adTypeParameterName, "banner")
                },
                {
                    AdType.Interstitial, new Parameter(_adTypeParameterName, "interstitial")
                },
                {
                    AdType.Rewarded, new Parameter(_adTypeParameterName, "rewarded")
                }
            };
        }

        public void AdLoadSuccess()
        {
            var parameters = GetAnalyticParameters(AdLogData);

            LogEvent(_adLoadSuccessEventName, parameters);
        }

        public void AdLoadFailed()
        {
            var parameters = GetAnalyticParameters(AdLogData);

            LogEvent(_adLoadFailedEventName, parameters);
        }
        public void AdShowSuccess()
        {
            var parameters = GetAnalyticParameters(AdLogData);

            LogEvent(_adShowSuccessEventName, parameters);
        }
        public void AdShowFailed()
        {
            var parameters = GetAnalyticParameters(AdLogData);

            LogEvent(_adShowFailedEventName, parameters);
        }

        private Parameter[] GetAnalyticParameters(AnalyticAdLogData data)
        {
            if (_cacheParameters.TryGetValue(data, out var parameters))
            {
                return parameters;
            }

            parameters = new[]
            {
                GetAdIdParameter(data.AdId), _adTypeParameters[data.AdType]
            };

            _cacheParameters.Add(data, parameters);
            return parameters;
        }

        public void AdLoadRetry(int retryCount)
        {
            LogEvent(_adLoadRetryEventName, new[]
            {
                GetAdIdParameter(AdLogData.AdId), _adTypeParameters[AdLogData.AdType], GetAdRetryCountParameter(retryCount)
            });
        }

        private void LogEvent(string eventName, Parameter[] parameters)
        {
            if (FirebaseInit.Instance.IsFirebaseInitialized)
            {
                FirebaseAnalytics.LogEvent(eventName, parameters);
            }
        }

        private Parameter GetAdIdParameter(string adID)
        {
            if (_adIdParameters.TryGetValue(adID, out var parameter))
            {
                return parameter;
            }

            var adIdParameter = new Parameter(_adIdParameterName, adID);
            _adIdParameters.Add(adID, adIdParameter);
            return adIdParameter;
        }

        private Parameter GetAdRetryCountParameter(int retryCount)
        {
            if (_adRetryCountParameters.TryGetValue(retryCount, out var parameter))
            {
                return parameter;
            }

            var adRetryCountParameter = new Parameter(_adRetryCountParameterName, retryCount);
            _adRetryCountParameters.Add(retryCount, adRetryCountParameter);
            return adRetryCountParameter;
        }
    }
}