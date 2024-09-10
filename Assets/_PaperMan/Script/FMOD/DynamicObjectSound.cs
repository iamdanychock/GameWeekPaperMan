using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(StudioEventEmitter))]
public class DynamicObjectSound : MonoBehaviour
{
    [SerializeField] private EventReference _eventReference;
    private SphereCollider _SphereCollider;
    private StudioEventEmitter _Emitter;

    


    //Soit le son est déclenché par un evènement soit il est déclenché automatiquement par TriggerEnter
    [SerializeField] private bool _IsPlayingAutomatically;

    private bool _HasPlayed;


    private void Awake()
    {
        _SphereCollider = GetComponent<SphereCollider>();
        _Emitter = GetComponent<StudioEventEmitter>();
    }


    private void Start()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {  
        if (_IsPlayingAutomatically)
        {
            PlaySFX();
            _HasPlayed = true;
        }
    }



    public void PlaySFX()
    {
        _Emitter.EventReference = _eventReference;
        _Emitter.Play();
    }








}
