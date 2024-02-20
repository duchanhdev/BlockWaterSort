using System;
using UnityEngine;

namespace BlockSort.GameLogic
{
    [Serializable]
    public class Block
    {
        [SerializeField]
        private char charColor;

        [SerializeField]
        private Color color;

        public Block(char c)
        {
            color = getColorByChar(c);
            charColor = c;
        }

        public Block(Block orther)
        {
            color = orther.color;
            charColor = orther.charColor;
        }

        public Block(Color color)
        {
            this.color = color;
        }

        public Color getColorByChar(char c)
        {
            switch (c)
            {
                case 'r':
                    return Color.red;
                case 'b':
                    return Color.blue;
                case 'g':
                    return Color.green;
                case 'y':
                    return Color.yellow;
                case 'c':
                    return Color.cyan;
                case 'o':
                    return new Color(1.0f, 0.647f, 0f);
                case 'p':
                    return new Color(1.0f, 0.753f, 0.796f);
                case 'w':
                    return Color.white;
                case 'v':
                    return new Color(0.561f, 0f, 1f);
                case 'n':
                    return new Color(0f, 0f, 50.2f);
                case 'm':
                    return Color.magenta;
                case 'a':
                    return new Color(0f, 0.498f, 1f);
                case 'd':
                    return new Color(1f, 0.498f, 0.314f);
                case 'e':
                    return new Color(0.380f, 0.251f, 0.318f);
                case 'f':
                    return new Color(0.443f, 0.737f, 0.471f);
                case 'h':
                    return new Color(0.875f, 0.451f, 1f);
                case 'k':
                    return new Color(0.765f, 0.69f, 0.569f);
                    ;
                case 'i':
                    return new Color(0.294f, 0f, 0.51f);
                    ;
                case 'j':
                    return new Color(0, 0.659f, 0.420f);
                case 't':
                    return new Color(0f, 0.502f, 0.502f);
                    ;
                case 'l':
                    return new Color(0.196f, 0.804f, 196f);
                default:
                    return Color.black;
            }
        }

        public bool Equals(Block orther)
        {
            return color.Equals(orther.color);
        }

        public Color GetColor()
        {
            return color;
        }

        public char GetCharColor()
        {
            return charColor;
        }
    }
}
