using NaughtyAttributes;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.UI.Popup
{
    public sealed class PopUpController : MonoBehaviour
    {
        [Foldout("References")]
        [SerializeField]
        private UIPopup _popupUI = default!;

        [Foldout("References")]
        [SerializeField]
        private GameObjectVariable _popupUIReference = default!;

        private void Awake()
        {
            _popupUIReference.Value = _popupUI.gameObject;
        }
    }
}