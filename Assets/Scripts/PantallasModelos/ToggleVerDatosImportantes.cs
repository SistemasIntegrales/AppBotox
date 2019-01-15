using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVerDatosImportantes : MonoBehaviour
{

	public GameObject DatosImportantes;

	public void TogglePanelInfo()
	{
		if (!DatosImportantes.activeSelf)
		{
			DatosImportantes.SetActive(true);
		}
		else
		{
			DatosImportantes.SetActive(false);
		}
	}
}
