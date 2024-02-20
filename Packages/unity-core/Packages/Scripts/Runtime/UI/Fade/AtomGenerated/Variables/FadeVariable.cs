using System;
using Egitech.Core.Runtime.UI.Fade.AtomGenerated.Events;
using Egitech.Core.Runtime.UI.Fade.AtomGenerated.Functions;
using Egitech.Core.Runtime.UI.Fade.AtomGenerated.Pairs;
using UnityAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.UI.Fade.AtomGenerated.Variables
{
    /// <summary>
    ///     Variable of type `Fade`. Inherits from `AtomVariable&lt;Fade, FadePair, FadeEvent, FadePairEvent, FadeFadeFunction
    ///     &gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Fade", fileName = "FadeVariable")]
    public sealed class FadeVariable : AtomVariable<Data.Fade, FadePair, FadeEvent, FadePairEvent, FadeFadeFunction>
    {
        protected override bool ValueEquals(Data.Fade other)
        {
            throw new NotImplementedException();
        }
    }
}