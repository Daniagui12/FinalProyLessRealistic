using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInicioPreparacion : MonoBehaviour
{
    private DialogueViewer1 viewer;

    private void Awake()
    {
        viewer = FindObjectOfType<DialogueViewer1>();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            viewer.ChangeNode();
            Destroy(gameObject);            
        }
    }
}
