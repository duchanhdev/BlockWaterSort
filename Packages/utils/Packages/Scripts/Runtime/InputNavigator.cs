using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Egitech.Utils.Runtime
{
    public sealed class InputNavigator : MonoBehaviour
    {
        private TouchScreenKeyboard? _keyboard;
        private EventSystem? _system;

        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.Tab)) // switch to touch
            if (_keyboard is not { status: TouchScreenKeyboard.Status.Done })
            {
                return;
            }

            // Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (_system.currentSelectedGameObject.TryGetComponent<Selectable>(out var component))
            {
                var next = component.FindSelectableOnDown();
                if (next.TryGetComponent<IPointerClickHandler>(out var pointer))
                {
                    pointer.OnPointerClick(new PointerEventData(_system)); //if it's an input field, also set the text caret
                }

                _system.SetSelectedGameObject(next.gameObject, new BaseEventData(_system));
            }
            else
            {
                Debug.Log("next navigation element not found");
            }
        }

        public void ShowKeyBoard()
        {
            _system = EventSystem.current; // EventSystemManager.currentSystem;
            _keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        }
    }
}