using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;

namespace Egitech.Utils.Runtime.Helper
{
    public static class UniTaskHelper
    {
        public static Action<T> Action<T>(Func<T, UniTaskVoid> asyncAction)
        {
            return t1 => asyncAction(t1).Forget();
        }

        public static UnityAction<T> UnityAction<T>(Func<T, UniTaskVoid> asyncAction)
        {
            return t1 => asyncAction(t1).Forget();
        }
    }
}