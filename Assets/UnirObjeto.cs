using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnirObjeto : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name=="baby")  
        {
            if(SceneManager.GetActiveScene().name=="HospitalPC")
            {
                if (GameObject.Find("CanvasTwine").GetComponent<DialogueViewer1>().lastNodeTitle == "Trasladar al bebé con madre")
                {
                    Debug.Log("EL BEBE SE ACOMODO POR UNIR OBJETO");
                    other.gameObject.transform.localPosition = new Vector3(-0.5999959f, 0.1000022f, 0.08000135f);
                    other.gameObject.transform.localRotation = Quaternion.Euler(-90f, 90f, 0f);
                }
                else if (other.gameObject.GetComponent<DragObject>() != null)
                {
                    if (GameObject.Find("CanvasTwine").GetComponent<DialogueViewer1>().lastNodeTitle == "Ordenar pinzamiento habitual del cordón umbilical")
                    {
                        other.gameObject.transform.localRotation = Quaternion.Euler(-270f, 90f, 0f);
                        other.gameObject.transform.localPosition = new Vector3(27.81001f, -5.17f, -0.890001f);
                    }

                    if (other.gameObject.name == "Gorro" && GameObject.Find("CanvasTwine").GetComponent<DialogueViewer1>().lastNodeTitle == "Bebé en contacto con el calentador") other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }
            else if(SceneManager.GetActiveScene().name == "HospitalActualizado")
            {
                if (GameObject.Find("CanvasTwine").GetComponent<DialogueViewer1>().lastNodeTitle == "Trasladar al bebé con madre")
                {
                    Debug.Log("EL BEBE SE ACOMODO POR UNIR OBJETO");
                    other.gameObject.transform.localPosition = new Vector3(-0.5999959f, 0.1000022f, 0.08000135f);
                    other.gameObject.transform.localRotation = Quaternion.Euler(-90f, 90f, 0f);
                }
                else if (other.gameObject.GetComponent<DragObject>() != null)
                {
                    if (GameObject.Find("CanvasTwine").GetComponent<DialogueViewer1>().lastNodeTitle == "Ordenar pinzamiento habitual del cordón umbilical")
                    {
                        other.gameObject.transform.localRotation = Quaternion.Euler(-270f, 90f, 0f);
                        other.gameObject.transform.localPosition = new Vector3(24.02f, -4.069994f, -2.212271f);
                    }

                    if (other.gameObject.name == "Gorro" && GameObject.Find("CanvasTwine").GetComponent<DialogueViewer1>().lastNodeTitle == "Bebé en contacto con el calentador") other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }
                     
        }
    }
}
