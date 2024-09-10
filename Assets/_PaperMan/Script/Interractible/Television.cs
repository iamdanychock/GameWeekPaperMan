using System.Collections;
using UnityEngine;
using Com.IsartDigital.PaperMan;
using UnityEngine.Video;

public class Television : Interactable
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private string onOffValueName = "_IsOn";
    [SerializeField] private bool isOn = true;
    [SerializeField] private float turningOffDuration = .8f;
    [SerializeField] private AnimationCurve turnOffCurve;
    [SerializeField] private VideoPlayer videoPlayer;
    private bool isInAnimation = false;

    private float startPlaybackSpeed;

    private void Start()
    {
        meshRenderer.sharedMaterial.SetFloat(onOffValueName, isOn ? 1 : 0);
        startPlaybackSpeed = videoPlayer.playbackSpeed;
    }

    protected override void Interact()
    {
        if (isInAnimation) return;

        // turn off or on
        StartCoroutine(TurnOnOff(isOn));
    }

    /// <summary>
    /// make the value turningOffDuration go from 0 to 1 or 1 to 0 weither the tv is turning on or off
    /// </summary>
    private IEnumerator TurnOnOff(bool isGoingOff)
    {
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

        if (isGoingOff) videoPlayer.playbackSpeed = 0f;
        mat.SetFloat(onOffValueName, isGoingOff ? 0 : 1);
        isInAnimation = false;
    }
}