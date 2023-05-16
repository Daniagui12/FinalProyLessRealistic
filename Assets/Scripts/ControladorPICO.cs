using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPICO : MonoBehaviour
{
    public int cantidadPICO = 0;
    public int cantidadPICORefuerzo = 0;
    public bool tapoHueco = false;
    
    void IncrementarValorVPP()
    {
        if (FindObjectOfType<ControllerBaby>() != null)
        {
            ControllerBaby baby = FindObjectOfType<ControllerBaby>();
            if (baby.seVentiloBebe && (baby.pasoNeopuff || baby.pasoNeotee) && baby.estaEnIntervencion) cantidadPICO++;
        }
    }

    void IncrementarIntentoDarVPP()
    {
        if (FindObjectOfType<ControllerBaby>() != null)
        {
            ControllerBaby baby = FindObjectOfType<ControllerBaby>();
            if (baby.seVentiloBebe && (baby.pasoNeopuff || baby.pasoNeotee) && baby.estaEnIntervencion) tapoHueco = true;
        }
    }

    public void ResetearInformacion()
    {
        cantidadPICO = 0;
        tapoHueco = false;
    }

    private void Update()
    {
        if(FindObjectOfType<DialogueViewer1>()!=null && FindObjectOfType<ControllerBaby>() != null)
        {
            DialogueViewer1 viewer1 = FindObjectOfType<DialogueViewer1>();
            ControllerBaby baby = FindObjectOfType<ControllerBaby>();
            if ((viewer1.lastNodeTitle== "Finalizar Video Neotee" || viewer1.lastNodeTitle == "Finalizar Video Neopuff" || viewer1.lastNodeTitle == "Ordenar realizar VPP tercera parte"))
            {
                if (cantidadPICO == 3 && baby.estaEnIntervencion) viewer1.ChangeNode();
            }
        }
    }
}
