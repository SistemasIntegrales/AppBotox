using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoModelo : MonoBehaviour
{
	public GameObject MenuIzquierdo;
	
	//Variables para tomar al modelo
	public GameObject cabeza;// brazo, pierna;
	private int objectActive = 0; //0, 1 y 2 (cabeza, brazo, pierna)

	/*public Transform PlayerTransform;
	private Vector3 _cameraOffset;
	[Range(0.01f, 1.0f)] public float SmoothFactor = 0.5f;*/

	private Touch initTouch = new Touch();
	//public Camera camera;

	private float rotX = 0f;
	private float rotY = 0f;
	private Vector3 originRot;

	[Range(0.01f, 1.0f)] public float rotSpeed = 0.4f;
	public float dir = 1; //Posibilidad del player para invertir el movimiento

	// Use this for initialization
	void Start ()
	{
		
		//originRot = camera.transform.eulerAngles; //3 angulos almacenados
		//Iniciamos con cabeza activa
		originRot = cabeza.transform.eulerAngles;
		objectActive = 0;

		//Utilizar variables para modificar la rotacion
		rotX = originRot.x;
		rotY = originRot.y;
	}

	private void Update()
	{
		if (cabeza.activeInHierarchy)
		{
			objectActive = 0;
			originRot = cabeza.transform.eulerAngles;
			rotX = originRot.x;
			rotY = originRot.y;
		}
		/*else if (brazo.activeInHierarchy)
		{
			objectActive = 1;
			originRot = brazo.transform.eulerAngles;
			rotX = originRot.x;
			rotY = originRot.y;
		}
		else if (pierna.activeInHierarchy)
		{
			objectActive = 2;
			originRot = pierna.transform.eulerAngles;
			rotX = originRot.x;
			rotY = originRot.y;
		}*/
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		/*foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				initTouch = touch; //Current touch
			}
			else if (touch.phase == TouchPhase.Moved)
			{
				//swiping
				//Posicion de la que venimos, pos delta
				//Calcular la distancia del toque y la nueva posicion
				float deltaX = initTouch.position.x - touch.position.x; //diferencia entre pos inicial y el toque actual
				float deltaY = initTouch.position.y - touch.position.y;
				//Rotacion delta x sera manipulada por Y ya al viceversa
				//Tomar los valores de arriba y ponerlos en rotX y rotY
				rotX -= deltaY * Time.deltaTime * rotSpeed * dir;
				//Como es muy rapido, se divide por el tiempo (misma vel en todos dispositivos)
				rotY += deltaX * Time.deltaTime * rotSpeed * dir;
				
				//Clamp/bloquear/limitar/restringir la rotacion del usuario (si se desea)
				//rotX = Mathf.Clamp(rotX, -45f, 45f);
				
				camera.transform.eulerAngles = new Vector3(rotX, rotY, 0f);
			}
			else if (touch.phase == TouchPhase.Ended)
			{
				//Reset touch
				initTouch = new Touch();
			}
		}*/
		
		// QUE NO SE MUEVA CUANDO EL MENU ESTE ACTIVO
		// CONFLICTO CON INFO GENERAL
		if (!MenuIzquierdo.activeSelf)
		{
			//Verificar que no se esten usando DOS DEDOS (PARA QUE NO ENTRE EN CONFLICTO CON PINCH)
			if (Input.touchCount < 2)
			{
				switch (objectActive)
				{
					//Dependiendo del elemento activo, mover ese
					case 0:
						foreach (Touch touch in Input.touches)
						{
							if (touch.phase == TouchPhase.Began)
							{
								initTouch = touch; //Current touch
							}
							else if (touch.phase == TouchPhase.Moved)
							{
								//swiping
								float deltaX = initTouch.position.x - touch.position.x; //diferencia entre pos inicial y el toque actual
								float deltaY = initTouch.position.y - touch.position.y;
								rotX += deltaY * Time.deltaTime * rotSpeed * dir;
								//Como es muy rapido, se divide por el tiempo (misma vel en todos dispositivos)
								rotY += deltaX * Time.deltaTime * rotSpeed * dir;
						
								cabeza.transform.eulerAngles = new Vector3(rotX, rotY, 0f);
							}
							else if (touch.phase == TouchPhase.Ended)
							{
								//Reset touch
								initTouch = new Touch();
							}
						}
			
						break;
					default:
						break;
				}
			}
		}
	}
}
