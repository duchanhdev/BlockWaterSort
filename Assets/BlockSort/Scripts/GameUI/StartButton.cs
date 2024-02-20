using FirebaseHelper.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BlockSort.GameUI
{
    [RequireComponent(typeof(Button))]
    public class StartButton : MonoBehaviour
    {
        private void Start()
        {
            var btn = GetComponent<Button>();
            btn.onClick.AddListener(TaskOnClick);
        }

        private static void TaskOnClick()
        {
            //AnalyticsController.LogLevelStart(GameLogic.GameLogic.GetInstance().GetGame().GetLevel(), "{0}");
            //SceneManager.LoadSceneAsync("Game").completed += _ => { GoogleAdMobController.Instance.RequestBannerAd(); };
            SceneManager.LoadSceneAsync("Game");
        }
    }
}
