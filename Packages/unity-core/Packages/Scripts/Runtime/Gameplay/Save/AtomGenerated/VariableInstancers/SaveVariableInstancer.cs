using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Events;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Functions;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Pairs;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Variables;
using UnityAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.VariableInstancers
{
    /// <summary>
    ///     Variable Instancer of type `Save`. Inherits from `AtomVariableInstancer&lt;SaveVariable, SavePair, Save, SaveEvent,
    ///     SavePairEvent, SaveSaveFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/Save Variable Instancer")]
    public class SaveVariableInstancer : AtomVariableInstancer<
        SaveVariable,
        SavePair,
        Save,
        SaveEvent,
        SavePairEvent,
        SaveSaveFunction>
    {
    }
}