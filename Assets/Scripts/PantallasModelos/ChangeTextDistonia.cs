using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextDistonia : MonoBehaviour
{

	public Text InfoMusculos;

	public GameObject Panel;

	private GameObject _musculoResaltado;
	private SkinnedMeshRenderer _renderer;
	private SkinnedMeshRenderer[] renderers;
	private Material _materialOriginal;
	public Material _materialFosforescente;

	public GameObject[] ocultarEscapulae, ocultarLonCapitis, ocultarLonColli, ocultarEscMedio,
		ocultarSemispinalisCapitis, ocultarSemispinalisCervis, ocultarSpleniusCapitis;

	private float rotX = 0f;
	private float rotY = 0f;
	private Vector3 originRot;

	private int _intModelo;

	private void Start()
	{
		Panel.SetActive(false);
		
		originRot = transform.eulerAngles;

		//Utilizar variables para modificar la rotacion
		rotX = originRot.x;
		rotY = originRot.y;
	}

	public void MostrarPanel()
	{
		Panel.SetActive(true);
	}
	public void OcultarPanel()
	{
		LimpiarSeleccion();
		MostrarTodosMusculos();
		Panel.SetActive(false);
		transform.eulerAngles = new Vector3(rotX, rotY, 0f);
	}

	void MostrarTodosMusculos()
	{
		float alpha = 1f;
		float modo = 0;
		foreach (GameObject tapa in ocultarEscapulae)
		{
			TransparentarMusculos(tapa, alpha, modo);
		}
		foreach (GameObject tapa in ocultarLonCapitis)
		{
			TransparentarMusculos(tapa, alpha, modo);
		}
		foreach (GameObject tapa in ocultarLonColli)
		{
			TransparentarMusculos(tapa, alpha, modo);
		}
		foreach (GameObject tapa in ocultarEscMedio)
		{
			TransparentarMusculos(tapa, alpha, modo);
		}
		foreach (GameObject tapa in ocultarSemispinalisCapitis)
		{
			TransparentarMusculos(tapa, alpha, modo);
		}
		foreach (GameObject tapa in ocultarSemispinalisCervis)
		{
			TransparentarMusculos(tapa, alpha, modo);
		}
		foreach (GameObject tapa in ocultarSpleniusCapitis)
		{
			TransparentarMusculos(tapa, alpha, modo);
		}
	}
	
	void Seleccion(int posicion)
	{
		if (_musculoResaltado != null)
		{
			LimpiarSeleccion();
		}
		_musculoResaltado = transform.GetChild(3).GetChild(posicion).gameObject;
		renderers = _musculoResaltado.GetComponentsInChildren<SkinnedMeshRenderer>();
		//renderers[0] = _musculoResaltado.GetComponent<SkinnedMeshRenderer>();
		
		//_renderer = _musculoResaltado.GetComponent<SkinnedMeshRenderer>();
		_renderer = renderers[0];
		_materialOriginal = _renderer.material;
		_renderer.material = _materialFosforescente;

		
		renderers[0].material = _materialFosforescente;
	}
	void LimpiarSeleccion()
	{
		if (_musculoResaltado == null)
		{
			return;
		}
		//Regresar a su color original
		_renderer.material = _materialOriginal;
	}

	void TransparentarMusculos(GameObject musculo, float alpha, float modo)
	{
		SkinnedMeshRenderer[] skinnyRenders = musculo.GetComponentsInChildren<SkinnedMeshRenderer>();
		foreach (SkinnedMeshRenderer skinnyRender in skinnyRenders)
		{
			Material[] mats = skinnyRender.materials;
			foreach (Material m in mats)
			{
				Color c = m.color;
				//c = new Color(255,255,255,0.2f);
				c.a = alpha;
				m.color = c;
				m.SetFloat("_Mode", modo);
				m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				//m.SetInt("_ZWrite", 0); //Modo Transparente
				m.SetInt("_ZWrite", 1); //Modo Opacidad
				m.DisableKeyword("_ALPHATEST_ON");
				m.EnableKeyword("_ALPHABLEND_ON");
				m.DisableKeyword("_ALPHAPREMULTIPLY_ON");

				if (modo!=3)
				{
					m.renderQueue = -1;
				}
				else
				{
					m.renderQueue = 3000;
				}
				
				/*Color c = m.color;
				c.a = alpha;
				m.color = c;
				m.SetFloat("_Mode", modo);
				m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
				m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				//m.SetInt("_ZWrite", 0); //Modo Transparente
				//m.SetInt("_ZWrite", 1); //Modo Opacidad
				m.DisableKeyword("_ALPHATEST_ON");
				m.EnableKeyword("_ALPHABLEND_ON");
				m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				//m.renderQueue = 3000;*/
			}
			skinnyRender.materials = mats;
		}
		
	}

	public void CambiarInfoDistonia(int id)
	{
		MostrarTodosMusculos();
		float alpha = 0.2f;
		float modo = 3;
		switch (id)
		{
			//Antecaput
			case 0: //esternocleidoantecaput - 0 al es el 37
				InfoMusculos.text = "15-75U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(37);
				break;
			case 1: //Levator Escapulae
				InfoMusculos.text = "25-100U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(24);
				foreach (GameObject tapa in ocultarEscapulae)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 2: //Largo de la cabeza || Longus capitis
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(4);
				foreach (GameObject tapa in ocultarLonCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;

			
			//Anterocollis
			case 3: //Levator Scapulae || Elevador de la escapula
				InfoMusculos.text = "25-100U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(24);
				foreach (GameObject tapa in ocultarEscapulae)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 4: //Longus Collis || Largo del cuello
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(5);
				foreach (GameObject tapa in ocultarLonColli)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 5: //Scalenus Medius || Escaleno medio
				InfoMusculos.text = "15-50U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(1);
				foreach (GameObject tapa in ocultarEscMedio)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;

			
			//Lateral Shift
			case 6: //Esternocleido
				InfoMusculos.text = "15-75U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(37);
				break;
			case 7: //Levator Scapulae || Elevador de la escapula
				InfoMusculos.text = "25-100U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(24);
				foreach (GameObject tapa in ocultarEscapulae)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 8: //Longisimo del cuello || Longissimus cervis
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(5);
				foreach (GameObject tapa in ocultarLonColli)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 9: //Scalenus Medius
				InfoMusculos.text = "15-50U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(5);
				foreach (GameObject tapa in ocultarEscMedio)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 10: //Semispinalis Capitis || Semiespinoso de la cabeza
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(53);
				foreach (GameObject tapa in ocultarSemispinalisCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 11: //Semispinalis Cervis || Semiespinoso del cuello
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(54);
				foreach (GameObject tapa in ocultarSemispinalisCervis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 12: //Splenius Capitis || Splenio de la cabeza
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(36);
				foreach (GameObject tapa in ocultarSpleniusCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 13: //Trapecius
				InfoMusculos.text = "50-100U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(40);
				break;

			
			//Laterocaput
			case 14: //Levator Scapulae || Elevador de la escapula
				InfoMusculos.text = "25-100U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(24);
				foreach (GameObject tapa in ocultarEscapulae)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 15: //Longuissimus Capitis || Largo de la cabeza || Longuisimo de la cabeza
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(3);
				foreach (GameObject tapa in ocultarSemispinalisCervis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 16: //Semispinalis Capitis || Semiespinoso de la cabeza
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(53);
				foreach (GameObject tapa in ocultarSemispinalisCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 17: //Splenius Capitis || Esplenio de la cabeza
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(36);
				foreach (GameObject tapa in ocultarSpleniusCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 18: //Esternocleido
				InfoMusculos.text = "15-75U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(37);
				break;
			case 19: //Trapecius
				InfoMusculos.text = "50-100U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(40);
				break;

			
			//Laterocollis
			case 20: //Levator Scapulae || Elevador de la escapula
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(24);
				foreach (GameObject tapa in ocultarEscapulae)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 21: //Longisimo del cuello || Longissimus cervis
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(5);
				foreach (GameObject tapa in ocultarLonColli)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 22: //Scalenus Medius || Escaleno medio
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(1);
				foreach (GameObject tapa in ocultarEscMedio)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 23: //Semispinalis Cerviss || Semiespinoso del cuello
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(54);
				foreach (GameObject tapa in ocultarSemispinalisCervis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;

			
			//Retrocaput
			case 24: //Obliquus Capitis Inferior
				InfoMusculos.text = "50-100U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(54);
				foreach (GameObject tapa in ocultarSpleniusCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 25: //Semispinalis Capitis
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(53);
				foreach (GameObject tapa in ocultarSemispinalisCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 26: //Splenius Capitis || Esplenio de la cabeza
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(36);
				foreach (GameObject tapa in ocultarSpleniusCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 27: //Trapecio
				InfoMusculos.text = "50-100U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(40);
				break;

			
			//Retrocollis
			case 28: //Semispinalis Cervis
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(54);
				foreach (GameObject tapa in ocultarSemispinalisCervis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;

			
			//Sagital Shift aka Anterior Shift
			case 29: //Levator Scapulae || Elevador de la Escapula
				InfoMusculos.text = "25-100U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(24);
				foreach (GameObject tapa in ocultarEscapulae)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 30: //Longus Colli
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(5);
				foreach (GameObject tapa in ocultarLonColli)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 31: //Obliquus Capitis || Oblicuo de la cabeza
				InfoMusculos.text = "50-100U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(54);
				foreach (GameObject tapa in ocultarSpleniusCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 32: //Scalenus Medius || Escaleno medio
				InfoMusculos.text = "15-50U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(1);
				foreach (GameObject tapa in ocultarEscMedio)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 33: //Semispinalis Capitis
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(53);
				foreach (GameObject tapa in ocultarSemispinalisCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 34: //Splenius Capitis
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(36);
				foreach (GameObject tapa in ocultarSpleniusCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 35: //Trapecius
				InfoMusculos.text = "50-100U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(40);
				break;

			
			//Torticaput
			case 36: //Esternocleido
				InfoMusculos.text = "15-75U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(37);
				break;
			case 37: //Longuissimus Capitis || Longisimo de la cabeza
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(3);
				foreach (GameObject tapa in ocultarSemispinalisCervis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 38: //Obliquus Capitis || Oblicuo de la cabeza
				InfoMusculos.text = "50-100U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(54);
				foreach (GameObject tapa in ocultarSpleniusCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 39: //Semispinalis Capitis || Semiespinal de la cabeza
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(53);
				foreach (GameObject tapa in ocultarSemispinalisCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 40: //Splenius Capitis || Esplenio
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(36);
				foreach (GameObject tapa in ocultarSpleniusCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 41: //Trapecio
				InfoMusculos.text = "50-100U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(40);
				break;

			//Torticolis
			case 42: //Levator Scapulae
				InfoMusculos.text = "25-100U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(24);
				foreach (GameObject tapa in ocultarEscapulae)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 43: //Longissimus Cervis
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(5);
				foreach (GameObject tapa in ocultarLonColli)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 44: //Semispinalis Cervis || Semiespinoso del cuello
				InfoMusculos.text = "50-150U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(54);
				foreach (GameObject tapa in ocultarSemispinalisCervis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 45: //Splenius Capitis || Esplenio
				InfoMusculos.text = "20-60U/VISITA";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(36);
				foreach (GameObject tapa in ocultarSpleniusCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;

			default:
				break;
		}
	}
}
