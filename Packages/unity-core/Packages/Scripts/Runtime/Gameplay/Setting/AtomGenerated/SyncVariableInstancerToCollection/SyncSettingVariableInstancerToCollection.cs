using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.VariableInstancers;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.Variables;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.SyncVariableInstancerToCollection
{
    /// <summary>
    ///     Adds Variable Instancer's Variable of type Setting to a Collection or List on OnEnable and removes it on OnDestroy.
    /// </summary>
    [AddComponentMenu(
        "Unity Atoms/Sync Variable Instancer to Collection/Sync Setting Variable Instancer to Collection")]
    [EditorIcon("atom-icon-delicate")]
    public class SyncSettingVariableInstancerToCollection : SyncVariableInstancerToCollection<Setting, SettingVariable
        ,
        SettingVariableInstancer>
    {
    }
}