using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.Constants;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.Events;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.Functions;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.Pairs;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.References;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.VariableInstancers;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.Variables;
using UnityAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.Actions.SetVariableValue
{
    /// <summary>
    ///     Set variable value Action of type `Setting`. Inherits from `SetVariableValue&lt;Setting, SettingPair,
    ///     SettingVariable, SettingConstant, SettingReference, SettingEvent, SettingPairEvent, SettingVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Setting", fileName = "SetSettingVariableValue")]
    public sealed class SetSettingVariableValue : SetVariableValue<
        Setting,
        SettingPair,
        SettingVariable,
        SettingConstant,
        SettingReference,
        SettingEvent,
        SettingPairEvent,
        SettingSettingFunction,
        SettingVariableInstancer>
    {
    }
}