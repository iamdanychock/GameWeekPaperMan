using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimScript : MonoBehaviour
{
    [SerializeField] private EventReference _DeathIn;
    [SerializeField] private EventReference _DeathOut;

    private EventInstance _Instance;



    public void PlayIn()
    {
        _Instance = RuntimeManager.CreateInstance(_DeathIn);
        _Instance.start();
    }
    public void PlayOut()
    {
        _Instance = RuntimeManager.CreateInstance(_DeathOut);
        _Instance.start();
    }

}
