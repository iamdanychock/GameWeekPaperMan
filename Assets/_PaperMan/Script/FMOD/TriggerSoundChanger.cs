using Com.IsartDigital.PaperMan.Sound;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TriggerSoundChanger : MonoBehaviour
{

    [SerializeField] private int _SoundParameter;
    [SerializeField] private string _SoundReference;

    private AudioManager audioManager => AudioManager.instance;


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.UpdateAmbiance(_SoundReference, _SoundParameter);
        }

    }






}
