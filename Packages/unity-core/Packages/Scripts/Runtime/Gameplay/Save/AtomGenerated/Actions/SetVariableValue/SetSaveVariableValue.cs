using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Constants;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Events;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Functions;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Pairs;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.References;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.VariableInstancers;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Variables;
using UnityAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Actions.SetVariableValue
{
    /// <summary>
    ///     Set variable value Action of type `Save`. Inherits from `SetVariableValue&lt;Save, SavePair, SaveVariable,
    ///     SaveConstant, SaveReference, SaveEvent, SavePairEvent, SaveVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Save", fileName = "SetSaveVariableValue")]
    public sealed class SetSaveVariableValue : SetVariableValue<
        Save,
        SavePair,
        SaveVariable,
        SaveConstant,
        SaveReference,
        SaveEvent,
        SavePairEvent,
        SaveSaveFunction,
        SaveVariableInstancer>
    {
    }
}