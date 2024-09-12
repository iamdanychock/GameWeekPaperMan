using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerSFX : MonoBehaviour
{

    #region Events
    [SerializeField] private EventReference _FootSteps;
    [SerializeField] private EventReference _Fall;
    [SerializeField] private EventReference _Presence;

    #endregion

    private EventInstance _EventInstance;
    private EventInstance _PresenceInstance;

    private void Awake()
    {
    }


    public void PlaySFX(EventReference pEventToPlay)
    {
        _EventInstance = RuntimeManager.CreateInstance(pEventToPlay);

        switch (Player.Instance.GroundSound)
        {
            case GROUND_SOUNDS.WOOD:
                _EventInstance.setParameterByName("footstep_state", 1);

                break;
            case GROUND_SOUNDS.CONCRETE:
                _EventInstance.setParameterByName("footstep_state", 0);

                break;
            case GROUND_SOUNDS.NOTHING:
                _EventInstance.setParameterByName("footstep_state", 1);
                break;
        }

        _EventInstance.start();
    }

    public void PlayStep()
    {
        PlaySFX(_FootSteps);
        
    }

    public void PlayPresence()
    {
        _PresenceInstance = RuntimeManager.CreateInstance(_Presence);
        _PresenceInstance.start();
    }

    public void StopPresence()
    {
        _PresenceInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

    }

    public void StopSteps()
    {
        _EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void PlayFall()
    {
        PlaySFX(_Fall);
    }
    


}
