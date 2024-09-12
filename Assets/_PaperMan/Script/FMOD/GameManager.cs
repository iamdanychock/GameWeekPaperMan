using Com.IsartDigital.PaperMan.Sound;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Transform playerStartPos;
    [SerializeField] private float spawnYOffset = .5f;
    private Checkpoint lastCheckpoint;


    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else Instance = this;
    }

    void Start()
    {

    }


    public void SetLastCheckpoint(Checkpoint _checkpoint)
    {
        lastCheckpoint = _checkpoint;
    }

    public Vector3 GetPlayerPos()
    {
        return (lastCheckpoint ? lastCheckpoint.transform.position : playerStartPos.position) + new Vector3(0, spawnYOffset, 0);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
