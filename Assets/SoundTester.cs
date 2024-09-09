using Com.IsartDigital.PaperMan.Sound;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTester : MonoBehaviour
{

    [SerializeField] private EventReference SoundToPlay;
    void Start()
    {
        
    }


    public void PlaySound()
    {
        AudioManager.instance.PlayOneShot(SoundToPlay,transform.position);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
