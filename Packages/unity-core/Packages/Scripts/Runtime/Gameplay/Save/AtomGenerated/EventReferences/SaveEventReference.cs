using System;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.EventInstancers;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Events;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.VariableInstancers;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Variables;
using UnityAtoms;

namespace Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.EventReferences
{
    /// <summary>
    ///     Event Reference of type `Save`. Inherits from `AtomEventReference&lt;Save, SaveVariable, SaveEvent,
    ///     SaveVariableInstancer, SaveEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class SaveEventReference : AtomEventReference<
        Save,
        SaveVariable,
        SaveEvent,
        SaveVariableInstancer,
        SaveEventInstancer>, IGetEvent
    {
    }
}