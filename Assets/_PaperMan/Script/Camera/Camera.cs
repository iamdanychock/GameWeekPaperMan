using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] float Z_OFFSET = -8;
    [SerializeField] float Y_OFFSET = 4;

    //4 mean the camera will reach the point in approximately 1/4 seconds
    [SerializeField] float EASING = 4f;

    [SerializeField] bool IGNORE_Z = false;

    //The position that the camera follow
    public Vector3 PointOfInterrest; 

    void Start()
    {
    }

    void Update()
    {
        //By default following the player
        PointOfInterrest = Player.Instance.transform.position;

        FollowPointOfInterrest();
    }

    void FollowPointOfInterrest()
    {
        //Offset the POI so the camera is looking at it instead of teleporting to it
        PointOfInterrest += new Vector3(0, Y_OFFSET, Z_OFFSET);

        //Possibly ignore Z offset
        if (IGNORE_Z)
            PointOfInterrest.z = Z_OFFSET;

        //Apply with lerp to smooth movement
        transform.position = Vector3.Lerp(transform.position,PointOfInterrest,EASING * Time.deltaTime);
    }
}
