using Firebase.Analytics;
using UnityEngine;

namespace FirebaseHelper.Runtime.Event
{
    [CreateAssetMenu(fileName = "EV_FB_Analytic_", menuName = "Firebase/Analytic/Int Event")]
    public class AnalyticIntEvent : AnalyticEvent
    {
        public void Log(int input)
        {
            if (FirebaseInit.Instance.IsFirebaseInitialized)
            {
                FirebaseAnalytics.LogEvent(string.Format(_eventName, input));
            }
        }
    }
}