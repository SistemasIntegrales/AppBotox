using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class CambiarDistonia : MonoBehaviour
{
	public Animator animator;
	//float smooth = 5.0f;
	private int posActual = 0;
	
	[SerializeField] private GameObject _puntosRefAnatomica;

	[SerializeField] private GameObject[] botonesMusculosDistonias;

	private bool toggle = true;
	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator>();
		animator.speed = 0f;
		botonesMusculosDistonias[0].SetActive(true);

		//Todos los puntos de referencia
		_puntosRefAnatomica.SetActive(false);
	}

	IEnumerator WaitAndMove(int posActual, int posNueva)
	{
		int aguante = posNueva - posActual;
		if (aguante < 0)
		{
			animator.SetFloat("SpeedMov",-1);
			animator.speed = 1f;
		}
		else
		{
			animator.SetFloat("SpeedMov",1);
			animator.speed = 1f;
		}
		yield return new WaitForSeconds(Math.Abs(aguante));
		animator.speed = 0f;
	}

	public void Dropdown_IndexChanged(int index)
	{
		foreach (GameObject botonMusc in botonesMusculosDistonias)
		{
			botonMusc.SetActive(false);
		}

		toggle = true;
		
		_puntosRefAnatomica.SetActive(false);
		
		/*Aqui cambiar la posicion del modelo*/
		
		switch (index)
		{
			case 0:
				StartCoroutine(WaitAndMove(posActual, 0));
				botonesMusculosDistonias[0].SetActive(true); //Laterocollis
				posActual = 0;
				break;
			case 1:
				StartCoroutine(WaitAndMove(posActual, 1));
				botonesMusculosDistonias[1].SetActive(true); //Torticollis
				posActual = 1;
				break;
			case 2:
				StartCoroutine(WaitAndMove(posActual, 2));
				botonesMusculosDistonias[2].SetActive(true); //Antecollis
				posActual = 2;
				break;
			case 3:
				StartCoroutine(WaitAndMove(posActual, 3));
				botonesMusculosDistonias[3].SetActive(true); //Retrocollis
				posActual = 3;
				break;
			case 4:
				StartCoroutine(WaitAndMove(posActual, 4));
				botonesMusculosDistonias[4].SetActive(true); //Lateral Shift
				posActual = 4;
				break;
			case 5:
				StartCoroutine(WaitAndMove(posActual, 5));
				botonesMusculosDistonias[5].SetActive(true); //Laterocaput
				posActual = 5;
				break;
			case 6:
				StartCoroutine(WaitAndMove(posActual, 6));
				botonesMusculosDistonias[6].SetActive(true); //Torticaput
				posActual = 6;
				break;
			case 7:
				StartCoroutine(WaitAndMove(posActual, 7));
				botonesMusculosDistonias[7].SetActive(true); //Antecaput
				posActual = 7;
				break;
			case 8:
				StartCoroutine(WaitAndMove(posActual, 8));
				botonesMusculosDistonias[8].SetActive(true); //Retrocaput
				posActual = 8;
				break;
			case 9:
				StartCoroutine(WaitAndMove(posActual, 9));
				botonesMusculosDistonias[9].SetActive(true); //Sagittal Shift
				posActual = 9;
				break;
			default:
				break;
		}
	}

	public void EncenderReferencias()
	{
		if (toggle)
		{
			_puntosRefAnatomica.SetActive(true);
			toggle = false;
		}
		else
		{
			_puntosRefAnatomica.SetActive(false);
			toggle = true;
		}
	}
}
