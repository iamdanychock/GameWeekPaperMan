using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;

namespace Com.IsartDigital.PaperMan.Sound
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance { get; private set; }

        [SerializeField] private EventReference _EnigmaReference;
        [SerializeField] private EventReference _UIMusicReference;
        [SerializeField] private EventReference _AmbReference;





        private Bus MusicBus;
        private Bus SFXBus;
        private Bus MasterBus;

        private EventInstance _AmbiantSound;
        private EventInstance _Music;


        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            if (instance != null)
            {
                Debug.LogError("Found more than on Audio Manager in the scene.");
            }
            instance = this;

            MasterBus = RuntimeManager.GetBus("bus:/");
            SFXBus = RuntimeManager.GetBus("bus:/SFX");
            MusicBus = RuntimeManager.GetBus("bus:/Music");

            //MasterBus.setVolume(SettingsSaveFile.masterVolumeValue);
            //MusicBus.setVolume(SettingsSaveFile.musicVolumeValue);
            //SFXBus.setVolume(SettingsSaveFile.SFXVolumeValue);


            //Temporary till the game starts
            SetAmbiance(_AmbReference);


        }


        public void PlayOneShot(EventReference sound, Vector3 worldPos)
        {
            RuntimeManager.PlayOneShot(sound);
            
        }


        public void ChangeMusicVolume(float pVolume)
        {
            SettingsSaveFile.musicVolumeValue = pVolume;    
            MusicBus.setVolume(pVolume);
        }

        public void ChangeSFXVolume(float pVolume)
        {
            SettingsSaveFile.SFXVolumeValue = pVolume;
            SFXBus.setVolume(pVolume);
        }

        public void ChangeMasterVolume(float pVolume)
        {
            SettingsSaveFile.masterVolumeValue = pVolume;
            MasterBus.setVolume(pVolume);
            
        }

        public EventInstance CreateLoop(EventReference pSound) => RuntimeManager.CreateInstance(pSound);


        public void SetMusic(EventReference pMusic)
        {
            _Music = CreateLoop(pMusic);
            _Music.start();
            RuntimeManager.AttachInstanceToGameObject(_Music, GetComponent<Transform>());
        }


        public void SetAmbiance(EventReference pAmbiance)
        {
            _AmbiantSound = CreateLoop(pAmbiance);
            _AmbiantSound.start();
            RuntimeManager.AttachInstanceToGameObject(_AmbiantSound, GetComponent<Transform>());
        }


        public void UpdateAmbiance(string pParameterName, int pValue)
        {
            _AmbiantSound.setParameterByName(pParameterName, pValue);
        }

        public void UpdateAmbianceGlobal(string pParameterName, int pValue)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName(pParameterName, pValue);
        }
        public void PlayUIMusic()
        {
            SetMusic(_EnigmaReference);

        }

        public void PlayEnigmaResolved()
        {
            SetMusic(_EnigmaReference);
        }


        public void StopMusic()
        {
            _Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }


        void Update()
        {
            transform.position = UnityEngine.Camera.main.transform.position;
        }
    }
}

