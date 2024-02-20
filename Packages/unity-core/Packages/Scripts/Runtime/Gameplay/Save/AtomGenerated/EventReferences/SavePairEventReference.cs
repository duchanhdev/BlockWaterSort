using System;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.EventInstancers;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Events;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Pairs;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.VariableInstancers;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Variables;
using UnityAtoms;

namespace Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.EventReferences
{
    /// <summary>
    ///     Event Reference of type `SavePair`. Inherits from `AtomEventReference&lt;SavePair, SaveVariable, SavePairEvent,
    ///     SaveVariableInstancer, SavePairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class SavePairEventReference : AtomEventReference<
        SavePair,
        SaveVariable,
        SavePairEvent,
        SaveVariableInstancer,
        SavePairEventInstancer>, IGetEvent
    {
    }
}