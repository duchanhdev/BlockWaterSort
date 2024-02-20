using NaughtyAttributes;
using UnityEngine;

namespace BlockSort.Utils
{
    public sealed class ScaleToScreen : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            Scale();
        }

        [Button]
        private void Scale()
        {
            var screenHeight = Camera.main.orthographicSize * 2;
            var screenWidth = screenHeight / Screen.height * Screen.width;
            var spriteSize = _spriteRenderer.sprite.bounds.size;

            transform.localScale = new Vector2(screenWidth / spriteSize.x, screenHeight / spriteSize.y);
        }
    }
}
