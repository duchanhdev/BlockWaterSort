using System;
using Egitech.Core.Runtime.AtomGenerated.Constants;
using Egitech.Core.Runtime.AtomGenerated.Events;
using Egitech.Core.Runtime.AtomGenerated.Functions;
using Egitech.Core.Runtime.AtomGenerated.Pairs;
using Egitech.Core.Runtime.AtomGenerated.VariableInstancers;
using Egitech.Core.Runtime.AtomGenerated.Variables;
using UnityAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.AtomGenerated.References
{
    /// <summary>
    ///     Reference of type `Sprite`. Inherits from `AtomReference&lt;Sprite, SpritePair, SpriteConstant, SpriteVariable,
    ///     SpriteEvent, SpritePairEvent, SpriteSpriteFunction, SpriteVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class SpriteReference : AtomReference<
        Sprite,
        SpritePair,
        SpriteConstant,
        SpriteVariable,
        SpriteEvent,
        SpritePairEvent,
        SpriteSpriteFunction,
        SpriteVariableInstancer>, IEquatable<SpriteReference>
    {
        public SpriteReference()
        {
        }

        public SpriteReference(Sprite value) : base(value)
        {
        }

        public bool Equals(SpriteReference other)
        {
            return base.Equals(other);
        }

        protected override bool ValueEquals(Sprite other)
        {
            throw new NotImplementedException();
        }
    }
}