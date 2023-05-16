using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMinutoOro : MonoBehaviour
{
    Audio inicio;
    int contador = 0;
    private void Awake()
    {
        inicio = FindObjectOfType<Audio>();
    }
    // Start is called before the first frame update
    public DialogueViewer1 viewer;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            viewer.ChangeNode();
            Destroy(GameObject.Find("ControllerSaludo"));
            Destroy(gameObject);
        }
    }


    public bool estaCercaDeMadre()
    {
        return false;
    }

}
