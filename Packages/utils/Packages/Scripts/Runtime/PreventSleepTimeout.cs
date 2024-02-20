using UnityEngine;

namespace Egitech.Utils.Runtime
{
    public sealed class PreventSleepTimeout : MonoBehaviour
    {
        private void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}