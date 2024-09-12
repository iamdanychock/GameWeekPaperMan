using FMODUnity;
using System.Collections;
using UnityEngine;

public class ManageLightString : MonoBehaviour
{
    [SerializeField] private Transform lightStringTransform;
    [SerializeField] private Transform ClosestLight;

    const float WAIT_TIME = 2;

    private void Start()
    {
        ChangeStateAllLights(false);
    }

    public void ChangeStateAllLights(bool turnOn)
    {
        StartCoroutine(ChangeStateCoroutine(turnOn));
    }

    
    IEnumerator ChangeStateCoroutine(bool turnOn)
    {
        yield return new WaitForSeconds(WAIT_TIME);

        ChangeStateLightAndChildren(lightStringTransform, turnOn);
    }

    private void PlayLightBulbAmb()
    {

        GameObject posSound;

        if (ClosestLight != null)
            posSound = ClosestLight.gameObject;
        else
            posSound = gameObject;

        RuntimeManager.PlayOneShotAttached("event:/Amb/enviro_lightbulb", posSound);
    }

    private void ChangeStateLightAndChildren(Transform parent, bool turnOn)
    {
        // change the light state
        if (parent.TryGetComponent(out Light light))
            light.enabled = turnOn;
        PlayLightBulbAmb();
        // repeat the method on the children
        foreach (Transform child in parent)
            ChangeStateLightAndChildren(child, turnOn);
    }
}
