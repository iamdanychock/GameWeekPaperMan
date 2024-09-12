using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    [SerializeField] private Slider _MasterSlider;
    [SerializeField] private Slider _SFXSlider;
    [SerializeField] private Slider _MusicSlider;



    [SerializeField] private Button _ScreenSizeButton;

    private GameObject _PanelSettings => transform.GetChild(0).gameObject;





    void Start()
    {
        _MasterSlider.value = SettingsSaveFile.masterVolumeValue;
        _SFXSlider.value = SettingsSaveFile.SFXVolumeValue;
        _MusicSlider.value = SettingsSaveFile.musicVolumeValue;


        

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



    public void Visibility()
    {
        _PanelSettings.SetActive(!_PanelSettings.activeInHierarchy);

    }

    // Update is called once per frame
    void Update()
    {




    }
}
