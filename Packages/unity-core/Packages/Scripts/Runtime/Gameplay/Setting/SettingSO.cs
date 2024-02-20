using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Localization;

namespace Egitech.Core.Runtime.Gameplay.Setting
{
    [CreateAssetMenu(fileName = "SETTING_BaseSettings", menuName = "Game Data/Settings")]
    public sealed class SettingSO : ScriptableObject
    {
        [SerializeField]
        private Locale _locale = default!;

        [SerializeField]
        private FloatReference _masterVolume = default!;

        [SerializeField]
        private FloatReference _musicVolume = default!;

        [SerializeField]
        private FloatReference _sfxVolume = default!;

        public Locale Locale => _locale;

        public FloatReference MasterVolume => _masterVolume;

        public FloatReference MusicVolume => _musicVolume;

        public FloatReference SFXVolume => _sfxVolume;

        public void LoadSaveAudioSettings(float newMasterVolume, float newMusicVolume, float newSFXVolume)
        {
            _masterVolume.Value = newMasterVolume;
            _musicVolume.Value = newMusicVolume;
            _sfxVolume.Value = newSFXVolume;
        }

        public void LoadSaveLanguageSettings(Locale newLocale)
        {
            _locale = newLocale;
        }

        public void LoadSavedSettings(Save.Save savedFile)
        {
            LoadSaveAudioSettings(savedFile.Setting.MasterVolume, savedFile.Setting.MusicVolume,
                savedFile.Setting.SFXVolume);
            LoadSaveLanguageSettings(savedFile.Setting.Locale);
        }
    }
}