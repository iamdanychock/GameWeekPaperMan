using Com.IsartDigital.PaperMan;
using UnityEngine;
using UnityEngine.Events;

public class HintLetter2Trigger : MonoBehaviour
{
    [SerializeField] private GameObject inspectedGameObject;

    [SerializeField] private UnityEvent triggerEvent;

    private void Start()
    {
        Object3DContainer.Instance.onHide += OnHide;
    }

    private bool didIt = false;
    private void OnHide(GameObject _object)
    {
        if (_object.name.Contains(inspectedGameObject.name) && !didIt)
        {
            Debug.Log("nice");
            didIt = true;
            triggerEvent?.Invoke();
        }
    }
}
