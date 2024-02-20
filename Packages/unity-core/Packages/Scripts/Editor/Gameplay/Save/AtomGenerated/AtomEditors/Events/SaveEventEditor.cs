#if UNITY_2019_1_OR_NEWER
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Events;
using UnityAtoms.Editor;
using UnityEditor;

namespace Egitech.Core.Editor.Gameplay.Save.AtomGenerated.AtomEditors.Events
{
    /// <summary>
    ///     Event property drawer of type `Save`. Inherits from `AtomEventEditor&lt;Save, SaveEvent&gt;`. Only availble in
    ///     `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(SaveEvent))]
    public sealed class SaveEventEditor : AtomEventEditor<Runtime.Gameplay.Save.Save, SaveEvent>
    {
    }
}
#endif