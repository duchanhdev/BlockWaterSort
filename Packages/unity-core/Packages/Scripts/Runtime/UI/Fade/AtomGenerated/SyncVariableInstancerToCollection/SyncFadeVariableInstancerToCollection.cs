using Egitech.Core.Runtime.UI.Fade.AtomGenerated.VariableInstancers;
using Egitech.Core.Runtime.UI.Fade.AtomGenerated.Variables;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.UI.Fade.AtomGenerated.SyncVariableInstancerToCollection
{
    /// <summary>
    ///     Adds Variable Instancer's Variable of type Fade to a Collection or List on OnEnable and removes it on OnDestroy.
    /// </summary>
    [AddComponentMenu("Unity Atoms/Sync Variable Instancer to Collection/Sync Fade Variable Instancer to Collection")]
    [EditorIcon("atom-icon-delicate")]
    public class
        SyncFadeVariableInstancerToCollection : SyncVariableInstancerToCollection<Data.Fade, FadeVariable,
            FadeVariableInstancer>
    {
    }
}