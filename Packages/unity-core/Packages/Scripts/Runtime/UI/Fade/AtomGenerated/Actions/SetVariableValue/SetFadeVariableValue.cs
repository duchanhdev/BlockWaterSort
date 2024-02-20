using Egitech.Core.Runtime.UI.Fade.AtomGenerated.Constants;
using Egitech.Core.Runtime.UI.Fade.AtomGenerated.Events;
using Egitech.Core.Runtime.UI.Fade.AtomGenerated.Functions;
using Egitech.Core.Runtime.UI.Fade.AtomGenerated.Pairs;
using Egitech.Core.Runtime.UI.Fade.AtomGenerated.References;
using Egitech.Core.Runtime.UI.Fade.AtomGenerated.VariableInstancers;
using Egitech.Core.Runtime.UI.Fade.AtomGenerated.Variables;
using UnityAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.UI.Fade.AtomGenerated.Actions.SetVariableValue
{
    /// <summary>
    ///     Set variable value Action of type `Fade`. Inherits from `SetVariableValue&lt;Fade, FadePair, FadeVariable,
    ///     FadeConstant, FadeReference, FadeEvent, FadePairEvent, FadeVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Fade", fileName = "SetFadeVariableValue")]
    public sealed class SetFadeVariableValue : SetVariableValue<
        Data.Fade,
        FadePair,
        FadeVariable,
        FadeConstant,
        FadeReference,
        FadeEvent,
        FadePairEvent,
        FadeFadeFunction,
        FadeVariableInstancer>
    {
    }
}