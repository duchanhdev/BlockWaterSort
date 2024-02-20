#if UNITY_2019_1_OR_NEWER
using Egitech.Core.Runtime.AtomGenerated.Events;
using UnityAtoms.Editor;
using UnityEditor;

namespace Egitech.Core.Editor.Sprite.AtomGenerated.AtomEditors.Events
{
    /// <summary>
    ///     Event property drawer of type `Sprite`. Inherits from `AtomEventEditor&lt;Sprite, SpriteEvent&gt;`. Only availble
    ///     in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(SpriteEvent))]
    public sealed class SpriteEventEditor : AtomEventEditor<UnityEngine.Sprite, SpriteEvent>
    {
    }
}
#endif