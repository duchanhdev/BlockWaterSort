using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.VariableInstancers;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Variables;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.SyncVariableInstancerToCollection
{
    /// <summary>
    ///     Adds Variable Instancer's Variable of type Save to a Collection or List on OnEnable and removes it on OnDestroy.
    /// </summary>
    [AddComponentMenu("Unity Atoms/Sync Variable Instancer to Collection/Sync Save Variable Instancer to Collection")]
    [EditorIcon("atom-icon-delicate")]
    public class
        SyncSaveVariableInstancerToCollection : SyncVariableInstancerToCollection<Save, SaveVariable,
            SaveVariableInstancer>
    {
    }
}