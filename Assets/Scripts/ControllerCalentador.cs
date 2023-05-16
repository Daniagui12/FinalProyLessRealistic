using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerCalentador : MonoBehaviour
{
    public DialogueViewer1 viewer;
    bool pasoCalentador = false;
    bool entro = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "baby" && pasoCalentador && !entro && GameObject.Find("CanvasTwine").GetComponent<DialogueViewer1>().lastNodeTitle != "Trasladar al bebé con madre" && GameObject.Find("CanvasTwine").GetComponent<DialogueViewer1>().lastNodeTitle != "Bebé en contacto con madre")
        {

            GameObject.Find("Baby").transform.localPosition = new Vector3(-15.196f, 7.518951f, 21.25324f);
            GameObject.Find("Baby").transform.localRotation = Quaternion.Euler(-192.483f, 179.56f, -2.119995f);

            Destroy(GameObject.Find("Baby").GetComponent<OVRGrabbable>());
            Destroy(GameObject.Find("Baby").GetComponent<Rigidbody>());
            Destroy(GameObject.Find("Baby").GetComponent<BoxCollider>());
                        

            entro = true;
            viewer.ChangeNode();
            
        }

    }


    public void cambiarAPasoCalentador()
    {
        if (pasoCalentador) pasoCalentador = false;
        else pasoCalentador = true;
    }
}
