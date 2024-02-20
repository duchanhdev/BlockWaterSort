using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.EventReferences;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.Events;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.UnityEvents;
using UnityAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.EventReferenceListeners
{
    /// <summary>
    ///     Event Reference Listener of type `Setting`. Inherits from `AtomEventReferenceListener&lt;Setting, SettingEvent,
    ///     SettingEventReference, SettingUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Setting Event Reference Listener")]
    public sealed class SettingEventReferenceListener : AtomEventReferenceListener<
        Setting,
        SettingEvent,
        SettingEventReference,
        SettingUnityEvent>
    {
    }
}