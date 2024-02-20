using System;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.EventInstancers;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.Events;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.VariableInstancers;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.Variables;
using UnityAtoms;

namespace Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.EventReferences
{
    /// <summary>
    ///     Event Reference of type `Setting`. Inherits from `AtomEventReference&lt;Setting, SettingVariable, SettingEvent,
    ///     SettingVariableInstancer, SettingEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class SettingEventReference : AtomEventReference<
        Setting,
        SettingVariable,
        SettingEvent,
        SettingVariableInstancer,
        SettingEventInstancer>, IGetEvent
    {
    }
}