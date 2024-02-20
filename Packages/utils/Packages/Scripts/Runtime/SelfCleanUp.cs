using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Egitech.Utils.Runtime
{
    public sealed class SelfCleanup : MonoBehaviour
    {
        private void OnDestroy()
        {
            Addressables.ReleaseInstance(gameObject);
        }
    }
}