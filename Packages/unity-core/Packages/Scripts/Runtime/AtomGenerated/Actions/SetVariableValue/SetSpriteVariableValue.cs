using Egitech.Core.Runtime.AtomGenerated.Constants;
using Egitech.Core.Runtime.AtomGenerated.Events;
using Egitech.Core.Runtime.AtomGenerated.Functions;
using Egitech.Core.Runtime.AtomGenerated.Pairs;
using Egitech.Core.Runtime.AtomGenerated.References;
using Egitech.Core.Runtime.AtomGenerated.VariableInstancers;
using Egitech.Core.Runtime.AtomGenerated.Variables;
using UnityAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.AtomGenerated.Actions.SetVariableValue
{
    /// <summary>
    ///     Set variable value Action of type `Sprite`. Inherits from `SetVariableValue&lt;Sprite, SpritePair, SpriteVariable,
    ///     SpriteConstant, SpriteReference, SpriteEvent, SpritePairEvent, SpriteVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Sprite", fileName = "SetSpriteVariableValue")]
    public sealed class SetSpriteVariableValue : SetVariableValue<
        Sprite,
        SpritePair,
        SpriteVariable,
        SpriteConstant,
        SpriteReference,
        SpriteEvent,
        SpritePairEvent,
        SpriteSpriteFunction,
        SpriteVariableInstancer>
    {
    }
}