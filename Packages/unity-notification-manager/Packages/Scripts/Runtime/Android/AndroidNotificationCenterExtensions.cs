#if UNITY_ANDROID
using UnityEngine;

namespace NotificationManager.Runtime.Android
{
    /// <summary>
    ///     Class for managing Android notification center extensions.
    /// </summary>
    public class AndroidNotificationCenterExtensions
    {
        private static bool _initialized;
        private static AndroidJavaObject _androidNotificationExtensions;

        /// <summary>
        ///     Initialize the Android notification center extensions.
        /// </summary>
        public static bool Initialize()
        {
            if (_initialized)
            {
                return true;
            }

#if UNITY_EDITOR
            _androidNotificationExtensions = null;
            _initialized = false;
#else
            var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            var context = activity.Call<AndroidJavaObject>("getApplicationContext");

            var managerClass = new AndroidJavaClass("com.unity.androidnotifications.UnityNotificationManager");
            var notificationManagerImpl = managerClass.CallStatic<AndroidJavaObject>("getNotificationManagerImpl", context, activity);
            var notificationManager = notificationManagerImpl.Call<AndroidJavaObject>("getNotificationManager");

            var pluginClass = new AndroidJavaClass("com.unity.androidnotifications.AndroidNotificationCenterExtensions");
            _androidNotificationExtensions = pluginClass.CallStatic<AndroidJavaObject>("getExtensionsImpl", context, notificationManager);

            _initialized = true;
#endif
            return _initialized;
        }

        /// <summary>
        ///     Checks whether notifications are enabled on any channel.
        /// </summary>
        public static bool AreNotificationsEnabled()
        {
            if (!_initialized)
            {
                // By default notifications are enabled
                return true;
            }

            return _androidNotificationExtensions.Call<bool>("areNotificationsEnabled");
        }

        /// <summary>
        ///     Checks whether notifications are enabled on a specific channel.
        /// </summary>
        /// <param name="channelId">The ID of the channel.</param>
        public static bool AreNotificationsEnabled(string channelId)
        {
            if (!_initialized)
            {
                // By default notifications are enabled
                return true;
            }

            return _androidNotificationExtensions.Call<bool>("areNotificationsEnabled", channelId);
        }
    }
}
#endif