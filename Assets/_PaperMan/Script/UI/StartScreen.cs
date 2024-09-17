using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Animator _StartGameAnimator;
    private Animator _FadeAnimator =>GetComponent<Animator>();

    public bool _HasGameStarted;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !_HasGameStarted)
        {
            _HasGameStarted = true;
            _FadeAnimator.SetTrigger("StartGame");
        }
            
    }


    public void Liberate()
    {
        _StartGameAnimator.SetTrigger("HasGameStarted");
    }
}
