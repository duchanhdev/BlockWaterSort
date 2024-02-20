using UnityEngine;

namespace BlockSort.GameLogic
{
    public class SaveMigrator: MonoBehaviour
    {
        private void Awake()
        {
            SaveMigrate();
        }

        private static void SaveMigrate()
        {
            var saveString = ES3.LoadRawString(ES3Settings.defaultSettings.path);
            saveString = saveString.Replace("Assets.BlockSort.Scripts.GameLogic.GameStatus", "BlockSort.GameLogic.GameStatus");
            saveString = saveString.Replace("Assets.BlockSort.Scripts.GameLogic.PlayerInfoSO", "BlockSort.GameLogic.PlayerInfoSO");
            ES3.SaveRaw(saveString, ES3Settings.defaultSettings.path);
        }
    }
}
