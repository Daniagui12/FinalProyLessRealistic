using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarAnimacion1 : MonoBehaviour
{
    DialogueViewer1 viewer;

    private void Awake()
    {
        viewer = FindObjectOfType<DialogueViewer1>();
    }
    void Update()
    {
        

        if (gameObject.GetComponent<OVRGrabbable>().isGrabbed && OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && viewer.lastNodeTitle== "Subir presion blender")
        {
            gameObject.GetComponent<Animator>().SetTrigger("Animacion100");
            if (GameObject.Find("baby") != null)
            {
                if (GameObject.Find("baby").GetComponent<ControllerBaby>().pasoBolsa) GameObject.Find("baby").GetComponent<Animator>().SetTrigger("Animacion");
            }
        }
        else if (gameObject.GetComponent<OVRGrabbable>().isGrabbed && OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger) && viewer.lastNodeTitle == "Subir presion blender")
        {
            gameObject.GetComponent<Animator>().SetTrigger("Desactivar100");
            if (GameObject.Find("baby") != null)
            {
                if (GameObject.Find("baby").GetComponent<ControllerBaby>().pasoBolsa) GameObject.Find("baby").GetComponent<Animator>().SetTrigger("Desactivar");
            }
        }

        else if (gameObject.GetComponent<OVRGrabbable>().isGrabbed && OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            gameObject.GetComponent<Animator>().SetTrigger("Animacion");
            if(GameObject.Find("baby")!=null)
            {
                if (GameObject.Find("baby").GetComponent<ControllerBaby>().pasoBolsa) GameObject.Find("baby").GetComponent<Animator>().SetTrigger("Animacion");
            }
        }
        else if(gameObject.GetComponent<OVRGrabbable>().isGrabbed && OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
        {
            gameObject.GetComponent<Animator>().SetTrigger("Desactivar");
            if (GameObject.Find("baby") != null)
            {
                if (GameObject.Find("baby").GetComponent<ControllerBaby>().pasoBolsa) GameObject.Find("baby").GetComponent<Animator>().SetTrigger("Desactivar");
            }
        }
    }

    public void CambiarAnimacion()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Animacion");
    }
}
