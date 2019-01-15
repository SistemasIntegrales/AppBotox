using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoBotox : MonoBehaviour
{
    public RawImage RawImage;
    public VideoPlayer VideoPlayer;

    public GameObject primerPlay, play, pause, rewind, forward;
    
    //public Slider Slider;

    // Use this for initialization
    void Start ()
    {
        //StartCoroutine(PlayVideo());
        play.SetActive(false);
        pause.SetActive(false);
        rewind.SetActive(false);
        forward.SetActive(false);
    }

    /*void Update()
    {
        if (!VideoPlayer.isPrepared)
        {
            return;
        }
        Slider.value = (float) NTime;
    }*/

    public void PlayInicial()
    {
        StartCoroutine(PlayVideo());
    }
    
    IEnumerator PlayVideo()
    {
        primerPlay.SetActive(false);
        
        VideoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!VideoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }

        RawImage.texture = VideoPlayer.texture;
        
        //play.SetActive(false);
        pause.SetActive(true);
        rewind.SetActive(true);
        forward.SetActive(true);
        
        VideoPlayer.Play();
    }
    
    public double Time
    {
        get { return VideoPlayer.time; }
    }
    public ulong Duration
    {
        // frames / frames per second --> seconds
        get { return (ulong) (VideoPlayer.frameCount / VideoPlayer.frameRate); }
    }
    public double NTime
    {
        //Tiempo del video de 0 a 1
        get { return Time / Duration; }
    }
	
    public void RunVideo()
    {
        if (!VideoPlayer.isPrepared) return;
        VideoPlayer.Play();
        Debug.Log("Video playing");
        pause.SetActive(true);
        play.SetActive(false);
    }

    public void PauseVideo()
    {
        if (!VideoPlayer.isPlaying) return;
        VideoPlayer.Pause();
        pause.SetActive(false);
        play.SetActive(true);
    }

    public void RewindVideo()
    {
        VideoPlayer.time = VideoPlayer.time - 10f;
        //videoPlayer.time = videoPlayer.time - deltaTime*Speed;
    }
    public void ForwardVideo()
    {
        VideoPlayer.time = VideoPlayer.time + 10f;
    }
}
