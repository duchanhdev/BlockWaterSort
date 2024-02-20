using Firebase.Analytics;
using UnityEngine;

namespace FirebaseHelper.Runtime.Event
{
    [CreateAssetMenu(fileName = "EV_FB_Analytic_", menuName = "Firebase/Analytic/Event")]
    public class AnalyticEvent : ScriptableObject
    {
        [SerializeField]
        protected string _eventName = "egi_btn__click";

        public void Log()
        {
            if (FirebaseInit.Instance.IsFirebaseInitialized)
            {
                FirebaseAnalytics.LogEvent(_eventName);
            }
        }
    }
}