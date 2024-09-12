using System.Collections;
using UnityEngine;
using Com.IsartDigital.PaperMan;
using UnityEngine.Video;
using UnityEngine.Events;

public class Television : Interactable
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private string onOffValueName = "_IsOn";
    [SerializeField] private bool isOn = true;
    [SerializeField] private bool canBeTurnOn = false;
    [SerializeField] private bool killPlayer = false;
    [SerializeField] private float turningOffDuration = .5f;
    [SerializeField] private AnimationCurve turnOffCurve;
    [SerializeField] private VideoPlayer videoPlayer;
    private bool isInAnimation = false;

    [SerializeField] private UnityEvent onTurnedOff;

    private float startPlaybackSpeed;

    protected override void Start()
    {
        base.Start();

        meshRenderer?.sharedMaterial.SetFloat(onOffValueName, isOn ? 1 : 0);
        startPlaybackSpeed = videoPlayer.playbackSpeed;
    }

    protected override void Interact()
    {
        if (isInAnimation) return;

        Debug.Log("interacted");

        // turn off or on
        if (!(!isOn && canBeTurnOn)) 
            StartCoroutine(TurnOnOff(isOn));
    }

    /// <summary>
    /// make the value turningOffDuration go from 0 to 1 or 1 to 0 weither the tv is turning on or off
    /// </summary>
    private IEnumerator TurnOnOff(bool isGoingOff)
    {
        if (meshRenderer == null) yield break;

        // get the mat and disable values
        Material mat = meshRenderer.sharedMaterial;
        isInAnimation = true;
        isOn = !isGoingOff;
        if (!isGoingOff) videoPlayer.playbackSpeed = startPlaybackSpeed;

        float elapsedTime = 0;
        float ratio;

        while (elapsedTime < turningOffDuration)
        {
            ratio = elapsedTime / turningOffDuration;
            ratio = turnOffCurve.Evaluate(ratio);
            mat.SetFloat(onOffValueName, isGoingOff ? 1 - ratio : ratio);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // action to do if the tv is off
        if (isGoingOff)
        {
            onTurnedOff?.Invoke();
            videoPlayer.playbackSpeed = 0f;
            // kill the player if they want to turn off the tv and if the tv is a killer
            if (killPlayer) Player.Instance.Kill();
        }
        // set end anim values
        mat.SetFloat(onOffValueName, isGoingOff ? 0 : 1);
        isInAnimation = false;
    }
}