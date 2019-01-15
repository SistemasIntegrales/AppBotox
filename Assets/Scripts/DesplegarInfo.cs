using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesplegarInfo : MonoBehaviour {
	
	public GameObject ayuda;
	public GameObject buttonAyuda1;
	public GameObject buttonAyuda2;

	public void Enable()
	{
		ayuda.SetActive(true);
		buttonAyuda1.SetActive(false);
		buttonAyuda2.SetActive(true);
	}

	public void Disable()
	{
		ayuda.SetActive(false);
		buttonAyuda1.SetActive(true);
		buttonAyuda2.SetActive(false);
	}

	// Use this for initialization
	void Start () {
		ayuda.SetActive(false);
		buttonAyuda2.SetActive(false);
	}
	
	// Update is called once per frame
	/*void Update () {
		
	}*/
}
