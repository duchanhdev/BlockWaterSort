using Egitech.Core.Runtime;
using Firebase.Analytics;
using UnityEngine;

namespace FirebaseHelper.Runtime
{
    public sealed class AnalyticsController : Singleton<AnalyticsController>
    {
        private const string EVENT_AD_REWARD_PROMPT = "ad_reward_prompt";

        private const string EVENT_AD_REWARD_IMPRESSION = "ad_reward_impression";

        private const string EVENT_LEVEL_FAIL = "level_fail";

        private const string EVENT_LEVEL_SUCCESS = "level_success";

        private const string EVENT_LEVEL_WRONG_ANSWER = "level_wrong_answer";

        private const string EVENT_GAME_START = "game_start";

        private const string EVENT_GAME_COMPLETE = "game_complete";

        private const string PARAM_AD_UNIT_ID = "ad_unit_id";

        private const string PARAM_ELAPSED_TIME_SEC = "elapsed_time_sec";

        private const string PARAM_HINT_USED = "hint_used";

        private const string PARAM_NUMBER_OF_ATTEMPTS = "number_of_attempts";

        private const string PARAM_NUMBER_OF_CORRECT_ANSWERS = "number_of_correct_answers";

        public const string SCREEN_MAIN = "main";

        public const string SCREEN_GAME = "game";

        public static void LogGameStart()
        {
            if (!FirebaseInit.Instance.IsFirebaseInitialized)
            {
                return;
            }

            FirebaseAnalytics.LogEvent(EVENT_GAME_START);
        }

        public static void LogLevelStart(int levelIndex, string format = "level_{0}")
        {
            if (!FirebaseInit.Instance.IsFirebaseInitialized)
            {
                return;
            }
            
            var levelName = string.Format(format, levelIndex);
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart,
                FirebaseAnalytics.ParameterLevelName, levelName);
        }

        public static void LogLevelWrongAnswer(string levelName)
        {
            if (!FirebaseInit.Instance.IsFirebaseInitialized)
            {
                return;
            }
            
            FirebaseAnalytics.LogEvent(EVENT_LEVEL_WRONG_ANSWER,
                FirebaseAnalytics.ParameterLevelName, levelName);
        }

        public static void LogAdRewardPrompt(string adUnitId)
        {
            if (!FirebaseInit.Instance.IsFirebaseInitialized)
            {
                return;
            }
            
            FirebaseAnalytics.LogEvent(EVENT_AD_REWARD_PROMPT, PARAM_AD_UNIT_ID, adUnitId);
        }

        public static void LogAdRewardImpression(string adUnitId)
        {
            if (!FirebaseInit.Instance.IsFirebaseInitialized)
            {
                return;
            }
            
            FirebaseAnalytics.LogEvent(EVENT_AD_REWARD_IMPRESSION, PARAM_AD_UNIT_ID, adUnitId);
        }

        public static void LogLevelSuccess(int levelIndex)
        {
            if (!FirebaseInit.Instance.IsFirebaseInitialized)
            {
                return;
            }
            
            var levelName = $"level_{levelIndex}";
            FirebaseAnalytics.LogEvent(EVENT_LEVEL_SUCCESS, FirebaseAnalytics.ParameterLevelName, levelName);
        }

        public static void LogLevelFail(string levelName)
        {
            if (!FirebaseInit.Instance.IsFirebaseInitialized)
            {
                return;
            }
            
            FirebaseAnalytics.LogEvent(EVENT_LEVEL_FAIL, FirebaseAnalytics.ParameterLevelName, levelName);
        }

        public static void LogGameComplete()
        {
            if (!FirebaseInit.Instance.IsFirebaseInitialized)
            {
                return;
            }
            
            FirebaseAnalytics.LogEvent(EVENT_GAME_COMPLETE);
        }

        public static void LogGameOverWithScore(float score, string format = "game_finish_over_{0}")
        {
            if (!FirebaseInit.Instance.IsFirebaseInitialized)
            {
                return;
            }
            
            var eventName = string.Format(format, score);
            FirebaseAnalytics.LogEvent(eventName);
        }

        public static void SetScreenName(string screenName)
        {
            if (!FirebaseInit.Instance.IsFirebaseInitialized)
            {
                return;
            }
            
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventScreenView, FirebaseAnalytics.ParameterScreenName,
                screenName);
        }
    }
}