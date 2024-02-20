using System;
using Egitech.Core.Runtime.AtomGenerated.EventInstancers;
using Egitech.Core.Runtime.AtomGenerated.Events;
using Egitech.Core.Runtime.AtomGenerated.VariableInstancers;
using Egitech.Core.Runtime.AtomGenerated.Variables;
using UnityAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.AtomGenerated.EventReferences
{
    /// <summary>
    ///     Event Reference of type `Sprite`. Inherits from `AtomEventReference&lt;Sprite, SpriteVariable, SpriteEvent,
    ///     SpriteVariableInstancer, SpriteEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class SpriteEventReference : AtomEventReference<
        Sprite,
        SpriteVariable,
        SpriteEvent,
        SpriteVariableInstancer,
        SpriteEventInstancer>, IGetEvent
    {
    }
}