using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAspiracion : MonoBehaviour
{

    public DialogueViewer1 viewer;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Perilla De Goma" && FindObjectOfType<ControllerBaby>().pasoPera)
        {
            if(gameObject.name== "cc_game_body") FindObjectOfType<ControllerBaby>().seAspiroBocaBebe = true;
            if(gameObject.name == "cc_game_tongue") FindObjectOfType<ControllerBaby>().seAspiroNarizBebe = true;
        }

    }
}
