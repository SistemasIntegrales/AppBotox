using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoModelo : MonoBehaviour
{
	public GameObject MenuIzquierdo, MenuDerecho;

	private Touch _touch;

	private Quaternion rotationY;

	[Range(0.01f, 1.0f)] [SerializeField] private float rotSpeed = 0.3f;
	
	void Update ()
	{	
		// QUE NO SE MUEVA CUANDO EL MENU ESTE ACTIVO
		// CONFLICTO CON INFO GENERAL
		if (!MenuIzquierdo.activeSelf || !MenuDerecho.activeSelf)
		{
			//Verificar que no se esten usando DOS DEDOS (PARA QUE NO ENTRE EN CONFLICTO CON PINCH)
			if (Input.touchCount == 1)
			{
				_touch = Input.GetTouch(0);
				if (_touch.phase == TouchPhase.Moved)
				{
					//swiping
					rotationY = Quaternion.Euler(
						0f,
						- _touch.deltaPosition.x * rotSpeed,
						0f
					);
								
					transform.rotation = rotationY * transform.rotation;
				}
			}
		}
	}
}
