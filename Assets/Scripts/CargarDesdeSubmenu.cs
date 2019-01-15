using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarDesdeSubmenu : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void AllergenPressed()
	{
		//Cambio a Pantalla Allergen
		SceneManager.LoadSceneAsync("InfoAllergen");
	}
	public void BotoxPressed()
	{
		//Cambio a Pantalla Botox
		SceneManager.LoadSceneAsync("InfoBotox");
	}
}
