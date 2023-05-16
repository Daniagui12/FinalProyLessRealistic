using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NuevaSeleccionEquipos : MonoBehaviour
{
    CambioMaterial scriptCambio;
    Audio inicio;
    bool seDijoRecordatorioIncorrectos = false;

    private void Awake()
    {
        inicio = FindObjectOfType<Audio>();
        scriptCambio = FindObjectOfType<CambioMaterial>();
    }

    void Update()
    {
        if (gameObject.GetComponent<OVRGrabbable>().isGrabbed && OVRInput.GetDown(OVRInput.RawButton.A))
        {
            SeleccionarElemento();
        }
    }

    private void SeleccionarElemento()
    {
        if (!gameObject.GetComponent<ActividadEquipos>().fueSeleccionado) gameObject.GetComponent<ActividadEquipos>().fueSeleccionado = true;
        else gameObject.GetComponent<ActividadEquipos>().fueSeleccionado = false;
        GameObject.Find("NombreHerramientaActual").GetComponent<TextMeshProUGUI>().text = gameObject.name;
        
        if (gameObject.GetComponent<ActividadEquipos>().fueSeleccionado) AñadirAPreparados(gameObject.GetComponent<Collider>());
        else EliminarDePreparados(gameObject.GetComponent<Collider>());
    }

    private void AñadirAPreparados(Collider other)
    {
        if (other.gameObject.tag == "ActividadEquipos" && !inicio.listaNueva.Contains(other.gameObject.name))
        {
            //Como ya selecciono algo entonces se destruye la flecha
            GameObject.Find("FlechaSeleccionEquipos").transform.GetChild(0).gameObject.SetActive(false);

            inicio.listaNueva.Add(other.gameObject.name);
            scriptCambio.objetosSeleccionados.Add(other.gameObject.name);
            if (scriptCambio.objetosSeleccionados.Count == 1) GameObject.Find("BtnSiguienteEquipos").AddComponent(typeof(ActividadEquipos));

            if (scriptCambio.objetosRequeridos.Contains(other.gameObject.name))
            {
                inicio.listaCorrectos.Add(other.gameObject.name);
                CambiarMaterial(inicio.seleccionado);
            }
            else
            {
                inicio.listaIncorrectos.Add(other.gameObject.name);
                CambiarMaterial(inicio.incorrectoMat);
                if (inicio.listaIncorrectos.Count == 1 && !seDijoRecordatorioIncorrectos && GameObject.Find("Particle System equipo")==null)
                {
                    seDijoRecordatorioIncorrectos = true;
                    inicio.audioData.enabled = false;
                    inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/RecodatorioRevisarIncorrectos");
                    inicio.ovrLipsync.enabled = false;
                    inicio.ovrLipsync.enabled = true;
                    inicio.audioData.enabled = true;
                }
            }

            if (inicio.listaIncorrectos.Count == 0)
            {
                GameObject.Find("Preparados").GetComponent<TextMeshProUGUI>().text = "" + inicio.listaCorrectos.Count + "/" + scriptCambio.objetosRequeridos.Count + " elementos preparados correctamente";
            
            }
            else
            {
                if (inicio.listaIncorrectos.Count == 1) GameObject.Find("Preparados").GetComponent<TextMeshProUGUI>().text = "" + inicio.listaIncorrectos.Count + " elemento preparado incorrectamente";
                else GameObject.Find("Preparados").GetComponent<TextMeshProUGUI>().text = "" + inicio.listaIncorrectos.Count + " elementos preparados incorrectamente";
            }

            if (inicio.listaCorrectos.Count == scriptCambio.objetosRequeridos.Count && inicio.listaIncorrectos.Count == 0)
            {
                GameObject.Find("BtnSiguienteEquipos").GetComponent<ActividadEquipos>().validarElementosPreparados();
                GameObject.Find("FlechaEquipo").SetActive(false);
            }

        }
    }

    private void EliminarDePreparados(Collider other)
    {
        inicio.listaNueva.Remove(other.gameObject.name);
        if (scriptCambio.objetosSeleccionados.Contains(other.gameObject.name)) scriptCambio.objetosSeleccionados.Remove(other.gameObject.name);

        if (scriptCambio.objetosRequeridos.Contains(other.gameObject.name)) inicio.listaCorrectos.Remove(other.gameObject.name);
        else inicio.listaIncorrectos.Remove(other.gameObject.name);
        CambiarMaterial(inicio.normal);

        if (inicio.listaIncorrectos.Count == 0) GameObject.Find("Preparados").GetComponent<TextMeshProUGUI>().text = "" + inicio.listaCorrectos.Count + "/" + scriptCambio.objetosRequeridos.Count + " elementos preparados correctamente";
        else
        {
            if (inicio.listaIncorrectos.Count == 1) GameObject.Find("Preparados").GetComponent<TextMeshProUGUI>().text = "" + inicio.listaIncorrectos.Count + " elemento preparado incorrectamente";
            else GameObject.Find("Preparados").GetComponent<TextMeshProUGUI>().text = "" + inicio.listaIncorrectos.Count + " elementos preparados incorrectamente";
            
        }

        if (inicio.listaCorrectos.Count == scriptCambio.objetosRequeridos.Count && inicio.listaIncorrectos.Count == 0)
            GameObject.Find("BtnSiguienteEquipos").GetComponent<ActividadEquipos>().validarElementosPreparados();

    }


    private void CambiarMaterial(Material materialObjeto)
    {
        int cantHijos = gameObject.transform.childCount;
        for (int j = 0; j < cantHijos; j++)
        {
            if (gameObject.transform.GetChild(j).name == "Outline")
            {
                if (gameObject.transform.GetChild(j).GetComponent<MeshRenderer>() != null) gameObject.transform.GetChild(j).GetComponent<MeshRenderer>().material = materialObjeto;
                else
                {
                    Material[] mats = gameObject.transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().materials;
                    mats[0] = materialObjeto;
                    gameObject.transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().materials = mats;
                }
            }
        }
    }
}
