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

        //4 mean the camera will reach the point in approximately 1/4 seconds
        [SerializeField] float EASING = 4f;

        [SerializeField] bool IGNORE_Z = false;

        public float leftLimit;
        public float rightLimit;

        //The position that the camera follow
        public Vector3 PointOfInterrest;

        private void Start()
        {
            if (Instance != null)
                throw new UnityException("Double Singleton !");
            Instance = this;

            leftLimit = float.NegativeInfinity;
            rightLimit = float.PositiveInfinity;
        }

        void Update()
        {
            //By default following the player
            PointOfInterrest = Player.Instance.transform.position;

            FollowPointOfInterrest();
            SendHideObjectRayCast();
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

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}
