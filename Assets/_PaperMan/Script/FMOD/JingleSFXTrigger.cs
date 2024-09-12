using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.IsartDigital.PaperMan.Sound;

[RequireComponent(typeof(BoxCollider))]
public class JingleSFXTrigger : MonoBehaviour
{

    [SerializeField] private bool isEnteringRight;

    private bool HasBeenTrigger;
    private BoxCollider triggerCollider => GetComponent<BoxCollider>();


    public void OnTriggerEnter(Collider other)
    {

        Vector3 relativePosition = triggerCollider.transform.InverseTransformPoint(other.transform.position);

        if (relativePosition.x > 0 && !HasBeenTrigger && isEnteringRight)
        {
            HasBeenTrigger = true;
            AudioManager.instance.PlayEnigmaResolved();
        }

        else if (relativePosition.x < 0 && !HasBeenTrigger && !isEnteringRight)
        {
            HasBeenTrigger = true;
            AudioManager.instance.PlayEnigmaResolved();
        }
    }

    
}
