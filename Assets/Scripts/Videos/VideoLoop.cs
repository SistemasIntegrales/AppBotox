using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoLoop : MonoBehaviour
{
	public RawImage RawImage;
	public VideoPlayer VideoPlayer;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(RunVideo());
	}
	
	// Update is called once per frame
	/*void Update ()
	{
		StartCoroutine(PlayVideo());
	}*/

	IEnumerator RunVideo()
	{
		VideoPlayer.Prepare();
		WaitForSeconds waitForSeconds = new WaitForSeconds(1);
		while (!VideoPlayer.isPrepared)
		{
			yield return waitForSeconds;
			break;
		}

		RawImage.texture = VideoPlayer.texture;
		VideoPlayer.Play();
	}
}
