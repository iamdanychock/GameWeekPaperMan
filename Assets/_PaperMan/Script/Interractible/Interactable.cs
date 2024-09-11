using System;
using UnityEngine;
using UnityEngine.Events;

namespace Com.IsartDigital.PaperMan
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] private float outlineStrength = 1.1f;

        [SerializeField] private string playerTag = "Player";
        [SerializeField] private string interactionInput = "Interact";

        [SerializeField] private bool activateOutline = true;
        [SerializeField] private UnityEvent onInteract;
        [SerializeField] private UnityEvent onEntered;
        [SerializeField] private UnityEvent onExited;

        const string OUTLINE_SHADER = "Outline";
        const string OUTLINE_SCALE = "_Scale";

        private Action doAction;
        protected bool canInteract = true;

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            doAction?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(playerTag)) 
                PlayerEntered();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(playerTag))
                PlayerExited();
        }

        protected virtual void PlayerEntered()
        {
            doAction = DoActionPlayerIn;

            onEntered?.Invoke();
            ChangeOutlineSizeAllChildrens(transform, outlineStrength);
        }

        protected virtual void PlayerExited()
        {
            doAction = null;

            onExited?.Invoke();
            ChangeOutlineSizeAllChildrens(transform, 1f);
        }

        private void DoActionPlayerIn()
        {
            // the player interact with this object
            if (Input.GetButtonDown(interactionInput) && canInteract)
                Interact();
        }

        private void ChangeOutlineSizeAllChildrens(Transform _transform, float newOutlineSize)
        {
            MeshRenderer meshRenderer = _transform.GetComponent<MeshRenderer>();

            if(meshRenderer != null && meshRenderer.materials.Length > 1)
            {
                Material shader = meshRenderer.materials[1];

                if (shader.name.Contains(OUTLINE_SHADER))
                    shader.SetFloat(OUTLINE_SCALE, newOutlineSize);    
            }


            foreach (Transform child in _transform)
                ChangeOutlineSizeAllChildrens(child, newOutlineSize);
        }

        protected virtual void Interact()
        {
            onInteract?.Invoke();
        }
    }
}