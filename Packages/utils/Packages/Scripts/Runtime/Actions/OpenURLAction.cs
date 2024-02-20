using UnityAtoms;
using UnityEngine;

namespace Egitech.Utils.Runtime.Actions
{
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Networking/Open URL", fileName = "AC_OpenURL")]
    public sealed class OpenURLAction : AtomAction
    {
        [SerializeField]
        private string _url = string.Empty;

        public override void Do()
        {
            base.Do();
            Application.OpenURL(_url);
        }
    }
}