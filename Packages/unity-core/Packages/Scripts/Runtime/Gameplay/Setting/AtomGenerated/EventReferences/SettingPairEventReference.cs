using System;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.EventInstancers;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.Events;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.Pairs;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.VariableInstancers;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.Variables;
using UnityAtoms;

namespace Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.EventReferences
{
    /// <summary>
    ///     Event Reference of type `SettingPair`. Inherits from `AtomEventReference&lt;SettingPair, SettingVariable,
    ///     SettingPairEvent, SettingVariableInstancer, SettingPairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class SettingPairEventReference : AtomEventReference<
        SettingPair,
        SettingVariable,
        SettingPairEvent,
        SettingVariableInstancer,
        SettingPairEventInstancer>, IGetEvent
    {
    }
}