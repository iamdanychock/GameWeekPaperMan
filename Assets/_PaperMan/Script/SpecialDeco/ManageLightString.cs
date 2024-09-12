using UnityEngine;

public class ManageLightString : MonoBehaviour
{
    [SerializeField] private Transform lightStringTransform;

    private void Start()
    {
        ChangeStateAllLights(false);
    }

    public void ChangeStateAllLights(bool turnOn)
    {
        ChangeStateLightAndChildren(lightStringTransform, turnOn);
    }

    private void ChangeStateLightAndChildren(Transform parent, bool turnOn)
    {
        // change the light state
        if (parent.TryGetComponent(out Light light))
            light.enabled = turnOn;

        // repeat the method on the children
        foreach (Transform child in parent)
            ChangeStateLightAndChildren(child, turnOn);
    }
}
