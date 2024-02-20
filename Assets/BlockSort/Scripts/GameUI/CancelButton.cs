using UnityEngine;
using UnityEngine.UI;

namespace BlockSort.GameUI
{
    public class CancelButton : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            var btn = gameObject.GetComponent<Button>();
            btn.onClick.AddListener(TaskOnClick);
        }

        private void TaskOnClick()
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
