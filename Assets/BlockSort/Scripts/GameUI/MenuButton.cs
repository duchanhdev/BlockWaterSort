using UnityEngine;
using UnityEngine.UI;

namespace BlockSort.GameUI
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField]
        private GameUIManager gameUIManager;

        private void Start()
        {
            var btn = gameObject.GetComponent<Button>();
            btn.onClick.AddListener(TaskOnClick);
        }

        private void TaskOnClick()
        {
            gameUIManager.ShowMenu();
        }
    }
}
