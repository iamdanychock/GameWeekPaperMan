using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] float ACCELERATION = 3f;
    [SerializeField] float MAX_SPEED = 3f;

    [SerializeField] float ZIPLINE_EASE = 16;
    [SerializeField] float ZIPLINE_RANGE = 3;
    [SerializeField] float ZIPLINE_Y_OFFSET = -1;

    const string INTERRACTION_INPUT = "Interact";
    const string HORIZONTAL_AXIS = "Horizontal";
    const string VERTICAL_AXIS = "Vertical";

    const string ZIPLINE_TAG = "Zipline";

    const string WALKING_ANIM = "Walking";
    const string ZIPLINE_GRAB_ANIM = "ZiplineGrab";
    const string ZIPLINE_IDLE_ANIM = "ZiplineIdle";
    const string ZIPLINE_RELEASE_ANIM = "ZiplineRelease";
    const string IDLE_ANIM = "Idle";

    Zipline _zipline = null;

    Rigidbody _rigidComponent => GetComponent<Rigidbody>();
    Animator _animatorComponent => GetComponent<Animator>();
    ParticleSystem.EmissionModule _particleSystemMain;

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
        _particleSystemMain = GetComponentInChildren<ParticleSystem>().emission;

        SetModNormal();
    }

    void Update()
    {
        _state();
    }

    public void SetModNormal()
    {
        _rigidComponent.isKinematic = false;
        _state = DoActionNormal;
    }

    void DoActionNormal()
    {
        NormalMovements();

        //Dont care about the zipline if the player isnt pressing interract key
        if (!Input.GetButtonDown(INTERRACTION_INPUT))
            return;

        Zipline ziplineHit = CheckForZipline();

        if (ziplineHit != null)
            SetModZipline(ziplineHit);
    }

    void NormalMovements()
    {
        _velocity = Vector3.zero;

        //Handle four different inputs
        _velocity.z += Input.GetAxis(VERTICAL_AXIS);
        _velocity.x += Input.GetAxis(HORIZONTAL_AXIS);

        //Animation handling 
        _animatorComponent.SetBool(IDLE_ANIM, _velocity == Vector3.zero);
        _animatorComponent.SetBool(WALKING_ANIM, _velocity != Vector3.zero);

        //Walk Particle
        _particleSystemMain.enabled = _velocity == Vector3.zero ? false : true;

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

        _particleSystemMain.enabled = false;

        _animatorComponent.SetBool(WALKING_ANIM, false);
        _animatorComponent.SetBool(IDLE_ANIM, false);
        _animatorComponent.SetTrigger(ZIPLINE_GRAB_ANIM);

        _rigidComponent.isKinematic = true;

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

        transform.position = Vector3.Lerp(transform.position, _zipline.transform.position + Vector3.up * ZIPLINE_Y_OFFSET, ZIPLINE_EASE * Time.deltaTime);
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
