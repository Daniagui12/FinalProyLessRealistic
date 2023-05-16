using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActividadLavadaManos : MonoBehaviour
{
    public TextMeshProUGUI texto;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {            
            GameObject.Find("Lavamanos").transform.GetChild(0).gameObject.SetActive(true);
            if(GameObject.Find("FlechaBio")!=null) GameObject.Find("FlechaBio").SetActive(false);
            StartCoroutine(Recordatorio());
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("Lavamanos").transform.GetChild(0).gameObject.SetActive(false);
        }

    }

    IEnumerator Recordatorio()
    {
        yield return new WaitForSeconds(3f);
        if (!GameObject.Find("ColliderLavamanosHijo").GetComponent<ActividadBioseguridad>().entraronManos)
        {
            texto.text = "Coloca las manos en el agua";
        }
    }
}
