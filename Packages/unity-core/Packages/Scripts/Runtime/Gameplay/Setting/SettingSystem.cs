using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.Events;
using Egitech.Core.Runtime.Gameplay.Setting.AtomGenerated.References;
using NaughtyAttributes;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Egitech.Core.Runtime.Gameplay.Setting
{
    public class SettingSystem : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField]
        private SettingReference _currentSetting = default!;

        [Foldout("Broadcasting on")]
        [SerializeField]
        private FloatEvent _onMasterVolumeChanged = default!;

        [Foldout("Broadcasting on")]
        [SerializeField]
        private FloatEvent _onSFXVolumeChanged = default!;

        [Foldout("Broadcasting on")]
        [SerializeField]
        private FloatEvent _onMusicVolumeChanged = default!;

        [Foldout("Broadcasting on")]
        [SerializeField]
        private VoidEvent _onLoadingSavedSetting = default!;

        [Foldout("Broadcasting on")]
        [SerializeField]
        private SettingEvent _onSavingSetting = default!;

        [Foldout("Listening to")]
        [SerializeField]
        private VoidEvent _onChangingSetting = default!;

        [Foldout("Listening to")]
        [SerializeField]
        private SettingEvent _onSavedSettingLoaded = default!;

        private void Start()
        {
            _onLoadingSavedSetting.Raise();
        }

        private void OnEnable()
        {
            _onSavedSettingLoaded.Register(OnSaveLoaded);
            _onChangingSetting.Register(OnChangingSetting);
        }

        private void OnDisable()
        {
            _onSavedSettingLoaded.Unregister(OnSaveLoaded);
            _onChangingSetting.Unregister(OnChangingSetting);
        }

        public void ChangeMusicVolume(float newVolume)
        {
            _currentSetting.Value = _currentSetting.Value.With(musicVolume: newVolume);
            _onSavingSetting.Raise(_currentSetting.Value);
            _onMusicVolumeChanged.Raise(_currentSetting.Value.MusicVolume);
        }

        public void ChangeSFXVolume(float newVolume)
        {
            _currentSetting.Value = _currentSetting.Value.With(sfxVolume: newVolume);
            _onSavingSetting.Raise(_currentSetting.Value);
            _onSFXVolumeChanged.Raise(_currentSetting.Value.SFXVolume);
        }

        public void ChangeGraphicQuality(int newQuality)
        {
            _currentSetting.Value = _currentSetting.Value.With(qualityLevel: newQuality);
            _onSavingSetting.Raise(_currentSetting.Value);
            QualitySettings.SetQualityLevel(_currentSetting.Value.QualityLevel, true);
        }

        public void ChangeLocale(Locale locale)
        {
            _currentSetting.Value = _currentSetting.Value.With(locale: locale);
            _onSavingSetting.Raise(_currentSetting.Value);
            LocalizationSettings.SelectedLocale = _currentSetting.Value.Locale;
        }

        private void OnChangingSetting()
        {
            _onSavingSetting.Raise(_currentSetting.Value);
        }

        private void OnSaveLoaded(Setting setting)
        {
            _currentSetting.Value = setting;
            OnCurrentSettingChanged();
        }

        private void OnCurrentSettingChanged()
        {
            _onMusicVolumeChanged.Raise(_currentSetting.Value.MusicVolume);
            _onSFXVolumeChanged.Raise(_currentSetting.Value.SFXVolume);
            _onMasterVolumeChanged.Raise(_currentSetting.Value.MasterVolume);

            QualitySettings.SetQualityLevel(_currentSetting.Value.QualityLevel, true);

            LocalizationSettings.SelectedLocale = _currentSetting.Value.Locale;
        }
    }
}