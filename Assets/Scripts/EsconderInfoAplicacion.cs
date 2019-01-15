using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.Experimental.UIElements.Button;

public class EsconderInfoAplicacion : MonoBehaviour
{

	public Button bajarInfo;
	public GameObject backAplicInfo;
	public GameObject tituloSolamente;

	public void Disable()
	{
		backAplicInfo.SetActive(false);
		tituloSolamente.SetActive(true);
	}
}
