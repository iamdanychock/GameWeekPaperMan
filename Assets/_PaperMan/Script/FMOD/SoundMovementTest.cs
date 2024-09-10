using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMovementTest : MonoBehaviour
{
    [SerializeField] private EventReference soundToPlay;
    EventInstance soundEvent; 

    public float moveSpeed = 5f;

    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveZ = 1f; 
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveZ = -1f; 
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f; 
        }

        if (Input.GetKey(KeyCode.Space))
        {
            moveY = 1f; 
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveY = -1f;
        }

        RuntimeManager.AttachInstanceToGameObject(soundEvent, transform);

        Vector3 move = new Vector3(moveX, moveY, moveZ) * moveSpeed * Time.deltaTime;
        transform.Translate(move);
    }



}
