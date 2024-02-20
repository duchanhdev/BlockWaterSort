#if UNITY_ANDROID
using System;
using Cysharp.Threading.Tasks;
using NotificationManager.Runtime.Core;
using Unity.Notifications.Android;

namespace NotificationManager.Runtime.Android
{
    /// <summary>
    ///     Android implementation of <see cref="IGameNotificationsPlatform" />.
    /// </summary>
    public class AndroidNotificationsPlatform : IGameNotificationsPlatform<AndroidGameNotification>, IDisposable
    {
        /// <summary>
        ///     Instantiate a new instance of <see cref="AndroidNotificationsPlatform" />.
        /// </summary>
        public AndroidNotificationsPlatform()
        {
            AndroidNotificationCenter.OnNotificationReceived += OnLocalNotificationReceived;
        }

        /// <summary>
        ///     Gets or sets the default channel ID for notifications.
        /// </summary>
        /// <value>The default channel ID for new notifications, or null.</value>
        public string DefaultChannelId { get; set; }

        /// <summary>
        ///     Unregister delegates.
        /// </summary>
        public void Dispose()
        {
            AndroidNotificationCenter.OnNotificationReceived -= OnLocalNotificationReceived;
        }

        /// <inheritdoc />
        public event Action<IGameNotification> NotificationReceived;

        public async UniTask RequestNotificationPermission()
        {
            var request = new PermissionRequest();
            while (request.Status == PermissionStatus.RequestPending)
            {
                await UniTask.Yield();
            }
        }

        /// <inheritdoc />
        /// <remarks>
        ///     Will set the <see cref="AndroidGameNotification.Id" /> field of <paramref name="gameNotification" />.
        /// </remarks>
        public void ScheduleNotification(AndroidGameNotification gameNotification)
        {
            if (gameNotification == null)
            {
                throw new ArgumentNullException(nameof(gameNotification));
            }

            if (gameNotification.Id.HasValue)
            {
                AndroidNotificationCenter.SendNotificationWithExplicitID(gameNotification.InternalNotification,
                    gameNotification.DeliveredChannel,
                    gameNotification.Id.Value);
            }
            else
            {
                var notificationId = AndroidNotificationCenter.SendNotification(gameNotification.InternalNotification,
                    gameNotification.DeliveredChannel);
                gameNotification.Id = notificationId;
            }

            gameNotification.OnScheduled();
        }

        /// <inheritdoc />
        /// <remarks>
        ///     Will set the <see cref="AndroidGameNotification.Id" /> field of <paramref name="gameNotification" />.
        /// </remarks>
        public void ScheduleNotification(IGameNotification gameNotification)
        {
            if (gameNotification == null)
            {
                throw new ArgumentNullException(nameof(gameNotification));
            }

            if (gameNotification is not AndroidGameNotification androidNotification)
            {
                throw new InvalidOperationException(
                    "Notification provided to ScheduleNotification isn't an AndroidGameNotification.");
            }

            ScheduleNotification(androidNotification);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Create a new <see cref="AndroidGameNotification" />.
        /// </summary>
        public AndroidGameNotification CreateNotification()
        {
            var notification = new AndroidGameNotification
            {
                DeliveredChannel = DefaultChannelId
            };

            return notification;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Create a new <see cref="AndroidGameNotification" />.
        /// </summary>
        IGameNotification IGameNotificationsPlatform.CreateNotification()
        {
            return CreateNotification();
        }

        /// <inheritdoc />
        public void CancelNotification(int notificationId)
        {
            AndroidNotificationCenter.CancelScheduledNotification(notificationId);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Not currently implemented on Android
        /// </summary>
        public void DismissNotification(int notificationId)
        {
            AndroidNotificationCenter.CancelDisplayedNotification(notificationId);
        }

        /// <inheritdoc />
        public void CancelAllScheduledNotifications()
        {
            AndroidNotificationCenter.CancelAllScheduledNotifications();
        }

        /// <inheritdoc />
        public void DismissAllDisplayedNotifications()
        {
            AndroidNotificationCenter.CancelAllDisplayedNotifications();
        }

        /// <inheritdoc />
        IGameNotification IGameNotificationsPlatform.GetLastNotification()
        {
            return GetLastNotification();
        }

        /// <inheritdoc />
        public AndroidGameNotification GetLastNotification()
        {
            var data = AndroidNotificationCenter.GetLastNotificationIntent();

            return data != null ? new AndroidGameNotification(data.Notification, data.Id, data.Channel) : null;
        }

        /// <summary>
        ///     Does nothing on Android.
        /// </summary>
        public void OnForeground()
        {
        }

        /// <summary>
        ///     Does nothing on Android.
        /// </summary>
        public void OnBackground()
        {
        }

        // Event handler for receiving local notifications.
        private void OnLocalNotificationReceived(AndroidNotificationIntentData data)
        {
            // Create a new AndroidGameNotification out of the delivered notification, but only
            // if the event is registered
            NotificationReceived?.Invoke(new AndroidGameNotification(data.Notification, data.Id, data.Channel));
        }
    }
}
#endif