using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.IsartDigital.PaperMan
{
    public class Object3DContainer : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public static Object3DContainer Instance { get; private set; }

        [SerializeField] private GameObject canvas;
        [SerializeField] private GameObject object3dToRotate;
        [SerializeField] private Transform containerTransform;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private float rotationSpeed = .01f;
        private Quaternion _rotationWhenBeginDrag;
        private Vector2 _mouseLastPos;

        private void Awake()
        {
            if (!Instance) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            Hide3DObject();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _rotationWhenBeginDrag = containerTransform.rotation;
            _mouseLastPos = eventData.position;
        }

        /// <summary>
        /// rotate the 3d object by using the mouse drag
        /// </summary>
        public void OnDrag(PointerEventData eventData)
        {
            Vector2 mousePositionFromStart = eventData.position - _mouseLastPos;
            containerTransform.rotation = Quaternion.AngleAxis(-rotationSpeed * mousePositionFromStart.x, cameraTransform.up) * _rotationWhenBeginDrag;
            Vector3 cameraRight = Quaternion.AngleAxis(-rotationSpeed * mousePositionFromStart.x, cameraTransform.up) * cameraTransform.right;
            containerTransform.rotation = Quaternion.AngleAxis(rotationSpeed * mousePositionFromStart.y, cameraRight) * containerTransform.rotation;

            // save the values for the next frame
            _mouseLastPos = eventData.position;
            _rotationWhenBeginDrag = containerTransform.rotation;
        }

        public void OnEndDrag(PointerEventData eventData) { }

        public void Show3DObject(GameObject _3dObject)
        {
            // destroy the last _3dObject
            if (containerTransform.childCount > 0)
                Destroy(containerTransform.GetChild(0));

            // reset container rotation then create the new 3d object
            containerTransform.rotation = Quaternion.identity;
            Instantiate(_3dObject, containerTransform);

            // show the canvas
            canvas.gameObject.SetActive(true);
        }

        public void Hide3DObject()
        {
            canvas.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
    }
}
