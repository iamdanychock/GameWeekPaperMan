using Com.IsartDigital.PaperMan.Sound;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TriggerSoundChanger : MonoBehaviour
{

    [SerializeField] private int _SoundParameterLeft;
    [SerializeField] private int _SoundParameterRight;

    [SerializeField] private string _SoundReference;

    private AudioManager audioManager => AudioManager.instance;

    private BoxCollider triggerCollider => GetComponent<BoxCollider>();

    public void OnTriggerEnter(Collider other)
    {

        Vector3 relativePosition = triggerCollider.transform.InverseTransformPoint(other.transform.position);

        if (relativePosition.x > 0)
        {
            RightEnter();
        }
        else if (relativePosition.x < 0)
        {
            LeftEnter();
        }
    }

    void RightEnter()
    {
        audioManager.UpdateAmbiance(_SoundReference, _SoundParameterLeft);
    }

    void LeftEnter()
    {
        audioManager.UpdateAmbiance(_SoundReference, _SoundParameterRight);

    }
}





