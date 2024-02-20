using System;
using Egitech.Core.Runtime.AtomGenerated.Events;
using Egitech.Core.Runtime.AtomGenerated.Functions;
using Egitech.Core.Runtime.AtomGenerated.Pairs;
using UnityAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.AtomGenerated.Variables
{
    /// <summary>
    ///     Variable of type `Sprite`. Inherits from `AtomVariable&lt;Sprite, SpritePair, SpriteEvent, SpritePairEvent,
    ///     SpriteSpriteFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Sprite", fileName = "SpriteVariable")]
    public sealed class
        SpriteVariable : AtomVariable<Sprite, SpritePair, SpriteEvent, SpritePairEvent, SpriteSpriteFunction>
    {
        protected override bool ValueEquals(Sprite other)
        {
            throw new NotImplementedException();
        }
    }
}