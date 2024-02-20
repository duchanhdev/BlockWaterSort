using UnityEditor;

namespace Egitech.Core.Editor
{
    public sealed class ReserializeAssets
    {
        [MenuItem("Assets/Reserialize All Assets")]
        public static void Reserialize()
        {
            AssetDatabase.ForceReserializeAssets();
        }
    }
}