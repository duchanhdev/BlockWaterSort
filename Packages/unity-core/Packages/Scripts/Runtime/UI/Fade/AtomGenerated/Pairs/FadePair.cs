using System;
using UnityAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.UI.Fade.AtomGenerated.Pairs
{
    /// <summary>
    ///     IPair of type `&lt;Fade&gt;`. Inherits from `IPair&lt;Fade&gt;`.
    /// </summary>
    [Serializable]
    public struct FadePair : IPair<Data.Fade>
    {
        [SerializeField]
        private Data.Fade _item1;

        [SerializeField]
        private Data.Fade _item2;

        public Data.Fade Item1
        {
            get => _item1;
            set => _item1 = value;
        }

        public Data.Fade Item2
        {
            get => _item2;
            set => _item2 = value;
        }

        public void Deconstruct(out Data.Fade item1, out Data.Fade item2)
        {
            item1 = Item1;
            item2 = Item2;
        }
    }
}