using Com.IsartDigital.PaperMan.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public void Visibility()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);

        if (gameObject.activeInHierarchy)
        {
            PauseGame();
            AudioManager.instance.UpdateAmbianceGlobal("pause_state", 1);
        }
        else
        {
            ResumeGame();
            AudioManager.instance.UpdateAmbianceGlobal("pause_state", 0);

        }

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
