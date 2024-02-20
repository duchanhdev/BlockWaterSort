using UnityEngine;

namespace Egitech.Utils.Runtime.UI
{
    /// <summary>
    ///     Resizes a UI element with a RectTransform to respect the safe areas of the current device.
    ///     This is particularly useful on an iPhone X, where we have to avoid the notch and the screen
    ///     corners.
    ///     The easiest way to use it is to create a root Canvas object, attach this script to a game object called
    ///     "SafeAreaContainer"
    ///     that is the child of the root canvas, and then layout the UI elements within the SafeAreaContainer, which
    ///     will adjust size appropriately for the current device.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public sealed class UIPinToSafeArea : MonoBehaviour
    {
        private Rect _lastSafeArea;
        private RectTransform _parentRectTransform = default!;

        private void Start()
        {
            _parentRectTransform = GetComponentInParent<RectTransform>();
        }

        private void Update()
        {
            if (_lastSafeArea != Screen.safeArea)
            {
                ApplySafeArea();
            }
        }

        private void ApplySafeArea()
        {
            var safeAreaRect = Screen.safeArea;
            var scaleRatio = _parentRectTransform.rect.width / Screen.width;
            var left = safeAreaRect.xMin * scaleRatio;
            var right = -(Screen.width - safeAreaRect.xMax) * scaleRatio;
            var top = -safeAreaRect.yMin * scaleRatio;
            var bottom = (Screen.height - safeAreaRect.yMax) * scaleRatio;

            if (TryGetComponent<RectTransform>(out var rectTransform))
            {
                rectTransform.offsetMin = new Vector2(left, bottom);
                rectTransform.offsetMax = new Vector2(right, top);
            }

            _lastSafeArea = Screen.safeArea;
        }
    }
}