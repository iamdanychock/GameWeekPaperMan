using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndVideoManager : MonoBehaviour
{
    public static EndVideoManager instance;

    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private float videoDuration = 48f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        videoPlayer.gameObject.SetActive(false);
    }

    public void StartVideo()
    {
        StartCoroutine(PlayVideo());
    }

    private IEnumerator PlayVideo()
    {
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
        yield return new WaitForSeconds(videoDuration);
        Application.Quit();
    }
}
