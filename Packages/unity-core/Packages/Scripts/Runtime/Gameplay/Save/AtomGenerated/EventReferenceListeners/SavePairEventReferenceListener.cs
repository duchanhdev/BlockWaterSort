using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.EventReferences;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Events;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Pairs;
using Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.UnityEvents;
using UnityAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.EventReferenceListeners
{
    /// <summary>
    ///     Event Reference Listener of type `SavePair`. Inherits from `AtomEventReferenceListener&lt;SavePair, SavePairEvent,
    ///     SavePairEventReference, SavePairUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/SavePair Event Reference Listener")]
    public sealed class SavePairEventReferenceListener : AtomEventReferenceListener<
        SavePair,
        SavePairEvent,
        SavePairEventReference,
        SavePairUnityEvent>
    {
    }
}