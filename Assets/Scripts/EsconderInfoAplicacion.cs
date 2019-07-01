using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
