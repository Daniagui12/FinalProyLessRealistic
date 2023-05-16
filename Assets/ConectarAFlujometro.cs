using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConectarAFlujometro : MonoBehaviour
{
    void Update()
    {
        if(gameObject.name.Contains("Neotee"))
        {
            if (gameObject.GetComponent<OVRGrabbable>().isGrabbed)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.transform.localPosition = new Vector3(13.25f, 15.51f, -2.01f);
                gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 174.356f);

            }
        }
        else
        {
            //PARA PREPARAR LA BOLSA AUTOINFLABLE
            if (gameObject.GetComponent<OVRGrabbable>().isGrabbed)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("BolsaAutoinflable").transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                gameObject.transform.GetChild(2).gameObject.SetActive(true);
                GameObject.Find("BolsaAutoinflable").transform.GetChild(0).gameObject.SetActive(false);
                gameObject.transform.localPosition = new Vector3(13.25002f, 15.51f, -2.010001f);
                gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);

            }
        }
        
    }
}
