using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarboardsAndPlank : MonoBehaviour
{
    [SerializeField] private float screenshakeDuration = 1f;
    [SerializeField] private float screenshakeForce = .5f;

    public void PlayScreenshake()
    {
        Com.IsartDigital.PaperMan.Camera.Instance.Shake(screenshakeDuration, screenshakeForce);
    }
}