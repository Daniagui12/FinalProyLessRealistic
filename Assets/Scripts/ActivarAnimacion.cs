using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarAnimacion : MonoBehaviour
{
    int hizoPreparacionPera = 0;
    int hizoPreparacionBolsa = 0;

    public DialogueViewer1 viewer;

    private void Awake()
    {
        viewer = FindObjectOfType<DialogueViewer1>();
    }

    void Update()
    {
        if (gameObject.name == "Bolsa autoinflable" || gameObject.name == "Bolsa autoinflableP" || gameObject.name == "BolsaAutoinflableAnimacion")
        {
            if (GameObject.Find("baby") != null)
            {
                if((gameObject.GetComponent<OVRGrabbable>()!=null && gameObject.GetComponent<OVRGrabbable>().isGrabbed && OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)) 
                    || (GameObject.Find("baby").GetComponent<ControllerBaby>().bolsaEnContactoConBebe && OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))) 
                {
                    gameObject.GetComponent<Animator>().SetTrigger("Animacion");

                    if (GameObject.Find("baby").GetComponent<ControllerBaby>().cianotico && GameObject.Find("baby").GetComponent<ControllerBaby>().bolsaEnContactoConBebe)
                    {
                        GameObject.Find("baby").GetComponent<Animator>().SetTrigger("Animacion");
                        if (FindObjectOfType<ControladorVPP>() != null) FindObjectOfType<ControladorVPP>().IncrementarValorVPP();
                    }

                }
                else if ((gameObject.GetComponent<OVRGrabbable>() != null && gameObject.GetComponent<OVRGrabbable>().isGrabbed && OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
                    || (GameObject.Find("baby").GetComponent<ControllerBaby>().bolsaEnContactoConBebe && OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)))
                {
                    gameObject.GetComponent<Animator>().SetTrigger("Desactivar");
                    if (GameObject.Find("baby").GetComponent<ControllerBaby>().cianotico && GameObject.Find("baby").GetComponent<ControllerBaby>().bolsaEnContactoConBebe) GameObject.Find("baby").GetComponent<Animator>().SetTrigger("Desactivar");
                }
            }
            else
            {
                if (gameObject.GetComponent<OVRGrabbable>() != null)
                {
                    if (gameObject.GetComponent<OVRGrabbable>().isGrabbed && OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
                    {
                        gameObject.GetComponent<Animator>().SetTrigger("Animacion");
                        if (GameObject.Find("ControllerTwine").GetComponent<ControllerTwine>().pasoAnimacionBolsa) hizoPreparacionBolsa++;
                    }
                    else if (gameObject.GetComponent<OVRGrabbable>().isGrabbed && OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
                    {
                        gameObject.GetComponent<Animator>().SetTrigger("Desactivar");
                        if (GameObject.Find("ControllerTwine").GetComponent<ControllerTwine>().pasoAnimacionBolsa) hizoPreparacionBolsa++;
                    }
                }
            }
        }
        else if(gameObject.name == "Pera De Goma")
        {
            if (gameObject.GetComponent<OVRGrabbable>().isGrabbed && OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            {
                gameObject.GetComponent<Animator>().SetTrigger("Animacion");
                if (GameObject.Find("ControllerTwine").GetComponent<ControllerTwine>().pasoAnimacionPera) hizoPreparacionPera++;
            }
            else if (gameObject.GetComponent<OVRGrabbable>().isGrabbed && OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
            {
                gameObject.GetComponent<Animator>().SetTrigger("Desactivar");
                if (GameObject.Find("ControllerTwine").GetComponent<ControllerTwine>().pasoAnimacionPera) hizoPreparacionPera++;
            }
        }


        if((gameObject.name == "Pera De Goma" || gameObject.name == "Bolsa autoinflable" || gameObject.name == "Bolsa autoinflableP") && gameObject.GetComponent<OVRGrabbable>()!=null && !gameObject.GetComponent<OVRGrabbable>().isGrabbed)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Desactivar");
        }

        if(GameObject.Find("ControllerTwine")!=null)
        {
            if(GameObject.Find("ControllerTwine").GetComponent<ControllerTwine>().pasoAnimacionPera)
            {
                if (hizoPreparacionPera >= 2)
                {
                    if (GameObject.Find("ControllerTwine") != null) GameObject.Find("ControllerTwine").GetComponent<ControllerTwine>().terminoPasoPeraTutorial = true;
                    viewer.ChangeNode();
                }
            }

            if (GameObject.Find("ControllerTwine").GetComponent<ControllerTwine>().pasoAnimacionBolsa)
            {
                if (hizoPreparacionBolsa >= 2)
                {
                    if (GameObject.Find("ControllerTwine") != null) GameObject.Find("ControllerTwine").GetComponent<ControllerTwine>().terminoPasoBolsaTutorial = true;
                    viewer.ChangeNode();
                }
            }
        }
    }

    public void CambiarAnimacion()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Animacion");
    }
}
