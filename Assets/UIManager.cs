using Com.IsartDigital.PaperMan.Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }



    const string PAUSE_INPUT = "Pause";
    [NonSerialized] public bool _HasGameStarted = true; //true temporaire pour test

    [SerializeField] private SettingsManager _settingsManager;
    [SerializeField] private PauseManager _pauseManager;
    [SerializeField] private Animator _DeathAnimator;

    


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than on Pause Menu in the scene.");
        }
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(PAUSE_INPUT) && _HasGameStarted)
        {
            _pauseManager.Visibility();
        }

    }



    public void OnPlayerDying()
    {
        _DeathAnimator.SetTrigger("IsDying");
    }

    public void OnPlayerSpawning()
    {
        _DeathAnimator.SetTrigger("IsSpawning");

    }
}
