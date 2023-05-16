using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReconocerSiCae : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Gorro" && GameObject.Find("CanvasTwine").GetComponent<DialogueViewer1>().lastNodeTitle== "Paso Gorro caso 4")
        {
            GameObject.Find("CanvasTwine").GetComponent<DialogueViewer1>().ChangeNode();
        }
        if (other.gameObject.tag == "equipos")
        {
            StartCoroutine(ProcesoPorSiSeCae(other, new Vector3(-51.933f, 2.826f, -24.288f)));
        }
        else if(other.gameObject.name == "baby" && GameObject.Find("CanvasTwine").GetComponent<DialogueViewer1>().lastNodeTitle == "Ordenar pinzamiento habitual del cordón umbilical")
        {
            if(SceneManager.GetActiveScene().name == "HospitalActualizado") StartCoroutine(ProcesoPorSiSeCae(other, new Vector3(27.81001f, -5.17f, -0.890001f)));
            else if (SceneManager.GetActiveScene().name == "HospitalPC") StartCoroutine(ProcesoPorSiSeCae(other, new Vector3(27.81001f, -5.17f, -0.890001f)));
            Debug.Log("EL BEBE SE ACOMODO PORQUE SE CAYÓ calentador");
        }
        else if (other.gameObject.name == "baby" && GameObject.Find("CanvasTwine").GetComponent<DialogueViewer1>().lastNodeTitle == "Trasladar al bebé con madre")
        {
            StartCoroutine(ProcesoPorSiSeCae(other, new Vector3(-0.5999959f, 0.1000022f, 0.08000135f)));
            other.gameObject.transform.localRotation = Quaternion.Euler(-90f, 90f, 0f);
            Debug.Log("EL BEBE SE ACOMODO PORQUE SE CAYÓ trasladar");
        }
    }

    IEnumerator ProcesoPorSiSeCae(Collider other, Vector3 vector)
    {

        other.gameObject.GetComponent<Rigidbody>().constraints= RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(1);
        Destroy(other.gameObject.GetComponent<Rigidbody>());
        yield return new WaitForSeconds(1);
        other.gameObject.transform.localPosition = vector;
        yield return new WaitForSeconds(1);
        other.gameObject.AddComponent<Rigidbody>();
        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }
}


