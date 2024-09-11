using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObject : MonoBehaviour
{
    [SerializeField] float MIN_ALPHA = .3f;
    [SerializeField] float FADING_EASE = 8;

    const float TIME_BEFORE_FADING_IN = .5f;

    bool fadingIn = false;

    MeshRenderer meshRenderer;
    Color materialColor;

    float timeNoFadeCall = -1;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        materialColor = meshRenderer.material.color;
    }

    private void Update()
    {
        timeNoFadeCall -= Time.deltaTime;
        fadingIn = timeNoFadeCall > 0;

        materialColor.a = Mathf.Lerp(materialColor.a ,fadingIn ? MIN_ALPHA : 1, FADING_EASE * Time.deltaTime);
        meshRenderer.material.color = materialColor;
    }

    public void Fade()
    {
        timeNoFadeCall = TIME_BEFORE_FADING_IN;
    }
}
