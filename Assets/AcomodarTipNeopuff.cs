using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcomodarTipNeopuff : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if(gameObject.GetComponent<OVRGrabbable>().isGrabbed)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("GuiaConexionTipNeopuff").transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            GameObject.Find("GuiaConexionTipNeopuff").transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.localPosition = new Vector3(21.273f, 19.706f, -1.719f);
            gameObject.transform.localRotation = Quaternion.Euler(16.787f, -901.229f, 171.688f);
        }
    }
}
