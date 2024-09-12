using FMOD;
using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarboardsAndPlank : MonoBehaviour
{
    [SerializeField] private float screenshakeDuration = 1f;
    [SerializeField] private float screenshakeForce = .5f;
    [SerializeField] private List<ParticleSystem> boxParticles;

    [SerializeField] private EventReference _PlanksBreakSoundReference;

    private EventInstance _PlanksBreakSoundInstance;

    public void PlayScreenshake()
    {
        Com.IsartDigital.PaperMan.Camera.Instance.Shake(screenshakeDuration, screenshakeForce);
    }

    public void PlayParticles()
    {
        foreach (var particle in boxParticles)
            particle.Play();
    }

    public void PlayPlanksBreakSound()
    {

        RuntimeManager.PlayOneShotAttached(_PlanksBreakSoundReference, gameObject);

    }
}