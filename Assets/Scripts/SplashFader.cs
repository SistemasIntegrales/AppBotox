using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashFader : MonoBehaviour
{
	public CanvasGroup CanvasGroup;

	public void FadeIn()
	{
		StartCoroutine(FadeCanvasGroup(CanvasGroup, CanvasGroup.alpha, 1));
	}

	IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 4.5f)
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

	// Use this for initialization
	void Start ()
	{
		FadeIn();
	}
}
