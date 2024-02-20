#if UNITY_2019_1_OR_NEWER
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.Events;
using UnityAtoms.Editor;
using UnityEditor;

namespace Egitech.Core.Editor.Gameplay.Setting.AtomGenerated.AtomEditors.Events
{
    /// <summary>
    ///     Event property drawer of type `Setting`. Inherits from `AtomEventEditor&lt;Setting, SettingEvent&gt;`. Only
    ///     availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(SettingEvent))]
    public sealed class SettingEventEditor : AtomEventEditor<Runtime.Gameplay.Setting.Setting, SettingEvent>
    {
    }
}
#endif