using System;
using UnityAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.Pairs
{
    /// <summary>
    ///     IPair of type `&lt;Setting&gt;`. Inherits from `IPair&lt;Setting&gt;`.
    /// </summary>
    [Serializable]
    public struct SettingPair : IPair<Setting>
    {
        [SerializeField]
        private Setting _item1;

        [SerializeField]
        private Setting _item2;

        public Setting Item1
        {
            get => _item1;
            set => _item1 = value;
        }

        public Setting Item2
        {
            get => _item2;
            set => _item2 = value;
        }

        public void Deconstruct(out Setting item1, out Setting item2)
        {
            item1 = Item1;
            item2 = Item2;
        }
    }
}