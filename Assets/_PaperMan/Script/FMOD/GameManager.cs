using Com.IsartDigital.PaperMan.Sound;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] public EventReference _Music1;


    void Start()
    {
        AudioManager.instance.SetMusic(_Music1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
