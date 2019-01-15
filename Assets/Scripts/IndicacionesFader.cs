using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicacionesFader : MonoBehaviour
{
	public CanvasGroup CanvasGroup;
	
	public GameObject FlechaClosed;
	public GameObject FlechaDisplay;

	private bool _toggle = true;

	// Use this for initialization
	void Start () {
		FlechaDisplay.SetActive(false);
	}
	
	public void Display()
	{
		if (_toggle)
		{
			FadeIn();
			FlechaClosed.SetActive(false);
			FlechaDisplay.SetActive(true);
			_toggle = false;
		}
		else
		{
			FadeOut();
			FlechaClosed.SetActive(true);
			FlechaDisplay.SetActive(false);
			_toggle = true;
		}
	}
	
	public void FadeIn()
	{
		StartCoroutine(FadeCanvasGroup(CanvasGroup, CanvasGroup.alpha, 1));
	}
	
	public void FadeOut()
	{
		StartCoroutine(FadeCanvasGroup(CanvasGroup, CanvasGroup.alpha, 0));
	}

	IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 0.5f)
	{
		float timeStartedLerping = Time.time;
		float timeSinceStarted = Time.time - timeStartedLerping;
		float percentageComplete = timeSinceStarted / lerpTime;
		
		while (true)
		{
			timeSinceStarted = Time.time - timeStartedLerping;
			percentageComplete = timeSinceStarted / lerpTime;

			float currentValue = Mathf.Lerp(start, end, percentageComplete);

			cg.alpha = currentValue;

			if (percentageComplete >= 1) break;
			
			yield return new WaitForEndOfFrame();
		}
	}
}
