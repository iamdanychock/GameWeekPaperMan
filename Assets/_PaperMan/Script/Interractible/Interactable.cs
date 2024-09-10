using System;
using UnityEngine;

namespace Com.IsartDigital.PaperMan
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] private string playerTag = "Player";
        [SerializeField] private string interactionInput = "Interact";

        [SerializeField] private bool activateOutline = true;

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
        }

        protected virtual void PlayerExited()
        {
            doAction = null;
        }

        private void DoActionPlayerIn()
        {
            // the player interact with this object
            if (Input.GetButtonDown(interactionInput) && canInteract)
                Interact();
        }

        private void ChangeOutlineSizeAllChildrens(Transform _transform, float newOutlineSize)
        {
            _transform.GetComponent<MeshRenderer>();
            foreach (Transform child in _transform)
                ChangeOutlineSizeAllChildrens(child, newOutlineSize);
        }

        protected abstract void Interact();
    }
}