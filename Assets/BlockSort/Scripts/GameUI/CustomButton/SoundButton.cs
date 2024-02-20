using BlockSort.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace BlockSort.GameUI.CustomButton
{
    public class SoundButton : MonoBehaviour
    {
        [SerializeField]
        private Image charmSoundUp;

        [SerializeField]
        private Image charmSoundMute;

        private void Start()
        {
            ShowCharmSoundImage();
            var btn = gameObject.GetComponent<Button>();
            btn.onClick.AddListener(TaskOnClick);
        }

        private void ShowCharmSoundImage()
        {
            var isSoundUp = SoundController.Instance.IsTurnOn;
            charmSoundUp.gameObject.SetActive(isSoundUp);
            charmSoundMute.gameObject.SetActive(!isSoundUp);
        }

        private void TaskOnClick()
        {
            SoundController.Instance.ChangeStatusSound();
            ShowCharmSoundImage();
        }
    }
}
