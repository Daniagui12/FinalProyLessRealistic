using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoNeotee : MonoBehaviour
{
    public GameObject extremoPICO;
    public GameObject neoteeCompleto;
    public GameObject colliderMascara;
    public bool pasoMascara = false;

    public GameObject duplicado;
    void Update()
    {
        if (gameObject.GetComponent<OVRGrabbable>() != null)
        {
            if (gameObject.GetComponent<OVRGrabbable>().isGrabbed)
            {
                
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                extremoPICO.SetActive(true);
                neoteeCompleto.SetActive(false);
                GameObject.Find("SK_NeoTee_B").transform.GetChild(0).transform.gameObject.SetActive(true);
                GameObject.Find("SK_NeoTee_B").transform.GetChild(1).transform.gameObject.SetActive(true);

                if(duplicado!=null) duplicado.SetActive(true);
            }
            else
            {
                extremoPICO.SetActive(false);
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                neoteeCompleto.SetActive(true);
                GameObject.Find("SK_NeoTee_B").transform.GetChild(0).transform.gameObject.SetActive(false);
                GameObject.Find("SK_NeoTee_B").transform.GetChild(1).transform.gameObject.SetActive(false);

                //Se reacomoda a la posicion original
                gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
                gameObject.transform.localRotation = Quaternion.Euler(-0f, 0f, -0f);

                if (duplicado != null) duplicado.SetActive(false);
            }
        }

        if (pasoMascara) colliderMascara.SetActive(true);
    }

    public void CambiarAPasoMascara(string valor)
    {
        pasoMascara = bool.Parse(valor);
    }
}
