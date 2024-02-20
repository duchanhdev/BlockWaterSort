using System;

//using UnityEngine.SceneManagement;
//using System.Globalization;

namespace Egitech.Core.Editor
{
    // Suppressing warnings related to the use of private structures which are confusing the compiler as these data structures are used by .json files.
#pragma warning disable 0649

    /// <summary>
    ///     Data structure containing the target and replacement fileIDs and GUIDs which will require remapping from previous
    ///     version of TextMesh Pro to the new TextMesh Pro UPM package.
    /// </summary>
    [Serializable]
    internal struct AssetConversionRecord
    {
        //public string referencedResource;
        public string target;
        public string replacement;
    }
}