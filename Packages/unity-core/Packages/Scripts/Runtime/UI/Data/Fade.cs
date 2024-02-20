using System;
using UnityEngine;

namespace Egitech.Core.Runtime.UI.Data
{
    [Serializable]
    public struct Fade : IEquatable<Fade>
    {
        [SerializeField]
        private bool _isFadeIn;

        [SerializeField]
        private float _duration;

        [SerializeField]
        private Color _color;

        public Fade(bool isFadeIn, float duration, Color color)
        {
            _isFadeIn = isFadeIn;
            _duration = duration;
            _color = color;
        }

        public bool IsFadeIn => _isFadeIn;

        public float Duration => _duration;

        public Color Color => _color;

        public bool Equals(Fade other)
        {
            return _isFadeIn == other._isFadeIn && _duration.Equals(other._duration) && _color.Equals(other._color);
        }

        public static Fade In(float duration)
        {
            return new Fade(true, duration, Color.clear);
        }

        public static Fade Out(float duration)
        {
            return new Fade(false, duration, Color.black);
        }

        public override bool Equals(object? obj)
        {
            return obj is Fade other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_isFadeIn, _duration, _color);
        }

        public static bool operator ==(Fade left, Fade right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Fade left, Fade right)
        {
            return !left.Equals(right);
        }
    }
}