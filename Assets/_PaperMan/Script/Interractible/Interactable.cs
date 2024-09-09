using System;
using UnityEngine;

namespace Com.IsartDigital.PaperMan
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] private string playerTag = "Player";
        [SerializeField] private string interactionInput = "Interact";

        private Action doAction;
        protected bool canInteract = true;

        private void Update()
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
            if (Input.GetKeyDown(interactionInput) && canInteract)
                Interact();
        }

        protected abstract void Interact();
    }
}