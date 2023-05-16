using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPasarNodo : MonoBehaviour
{
    public DialogueViewer1 viewer;
    int cantidad = 0;
    float transcurrido = 0.0f;

    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "GrabVolumeSmallLeft" || other.gameObject.name == "GrabVolumeBig")
        {
            if (cantidad == 0)
            {
                cantidad++;
                viewer.ChangeNode();
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "GrabVolumeSmallLeft" || other.gameObject.name == "GrabVolumeBig")
        {
            if (FindObjectOfType<ControllerBaby>().pasoBolsa)
            {
                transcurrido += Time.fixedDeltaTime;
                if (transcurrido > 5.0f)
                {
                    // FindObjectOfType<ControllerBaby>().sello;
                }
            }
        }
    }
}
