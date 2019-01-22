using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoBotox : MonoBehaviour
{
    public RawImage PantallaChica, PantallaGrande;
    public VideoPlayer VideoPlayer;

    public GameObject primerPlay, play1, play2, pause1, pause2, rewind1, forward1, zoom1, PantallaGrandeGO, groupButGrandes;

    private bool zoomActivo = false;

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

        Texture texture = VideoPlayer.texture;
        PantallaChica.texture = texture;
        PantallaGrande.texture = texture;
        
        //play.SetActive(false);
        pause1.SetActive(true);
        rewind1.SetActive(true);
        forward1.SetActive(true);
        zoom1.SetActive(true);
        
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
        
        pause1.SetActive(true);
        pause2.SetActive(true);
        play1.SetActive(false);
        play2.SetActive(false);
    }

    public void PauseVideo()
    {
        if (!VideoPlayer.isPlaying) return;
        VideoPlayer.Pause();
        pause1.SetActive(false);
        pause2.SetActive(false);
        play1.SetActive(true);
        play2.SetActive(true);
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

    public void ZoomVideo()
    {
        if (zoomActivo)
        {
            zoomActivo = false;
            PantallaGrandeGO.SetActive(false);
            groupButGrandes.SetActive(false);
        }
        else
        {
            zoomActivo = true;
            PantallaGrandeGO.SetActive(true);
            groupButGrandes.SetActive(true);
        }
    }
}
