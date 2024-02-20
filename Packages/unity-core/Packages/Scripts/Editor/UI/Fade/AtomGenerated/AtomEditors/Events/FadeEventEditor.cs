#if UNITY_2019_1_OR_NEWER
using Egitech.Core.Runtime.UI.Fade.AtomGenerated.Events;
using UnityAtoms.Editor;
using UnityEditor;

namespace Egitech.Core.Editor.UI.Fade.AtomGenerated.AtomEditors.Events
{
    /// <summary>
    ///     Event property drawer of type `Fade`. Inherits from `AtomEventEditor&lt;Fade, FadeEvent&gt;`. Only availble in
    ///     `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(FadeEvent))]
    public sealed class FadeEventEditor : AtomEventEditor<Runtime.UI.Data.Fade, FadeEvent>
    {
    }
}
#endif