using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarboardsAndPlank : MonoBehaviour
{
    [SerializeField] private float screenshakeDuration = 1f;
    [SerializeField] private float screenshakeForce = .5f;
    [SerializeField] private Com.IsartDigital.PaperMan.Camera mainCamera;

    public void PlayScreenshake()
    {
        mainCamera.Shake(screenshakeDuration, screenshakeForce);
    }
}