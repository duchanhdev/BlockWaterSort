using Egitech.Core.Runtime.AtomGenerated.VariableInstancers;
using Egitech.Core.Runtime.AtomGenerated.Variables;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.AtomGenerated.SyncVariableInstancerToCollection
{
    /// <summary>
    ///     Adds Variable Instancer's Variable of type Sprite to a Collection or List on OnEnable and removes it on OnDestroy.
    /// </summary>
    [AddComponentMenu("Unity Atoms/Sync Variable Instancer to Collection/Sync Sprite Variable Instancer to Collection")]
    [EditorIcon("atom-icon-delicate")]
    public class
        SyncSpriteVariableInstancerToCollection : SyncVariableInstancerToCollection<Sprite, SpriteVariable,
            SpriteVariableInstancer>
    {
    }
}