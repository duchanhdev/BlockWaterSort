using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Localization;

namespace Egitech.Core.Runtime.Gameplay.Setting
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class Setting
    {
        [JsonProperty]
        [SerializeField]
        private float _masterVolume = 1;

        [JsonProperty]
        [SerializeField]
        private float _musicVolume = 1;

        [JsonProperty]
        [SerializeField]
        private float _sfxVolume = 1;

        [JsonProperty]
        [SerializeField]
        private int _qualityLevel = 1;

        [JsonProperty]
        [SerializeField]
        private Locale _locale;

        [JsonConstructor]
        public Setting(float masterVolume, float musicVolume, float sfxVolume, int qualityLevel, Locale locale)
        {
            _masterVolume = masterVolume;
            _musicVolume = musicVolume;
            _sfxVolume = sfxVolume;
            _qualityLevel = qualityLevel;
            _locale = locale;
        }

        public float MasterVolume => _masterVolume;

        public float MusicVolume => _musicVolume;

        public float SFXVolume => _sfxVolume;

        public int QualityLevel => _qualityLevel;

        public Locale Locale => _locale;

        public Setting With(float? masterVolume = null, float? musicVolume = null, float? sfxVolume = null,
            int? qualityLevel = null, Locale?
                locale = null)
        {
            return new Setting(masterVolume ?? _masterVolume, musicVolume ?? _musicVolume, sfxVolume ?? _sfxVolume,
                qualityLevel ?? _qualityLevel, locale != null ? locale : _locale);
        }
    }
}