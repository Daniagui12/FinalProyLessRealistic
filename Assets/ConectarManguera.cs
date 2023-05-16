using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConectarManguera : MonoBehaviour
{
    DialogueViewer1 viewer;

    private void Awake()
    {
        viewer = FindObjectOfType<DialogueViewer1>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "SK_BolsaAuto_Tip" || other.gameObject.name == "SK_Neotee_Tip" || other.gameObject.name == "SK_Neopuff_Tip")
        {            
            viewer.ChangeNode();
        }

    }
}
