#if UNITY_2019_1_OR_NEWER
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Events;
using UnityAtoms.Editor;
using UnityEditor;

namespace Egitech.Core.Editor.Gameplay.Save.AtomGenerated.AtomDrawers.Events
{
    /// <summary>
    ///     Event property drawer of type `Save`. Inherits from `AtomDrawer&lt;SaveEvent&gt;`. Only availble in
    ///     `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(SaveEvent))]
    public class SaveEventDrawer : AtomDrawer<SaveEvent>
    {
    }
}
#endif