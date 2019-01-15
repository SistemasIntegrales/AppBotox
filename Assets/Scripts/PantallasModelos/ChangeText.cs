using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * Lo que se hara:
 * El script estara attacheado a la camara
 * La camara detectara si en su trayectoria hay objetos
 * Se pica en la pantalla y si se pico un objeto
 * Cuando se pique dicha zona de aplicacion se obtendra su nombre y se desplegara como Text titulo.
 * Luego, siguiendo con el nombre de la zona, se obtendrá la info del array y se pasara como Text info.
 */

public class ChangeText : MonoBehaviour
{
	//public Camera camera = new Camera();
	public LayerMask touchInputMask = 9;

	public Text tituloAplicacion, infoAplicacion, infoCarasModelo;

	[SerializeField] private GameObject _panelCarasModelo;
	
	private GameObject _puntoSeleccionado;
	private string _infoSeleccion, _nombrePunto;

public GameObject[] puntosAlicacion, puntosDolor;
	private List<string> nombresAplicacion, nombresDolor;
	
	private string[] infoPuntosAplicacion = new[]
	{
		//1
		"Aplicar 5U, con aguja a 45 grados. \nFormar línea imaginaria del trago de la oreja hacia el borde más superior del músculo temporal.",
		
		"Aplicar 5U, con aguja a 45 grados. \nFormar línea imaginaria del trago de la oreja hacia el borde más superior del músculo temporal." +
		"\nInyectar 3cm por debajo del punto temporal derecho 1.",
		
		"Aplicar 5U, con aguja a 45 grados. Borde más anterior del músculo temporal, dentro de la línea de inserción del pelo.",
		
		"Aplicar 5U, con aguja a 45 grados. \nFormar línea imaginaria desde la mitad del hélix del pabellón auricular hacia el borde superior del músculo temporal.",
		
		//5
		"Aplicar 5U, con aguja a 45 grados. \nFormar línea imaginaria del trago de la oreja hacia el borde más superior del músculo temporal.",
		
		"Aplicar 5U, con aguja a 45 grados. \nFormar línea imaginaria del trago de la oreja hacia el borde más superior del músculo temporal." +
		"\nInyectar 3cm por debajo del punto temporal izquierdo 1.",
		
		"Aplicar 5U, con aguja a 45 grados. Borde más anterior del músculo temporal, dentro de la línea de inserción del pelo.",
		
		"Aplicar 5U, con aguja a 45 grados. \nFormar línea imaginaria desde la mitad del hélix del pabellón auricular hacia el borde superior del músculo temporal.",
		
		"Aplicar 5U, con aguja a 90 grados.",
		
		//10
		"Aplicar 5U, con aguja a 90 grados.",
		
		"Parte más prominente del cráneo posterior." +
		"\nPunto de referencia anatómico para trazar una línea imaginaria que va de éste hacia ambas apófisis mastoides" +
		"y otra línea imaginaria que une a ambas apófisis mastoides para formar un triángulo," +
		" indispensable para aplicación de las dosis.",
		
		//Paraespinal 1 derecho
		"Aplicar 5U, aguja a 45 grados. \nPalpar línea nucal, inyectar 3 centímetros por debajo de la línea nucal y un centímetro lateral a la línea media." +
		"\nLo anterior dentro del borde de inserción del pelo.",
		//Paraespinal 2 derecho
		"Aplicar 5U, aguja a 45 grados. \nInyectar 2 centímetros por arriba, 1 cm lateral al (nervio occipital derecho 1)Paraespinal 1 derecho.",
		//Paraespinal 1 izq
		"Aplicar 5U, aguja a 45 grados. \nPalpar línea nucal, inyectar 3 centímetros por debajo de la línea nucal y un centímetro lateral a la línea media." +
		"\nLo anterior dentro del borde de inserción del pelo.",
		
		"Aplicar 5U, aguja a 45 grados. \nInyectar 2 centímetros por arriba, 1 cm lateral al (nervio occipital izquierdo 1)Paraespinal 1 izquierdo.",
		
		//16 - nerv occ mayor der
		"Aplicar 5U, con aguja a 45 grados. \nTres centímetros por encima y un centímetro medial al nervio occipital menor derecho 1.",
		
		"Aplicar 5U, con aguja a 45 grados. \nTres centímetros por encima y un centímetro medial al nervio occipital menor derecho 1.",
		
		"Aplicar 5U, con aguja a 45 grados. \nFormar línea imaginaria entre el inion y la apófisis mastoides derecha, " +
		"inyectar en el punto medio de la línea imaginaria.",
		
		"Aplicar 5U, con aguja a 45 grados. \n Tres centímetros por encima y un centímetro lateral al nervio occipital menor derecho 1.",
		
		"Aplicar 5U, con aguja a 45 grados. \nFormar línea imaginaria entre el inion y la apófisis mastoides izquierda, " +
		"inyectar en el punto medio de la línea imaginaria.",
		
		//Nerv occ menor izq 1
		"Aplicar 5U, con aguja a 45 grados. \n Tres centímetros por encima y un centímetro lateral al nervio occipital menor izquierdo 1.",
		
		//Trapecio 2 derecho - Nerv supracalv derecho 1
		"Aplicar 5U, con aguja a 90 grados (subcutáneo)." +
		"\nInyectar en el punto medio formado del cuello al (Nerv supracalv derecho 2) trapecio 1 derecho.",
		
		//Trapecio 1 derecho - Nerv supracalv derecho 2
		"Aplicar 5U, con aguja a 90 grados (subcutáneo)." +
		"\nFormar línea imaginaria del acromion derecho al cuello, inyectar en el punto medio de la línea imaginaria.",
		
		//Trapecio 3 derecho - Nerv supracalv derecho 3
		"Aplicar 5U, con aguja a 90 grados (subcutáneo)." +
		"\nInyectar en el punto medio formado del (Nerv supracalv derecho 2) trapecio 1 derecho al acromion.",
		
		
		//Trapecio 2 izquierdo - Nerv supracalv derecho 1
		"Aplicar 5U, con aguja a 90 grados (subcutáneo)." +
		"\nInyectar en el punto medio formado del cuello al (Nerv supracalv izquierdo 2) trapecio 1 izquierdo.",
		
		//Trapecio 1 izquierdo - Nerv supracalv derecho 2
		"Aplicar 5U, con aguja a 90 grados (subcutáneo)." +
		"\nFormar línea imaginaria del acromion derecho al cuello, inyectar en el punto medio de la línea imaginaria.",
		
		//Trapecio 3 izquierdo - Nerv supracalv derecho 3
		"Aplicar 5U, con aguja a 90 grados (subcutáneo)." +
		"\nInyectar en el punto medio formado del (Nerv supracalv izquierdo 2) trapecio 1 izquierdo al acromion.",
		
		//Procer
		"Aplicar 5U, con aguja a 90 grados.",
		
		"Aplicar 5U, con aguja a 45 grados. \nFormar línea imaginaria del limbo externo del ojo (unión entre el iris y la conjuntiva)" +
		" hacia la parte más superior del músculo frontal, en el borde de implantación del pelo.",
		
		"Aplicar 5U, con aguja a 45 grados. \nFormar línea imaginaria del limbo externo del ojo (unión entre el iris y la conjuntiva)" +
		" hacia la parte más superior del músculo frontal, en el borde de implantación del pelo.",
		
		"Aplicar 5U, con aguja a 45 grados. \nFormar línea imaginaria del canto interno del ojo hacia la " +
		"parte más superior del músculo frontal, en el borde de implantación del pelo.",
		
		"Aplicar 5U, con aguja a 45 grados. \nFormar línea imaginaria del canto interno del ojo hacia la " +
		"parte más superior del músculo frontal, en el borde de implantación del pelo."
	};

	private string[] infoPuntosDolor = new[]
	{
		//Recordar no aplicar sobre los puntos previos.
		"Aplicar 5U en región temporal.",
		"Aplicar 5U en región temporal.",
		
		//Recordar no aplicar sobre los puntos previos.
		"Aplicar 5U en región temporal.",
		"Aplicar 5U en región temporal.",
		
		//Administrar por encima de los aplicados previamente.
		"Aplicar 5U en región occipital.",
		"Aplicar 5U en región occipital.",
		
		//No administrar sobre puntos previamente aplicados.
		"Aplicar 5U en área de trapecio.",
		"Aplicar 5U en área de trapecio.",
		"Aplicar 5U en área de trapecio.",
		"Aplicar 5U en área de trapecio.",
		
	};
	
	private string[] infoGeneral = new[] {
		"FRONTAL\n\n"+
		"-Aplicar siempre por encima de las líneas faciales hiperquinéticas.\n\n"+ 
		"-Aplicar la toxina cerca de los nervios correspondientes: nervio supratroclear, nervio supraorbitario.\n\n"+
		"-Los puntos frontales deben de ser subcutáneos, el prócer y corrugador van al músculo.\n\n"+
		"- En prócer y corrugador se aplica con aguja a 90 grados.\n",
		
		"LATERAL\n\n"+
		"-Aplicación bilateral.\n\n"+
		"-El objetivo es el nervio auriculotemporal.\n\n"+
		"-Todos los puntos son subcutáneos.\n\n"+
		"-Aplicar con aguja a 45 grados.\n",
		
		"POSTERIOR\n\n"+
		"- Realizar de manera bilateral.\n"+
		"- No aplicar los últimos puntos por debajo de la línea del pelo.\n\n"+
		"-Nervios objetivo: nervio occipital mayor, nervio occipital menor y tercer nervio occipital.\n\n"+
		"-Todos los puntos son subcutáneos y con aguja a 45 grados.\n",
		
		"SUPRACLAVICULAR\n\n"+

		"-La aguja se introduce perpendicular al trapecio en los 3 puntos.\n\n"+
		"-El nervio objetivo es el supraclavicular.\n\n"+ 
		"-Aplicar de manera bilateral.\n"
	};

	private void Start()
	{
		puntosAlicacion = GameObject.FindGameObjectsWithTag("MuestraInfo").OrderBy(go => go.name).ToArray();
		puntosDolor = GameObject.FindGameObjectsWithTag("MuestraInfoDolor").OrderBy(go => go.name).ToArray();
		
		nombresAplicacion = new List<string>();
		nombresDolor = new List<string>();
		
		foreach (var zonas in puntosAlicacion)
		{
			nombresAplicacion.Add(zonas.name);
		}
		
		
		foreach (var zonas in puntosDolor)
		{
			nombresDolor.Add(zonas.name);
		}
	}

	// Update is called once per frame
	void Update ()
	{

#if UNITY_EDITOR
		
		if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) //Para que no se ejecute el for de manera innecesaria
		{
			Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition); //Que esta tocando el toque
			
			RaycastHit hit;
	
			if (Physics.Raycast(ray, out hit, touchInputMask))
			{
				//Si estamos tocando un objecto, obtener referencia de ese objeto
				GameObject puntoTocado = hit.transform.gameObject;
				tituloAplicacion.text = puntoTocado.name;
				
				if (puntoTocado.tag == "MuestraInfo")
				{
					ObjetoSeleccionado(puntoTocado, "Apli", puntoTocado.name);
					int i = 0;
					foreach (var zona in puntosAlicacion)
					{
						if (puntoTocado == zona)
						{
							infoAplicacion.text = infoPuntosAplicacion[i];
							//Cambiar info general
							setInfoGeneral(i);
							_panelCarasModelo.SetActive(false);
						}

						i++;
					}
				}
				else
				{
					ObjetoSeleccionado(puntoTocado, "Dolor", puntoTocado.name);
					int i = 0;
					foreach (var zona in puntosDolor)
					{
						if (puntoTocado == zona)
						{
							infoAplicacion.text = infoPuntosDolor[i];
							
							SetInfoDolor(i);
							_panelCarasModelo.SetActive(false);
						}

						i++;
					}
				}
			}
			/*else
			{
				LimpiarSeleccion();
			}*/
		}
#endif

		if (Input.touchCount > 0) //Para que no se ejecute el for de manera innecesaria
		{
			foreach (Touch touch in Input.touches)
			{
				Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition); //Que esta tocando el toque
				RaycastHit hit;
	
				if (Physics.Raycast(ray, out hit, touchInputMask))
				{
					//Si estamos tocando un objecto, obtener referencia de ese objeto
					GameObject puntoTocado = hit.transform.gameObject;
					tituloAplicacion.text = puntoTocado.name;
					
					
					if (puntoTocado.tag == "MuestraInfo")
					{
						ObjetoSeleccionado(puntoTocado, "Apli", puntoTocado.name);
						int i = 0;
						foreach (var zona in puntosAlicacion)
						{
							if (puntoTocado == zona)
							{
								infoAplicacion.text = infoPuntosAplicacion[i];
								
								//Cambiar info general
								setInfoGeneral(i);
								_panelCarasModelo.SetActive(false);
							}

							i++;
						}
					}
					else
					{
						ObjetoSeleccionado(puntoTocado, "Dolor", puntoTocado.name);
						for (int i = 0; i < puntosDolor.Length; i++)
						{
							if (puntoTocado == puntosDolor[i])
							{
								infoAplicacion.text = infoPuntosDolor[i];

								SetInfoDolor(i);
								_panelCarasModelo.SetActive(false);
							}
						}
					}
				}
			}
		}
	}

	/**
	 * Frontales:
	 * Corrugadores [8], [9]
	 * Procer [27]
	 * Supraorbitario [28-29]
	 * Supratoclear [30-31]
	 *
	 * Laterales:
	 * Auriculotemporales [0-7]
	 *
	 * Posteriores:
	 * Inion [10]
	 * Nervios occipitales [11-20]
	 *
	 * Supraclavicular:
	 * Nervios supraclaviculares [21-26]
	 */
	//Oorden del string[] --> Frontal, lateral, posterior, supraclavicular
	private void setInfoGeneral(int puntoSelected)
	{
		if (puntoSelected <= 7)
		{
			infoCarasModelo.text = infoGeneral[1];
		} else if (puntoSelected == 8 || puntoSelected == 9)
		{
			infoCarasModelo.text = infoGeneral[0];
		}
		else if (puntoSelected > 9 && puntoSelected <= 20)
		{
			infoCarasModelo.text = infoGeneral[2];
		} else if (puntoSelected > 20 && puntoSelected <= 26)
		{
			infoCarasModelo.text = infoGeneral[3];
		}
		else if (puntoSelected > 26)
		{
			infoCarasModelo.text = infoGeneral[0];
		}
	}

	private void SetInfoDolor(int puntoSelected)
	{
		if (puntoSelected <= 3)
		{
			infoCarasModelo.text = "Recordar no aplicar sobre los puntos previos.";
		} else if (puntoSelected > 3 && puntoSelected <= 5)
		{
			infoCarasModelo.text = "Administrar por encima de los aplicados previamente.";
		} else if (puntoSelected > 5)
		{
			infoCarasModelo.text = "No administrar sobre puntos previamente aplicados.";
		}
	}

	void ObjetoSeleccionado(GameObject puntoTocado, string clasePunto, string nombrePunto)
	{
		if (_puntoSeleccionado != null)
		{
			if (puntoTocado == _puntoSeleccionado)
			{
				return;
			}
			LimpiarSeleccion();
		}

		_puntoSeleccionado = puntoTocado;
		_infoSeleccion = clasePunto;
		_nombrePunto = nombrePunto;

		Renderer[] renderers = _puntoSeleccionado.GetComponentsInChildren<Renderer>();

		foreach (var renderer in renderers)
		{
			Material material = renderer.material;
			material.color = Color.green;
			renderer.material = material;
		}
	}

	void LimpiarSeleccion()
	{
		if (_puntoSeleccionado == null)
		{
			return;
		}
		//Regresar a su color original
		Renderer[] renderers = _puntoSeleccionado.GetComponentsInChildren<Renderer>();

		if (_infoSeleccion == "Apli")
		{
			foreach (Renderer renderer in renderers)
			{
				Material material = renderer.material;
				//material.color = new Color(96, 23, 154); //Color aplicacion
				material.color = Color.HSVToRGB((float)0.7595, (float)0.8506, (float)0.6039);
				if (_nombrePunto == "Inion")
				{
					material.color = Color.cyan;
				}
				renderer.material = material;
			}
		}
		else
		{
			foreach (Renderer renderer in renderers)
			{
				Material material = renderer.material;
				//material.color = new Color(214, 9, 15); //Color dolor
				material.color = Color.red;
				renderer.material = material;
			}
		}
	}
}
