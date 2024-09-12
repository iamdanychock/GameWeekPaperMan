using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UIElements;

namespace Com.IsartDigital.PaperMan
{
    public class InspectableItem : Interactable
    {
        [Space]
        [SerializeField] private Object3DContainer.Item itemValues;

        private EventInstance _CassetteInstance;
        /// <summary>
        /// Show or hite the object 3d viewer canvas to see the objects
        /// </summary>
        protected override void Interact()
        {
            PlayItemSoundTake();
            if (Object3DContainer.Instance)
            {
                if (Object3DContainer.Instance.isActivated)
                     Object3DContainer.Instance.Hide3DObject();
                else Object3DContainer.Instance.Show3DObject(itemValues);
            }
        }

        public void PlayItemSoundTake()
        {
            RuntimeManager.PlayOneShot(itemValues.itemSoundTake, transform.position);

            PLAYBACK_STATE test;
            
            if (itemValues.itemCassette.Path.Length > 0)
            {
                _CassetteInstance.getPlaybackState(out test);

                if (test != PLAYBACK_STATE.PLAYING)
                {
                    _CassetteInstance = RuntimeManager.CreateInstance(itemValues.itemCassette);
                    _CassetteInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
                    _CassetteInstance.start();
                }

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