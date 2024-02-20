using System;
using UnityEngine;

namespace FirebaseHelper.Runtime.Event.Ad
{
    [Serializable]
    public struct AnalyticAdLogData
    {
        public AnalyticAdLogData(string adId, AdType adType)
        {
            AdId = adId;
            AdType = adType;
        }

        [field: SerializeField]
        public string AdId
        {
            get;
            private set;
        }

        [field: SerializeField]
        public AdType AdType
        {
            get;
            private set;
        }

        public bool Equals(AnalyticAdLogData other)
        {
            return AdId == other.AdId && AdType == other.AdType;
        }
        public override bool Equals(object obj)
        {
            return obj is AnalyticAdLogData other && Equals(other);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(AdId, (int)AdType);
        }
    }
}