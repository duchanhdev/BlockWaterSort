using System;
using System.Collections.Generic;
using Egitech.Core.Runtime;
using UnityEngine;

namespace BlockSort.Sound
{
    public class SoundController : Singleton<SoundController>
    {
        private const string ES3_SAVE_TURN_ON_NAME = "Sound";
        private static SoundController soundControllerInstance;
        private readonly List<AudioSource> _audioSourceList = new();

        public bool IsTurnOn { get; private set; }

        protected override void OnAwake()
        {
            base.OnAwake();
            
            if (!ES3.KeyExists(ES3_SAVE_TURN_ON_NAME))
            {
                IsTurnOn = true;
            }
        }

        private void SaveIsTurnOn()
        {
            try
            {
                ES3.Save(ES3_SAVE_TURN_ON_NAME, IsTurnOn);
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
            }
        }

        private void LoadIsTurnOn()
        {
            IsTurnOn = true;
            if (!ES3.KeyExists(ES3_SAVE_TURN_ON_NAME))
            {
                SaveIsTurnOn();
            }

            try
            {
                IsTurnOn = (bool)ES3.Load(ES3_SAVE_TURN_ON_NAME);
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
            }
        }

        public void AddAudioSource(AudioSource audioSource)
        {
            _audioSourceList.Add(audioSource);
        }

        public void RemoveAudioSource(AudioSource audioSource)
        {
            _audioSourceList.Remove(audioSource);
        }

        public void DisableAllAudioSource()
        {
            foreach (var audioSource in _audioSourceList)
            {
                audioSource.mute = true;
            }
        }

        public void EnableAllAudioSource()
        {
            foreach (var audioSource in _audioSourceList)
            {
                audioSource.mute = false;
            }
        }

        public void ChangeStatusSound()
        {
            IsTurnOn = !IsTurnOn;
            if (!IsTurnOn)
            {
                DisableAllAudioSource();
            }
            else
            {
                EnableAllAudioSource();
            }

            SaveIsTurnOn();
        }

        public void PlayOneAudioSource(AudioSource audioSource)
        {
            audioSource.Play();
            audioSource.mute = !IsTurnOn;
        }
    }
}
