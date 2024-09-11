using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.PaperMan
{
    public class Camera : MonoBehaviour
    {
        public static Camera Instance;

        [SerializeField] float Z_OFFSET = -8;
        [SerializeField] float Y_OFFSET = 4;
        [SerializeField] float MAX_ZOOM = 1.2f;
        [SerializeField] float TIME_BEFORE_ZOOMING = 2f;
        [SerializeField] float ZOOM_EASING_IN = .5f;
        [SerializeField] float ZOOM_EASING_OUT = 1f;

        //4 mean the camera will reach the point in approximately 1/4 seconds
        [SerializeField] float EASING = 4f;

        [SerializeField] bool IGNORE_Z = false;

        float startFOV;
        float zoomTimer = 0;

        const float SHAKE_VALUE_DIVIDOR = 10;
        float shakeValue = 0;
        float shakeTime = 0;

        public float leftLimit;
        public float rightLimit;

        UnityEngine.Camera _cameraComponent => GetComponent<UnityEngine.Camera>();

        //The position that the camera follow
        public Vector3 PointOfInterrest;

        private void Start()
        {
            if (Instance != null)
                throw new UnityException("Double Singleton !");
            Instance = this;

            startFOV = _cameraComponent.fieldOfView;

            leftLimit = float.NegativeInfinity;
            rightLimit = float.PositiveInfinity;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                Shake(2, 1);

            //By default following the player
            PointOfInterrest = Player.Instance.transform.position;

            FollowPointOfInterrest();
            SendHideObjectRayCast();
            ManageFOV();

            if (shakeValue > 0) Shake();
        }

        void Shake()
        {
            shakeValue -= Time.deltaTime * shakeTime;

            if (shakeTime < 0)
                shakeValue = 0;

            transform.position += new Vector3(Random.Range(-shakeValue, shakeValue), Random.Range(-shakeValue, shakeValue), Random.Range(-shakeValue, shakeValue));
        }

        void ManageFOV()
        {
            bool isMoving = Player.Instance.RigidComponent.velocity != Vector3.zero;
            zoomTimer += Time.deltaTime;

            if (isMoving)
                zoomTimer = 0;

            if (zoomTimer < TIME_BEFORE_ZOOMING && !isMoving)
                return;

            float ease = isMoving ? ZOOM_EASING_OUT : ZOOM_EASING_IN;

            _cameraComponent.fieldOfView = Mathf.Lerp(_cameraComponent.fieldOfView, isMoving ? startFOV : startFOV / MAX_ZOOM, Time.deltaTime * ease);
        }

        void SendHideObjectRayCast()
        {
            RaycastHit[] hit = Physics.RaycastAll(transform.position, (Player.Instance.transform.position - transform.position).normalized, Vector3.Distance(transform.position, Player.Instance.transform.position));
            foreach (var item in hit)
            {
                FadeObject fadeObject;
                if (!item.collider.TryGetComponent<FadeObject>(out fadeObject))
                    continue;

                fadeObject.Fade();
            }
        }

        void FollowPointOfInterrest()
        {
            //Offset the POI so the camera is looking at it instead of teleporting to it
            PointOfInterrest += new Vector3(0, Y_OFFSET, Z_OFFSET);

            if (PointOfInterrest.x > rightLimit) PointOfInterrest.x = rightLimit;
            else if (PointOfInterrest.x < leftLimit) PointOfInterrest.x = leftLimit;

            //Possibly ignore Z offset
            if (IGNORE_Z)
                PointOfInterrest.z = Z_OFFSET;

            //Apply with lerp to smooth movement
            transform.position = Vector3.Lerp(transform.position, PointOfInterrest, EASING * Time.deltaTime);
        }

        public void Shake(float seconds, float force)
        {
            shakeValue = force / SHAKE_VALUE_DIVIDOR;
            shakeTime = shakeValue / seconds;
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}
