using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConectaTipAFlujometro : MonoBehaviour
{
    DialogueViewer1 viewer;

    private void Awake()
    {
        viewer = FindObjectOfType<DialogueViewer1>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BolsaAuto_A" || other.gameObject.name.Contains("Neotee") || other.gameObject.name.Contains("Neopuff"))
        {
            viewer = FindObjectOfType<DialogueViewer1>();
            viewer.ChangeNode();
            
        }
    }
}
