using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeJustTitle : MonoBehaviour
{

	/**Texto se va a ocultar cuando se depliegue la info
	 * BackInfoDisplay se va a mostrar
	 */
	public GameObject tituloSolamente;
	public Text tituloSolamenteX2;
	public GameObject backAplicInfo;
	
	/**
	 * Partes del cuerpo, estos NO son puntos de aplicacion, es solo para mostrar su nombre en tituloSolamenteX2
	 * Estaran en otra LayerMak
	 */
	private LayerMask bodyPartsMask = 10;
	
	void Start ()
	{
		backAplicInfo.SetActive(false);
		tituloSolamente.SetActive(true);
	}

	private void Update()
	{
#if UNITY_EDITOR
		
		if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) //Para que no se ejecute el for de manera innecesaria
		{
			Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition); //Que esta tocando el toque
			//Debug.Log("Esta es la camara adjuntada: " + GetComponent<Camera>());
			RaycastHit hit;
	
			if (Physics.Raycast(ray, out hit, bodyPartsMask))
			{
				GameObject puntoTocado = hit.transform.gameObject;

				tituloSolamenteX2.text = puntoTocado.name;
				
				//Ocultar titulo y mostar display
				backAplicInfo.SetActive(false);
				tituloSolamente.SetActive(true);
				
				Debug.Log("Este es el nombre del objeto: " + tituloSolamenteX2.text);
				
				Debug.Log("_");
				
				Debug.Log("Esta es la mascara bodyPartsMask");
			}
		}
#endif
		if (Input.touchCount > 0) //Para que no se ejecute el for de manera innecesaria
		{
			foreach (Touch touch in Input.touches)
			{
				Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition); //Que esta tocando el toque
				RaycastHit hit;
	
				if (Physics.Raycast(ray, out hit, bodyPartsMask))
				{
					//Si estamos tocando un objecto, obtener referencia de ese objeto
					GameObject puntoTocado = hit.transform.gameObject;
					tituloSolamenteX2.text = puntoTocado.name;
				
					//Ocultar titulo y mostar display
					tituloSolamente.SetActive(false);
					backAplicInfo.SetActive(true);
					
					Debug.Log("Esta es la mascara bodyPartsMask");
				}
			}
		}
	}
}
