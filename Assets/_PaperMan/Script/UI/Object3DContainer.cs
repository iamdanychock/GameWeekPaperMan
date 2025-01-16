using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using System;
using TMPro;
using FMODUnity;
using FMOD.Studio;
using Unity.Services.Analytics.Internal;

namespace Com.IsartDigital.PaperMan
{
    public class Object3DContainer : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public static Object3DContainer Instance { get; private set; }

        [SerializeField] private GameObject canvas;
        [SerializeField] private GameObject object3dToRotate;
        [SerializeField] private Transform containerTransform;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemDescription;
        [NonSerialized] private EventReference itemSoundDrop;

        [Space]
        [SerializeField] private float rotationSpeed = .01f;
        [SerializeField] private float rotationSpeedController = .03f;
        private Quaternion _rotationWhenBeginDrag;
        private Vector2 _mouseLastPos;

        [Header("Anim")]
        [SerializeField] private AnimationCurve animationCurve;
        [SerializeField] private float animationDuration = 1.2f;
        private Vector3 object3dStartScale;

        private Coroutine animCoroutine;

        public bool isActivated = false;
        public bool canRotate = false;
        private bool isInAnimation = false;
        private float currentAnimDirection;

        private void Awake()
        {
            if (!Instance) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            Hide3DObject(false);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _rotationWhenBeginDrag = containerTransform.rotation;
            _mouseLastPos = eventData.position;
        }

        private void Update()
        {
            if (canRotate)
            {
                // rotate the object
                Vector2 mousePositionFromLastFrame = new Vector2(Input.GetAxis("RS_h"), -Input.GetAxis("RS_v"));
                containerTransform.rotation = Quaternion.AngleAxis(-rotationSpeedController * mousePositionFromLastFrame.x, cameraTransform.up) * _rotationWhenBeginDrag;
                Vector3 cameraRight = Quaternion.AngleAxis(-rotationSpeedController * mousePositionFromLastFrame.x, cameraTransform.up) * cameraTransform.right;
                containerTransform.rotation = Quaternion.AngleAxis(rotationSpeedController * mousePositionFromLastFrame.y, cameraRight) * containerTransform.rotation;

                _rotationWhenBeginDrag = containerTransform.rotation;
            }
        }

        /// <summary>
        /// rotate the 3d object by using the mouse drag
        /// </summary>
        public void OnDrag(PointerEventData eventData)
        {
            if (!canRotate) return;

            // rotate the object
            Vector2 mousePositionFromLastFrame = eventData.position - _mouseLastPos;
            containerTransform.rotation = Quaternion.AngleAxis(-rotationSpeed * mousePositionFromLastFrame.x, cameraTransform.up) * _rotationWhenBeginDrag;
            Vector3 cameraRight = Quaternion.AngleAxis(-rotationSpeed * mousePositionFromLastFrame.x, cameraTransform.up) * cameraTransform.right;
            containerTransform.rotation = Quaternion.AngleAxis(rotationSpeed * mousePositionFromLastFrame.y, cameraRight) * containerTransform.rotation;

            // save the values for the next frame
            _mouseLastPos = eventData.position;
            _rotationWhenBeginDrag = containerTransform.rotation;
        }

        public void OnEndDrag(PointerEventData eventData) { }

        /// <summary>
        /// Show the 3d object in the viewport with a spawn anim
        /// </summary>
        public void Show3DObject(Item item)
        {
            // destroy the last _3dObject
            if (object3dToRotate) Destroy(object3dToRotate);

            // reset container rotation then create the new 3d object
            containerTransform.rotation = Quaternion.identity;
            _rotationWhenBeginDrag = Quaternion.identity;
            object3dToRotate = Instantiate(item.itemObject, containerTransform);
            ChangeLayerAllChildren(object3dToRotate.transform, gameObject.layer);
            object3dStartScale = Vector3.one * item.itemScale;
            
            // show the canvas
            isActivated = true;
            canvas.gameObject.SetActive(true);
            StartAnim(1);

            // set the texts
            itemName.text = item.itemName;
            itemDescription.text = item.itemDescription;
            itemSoundDrop = item.itemSoundDrop;
        }

        private void ChangeLayerAllChildren(Transform _transform, int _layer)
        {
            _transform.gameObject.layer = _layer;
            foreach (Transform child in _transform)
                ChangeLayerAllChildren(child, _layer);
        }

        private void StartAnim(float direction)
        {
            // dont start the anim if it is already doing one or if he canvas is disabled
            if (!canvas.activeSelf || isInAnimation && direction == currentAnimDirection) return;

            if (animCoroutine != null)
                StopCoroutine(animCoroutine);
            animCoroutine = StartCoroutine(DoAnim(direction));
        }

        /// <summary>
        /// Make the 3d object follow a scale curve for animationDuration second
        /// </summary>
        /// <param name="direction"> 1 for going from 0 to 1 or -1 for it to go from 1 to 0
        private IEnumerator DoAnim(float direction)
        {
            isInAnimation = true;
            currentAnimDirection = direction;

            float elapsedTime = 0;
            float ratio;
            while (elapsedTime < animationDuration)
            {
                ratio = direction == 1 ? elapsedTime / animationDuration : 1 - elapsedTime / animationDuration;
                object3dToRotate.transform.localScale = object3dStartScale * animationCurve.Evaluate(ratio);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // set values at the end of anim
            object3dToRotate.transform.localScale = object3dStartScale;
            isInAnimation = false;
            currentAnimDirection = 0;

            // if the viewer is closing deactivate values
            if (direction == -1)
            {
                isActivated = false;
                canvas.gameObject.SetActive(false);

                // call event
                if (object3dToRotate)
                    onHide?.Invoke(object3dToRotate);
            }
            // if the viewer is opening activate values
            else
            {
                canRotate = true;
            }
        }

        public Action<GameObject> onHide;
        /// <summary>
        /// hide the 3d object and the canvas as well as doing an anim for the object
        /// </summary>
        public void Hide3DObject(bool doAnim = true)
        {
            // check if skip the anim or not
            if (doAnim) StartAnim(-1);
            else
            {
                // call event
                if (object3dToRotate)
                    onHide?.Invoke(object3dToRotate);

                canvas.gameObject.SetActive(false);
            }

            canRotate = false;
            PlayItemSoundDrop();
        }

        public void PlayItemSoundDrop()
        {
            RuntimeManager.PlayOneShot(itemSoundDrop, transform.position);
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        [Serializable]
        public struct Item
        {
            public string itemName;
            public string itemDescription;
            public float itemScale;
            public GameObject itemObject;
            public EventReference itemSoundTake;
            public EventReference itemSoundDrop;
            public EventReference itemCassette;
            public bool isCassette;
        }
    }
}
