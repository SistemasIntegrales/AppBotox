using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer Video;
    public Slider Slider;
    
    public RawImage RawImage;
    
    //Propiedades
    private bool finalAlcanzado;

    public bool isPlaying
    {
        get { return Video.isPlaying; }
    }

    public bool isPrepared
    {
        get { return Video.isPrepared; }
    }

    public bool isDone
    {
        get { return finalAlcanzado; }
    }

    public double Time
    {
        get { return Video.time; }
    }

    public ulong Duration
    {
        // frames / frames por segundo = segundos
        get { return (ulong) (Video.frameCount / Video.frameRate); }
    }

    public double NTime
    {
        //Tiempo del video de 0 a 1
        get { return Time / Duration; }
    }

    private void OnEnable()
    {
        //
        Video.errorReceived += errorRecieved;
        Video.frameReady += frameReady;
        Video.loopPointReached += loopPointReached;
        Video.prepareCompleted += prepareCompleted;
        Video.seekCompleted += seekCompleted;
        Video.started += started;
    }

    private void OnDisable()
    {
        Video.errorReceived -= errorRecieved;
        Video.frameReady -= frameReady;
        Video.loopPointReached -= loopPointReached;
        Video.prepareCompleted -= prepareCompleted;
        Video.seekCompleted -= seekCompleted;
        Video.started -= started;
    }

    void errorRecieved(VideoPlayer videoPlayer, string msg)
    {
        Debug.Log("Error en video: "+msg);
    }

    void frameReady(VideoPlayer videoPlayer, long frame)
    {
        //CPU tax is heavy
    }

    void loopPointReached(VideoPlayer videoPlayer)
    {
        Debug.Log("Video ha finalizado");
        finalAlcanzado = true;
    }

    void prepareCompleted(VideoPlayer videoPlayer)
    {
        Debug.Log("Video preparado");
        finalAlcanzado = false;
    }

    void seekCompleted(VideoPlayer videoPlayer)
    {
        Debug.Log("Video seeked");
        finalAlcanzado = false;
    }

    void started(VideoPlayer videoPlayer)
    {
        Debug.Log("Video corriendo");
    }

    private void Start()
    {
        Video.Prepare();
        StartCoroutine(SetVideo());
    }
    IEnumerator SetVideo()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!Video.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }

        RawImage.texture = Video.texture;
    }

    void Update()
    {
        if (!isPrepared)
        {
            return;
        }
        Slider.value = (float) NTime;
    }
    
    //Cargar, Correr, Pausar
    public void LoadVideo(string nombre)
    {
        //Application.dataPath --> manda al folder de Assets
        string temp = Application.dataPath + "/Video/" + nombre; //mp4, mov, avi
        if (Video.url == temp) return;
        Video.url = temp;
        Video.Prepare();
        
    }

    public void PlayVideo()
    {
        Debug.Log("Boton play presionado");
        if (!isPrepared)
        {
            Debug.Log("Video no preparado");
            return;
        }
        //RawImage.texture = Video.texture;
        Video.Play();
        Debug.Log("Video corrriendo");
    }

    public void PauseVideo()
    {
        Debug.Log("Boton pausa presionado");
        if (!isPlaying) return;
        Video.Pause();
        Debug.Log("Video pausado");
    }

    public void RestartVideo()
    {
        if (!isPrepared) return;
        PauseVideo();
        Seek(0);
    }

    public void Seek(float nTime)
    {
        if (!Video.canSetTime) return;
        //nTime 0 to 1
        if (!isPrepared) return;
        nTime = Mathf.Clamp(nTime, 0, 1);
        
        //Mandar el video a esa posicion
        Video.time = nTime * Duration;
    }

    public void IncrementPlaybackSpeed()
    {
        if (!Video.canSetPlaybackSpeed) return;
        //0 to 10
        Video.playbackSpeed += 1;
        Video.playbackSpeed = Mathf.Clamp(Video.playbackSpeed, 0, 10);
    }

    public void DecrementPlaybackSpeed()
    {
        if (!Video.canSetPlaybackSpeed) return;
        Video.playbackSpeed -= 1;
        Video.playbackSpeed = Mathf.Clamp(Video.playbackSpeed, 0, 10);
    }
}
