using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorVPP : MonoBehaviour
{
    public int cantidadVPP = 0;

    public void IncrementarValorVPP()
    {
        if (FindObjectOfType<ControllerBaby>() != null)
        {
            ControllerBaby baby = FindObjectOfType<ControllerBaby>();
            //si la bolsa esta en posición y esta en el proceso de ventilación, sube la cantidad
            if (baby.seVentiloBebe && (baby.pasoBolsa) && baby.estaEnIntervencion) cantidadVPP++;
        }
    }


    public void ResetearInformacion()
    {
        cantidadVPP = 0;
    }

    private void Update()
    {
        if (FindObjectOfType<DialogueViewer1>() != null && FindObjectOfType<ControllerBaby>() != null)
        {
            DialogueViewer1 viewer1 = FindObjectOfType<DialogueViewer1>();
            ControllerBaby baby = FindObjectOfType<ControllerBaby>();
            if ((viewer1.lastNodeTitle == "Finalizar Video bolsa" || viewer1.lastNodeTitle == "Ordenar realizar VPP tercera parte"))
            {
                if (cantidadVPP == 3 && baby.estaEnIntervencion) viewer1.ChangeNode();
            }
        }
    }
}
