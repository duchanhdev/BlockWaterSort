using UnityEngine;

namespace BlockSort.GameLogic
{
    [CreateAssetMenu(fileName = "LevelClass", menuName = "Data/LevelClass")]
    public class LevelSO : ScriptableObject
    {
        [SerializeField]
        private int numEmptyTube;

        [SerializeField]
        private string[] tubes;

        public int GetNumEmptyTube()
        {
            return numEmptyTube;
        }

        public string[] GetTubes()
        {
            return (string[])tubes.Clone();
        }

        public void SetNumEmptyTube(int num)
        {
            numEmptyTube = num;
        }

        public void SetTubes(string[] tubes)
        {
            this.tubes = tubes;
        }
    }
}
