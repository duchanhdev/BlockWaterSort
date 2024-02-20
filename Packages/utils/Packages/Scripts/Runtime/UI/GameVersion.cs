using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Egitech.Utils.Runtime.UI
{
    public sealed class GameVersion : MonoBehaviour
    {
        [Header("Config")]
#if PROD
        [SerializeField]
        private string _prefix = "GAME VERSION: ";
#else
        [SerializeField]
        private string _devPrefix = "GAME VERSION: ";
#endif

#if PROD
        [SerializeField]
        private string _suffix;
#else
        [SerializeField]
        private string _devSuffix = "_dev";
#endif

        [Header("References")]
        [SerializeField]
        private TextMeshProUGUI _versionNumberText = default!;

        private void Start()
        {
            UpdateGameVersionText();
        }

        [Button]
        private void UpdateGameVersionText()
        {
#if PROD
            _versionNumberText.text = $"{_prefix}{Application.version}{_suffix}";
#else
            _versionNumberText.text = $"{_devPrefix}{Application.version}{_devSuffix}";
#endif
        }
    }
}