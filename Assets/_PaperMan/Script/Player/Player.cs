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
    [SerializeField] float ZIPLINE_Y_OFFSET = -2;

    [SerializeField] float SPRITE_TURN_SPEED = 16;
    [SerializeField] AnimationCurve SPRITE_TURN_CURVE;

    [Header("Death")]
    [SerializeField] private float deathDuration = 1f;

    const string INTERRACTION_INPUT = "Interact";
    const string HORIZONTAL_AXIS = "Horizontal";
    const string VERTICAL_AXIS = "Vertical";

    const string WALKING_ANIM = "Walking";
    const string ZIPLINE_GRAB_ANIM = "ZiplineGrab";
    const string FALL_ANIM = "Fall";
    const string TOUCH_ANIM = "Touch";
    const string IDLE_ANIM = "Idle";
    private const string DEATH_TRIGGER_ANIM = "kill";

    const float ON_GROUND_DISTANCE = 2;

    bool _spriteLookingLeft = false;
    Vector2 _spriteStartSize;
    float _spriteTurnLerp = 0;

    public bool isTouching = false;
    bool isFalling = false;

    Zipline _zipline = null;

    public Action onRespawn;

    public Rigidbody RigidComponent => GetComponent<Rigidbody>();
    SpriteRenderer _spriteComponent => GetComponent<SpriteRenderer>();
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
        _spriteStartSize = _spriteComponent.size;

        SetModNormal();
    }

    void Update()
    {
        _state();

        // DEBUG
        #if UNITY_EDITOR
        if (Input.GetButtonDown("DebugKill"))
        {
            Kill();
        }
        #endif
    }

    public void SetModNormal()
    {
        RigidComponent.isKinematic = false;
        _state = DoActionNormal;
    }

    void DoActionNormal()
    {
        NormalMovements();

        //Dont care about the zipline if the player isnt pressing interract key
        if (!Input.GetButtonDown(INTERRACTION_INPUT))
            return;
    }

    void NormalMovements()
    {
        Vector3 lastVel = _velocity;
        _velocity = Vector3.zero;

        //Handle four different inputs
        _velocity.z += Input.GetAxis(VERTICAL_AXIS);
        _velocity.x += Input.GetAxis(HORIZONTAL_AXIS);

        //Animation handling 
        if (!Physics.Raycast(transform.position, Vector3.down, ON_GROUND_DISTANCE) && !isFalling)
        {
            isFalling = true;
            _animatorComponent.SetTrigger(FALL_ANIM);
        }
        else if(Physics.Raycast(transform.position, Vector3.down, ON_GROUND_DISTANCE))
        {
            if (isTouching && ((lastVel != Vector3.zero && _velocity == Vector3.zero) || isFalling))
                _animatorComponent.SetTrigger(TOUCH_ANIM);
            else if ((lastVel == Vector3.zero && _velocity != Vector3.zero) || (_velocity != Vector3.zero && isFalling))
                _animatorComponent.SetTrigger(WALKING_ANIM);
            else if (!isTouching && ((lastVel != Vector3.zero && _velocity == Vector3.zero) || isFalling))
                _animatorComponent.SetTrigger(IDLE_ANIM);

            isFalling = false;
        }

        //Flip sprite direction
        if ((_velocity.x > 0 && _spriteLookingLeft) || (_velocity.x < 0 && !_spriteLookingLeft))
            _spriteLookingLeft = !_spriteLookingLeft;

        _spriteComponent.flipX = _spriteComponent.size.x > 0;

        if (MathF.Abs(_spriteComponent.size.x) != _spriteStartSize.x || Mathf.Sign(_spriteComponent.size.x) != (_spriteLookingLeft ? 1 : -1))
            _spriteComponent.size += Vector2.right * (_spriteLookingLeft ? 1 : -1) * SPRITE_TURN_CURVE.Evaluate(Mathf.InverseLerp(-_spriteStartSize.x, _spriteStartSize.x, _spriteComponent.size.x)) * SPRITE_TURN_SPEED * Time.deltaTime * _spriteStartSize;

        if (MathF.Abs(_spriteComponent.size.x) > _spriteStartSize.x)
            _spriteComponent.size = Vector2.one * (_spriteLookingLeft ? 1 : -1) * _spriteStartSize;    

        //Walk Particle
        _particleSystemMain.enabled = _velocity == Vector3.zero ? false : true;

        //Apply inputs to velocity
        RigidComponent.velocity += _velocity;

        //Clamp to max speed
        RigidComponent.velocity = new Vector3(Mathf.Clamp(RigidComponent.velocity.x, -MAX_SPEED, MAX_SPEED), RigidComponent.velocity.y, Mathf.Clamp(RigidComponent.velocity.z, -MAX_SPEED, MAX_SPEED));
    }

    public void SetModZipline(Zipline ziplineToFollow)
    {
        _state = DoActionZipline;
        _zipline = ziplineToFollow;

        _particleSystemMain.enabled = false;

        _animatorComponent.SetTrigger(ZIPLINE_GRAB_ANIM);

        RigidComponent.isKinematic = true;
    }

    void DoActionZipline()
    {
        if(_zipline == null)
        {
            SetModNormal();
            return;
        }

        transform.position = Vector3.Lerp(transform.position, _zipline.transform.position + Vector3.up * ZIPLINE_Y_OFFSET, ZIPLINE_EASE * Time.deltaTime);
    }

    /// <summary>
    /// Kill the player
    /// </summary>
    public void Kill()
    {
        // init death values and deactivate the rigidbody
        RigidComponent.isKinematic = true;
        deathElapsedTime = 0;

        _state = DoActionDeath;
    }

    private float deathElapsedTime = 0;
    private void DoActionDeath()
    {
        deathElapsedTime += Time.deltaTime;

        // check if respawn the player
        if (deathElapsedTime > deathDuration)
        {
            // set the player pos when is alive again
            transform.position = GameManager.Instance.GetPlayerPos();
            RigidComponent.isKinematic = false;
            _animatorComponent.SetTrigger(DEATH_TRIGGER_ANIM);

            // reset the player as normal
            onRespawn?.Invoke();
            SetModNormal();
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
