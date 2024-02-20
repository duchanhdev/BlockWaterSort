using UnityEngine;
using UnityEngine.Localization;

namespace Egitech.Utils.Runtime.UI.Toggle
{
    [CreateAssetMenu(fileName = "STATE_UIToggle", menuName = "UI/Toggle")]
    public sealed class UIToggleState : ScriptableObject
    {
        [SerializeField]
        private LocalizedString _label = default!;

        [SerializeField]
        private Sprite _toggleButtonImage = default!;

        [SerializeField]
        private LocalizedString _chatModeLabel = default!;

        public LocalizedString ChatModeLabel => _chatModeLabel;

        public LocalizedString Label => _label;

        public Sprite ToggleButtonImage => _toggleButtonImage;
    }
}