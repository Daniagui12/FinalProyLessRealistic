using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctora_Controller : MonoBehaviour
{
    public DialogueViewer1 viewer;

    // Start is called before the first frame update
    public void pasarNodo()
    {
        if(viewer.lastNodeTitle!= "Ordenar pinzamiento inmediato del cordón umbilical" && viewer.lastNodeTitle != "Respuesta correcta pregunta cordon" &&
            viewer.lastNodeTitle != "Respuesta incorrecta2 pregunta cordon" && viewer.lastNodeTitle != "Respuesta incorrecta1 pregunta cordon") viewer.ChangeNode();
    }

    public void cortarCordon()
    {
        GameObject.Find("SK_Cordon").GetComponent<Animator>().SetTrigger("Corte");
    }
}
