using System;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Constants;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Events;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Functions;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Pairs;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.VariableInstancers;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Variables;
using UnityAtoms;

namespace Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.References
{
    /// <summary>
    ///     Reference of type `Save`. Inherits from `AtomReference&lt;Save, SavePair, SaveConstant, SaveVariable, SaveEvent,
    ///     SavePairEvent, SaveSaveFunction, SaveVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class SaveReference : AtomReference<
        Save,
        SavePair,
        SaveConstant,
        SaveVariable,
        SaveEvent,
        SavePairEvent,
        SaveSaveFunction,
        SaveVariableInstancer>, IEquatable<SaveReference>
    {
        public SaveReference()
        {
        }

        public SaveReference(Save value) : base(value)
        {
        }

        public bool Equals(SaveReference other)
        {
            return base.Equals(other);
        }

        protected override bool ValueEquals(Save other)
        {
            throw new NotImplementedException();
        }
    }
}