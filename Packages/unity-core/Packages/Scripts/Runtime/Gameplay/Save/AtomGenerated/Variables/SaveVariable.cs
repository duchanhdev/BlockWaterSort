using System;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Events;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Functions;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Pairs;
using UnityAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Variables
{
    /// <summary>
    ///     Variable of type `Save`. Inherits from `AtomVariable&lt;Save, SavePair, SaveEvent, SavePairEvent, SaveSaveFunction
    ///     &gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Save", fileName = "SaveVariable")]
    public sealed class SaveVariable : AtomVariable<Save, SavePair, SaveEvent, SavePairEvent, SaveSaveFunction>
    {
        protected override bool ValueEquals(Save other)
        {
            throw new NotImplementedException();
        }
    }
}