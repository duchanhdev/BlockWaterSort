using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Egitech.Core.Runtime;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using UnityEngine;

namespace FirebaseHelper.Runtime.RemoteConfig
{
    public sealed class RemoteConfigManager : Singleton<RemoteConfigManager>
    {
        [SerializeField]
        private ScriptableObject[] _defaults;

        private Action _initializeFirebaseConfig;

        protected override void OnAwake()
        {
            base.OnAwake();
            _initializeFirebaseConfig = UniTask.Action(InitializeFirebaseRemoteConfig);
            FirebaseInit.Instance.OnFirebaseInitialized += _initializeFirebaseConfig;
        }

        private void OnDestroy()
        {
            FirebaseInit.Instance.OnFirebaseInitialized -= _initializeFirebaseConfig;
        }

        private async UniTaskVoid InitializeFirebaseRemoteConfig()
        {
            await FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(DefaultValues)
                .ContinueWith(_ => FetchDataAsync());
        }

        private Dictionary<string, object> DefaultValues
        {
            get
            {
                var defaultValues = new Dictionary<string, object>();
                foreach (var scriptableObject in _defaults)
                {
                    if (scriptableObject is not IRemoteConfigData remoteConfigData)
                    {
                        continue;
                    }
                    var json = remoteConfigData.Serialize();
                    defaultValues.Add(remoteConfigData.ValueName, json);
                }
                return defaultValues;
            }
        }

        /// <summary>
        /// <para>
        /// Start a fetch request.
        /// </para>
        /// <para>
        /// FetchAsync only fetches new data if the current data is older than the provided timespan.
        /// Otherwise it assumes the data is "recent enough", and does nothing.
        /// </para>
        /// <para>
        /// By default the timespan is 12 hours, and for production apps, this is a good number.
        /// For this example though, it's set to a timespan of zero, so that changes in the console will always show up immediately.
        /// </para>
        /// </summary>
        private Task FetchDataAsync()
        {
            Debug.Log("Fetching data...");
            var fetchTask = FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
            return fetchTask.ContinueWithOnMainThread(FetchComplete);
        }

        private void FetchComplete(Task fetchTask)
        {
            if (fetchTask.IsCanceled)
            {
                Debug.Log("Fetch canceled.");
            }
            else if (fetchTask.IsFaulted)
            {
                Debug.Log("Fetch encountered an error.");
            }
            else if (fetchTask.IsCompleted)
            {
                Debug.Log("Fetch completed successfully!");
            }

            var info = FirebaseRemoteConfig.DefaultInstance.Info;
            switch (info.LastFetchStatus)
            {
                case LastFetchStatus.Success:
                    FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
                        .ContinueWithOnMainThread(task =>
                        {
                            PopulateRemoteConfigData();
                            Debug.Log(string.Format("Remote data loaded and ready (last fetch time {0}).",
                                info.FetchTime));
                        });

                    break;
                case LastFetchStatus.Failure:
                    switch (info.LastFetchFailureReason)
                    {
                        case FetchFailureReason.Error:
                            Debug.Log("Fetch failed for unknown reason");
                            break;
                        case FetchFailureReason.Throttled:
                            Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
                            break;
                    }
                    break;
                case LastFetchStatus.Pending:
                    Debug.Log("Latest Fetch call still pending.");
                    break;
            }
        }

        private void PopulateRemoteConfigData()
        {
            foreach (var scriptableObject in _defaults)
            {
                if (scriptableObject is not IRemoteConfigData remoteConfigData)
                {
                    continue;
                }
                var json = FirebaseRemoteConfig.DefaultInstance.GetValue(remoteConfigData.ValueName).StringValue;
                remoteConfigData.Deserialized(json);
            }
        }
    }
}