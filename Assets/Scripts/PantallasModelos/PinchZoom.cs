using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinchZoom : MonoBehaviour
{
	//Que no choque con el menu lateral izquierdo y que este se muestre cuando se toque un punto
	public GameObject MenuIzquierdo;
	
	public float zoomOutMin = 2f;
	public float zoomOutMax = 4.5f;

	private void Update()
	{
		if (Input.touchCount == 2)
		{
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

			float difference = currentMagnitude - prevMagnitude;
			
			Zoom(difference * 0.01f);
		}

		/*if (!MenuIzquierdo.activeSelf)
		{
			Zoom(Input.GetAxis("Mouse ScrollWheel"));
		}*/
		Zoom(Input.GetAxis("Mouse ScrollWheel"));
	}

	void Zoom(float increment)
	{
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
	}
}
