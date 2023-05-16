using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RetroalimentacionDispositivos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.name == "GrabVolumeSmallRight" || other.gameObject.name == "GrabVolumeSmallLeft") && gameObject.name != "Boton agregar" && gameObject.name != "BtnSiguienteEquipos")
        {
            if(GameObject.Find("NombreHerramientaActual")!=null) GameObject.Find("NombreHerramientaActual").GetComponent<TextMeshProUGUI>().text = gameObject.name;
        }
    }

    private void Update()
    {
        if(gameObject.GetComponent<OVRGrabbable>().isGrabbed)
        {
            if (GameObject.Find("NombreHerramientaActual") != null) GameObject.Find("NombreHerramientaActual").GetComponent<TextMeshProUGUI>().text = gameObject.name;
        }
    }
}
