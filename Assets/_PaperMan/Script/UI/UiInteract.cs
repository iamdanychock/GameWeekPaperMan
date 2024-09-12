using Com.IsartDigital.PaperMan;
using UnityEngine;

public class UiInteract : MonoBehaviour
{
    [SerializeField] private string setControllerTriggerName = "SetController";
    [SerializeField] private string setKeyboardTriggerName = "SetKeyboard";
    [SerializeField] private string noneTriggerName = "none";
    private Canvas canvas;
    private Animator animator;

    private bool isInKeyboard = true;
    private bool isActivated = false;

    private void Start()
    {
        // set the camera to the canvas
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = UnityEngine.Camera.main;

        animator = GetComponent<Animator>();
        Disable();

        // connect to know if the player is using a keyboard or a controller
        isInKeyboard = ControllerListener.Instance.currentPlayerController != ControllerListener.ControllerType.CONTROLLER;
        ControllerListener.Instance.onControllerActivation += OnControllerActivated;
        ControllerListener.Instance.onKeyboardActivation += OnKeyboardActivated;
    }

    private void OnKeyboardActivated()
    {
        isInKeyboard = true;
        if (isActivated) SetKeyboard();
    }

    private void OnControllerActivated()
    {
        isInKeyboard = false;
        if (isActivated) SetController();
    }

    public void Activate()
    {
        if (isInKeyboard) SetKeyboard();
        else SetController();
    }

    private void SetController()
    {
        isActivated = true;
        animator.SetTrigger(setControllerTriggerName);
    }

    private void SetKeyboard()
    {
        isActivated = true;
        animator.SetTrigger(setKeyboardTriggerName);
    }

    public void Disable()
    {
        isActivated = false;
        animator.SetTrigger(noneTriggerName);
    }
}