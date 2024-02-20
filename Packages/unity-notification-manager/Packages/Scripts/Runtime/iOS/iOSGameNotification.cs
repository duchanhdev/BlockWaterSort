using System;
using JetBrains.Annotations;
using NotificationManager.Runtime.Core;
using Unity.Notifications.iOS;
using UnityEngine;
using UnityEngine.Assertions;

namespace FirebaseHelper.Runtime.Notification.iOS
{
    /// <summary>
    ///     iOS implementation of <see cref="IGameNotification" />.
    /// </summary>
    public class iOSGameNotification : IGameNotification
    {
        /// <summary>
        ///     Instantiate a new instance of <see cref="iOSGameNotification" />.
        /// </summary>
        public iOSGameNotification()
        {
            InternalNotification = new iOSNotification
            {
                ShowInForeground = true // Deliver in foreground by default
            };
        }

        /// <summary>
        ///     Instantiate a new instance of <see cref="iOSGameNotification" /> from a delivered notification.
        /// </summary>
        /// <param name="internalNotification">The delivered notification.</param>
        internal iOSGameNotification(iOSNotification internalNotification)
        {
            InternalNotification = internalNotification;
        }

        /// <summary>
        ///     Gets the internal notification object used by the mobile notifications system.
        /// </summary>
        public iOSNotification InternalNotification { get; }

        /// <summary>
        ///     The category identifier for this notification.
        /// </summary>
        public string CategoryIdentifier
        {
            get => InternalNotification.CategoryIdentifier;
            set => InternalNotification.CategoryIdentifier = value;
        }

        /// <inheritdoc />
        /// <remarks>
        ///     Internally stored as a string. Gets parsed to an integer when retrieving.
        /// </remarks>
        /// <value>The identifier as an integer, or null if the identifier couldn't be parsed as a number.</value>
        public int? Id
        {
            get
            {
                if (int.TryParse(InternalNotification.Identifier, out var value))
                {
                    return value;
                }

                Debug.LogWarning("Internal iOS notification's identifier isn't a number.");
                return null;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                InternalNotification.Identifier = value.Value.ToString();
            }
        }

        /// <inheritdoc />
        public string Title
        {
            get => InternalNotification.Title;
            set => InternalNotification.Title = value;
        }

        /// <inheritdoc />
        public string Body
        {
            get => InternalNotification.Body;
            set => InternalNotification.Body = value;
        }

        /// <inheritdoc />
        public string Subtitle
        {
            get => InternalNotification.Subtitle;
            set => InternalNotification.Subtitle = value;
        }

        /// <inheritdoc />
        public string Data
        {
            get => InternalNotification.Data;
            set => InternalNotification.Data = value;
        }

        /// <inheritdoc />
        /// <remarks>
        ///     On iOS, this represents the notification's Category Identifier.
        /// </remarks>
        /// <value>The value of <see cref="CategoryIdentifier" />.</value>
        public string Group
        {
            get => CategoryIdentifier;
            set => CategoryIdentifier = value;
        }

        /// <inheritdoc />
        public int? BadgeNumber
        {
            get => InternalNotification.Badge != -1 ? InternalNotification.Badge : null;
            set => InternalNotification.Badge = value ?? -1;
        }

        /// <inheritdoc />
        public bool ShouldAutoCancel { get; set; }

        /// <inheritdoc />
        public bool Scheduled { get; private set; }

        /// <inheritdoc />
        /// <remarks>
        ///     <para>On iOS, setting this causes the notification to be delivered on a calendar time.</para>
        ///     <para>
        ///         If it has previously been manually set to a different type of trigger, or has not been set before,
        ///         this returns null.
        ///     </para>
        ///     <para>The millisecond component of the provided DateTime is ignored.</para>
        /// </remarks>
        /// <value>
        ///     A <see cref="DateTime" /> representing the delivery time of this message, or null if
        ///     not set or the trigger isn't a <see cref="iOSNotificationCalendarTrigger" />.
        /// </value>
        public DateTime? DeliveryTime
        {
            get
            {
                if (InternalNotification.Trigger is not iOSNotificationCalendarTrigger calendarTrigger)
                {
                    return null;
                }

                var now = DateTime.Now;
                var result = new DateTime
                (
                    calendarTrigger.Year ?? now.Year,
                    calendarTrigger.Month ?? now.Month,
                    calendarTrigger.Day ?? now.Day,
                    calendarTrigger.Hour ?? now.Hour,
                    calendarTrigger.Minute ?? now.Minute,
                    calendarTrigger.Second ?? now.Second,
                    calendarTrigger.UtcTime ? DateTimeKind.Utc : DateTimeKind.Local
                );

                return result.ToLocalTime();
            }
            set
            {
                if (!value.HasValue)
                {
                    return;
                }

                var date = value.Value.ToLocalTime();

                InternalNotification.Trigger = new iOSNotificationCalendarTrigger
                {
                    Year = date.Year,
                    Month = date.Month,
                    Day = date.Day,
                    Hour = date.Hour,
                    Minute = date.Minute,
                    Second = date.Second
                };
            }
        }

        /// <summary>
        ///     Does nothing on iOS.
        /// </summary>
        [CanBeNull]
        public string SmallIcon
        {
            get => null;
            set { }
        }

        /// <summary>
        ///     Does nothing on iOS.
        /// </summary>
        [CanBeNull]
        public string LargeIcon
        {
            get => null;
            set { }
        }

        /// <summary>
        ///     Mark this notifications scheduled flag.
        /// </summary>
        internal void OnScheduled()
        {
            Assert.IsFalse(Scheduled);
            Scheduled = true;
        }
    }
}