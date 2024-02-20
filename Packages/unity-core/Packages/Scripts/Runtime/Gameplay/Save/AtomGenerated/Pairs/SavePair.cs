using System;
using UnityAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.Gameplay.Save.AtomGenerated.Pairs
{
    /// <summary>
    ///     IPair of type `&lt;Save&gt;`. Inherits from `IPair&lt;Save&gt;`.
    /// </summary>
    [Serializable]
    public struct SavePair : IPair<Save>
    {
        [SerializeField]
        private Save _item1;

        [SerializeField]
        private Save _item2;

        public Save Item1
        {
            get => _item1;
            set => _item1 = value;
        }

        public Save Item2
        {
            get => _item2;
            set => _item2 = value;
        }

        public void Deconstruct(out Save item1, out Save item2)
        {
            item1 = Item1;
            item2 = Item2;
        }
    }
}