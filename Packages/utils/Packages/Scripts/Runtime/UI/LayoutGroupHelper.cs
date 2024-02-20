using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Egitech.Utils.Runtime.UI
{
    public static class LayoutGroupHelper
    {
        public static IEnumerator ChangeUpdate(this LayoutGroup horizLayoutGroup)
        {
            yield return new WaitForEndOfFrame();
            horizLayoutGroup.CalculateLayoutInputHorizontal();
            horizLayoutGroup.CalculateLayoutInputVertical();
            horizLayoutGroup.SetLayoutHorizontal();
            horizLayoutGroup.SetLayoutVertical();
            yield return null;
        }
    }
}