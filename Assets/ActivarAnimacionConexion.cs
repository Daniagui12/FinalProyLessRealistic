using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarAnimacionConexion : MonoBehaviour
{
    bool permitido = false;
    DialogueViewer1 viewer;
    int contador = 0;

    private void Awake()
    {
        viewer = FindObjectOfType<DialogueViewer1>();
    }
    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name== "GrabVolumeSmallRight" || other.gameObject.name == "GrabVolumeSmallLeft")
        {
            if (contador == 0)
            {
                contador++;
                viewer.ChangeNode();
            }
        }
    }
}
