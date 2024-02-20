using Cysharp.Threading.Tasks;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.Constants;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.Events;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Egitech.Core.Runtime.Gameplay.Save
{
    [CreateAssetMenu(fileName = "SaveSystem", menuName = "Game/SaveSystem")]
    public sealed class SaveSystem : DescriptionBaseSO
    {
        [Header("Config")]
        [SerializeField]
        private string _saveFileName = "save.cricket";

        [SerializeField]
        private string _backupSaveFileName = "save.cricket.bak";

        [SerializeField]
        private SettingConstant _defaultSetting = default!;

        [Header("Listening to")]
        [SerializeField]
        private SettingEvent _onSavingSetting = default!;

        [SerializeField]
        private VoidEvent _onLoadingSavedSetting = default!;

        [Header("Broadcasting on")]
        [SerializeField]
        private SettingEvent _onSavedSettingLoaded = default!;

        private Save? _saveData;

        private void OnEnable()
        {
            _onSavingSetting.Register(OnSavingSetting);
            _onLoadingSavedSetting.Register(OnLoadingSavedSetting);
        }

        private void OnDisable()
        {
            _onSavingSetting.Unregister(OnSavingSetting);
            _onLoadingSavedSetting.Unregister(OnLoadingSavedSetting);
        }

        private void OnLoadingSavedSetting()
        {
            if (LoadSaveDataFromDisk() && _saveData != null)
            {
                _onSavedSettingLoaded.Raise(_saveData.Setting);
            }
        }

        private void OnSavingSetting(Setting.Setting setting)
        {
            SaveDataToDisk(setting);
        }

        public bool LoadSaveDataFromDisk()
        {
            _saveData = new Save(_defaultSetting.Value);
            if (FileManager.LoadFromFile(_saveFileName, out var json) && !string.IsNullOrEmpty(json))
            {
                _saveData?.LoadFromJson(json);
            }
            return true;
        }

        private void SaveDataToDisk(Setting.Setting setting)
        {
            if (_saveData == null)
            {
                return;
            }

            _saveData.Setting = setting;
            if (FileManager.WriteToFile(_saveFileName, _saveData.ToJson()))
            {
                Debug.Log($"Save successful {_saveFileName}");
            }
        }

        public void SetNewGameData()
        {
            FileManager.WriteToFile(_saveFileName, string.Empty);

            SaveDataToDisk(_defaultSetting.Value);
        }

        public void WriteEmptySaveFile()
        {
            FileManager.WriteToFile(_saveFileName, string.Empty);
        }

        public async UniTaskVoid LoadSavedInventory()
        {
        }

        public void LoadSavedQuestLineStatus()
        {
        }
    }
}