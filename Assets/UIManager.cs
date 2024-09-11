using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    const string PAUSE_INPUT = "Pause";
    [NonSerialized] public bool _HasGameStarted = true; //true temporaire pour test

    [SerializeField] private SettingsManager _settingsManager;
    [SerializeField] private PauseManager _pauseManager;



    void Start()
    {
        
    }



    private void OnPlayerPause()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(PAUSE_INPUT) && _HasGameStarted)
        {
            _pauseManager.Visibility();
        }

    }
}
