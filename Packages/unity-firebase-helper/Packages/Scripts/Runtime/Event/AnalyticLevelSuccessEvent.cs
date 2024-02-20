using Firebase.Analytics;
using UnityEngine;

namespace FirebaseHelper.Runtime.Event
{
    [CreateAssetMenu(fileName = "EV_FB_Analytic_", menuName = "Firebase/Analytic/Level Success Event")]
    public class AnalyticLevelSuccessEvent : AnalyticEvent
    {
        [SerializeField]
        [Tooltip("Use first parameter of Log(int levelNumber, string difficultyName) method")]
        private string _eventFormat = "level_{0}_success";

        [SerializeField]
        private string _parameterName = "playmode";

        [SerializeField]
        [Tooltip("Use second parameter of Log(int levelNumber, string difficultyName) method")]
        private string _parameterValueFormat = "{0}";

        public void Log(int levelNumber, string difficultyName)
        {
            if (!FirebaseInit.Instance.IsFirebaseInitialized)
            {
                return;
            }

            var eventName = string.Format(_eventFormat, levelNumber);
            var parameterValue = string.Format(_parameterValueFormat, difficultyName);
            FirebaseAnalytics.LogEvent(eventName, _parameterName, parameterValue);
        }
    }
}