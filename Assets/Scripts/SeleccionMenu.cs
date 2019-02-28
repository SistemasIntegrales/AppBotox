using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeleccionMenu : MonoBehaviour
{
	private int sceneIndex;

	private void Start()
	{
		//Obtener el num de la escena de build settings
		sceneIndex = SceneManager.GetActiveScene().buildIndex;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			switch (sceneIndex)
			{
				case 3:
					SceneManager.LoadScene(sceneIndex - 2);
					break;
				case 4:
					SceneManager.LoadScene(sceneIndex - 3);
					break;
				case 5:
					SceneManager.LoadScene(sceneIndex - 4);
					break;
				case 6:
					SceneManager.LoadScene(sceneIndex - 5);
					break;
				case 7:
					SceneManager.LoadScene(sceneIndex - 6);
					break;
				case 8:
					SceneManager.LoadScene(sceneIndex - 7);
					break;
				default:
					break;
			}
		}
	}

	public void LoginButton()
	{
		SceneManager.LoadSceneAsync("_LoginMenu");
	}
	public void RegistroButton()
	{
		SceneManager.LoadSceneAsync("_RegisterMenu");
	}
	
	public void MainMenuButton()
	{
		SceneManager.LoadSceneAsync("MainMenu");
	}

	public void AllergenButtonPressed()
	{
		//Cambio a Pantalla Allergen
		SceneManager.LoadSceneAsync("InfoAllergen");
	}
	public void BotoxButtonPressed()
	{
		//Cambio a Pantalla Botox
		SceneManager.LoadSceneAsync("InfoBotox");
	}
	public void ZonasMigranaPressed()
	{
		//Cambio a Pantalla Aplicaciones
		SceneManager.LoadSceneAsync("MigranaScene");
	}
	public void DistoniaPressed()
	{
		//Cambio a Pantalla Aplicaciones
		SceneManager.LoadSceneAsync("DistoniaScene");
	}
	public void ReferenciasPressed()
	{
		//Cambio a Pantalla Aplicaciones
		SceneManager.LoadSceneAsync("Referencias");
	}
}
