using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.PaperMan
{
    public class InspectableItem : Interactable
    {
        /// <summary>
        /// Show or hite the object 3d viewer canvas to see the objects
        /// </summary>
        protected override void Interact()
        {
            if (Object3DContainer.Instance)
            {
                if (Object3DContainer.Instance.isActivated)
                     Object3DContainer.Instance.Hide3DObject();
                else Object3DContainer.Instance.DebugShow();
            }
        }
    }
}