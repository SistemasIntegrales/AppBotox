using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthGenerico : MonoBehaviour
{
    [SerializeField] private InputField _clave;
    [SerializeField] private GameObject _leyenda;

    public void Pasar()
    {
        if (_clave.text == "BotoxAcceso19")
        {
            SceneManager.LoadSceneAsync(2);
        }
        else
        {
            _leyenda.SetActive(true);
            StartCoroutine(QuitarLeyenda());
        }
    }

    IEnumerator QuitarLeyenda()
    {
        yield return new WaitForSeconds(2);
        _leyenda.SetActive(false);
    }
}
