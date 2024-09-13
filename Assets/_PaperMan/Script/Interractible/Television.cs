using System.Collections;
using UnityEngine;
using Com.IsartDigital.PaperMan;
using UnityEngine.Video;
using UnityEngine.Events;
using UnityEngine.Purchasing.MiniJSON;
using FMODUnity;
using FMOD.Studio;
using FMOD;

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
    [SerializeField] private EventReference _VideoSoundToPlayReference;

    private EventInstance SoundPlayingOnTVInstance;

    private bool isInAnimation = false;
    private bool startOnOffState;

    [SerializeField] private UnityEvent onTurnedOff;

    private float startPlaybackSpeed;

    protected override void Start()
    {
        base.Start();

        meshRenderer?.sharedMaterial.SetFloat(onOffValueName, isOn ? 1 : 0);


        startPlaybackSpeed = videoPlayer.playbackSpeed;
        startOnOffState = isOn;
        PlayTVSound();
        // connect to the death event of the player
        Player.Instance.onRespawn += OnPlayerRespawn;
    }

    /// <summary>
    /// return on the tv if it was turn off by the player but that the tv killed them
    /// and don't player the tv anim
    /// </summary>
    private void OnPlayerRespawn()
    {
        videoPlayer.playbackSpeed = startPlaybackSpeed;
        isOn = startOnOffState;
        isInAnimation = false;
        meshRenderer.sharedMaterial.SetFloat(onOffValueName, isOn ? 1 : 0);
        if (isOn)
        {
            InterractionActive = true;
            PlayTVSound();

        }
    }

    protected override void Interact()
    {
        if (isInAnimation) return;

        // turn off or on
        if (!(!isOn && canBeTurnOn))
        {
            RuntimeManager.PlayOneShot("event:/SFX/Interactions/switchoff", transform.position);
            StartCoroutine(TurnOnOff(isOn));

        }
    }

    /// <summary>
    /// make the value turningOffDuration go from 0 to 1 or 1 to 0 weither the tv is turning on or off
    /// </summary>
    private IEnumerator TurnOnOff(bool isGoingOff)
    {
        if (meshRenderer == null) yield break;

        InterractionActive = !isGoingOff;

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
        TurnTVOffSound();
        StopTVSound();
    }

    private void PlayTVSound()
    {
        SoundPlayingOnTVInstance = RuntimeManager.CreateInstance(_VideoSoundToPlayReference);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(SoundPlayingOnTVInstance, transform);
        SoundPlayingOnTVInstance.start();
    }



    private void StopTVSound()
    {
        SoundPlayingOnTVInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    private void TurnTVOffSound()
    {
        RuntimeManager.PlayOneShotAttached("event:/Amb/tvoff", gameObject);
    }
}