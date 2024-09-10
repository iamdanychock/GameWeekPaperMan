using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(StudioEventEmitter))]
public class PlayerSFX : MonoBehaviour
{

    #region Events
    public EventReference footSteps;

    #endregion

    private StudioEventEmitter studioEventEmitter;

    private void Awake()
    {
        studioEventEmitter = GetComponent<StudioEventEmitter>();
    }


    public void PlaySFX(EventReference pEventToPlay)
    {
        studioEventEmitter.EventReference = pEventToPlay;
        studioEventEmitter.Play();

    }


}
