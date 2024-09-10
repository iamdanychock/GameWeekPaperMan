using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

namespace Com.IsartDigital.PaperMan.Sound
{
    public class AudioManager : MonoBehaviour
    {

        public static AudioManager instance { get; private set; }

        

        private Bus MusicBus;
        private Bus SFXBus;
        private Bus MasterBus;


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

        }


        public void PlayOneShot(EventReference sound, Vector3 worldPos)
        {
            RuntimeManager.PlayOneShot(sound);
            
        }


        public void ChangeMusicVolume(float pVolume)
        {
            MusicBus.setVolume(pVolume);
        }

        public void ChangeSFXVolume(float pVolume)
        {
            SFXBus.setVolume(pVolume);
        }

        public void ChangeMasterVolume(float pVolume)
        {
            MasterBus.setVolume(pVolume);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

