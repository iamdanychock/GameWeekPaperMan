using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(StudioEventEmitter))]
public class PlayerSFX : MonoBehaviour
{

    #region Events
    [SerializeField] private EventReference _FootSteps;
    [SerializeField] private EventReference _Fall;

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
