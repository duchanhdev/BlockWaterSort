using UnityEngine;

namespace Egitech.Core.Runtime
{
    public abstract class DescriptionBaseSO : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField]
        [TextArea]
        [Tooltip("Editor only. Should describes where to use this event.")]
        private string _description = default!;
#endif
    }
}