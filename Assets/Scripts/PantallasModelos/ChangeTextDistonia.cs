using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextDistonia : MonoBehaviour
{

	public Text InfoMusculos;

	public GameObject Panel;

	private GameObject _musculoResaltado, _musculoResaltadoBilateral;
	private SkinnedMeshRenderer _renderer, _renderBilateral;
	private SkinnedMeshRenderer[] renderers, renderersBilateral;
	private Material _materialOriginal, _matBilateralOriginal;
	public Material _materialFosforescente, _materialFosforescenteInterior;

	public GameObject[] ocultarEscapulae, ocultarLonCapitis, ocultarLonColli, ocultarEscMedio,
		ocultarSemispinalisCapitis, transparenteSemispinalisCapitis, ocultarSemispinalisCervis,
		ocultarLongisimusCapitis, ocultarSpleniusCapitis, transparenteSplenius, transparenteLongisimoCabeza;

	private float rotX = 0f;
	private float rotY = 0f;
	private Vector3 originRot;

	private int _intModelo;
	
	[SerializeField] private float alpha = 0.7f, alpha2 = 0.4f;

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
		float original = 1f;
		float modo = 0;
		foreach (GameObject tapa in ocultarEscapulae)
		{
			TransparentarMusculos(tapa, original, modo);
		}
		foreach (GameObject tapa in ocultarLonCapitis)
		{
			TransparentarMusculos(tapa, original, modo);
		}
		foreach (GameObject tapa in ocultarLonColli)
		{
			TransparentarMusculos(tapa, original, modo);
		}
		foreach (GameObject tapa in ocultarEscMedio)
		{
			TransparentarMusculos(tapa, original, modo);
		}
		foreach (GameObject tapa in ocultarSemispinalisCapitis)
		{
			TransparentarMusculos(tapa, original, modo);
		}
		foreach (GameObject tapa in ocultarSemispinalisCervis)
		{
			TransparentarMusculos(tapa, original, modo);
		}

		foreach (GameObject tapa in ocultarLongisimusCapitis)
		{
			TransparentarMusculos(tapa, original, modo);
		}
		foreach (GameObject tapa in ocultarSpleniusCapitis)
		{
			TransparentarMusculos(tapa, original, modo);
		}
		foreach (GameObject tapa in transparenteSemispinalisCapitis)
		{
			tapa.SetActive(true);
		}

		foreach (GameObject tapa in transparenteSplenius)
		{
			tapa.SetActive(true);
		}
        foreach (GameObject tapa in transparenteLongisimoCabeza)
        {
            tapa.SetActive(true);
        }
	}
	
	void Seleccion(int posicion, int posicionBilateral, bool interior)
	{
		if (_musculoResaltado != null)
		{
			LimpiarSeleccion();
		}
		//Dos musculos en caso de ser bilateral o uno mismo si es izq o derecho
		_musculoResaltado = transform.GetChild(3).GetChild(posicion).gameObject;
		_musculoResaltadoBilateral = transform.GetChild(3).GetChild(posicionBilateral).gameObject;
		
		//Obtener renders para dos musculos
		renderers = _musculoResaltado.GetComponentsInChildren<SkinnedMeshRenderer>();
		renderersBilateral = _musculoResaltadoBilateral.GetComponentsInChildren<SkinnedMeshRenderer>();
		
		//Cambiar colores para dos musculos
		_renderer = renderers[0];
		_renderBilateral = renderersBilateral[0];
		
		//Cambio a material fosforescente para los dos
		_materialOriginal = _renderer.material;
		_matBilateralOriginal = _renderBilateral.material;
		
		//Fosforecente para los dos
		if (interior)
		{
			_renderer.material = _materialFosforescenteInterior;
			_renderBilateral.material = _materialFosforescenteInterior;
			renderers[0].material = _materialFosforescenteInterior;
			renderersBilateral[0].material = _materialFosforescenteInterior;
		}
		else
		{
			_renderer.material = _materialFosforescente;
			_renderBilateral.material = _materialFosforescente;
			
			renderers[0].material = _materialFosforescente;
			renderersBilateral[0].material = _materialFosforescente;
		}

	}
	void LimpiarSeleccion()
	{
		if (_musculoResaltado == null)
		{
			return;
		}
		//Regresar a su color original
		_renderer.material = _materialOriginal;
		_renderBilateral.material = _matBilateralOriginal;
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

				if (modo != 3)
				{
					m.renderQueue = -1;
				}
				else
				{
					m.renderQueue = 3000;
				}
			}
			skinnyRender.materials = mats;
		}
		
	}

	public void CambiarInfoDistonia(int id)
	{
		MostrarTodosMusculos();
		float modo = 3;
		switch (id)
		{
			//Antecaput - BILATERAL
			case 0: //esternocleido
				InfoMusculos.text = "15-75U/VISITA \n\nBilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(4, 5, false);
				break;
			case 1: //Levator Escapulae
				InfoMusculos.text = "25-100U/VISITA \n\nBilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(6, 7, false);
				foreach (GameObject tapa in ocultarEscapulae)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 2: //Largo de la cabeza || Longus capitis
				InfoMusculos.text = "50-150U/VISITA \n\nBilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
                transform.Rotate(23, 15, 0, Space.World);
                Seleccion(12, 13, true);
				foreach (GameObject tapa in ocultarLonCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;

			
			//Anterocollis - BILATERAL
			case 3: //Levator Scapulae || Elevador de la escapula
				InfoMusculos.text = "25-100U/VISITA\n\nBilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(6, 7, false);
				foreach (GameObject tapa in ocultarEscapulae)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 4: //Longus Collis || Largo del cuello
				InfoMusculos.text = "50-150U/VISITA\n\nBilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(14, 15, true);
				foreach (GameObject tapa in ocultarLonColli)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 5: //Scalenus Medius || Escaleno medio
				InfoMusculos.text = "15-50U/VISITA\n\nBilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
                transform.Rotate(13.5f, 30, 0, Space.World);
                Seleccion(1,2, true);
				foreach (GameObject tapa in ocultarEscMedio)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;

			
			//Lateral Shift - Laterocollis + Laterocaput = ipsilateral del lado derecho
			case 6: //Esternocleido
				InfoMusculos.text = "15-75U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(5, 5, false);
				break;
			case 7: //Levator Scapulae || Elevador de la escapula
				InfoMusculos.text = "25-100U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(6, 7, false);
				foreach (GameObject tapa in ocultarEscapulae)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 8: //Longisimo del cuello || Longissimus cervis
				InfoMusculos.text = "50-150U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(10, 10, true);
				foreach (GameObject tapa in ocultarEscapulae)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 9: //Scalenus Medius
				InfoMusculos.text = "15-50U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
                transform.Rotate(0, 13, 0, Space.World);
                Seleccion(1, 1, true);
				foreach (GameObject tapa in ocultarEscMedio)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 10: //Semispinalis Capitis || Semiespinoso de la cabeza - Izquierdo
				InfoMusculos.text = "50-150U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,165,0,Space.World);
				Seleccion(64, 64, true);
				foreach (GameObject tapa in ocultarSemispinalisCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				foreach (GameObject tapa in transparenteSemispinalisCapitis)
				{
					tapa.SetActive(false);
				}
				break;
			case 11: //Semispinalis Cervis || Semiespinoso del cuello - Derecho
				InfoMusculos.text = "50-150U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,210,0,Space.World);
				Seleccion(60, 60, true);
				foreach (GameObject tapa in ocultarSemispinalisCervis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 12: //Splenius Capitis || Splenio de la cabeza - Izquierdo
				InfoMusculos.text = "50-150U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(66, 66, false);
				foreach (GameObject tapa in ocultarSpleniusCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 13: //Trapecio - Izquierdo
				InfoMusculos.text = "50-100U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(68, 68, false);
				break;
			case 46: //Longissimus Capitis || Longisimo de la cabeza
				InfoMusculos.text = "50-150U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,148,0,Space.World);
				Seleccion(9, 9, true);
				foreach (GameObject tapa in ocultarLongisimusCapitis)
				{
					TransparentarMusculos(tapa, alpha2, modo);
				}
				break;
			

			
			//Laterocaput - ipsilateral del lado derecho
			case 14: //Levator Scapulae || Elevador de la escapula
				InfoMusculos.text = "25-100U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,220,0,Space.World);
				Seleccion(6, 6, false);
				foreach (GameObject tapa in ocultarEscapulae)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 15: //Longissimus Capitis || Longisimo de la cabeza
				InfoMusculos.text = "50-150U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,197,0,Space.World);
				Seleccion(8, 8, true);
				foreach (GameObject tapa in ocultarLongisimusCapitis)
				{
					TransparentarMusculos(tapa, alpha2, modo);
				}
				break;
			case 16: //Semispinalis Capitis || Semiespinoso de la cabeza
				InfoMusculos.text = "50-150U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,-165,0,Space.World);
				Seleccion(63, 63, false);
				foreach (GameObject tapa in ocultarSemispinalisCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				foreach (GameObject tapa in transparenteSemispinalisCapitis)
				{
					tapa.SetActive(false);
				}
				break;
			case 17: //Splenius Capitis || Esplenio
				InfoMusculos.text = "50-150U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(65, 65, false);
				foreach (GameObject tapa in ocultarSpleniusCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 18: //Esternocleido
				InfoMusculos.text = "15-75U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(4, 4, false);
				break;
			case 19: //Trapecio
				InfoMusculos.text = "50-100U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(67, 67, false);
				break;

			
			//Laterocollis - ipsilateral del lado derecho
			case 20: //Levator Scapulae || Elevador de la escapula
				InfoMusculos.text = "50-150U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(6, 6, false);
				foreach (GameObject tapa in ocultarEscapulae)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 21: //Longisimo del cuello || Longissimus cervis
				InfoMusculos.text = "50-150U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(10, 10, true);
				foreach (GameObject tapa in ocultarEscapulae)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 22: //Scalenus Medius || Escaleno medio
				InfoMusculos.text = "50-150U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
                transform.Rotate(0, 5, 0, Space.World);
                Seleccion(1, 1, false);
				foreach (GameObject tapa in ocultarEscMedio)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 23: //Semispinalis Cervis || Semiespinoso del cuello
				InfoMusculos.text = "50-150U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,205,0,Space.World);
				Seleccion(60, 60, false);
				foreach (GameObject tapa in ocultarSemispinalisCervis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;

			
			//Retrocaput - BILATERAL
			case 24: //Obliquus Capitis Inferior
				InfoMusculos.text = "50-100U/VISITA \n\nBilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(56, 57, true);
				foreach (GameObject tapa in ocultarSpleniusCapitis)
				{
					TransparentarMusculos(tapa, alpha2, modo);
				}
				break;
			case 25: //Semispinalis Capitis
				InfoMusculos.text = "50-150U/VISITA \n\nBilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,220,0,Space.World);
                Seleccion(63, 64, false);
				foreach (GameObject tapa in ocultarSemispinalisCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				foreach (GameObject tapa in transparenteSemispinalisCapitis)
				{
					tapa.SetActive(false);
				}
				break;
			case 26: //Splenius Capitis || Esplenio
				InfoMusculos.text = "50-150U/VISITA \n\nBilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(65, 66, false);
				foreach (GameObject tapa in ocultarSpleniusCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 27: //Trapecio
				InfoMusculos.text = "50-100U/VISITA \n\nBilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(67, 68, false);
				break;

			
			//Retrocollis - BILATERAL
			case 28: //Semispinalis Cervis
				InfoMusculos.text = "50-150U/VISITA\n\nBilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(60, 61, true);
				foreach (GameObject tapa in ocultarSemispinalisCervis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;

			
			//Sagital Shift aka Anterior Shift - BILATERAL
			case 29: //Levator Scapulae || Elevador de la Escapula
				InfoMusculos.text = "25-100U/VISITA \n\nBilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(6, 7, false);
				foreach (GameObject tapa in ocultarEscapulae)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 30: //Longus Colli
				InfoMusculos.text = "50-150U/VISITA \n\nBilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(14, 15, true);
				foreach (GameObject tapa in ocultarLonColli)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 31: //Obliquus Capitis || Oblicuo de la cabeza
				InfoMusculos.text = "50-100U/VISITA \n\nBilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(56, 57, true);
				foreach (GameObject tapa in ocultarSpleniusCapitis)
				{
					TransparentarMusculos(tapa, alpha2, modo);
				}
				break;
			case 32: //Scalenus Medius || Escaleno medio
				InfoMusculos.text = "15-50U/VISITA \n\nBilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(1, 2, true);
				foreach (GameObject tapa in ocultarEscMedio)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 33: //Semispinalis Capitis
				InfoMusculos.text = "50-150U/VISITA \n\nBilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,220,0,Space.World);
				Seleccion(63, 64, false);
				foreach (GameObject tapa in ocultarSemispinalisCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				foreach (GameObject tapa in transparenteSemispinalisCapitis)
				{
					tapa.SetActive(false);
				}
				break;
			case 34: //Splenius Capitis
				InfoMusculos.text = "50-150U/VISITA \n\nBilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(65, 66, false);
				foreach (GameObject tapa in ocultarSpleniusCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				foreach (GameObject tapa in transparenteSplenius)
				{
					tapa.SetActive(false);
				}
				break;
			case 35: //Trapecio
				InfoMusculos.text = "50-100U/VISITA \n\nBilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(67, 68, false);
				break;

			
			//Torticaput - Contraleral, tomar del lado izquierdo
			case 36: //Esternocleido
				InfoMusculos.text = "15-75U/VISITA \n\nContralateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				Seleccion(4, 4, false);
				break;
			case 37: //Longissimus Capitis || Longisimo de la cabeza
				InfoMusculos.text = "50-150U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(9, 9, true);
				foreach (GameObject tapa in ocultarLongisimusCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
                foreach (GameObject tapa in transparenteLongisimoCabeza)
                {
                    tapa.SetActive(false);
                }
                break;
			case 38: //Obliquus Capitis || Oblicuo de la cabeza
				InfoMusculos.text = "50-100U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,218,0,Space.World);
				Seleccion(57, 57, true);
				foreach (GameObject tapa in ocultarSpleniusCapitis)
				{
					TransparentarMusculos(tapa, alpha2, modo);
				}
				break;
			case 39: //Semispinalis Capitis || Semiespinal de la cabeza - contralateral
				InfoMusculos.text = "50-150U/VISITA \n\nContralateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,-165,0,Space.World);
				Seleccion(63, 63, false);
				foreach (GameObject tapa in ocultarSemispinalisCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				foreach (GameObject tapa in transparenteSemispinalisCapitis)
				{
					tapa.SetActive(false);
				}
				break;
			case 40: //Splenius Capitis || Esplenio de la cabeza
				InfoMusculos.text = "50-150U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(66, 66, false);
				foreach (GameObject tapa in ocultarSpleniusCapitis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 41: //Trapecio
				InfoMusculos.text = "50-100U/VISITA \n\nContralateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(67, 67, false);
				break;

			
			//Torticolis - ipsilateral del lado izquierdo
			case 42: //Levator Scapulae
				InfoMusculos.text = "25-100U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,155,0,Space.World);
				Seleccion(7, 7, false);
				foreach (GameObject tapa in ocultarEscapulae)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 43: //Longissimus Cervis || Longisimo del cuello
				InfoMusculos.text = "50-150U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,173,0,Space.World);
				Seleccion(11, 11, true);
				foreach (GameObject tapa in ocultarEscapulae)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 44: //Semispinalis Cervis || Semiespinoso del cuello
				InfoMusculos.text = "50-150U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,180,0,Space.World);
				Seleccion(61, 61, true);
				foreach (GameObject tapa in ocultarSemispinalisCervis)
				{
					TransparentarMusculos(tapa, alpha, modo);
				}
				break;
			case 45: //Splenius || Esplenio
				InfoMusculos.text = "20-60U/VISITA \n\nIpsilateral";
				transform.eulerAngles = new Vector3(rotX, rotY, 0f);
				transform.Rotate(0,155,0,Space.World);
				Seleccion(66, 66, false);
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
