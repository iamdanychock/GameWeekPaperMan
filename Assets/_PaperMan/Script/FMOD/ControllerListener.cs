using System;
using UnityEngine;

namespace Com.IsartDigital.PaperMan
{
    public class ControllerListener : MonoBehaviour
    {
        public static ControllerListener Instance { get; private set; }

        public enum ControllerType
        {
            CONTROLLER, KEYBOARD_MOUSE
        }

        // events values
        [HideInInspector]
        public ControllerType currentPlayerController = ControllerType.KEYBOARD_MOUSE;
        public event Action onControllerActivation;
        public event Action onKeyboardActivation;
        public static bool isUsingAController = false;

        // inputs values
        public static string[] controllerAxisNames = new string[] { "LT", "RT", "LS_h", "LS_v", "RS_h", "RS_v", "DPAD_h", "DPAD_v" };
        private int startBtnKeyCodeInt = 330;
        private Vector3 lastMousePos;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            lastMousePos = Input.mousePosition;
        }

        private void Update()
        {
            bool isAContollerBtnPressed = false;
            // get A, B, X, Y, LB, RB, Back, Start, left btn joystick, right btn joystick
            for (int i = 0; i < 10; i++)
            {
                KeyCode keyCode = (KeyCode)(startBtnKeyCodeInt + i);
                if (Input.GetKey(keyCode))
                {
                    isAContollerBtnPressed = true;
                    break;
                }
            }
            // get the other controller's btns (mainly axis)
            if (!isAContollerBtnPressed)
            {
                foreach (string axisName in controllerAxisNames)
                {
                    if (Mathf.RoundToInt(Input.GetAxis(axisName)) != 0)
                    {
                        isAContollerBtnPressed = true;
                        break;
                    }
                }
            }

            // check if a keyboard or mouse input is done
            if ((Input.anyKey || lastMousePos != Input.mousePosition) && !isAContollerBtnPressed)
            {
                // the player changed to a keybaord
                if (currentPlayerController != ControllerType.KEYBOARD_MOUSE)
                {
                    currentPlayerController = ControllerType.KEYBOARD_MOUSE;
                    isUsingAController = false;
                    onKeyboardActivation?.Invoke();
                }
            }
            // check if on controller
            else if (isAContollerBtnPressed)
            {
                // the player changed to a controller
                if (currentPlayerController != ControllerType.CONTROLLER)
                {
                    currentPlayerController = ControllerType.CONTROLLER;
                    isUsingAController = true;
                    onControllerActivation?.Invoke();
                }
            }

            lastMousePos = Input.mousePosition;
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
    }
}