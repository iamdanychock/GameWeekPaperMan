using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.DebugUI;

public class GlobalVolume : MonoBehaviour

{
    private Volume volume => GetComponent<Volume>();


    private int randomValue;

    public float changeInterval = 1.0f;

    private bool isToggling = true;

    public ClampedFloatParameter clampedFloat;


    private ChromaticAberration aberration;


    private void Awake()
    {
        volume.profile.TryGet(out aberration);
        StartCoroutine(ToggleRandomValue());
    }

    IEnumerator ToggleRandomValue()
    {
        while (isToggling)
        {
            clampedFloat = new ClampedFloatParameter(Random.Range(0f, 1f), 0f, 1f);
            aberration.intensity = clampedFloat;
            aberration.SetAllOverridesTo(true);


            yield return new WaitForSeconds(changeInterval);
        }
    }

    public void StopToggling()
    {
        isToggling = false;
    }
}
