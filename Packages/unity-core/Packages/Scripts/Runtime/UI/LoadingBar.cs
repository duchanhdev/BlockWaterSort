using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

namespace Egitech.Core.Runtime.UI
{
    public sealed class LoadingBar : MonoBehaviour
    {
        [Header("Children components")]
        [SerializeField]
        private Slider _loadingProgressSlider = default!;

        [Header("Listening to")]
        [SerializeField]
        private FloatEvent? _changeLoadingProgressEvent;

        [SerializeField]
        private BoolEvent? _hideResultOnLoadingEvent;

        private void OnEnable()
        {
            if (_hideResultOnLoadingEvent != null)
            {
                _hideResultOnLoadingEvent.Raise(false);
            }
            if (_changeLoadingProgressEvent != null)
            {
                _changeLoadingProgressEvent.Register(OnLoadingProgress);
            }
        }

        private void OnDisable()
        {
            if (_changeLoadingProgressEvent != null)
            {
                _changeLoadingProgressEvent.Unregister(OnLoadingProgress);
            }
        }

        private void OnLoadingProgress(float process)
        {
            _loadingProgressSlider.value = process;

            if (process >= 1)
            {
                gameObject.SetActive(false);
            }
        }
    }
}