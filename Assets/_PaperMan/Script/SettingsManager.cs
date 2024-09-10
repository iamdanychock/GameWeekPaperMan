using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    [SerializeField] private Scrollbar _MasterScrollbar;
    [SerializeField] private Scrollbar _SFXScrollbar;
    [SerializeField] private Scrollbar _MusicScrollbar;



    [SerializeField] private Button _ScreenSizeButton;


    


    void Start()
    {
        _MasterScrollbar.value = SettingsSaveFile.masterVolumeValue;
        _SFXScrollbar.value = SettingsSaveFile.SFXVolumeValue;
        _MusicScrollbar.value = SettingsSaveFile.musicVolumeValue;


        if (Screen.fullScreen)
            _ScreenSizeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Full Screen";
        else
            _ScreenSizeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Windowed";
    }


    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;

        if (Screen.fullScreen)
            _ScreenSizeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Full Screen";
        else
            _ScreenSizeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Windowed";



    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
