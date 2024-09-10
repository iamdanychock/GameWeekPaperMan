using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    public Sprite keyboardSprite;
    public Sprite gamepadSprite;
    public SpriteRenderer spriteRenderer;

    void Update()
    {
        string[] joystickNames = Input.GetJoystickNames();
        if (joystickNames.Length > 0)
        {
            spriteRenderer.sprite = gamepadSprite;
        }
        else
        {
            spriteRenderer.sprite = keyboardSprite;
        }
    }
}

