using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TrasladarMenuLateral : MonoBehaviour
{
	//private int velocidadMenus = 1;
	
	public GameObject menuIzq;
	public CanvasGroup cgIzquierdo;
	
	public GameObject menuDer;
	public CanvasGroup cgDerecho;

	public GameObject botonIzqMenu1; //Apuntando para ocultar
	public GameObject botonDerMenu1; //Apuntando para desplegar
	public GameObject botonIzqMenu2; //Apuntando para desplegar
	public GameObject botonDerMenu2; //Apuntando para ocultar

	// Use this for initialization
	void Start () {
		botonDerMenu1.SetActive(false);
		
		botonIzqMenu2.SetActive(false);
	}

	/////SIRVE
	public void EsconderMenuIzquierdo()
	{
		//Debug.Log("Escondiendo menu izquierdo");
		botonIzqMenu1.SetActive(false);
		FadeOut(cgIzquierdo);
		menuIzq.SetActive(false);
		botonDerMenu1.SetActive(true);
	}
	
	/////SIRVE
	public void DesplegarMenuIzquierdo()
	{
		//Debug.Log("Desplegando menu izquierdo");
		menuIzq.SetActive(true);
		FadeIn(cgIzquierdo);
		botonDerMenu1.SetActive(false);
		botonIzqMenu1.SetActive(true);
	}

	public void EsconderMenuDerecho()
	{
		//Debug.Log("Escondiendo menu derecho");
		botonDerMenu2.SetActive(false);
		FadeOut(cgDerecho);
		menuDer.SetActive(false);
		botonIzqMenu2.SetActive(true);
	}

	/////SIRVE
	public void DesplegarMenuDerecho()
	{
		//Debug.Log("Desplegando menu derecho");
		menuDer.SetActive(true);
		FadeIn(cgDerecho);
		botonIzqMenu2.SetActive(false);
		botonDerMenu2.SetActive(true);
	}
	
	public void FadeIn(CanvasGroup cg)
	{
		StartCoroutine(FadeCanvasGroup(cg, cg.alpha, 1));
	}
	
	public void FadeOut(CanvasGroup cg)
	{
		StartCoroutine(FadeCanvasGroup(cg, cg.alpha, 0));
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
