using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlaybackLogic : MonoBehaviour
{
    public string idleClipUrl;
    public string[] videoClipUrls;

    private VideoPlayer videoPlayer;
    private bool playingIdle;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayIdle();
    }
    private void PlayIdle()
    {
        playingIdle = true;
        videoPlayer.playbackSpeed = 0.1f;
        videoPlayer.isLooping = true;
        videoPlayer.url = idleClipUrl;
        videoPlayer.SetDirectAudioMute(0, true);
        videoPlayer.loopPointReached -= VideoPlayer_loopPointReached;
        videoPlayer.Play();
    }

    public void PlayRandom()
    {
        if (playingIdle)
        {
            int videoIndex = Mathf.CeilToInt(Random.value * videoClipUrls.Length - 1);
            playingIdle = false;
            videoPlayer.playbackSpeed = 1.0f;
            videoPlayer.isLooping = false;
            videoPlayer.url = videoClipUrls[videoIndex];
            videoPlayer.SetDirectAudioMute(0, false);
            videoPlayer.loopPointReached += VideoPlayer_loopPointReached;
            videoPlayer.Play();
        }
    }

    private void VideoPlayer_loopPointReached(VideoPlayer source)
    {
        PlayIdle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
