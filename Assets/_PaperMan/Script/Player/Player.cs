using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] float ACCELERATION = 3f;
    [SerializeField] float MAX_SPEED = 3f;

    [SerializeField] float ZIPLINE_EASE = 8;
    [SerializeField] float ZIPLINE_RANGE = 5000f;
    [SerializeField] float ZIPLINE_Y_OFFSET = -1;

    [SerializeField] KeyCode UP_KEY = KeyCode.Z;
    [SerializeField] KeyCode DOWN_KEY = KeyCode.S;
    [SerializeField] KeyCode RIGHT_KEY = KeyCode.D;
    [SerializeField] KeyCode LEFT_KEY = KeyCode.Q;
    [SerializeField] KeyCode INTERRACT_KEY = KeyCode.E;

    const string ZIPLINE_TAG = "Zipline";

    Zipline _zipline = null;

    Rigidbody _rigidComponent => GetComponent<Rigidbody>();

    Vector3 _velocity;

    Action _state;

    private void Awake()
    {
        if (Instance != null)
            throw new System.Exception("Double Singleton !");
        Instance = this;

    }

    void Start()
    {
        SetModNormal();
    }

    void Update()
    {
        _state();
    }

    void SetModNormal()
    {
        _state = DoActionNormal;
    }

    void DoActionNormal()
    {
        NormalMovements();

        //Dont care about the zipline if the player isnt pressing interract key
        if (!Input.GetKeyDown(INTERRACT_KEY))
            return;

        Zipline ziplineHit = CheckForZipline();

        if (ziplineHit != null)
            SetModZipline(ziplineHit);
    }

    void NormalMovements()
    {
        _velocity = Vector3.zero;

        //Handle four different inputs
        if (Input.GetKey(UP_KEY))
            _velocity.z += ACCELERATION;
        if (Input.GetKey(DOWN_KEY))
            _velocity.z -= ACCELERATION;
        if (Input.GetKey(RIGHT_KEY))
            _velocity.x += ACCELERATION;
        if (Input.GetKey(LEFT_KEY))
            _velocity.x -= ACCELERATION;

        //Apply inputs to velocity
        _rigidComponent.velocity += _velocity;

        //Clamp to max speed
        _rigidComponent.velocity = new Vector3(Mathf.Clamp(_rigidComponent.velocity.x, -MAX_SPEED, MAX_SPEED), _rigidComponent.velocity.y, Mathf.Clamp(_rigidComponent.velocity.z, -MAX_SPEED, MAX_SPEED));
    }

    Zipline CheckForZipline()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.up, out hit, ZIPLINE_RANGE))
        {
            if (hit.collider.tag == ZIPLINE_TAG)
                return hit.collider.GetComponent<Zipline>();
        }

        return null;
    }

    void SetModZipline(Zipline ziplineToFollow)
    {
        _state = DoActionZipline;
        _zipline = ziplineToFollow;

        _zipline.Activate();
    }

    void DoActionZipline()
    {
        if(_zipline == null)
        {
            SetModNormal();
            return;
        }

        //Ignore gravity
        _rigidComponent.velocity += Vector3.down * _rigidComponent.velocity.y;

        transform.position = Vector3.Lerp(transform.position, _zipline.transform.position, ZIPLINE_EASE * Time.deltaTime);
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
