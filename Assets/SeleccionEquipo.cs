using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SeleccionEquipo : MonoBehaviour
{
    CambioMaterial scriptCambio;
    public GameObject particlesMama;

    List<string> lista = new List<string>();
    List<string> listaCorrectos = new List<string>();
    List<string> listaIncorrectos = new List<string>();

    public bool hizoActividad = false;

    private void Awake()
    {
        scriptCambio = FindObjectOfType<CambioMaterial>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {      
        if(other.gameObject.tag=="ActividadEquipos" && !lista.Contains(other.gameObject.name))
        {
            hizoActividad = true;
            lista.Add(other.gameObject.name);
            scriptCambio.objetosSeleccionados.Add(other.gameObject.name);
            if (scriptCambio.objetosSeleccionados.Count == 1) GameObject.Find("BtnSiguienteEquipos").AddComponent(typeof(ActividadEquipos));

            if (scriptCambio.objetosRequeridos.Contains(other.gameObject.name))
            {
                listaCorrectos.Add(other.gameObject.name);
            }
            else listaIncorrectos.Add(other.gameObject.name);

            if (listaIncorrectos.Count == 0) GameObject.Find("Preparados").GetComponent<TextMeshProUGUI>().text = "" + listaCorrectos.Count + "/" + scriptCambio.objetosRequeridos.Count + " elementos preparados correctamente";
            else
            {
                if(listaIncorrectos.Count == 1) GameObject.Find("Preparados").GetComponent<TextMeshProUGUI>().text = "" + listaIncorrectos.Count + " elemento preparado incorrectamente";
                else GameObject.Find("Preparados").GetComponent<TextMeshProUGUI>().text = "" + listaIncorrectos.Count + " elementos preparados incorrectamente";
            }

            if (listaCorrectos.Count == scriptCambio.objetosRequeridos.Count && listaIncorrectos.Count == 0)
            {
                GameObject.Find("BtnSiguienteEquipos").GetComponent<ActividadEquipos>().validarElementosPreparados();
                particlesMama.SetActive(true);
                GameObject.Find("FlechaEquipo").SetActive(false);
            }
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        lista.Remove(other.gameObject.name);
        scriptCambio.objetosSeleccionados.Remove(other.gameObject.name);

        if (scriptCambio.objetosRequeridos.Contains(other.gameObject.name)) listaCorrectos.Remove(other.gameObject.name);
        else listaIncorrectos.Remove(other.gameObject.name);

        if (listaIncorrectos.Count == 0) GameObject.Find("Preparados").GetComponent<TextMeshProUGUI>().text = "" + listaCorrectos.Count + "/" + scriptCambio.objetosRequeridos.Count + " elementos preparados correctamente";
        else
        {
            if (listaIncorrectos.Count == 1) GameObject.Find("Preparados").GetComponent<TextMeshProUGUI>().text = "" + listaIncorrectos.Count + " elemento preparado incorrectamente";
            else GameObject.Find("Preparados").GetComponent<TextMeshProUGUI>().text = "" + listaIncorrectos.Count + " elementos preparados incorrectamente";
        }

        if (listaCorrectos.Count == scriptCambio.objetosRequeridos.Count && listaIncorrectos.Count == 0) GameObject.Find("BtnSiguienteEquipos").GetComponent<ActividadEquipos>().validarElementosPreparados();
    }
}
