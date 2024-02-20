using UnityEngine;

namespace FirebaseHelper.Runtime.Event
{
    [CreateAssetMenu(fileName = "EV_FB_Analytic_", menuName = "Firebase/Analytic/Level Start Event")]
    public class AnalyticLevelStartEvent : AnalyticEvent
    {
        public void Log(int input)
        {
            AnalyticsController.LogLevelStart(input);
        }
    }
}