using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAplicDolor : MonoBehaviour
{
	public GameObject Aplicaciones, Dolor;
	public Text TipoPunto;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(EsperarCarga());
	}

	IEnumerator EsperarCarga()
	{
		yield return new WaitForSeconds(1);
		Aplicaciones.SetActive(false);
		Dolor.SetActive(false);
	}
	
	// Update is called once per frame
	public void MostrarAplic()
	{
		Aplicaciones.SetActive(true);
		Dolor.SetActive(false);
		TipoPunto.text = "Punto de aplicación";
	}
	public void MostrarDolor()
	{
		Aplicaciones.SetActive(false);
		Dolor.SetActive(true);
		TipoPunto.text = "Punto de seguimiento de dolor";
	}
}
