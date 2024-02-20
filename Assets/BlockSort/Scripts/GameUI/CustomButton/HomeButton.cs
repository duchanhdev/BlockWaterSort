using FirebaseHelper.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BlockSort.GameUI.CustomButton
{
    [RequireComponent(typeof(Button))]
    public class HomeButton : MonoBehaviour
    {
        [SerializeField]
        private string SCENE_NAME = "Start";

        private void Start()
        {
            var btn = gameObject.GetComponent<Button>();
            btn.onClick.AddListener(TaskOnClick);
        }

        private void TaskOnClick()
        {
            var game = GameLogic.GameLogic.GetInstance().StartGame();
            SceneManager.LoadScene(SCENE_NAME);
            AnalyticsController.LogLevelStart(game.GetLevel(), "{0}");
        }
    }
}
