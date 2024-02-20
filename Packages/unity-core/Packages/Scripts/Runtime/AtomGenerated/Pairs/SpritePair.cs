using System;
using UnityAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.AtomGenerated.Pairs
{
    /// <summary>
    ///     IPair of type `&lt;Sprite&gt;`. Inherits from `IPair&lt;Sprite&gt;`.
    /// </summary>
    [Serializable]
    public struct SpritePair : IPair<Sprite>
    {
        [SerializeField]
        private Sprite _item1;

        [SerializeField]
        private Sprite _item2;

        public Sprite Item1
        {
            get => _item1;
            set => _item1 = value;
        }

        public Sprite Item2
        {
            get => _item2;
            set => _item2 = value;
        }

        public void Deconstruct(out Sprite item1, out Sprite item2)
        {
            item1 = Item1;
            item2 = Item2;
        }
    }
}