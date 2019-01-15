using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMenuSecu : MonoBehaviour
{
	//public GameObject subMenu;
	private bool toggle = true;

	public RawImage background;

	public GameObject Menu;
	public CanvasGroup CanvasGroup;

	[SerializeField] private Color backActivo;
	[SerializeField] private Color backInctivo; //Hexa: 919191
	

	// Use this for initialization
	public void EnableDisable()
	{
		if (toggle)
		{
			Menu.SetActive(true);
			toggle = false;
			background.color = backInctivo;
			FadeIn();
		}
		else
		{
			FadeOut();
			toggle = true;
			background.color = backActivo;
			Menu.SetActive(false);
		}
	}

	// Use this for initialization
	void Start () {
		Menu.SetActive(false);
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
