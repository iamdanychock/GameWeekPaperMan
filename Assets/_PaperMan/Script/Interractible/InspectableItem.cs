using UnityEngine;

namespace Com.IsartDigital.PaperMan
{
    public class InspectableItem : Interactable
    {
        [Space]
        [SerializeField] private Object3DContainer.Item itemValues;

        /// <summary>
        /// Show or hite the object 3d viewer canvas to see the objects
        /// </summary>
        protected override void Interact()
        {
            if (Object3DContainer.Instance)
            {
                if (Object3DContainer.Instance.isActivated)
                     Object3DContainer.Instance.Hide3DObject();
                else Object3DContainer.Instance.Show3DObject(itemValues);
            }
        }

        protected override void PlayerExited()
        {
            base.PlayerExited();

            // close the 3d object viewer if the player move away from the item
            if (Object3DContainer.Instance && Object3DContainer.Instance.isActivated)
                Object3DContainer.Instance.Hide3DObject();
        }
    }
}