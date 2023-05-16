using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConectarMascaritaNeotee : MonoBehaviour
{
    DialogueViewer1 viewer;

    private void Awake()
    {
        viewer = FindObjectOfType<DialogueViewer1>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MascaritaP")
        {            
            Destroy(GameObject.Find("MascaritaP"));

            viewer.ChangeNode();
        }
        else if(other.gameObject.name == "MascaritaPNeopuff") viewer.ChangeNode();
    }
}
