using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoSonda : MonoBehaviour
{
    public GameObject sondaPlug;
    public GameObject sondaCompleta;
    void Update()
    {
        if(gameObject.GetComponent<OVRGrabbable>().isGrabbed)
        {
            sondaPlug.SetActive(true);
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            sondaCompleta.SetActive(false);
            sondaCompleta.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
        }
        else
        {
            sondaPlug.SetActive(false);
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            gameObject.transform.GetChild(0).transform.gameObject.SetActive(false);
            sondaCompleta.SetActive(true);

            //Se reacomoda a la posicion original
            gameObject.transform.localPosition = new Vector3(-49.6008f, 2.4276f, -23.5876f);
            gameObject.transform.localRotation = Quaternion.Euler(-1.467f, 85.82001f, -92.118f);
        }
    }
}
