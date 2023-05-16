using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolsaAutoinflable : MonoBehaviour
{
    public DialogueViewer1 viewer;
    public bool pasoMascarita = false;


    public void ActivarAnimacion(string trigger)
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger(trigger);
    }

    public void cambiarPasoMascaritaATrue()
    {
        pasoMascarita = true;
    }
    

    public void cambiarPasoMascaritaAFalse()
    {
        pasoMascarita = false;
    }

}
