using System;
using Egitech.Core.Runtime.UI.Fade.AtomGenerated.EventInstancers;
using Egitech.Core.Runtime.UI.Fade.AtomGenerated.Events;
using Egitech.Core.Runtime.UI.Fade.AtomGenerated.VariableInstancers;
using Egitech.Core.Runtime.UI.Fade.AtomGenerated.Variables;
using UnityAtoms;

namespace Egitech.Core.Runtime.UI.Fade.AtomGenerated.EventReferences
{
    /// <summary>
    ///     Event Reference of type `Fade`. Inherits from `AtomEventReference&lt;Fade, FadeVariable, FadeEvent,
    ///     FadeVariableInstancer, FadeEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class FadeEventReference : AtomEventReference<
        Data.Fade,
        FadeVariable,
        FadeEvent,
        FadeVariableInstancer,
        FadeEventInstancer>, IGetEvent
    {
    }
}