using UnityEngine;

namespace FirebaseHelper.Runtime.Event
{
    [CreateAssetMenu(fileName = "EV_FB_Analytic_", menuName = "Firebase/Analytic/Game Over With Score")]
    public class AnalyticGameOverWithScore: AnalyticEvent
    {
        public void Log(float score)
        {
            AnalyticsController.LogGameOverWithScore(score, _eventName);
        }
    }
}