using Com.IsartDigital.PaperMan.Sound;
using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StudioEventEmitter))]

public class SoundTester : MonoBehaviour
{

    [SerializeField] private EventReference SoundToPlay;

    EventInstance SFXEventInstance;
    StudioEventEmitter _Emitter;
    void Start()
    {
        _Emitter = GetComponent<StudioEventEmitter>();
    }


    public void PlaySound()
    {
        _Emitter.EventReference = SoundToPlay;
        _Emitter.Play();
    }

    public void PlaySFX()
    {
        SFXEventInstance = RuntimeManager.CreateInstance(SoundToPlay);
        SFXEventInstance.start();

        RuntimeManager.AttachInstanceToGameObject(SFXEventInstance, transform);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
