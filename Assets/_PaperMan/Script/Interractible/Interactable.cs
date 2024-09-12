using System;
using UnityEngine;
using UnityEngine.Events;

namespace Com.IsartDigital.PaperMan
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] public float outlineStrength = 1.1f;

        [SerializeField] private string playerTag = "Player";
        [SerializeField] private string interactionInput = "Interact";

        [SerializeField] private bool activateOutline = true;
        [SerializeField] private bool activateInteractUI = true;
        [SerializeField] private UnityEvent onInteract;
        [SerializeField] private UnityEvent onEntered;
        [SerializeField] private UnityEvent onExited;

        const string OUTLINE_SHADER = "Outline";
        const string OUTLINE_SCALE = "_Scale";

        private Action doAction;
        protected bool canInteract = true;

        public bool PlayerInside = false;

        public bool InterractionActive = true;

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
            if (!InterractionActive)
                return;

            doAction = DoActionPlayerIn;

            onEntered?.Invoke();
            ChangeOutlineSizeAllChildrens(transform, outlineStrength);

            PlayerInside = true;

            // show or not the interact ui
            if (activateInteractUI)
                Player.Instance.interactUI.Activate();
        }

        protected virtual void PlayerExited()
        {
            doAction = null;

            onExited?.Invoke();
            Player.Instance.isTouching = false;

            ChangeOutlineSizeAllChildrens(transform, 1f);

            PlayerInside = false;

            // disable or not the interact ui
            if (activateInteractUI)
                Player.Instance.interactUI.Disable();
        }

        private void DoActionPlayerIn()
        {
            if (!InterractionActive)
            {
                PlayerExited();
                return;
            }

            // the player interact with this object
            if (Input.GetButtonDown(interactionInput) && canInteract)
                Interact();

            Player.Instance.isTouching = true;
        }

        public void ChangeOutlineSizeAllChildrens(Transform _transform, float newOutlineSize)
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