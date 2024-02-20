using TMPro;
using UnityEngine;

namespace BlockSort.GameUI
{
    public class LevelText : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textMesh;

        private void Start()
        {
            Debug.Log("Hello: " + GameLogic.GameLogic.GetInstance().GetGame());
            UpdateText(GameLogic.GameLogic.GetInstance().GetGame().GetLevel());
        }

        public void UpdateText(int level)
        {
            textMesh.text = "LEVEL " + level;
        }
    }
}
