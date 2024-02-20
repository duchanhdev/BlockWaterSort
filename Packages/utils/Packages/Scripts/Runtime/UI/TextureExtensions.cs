using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Egitech.Utils.Runtime.UI
{
    public static class TextureExtensions
    {
        public static async UniTask<Texture2D?> GetRemoteTexture(string url)
        {
            using var www = UnityWebRequestTexture.GetTexture(url);
            var asyncOp = www.SendWebRequest();

            while (asyncOp.isDone == false)
            {
                await UniTask.Delay(1000 / 30); //30 hertz
            }

            if (www.result == UnityWebRequest.Result.Success)
            {
                return DownloadHandlerTexture.GetContent(www);
            }
#if DEBUG
            Debug.Log($"{www.error}, URL:{www.url}");
#endif
            return null;
        }

        public static void SetRemoteImage(Image image, in string url)
        {
            SetRemoteImageAsync(image, url).Forget();
        }

        public static UniTask SetRemoteImageAsync(Image image, in string url)
        {
            return GetRemoteTexture(url)
                .ContinueWith(texture2D =>
                {
                    if (texture2D == null)
                    {
                        return;
                    }
                    var sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(.5f, .5f));
                    image.sprite = sprite;
                });
        }
    }
}