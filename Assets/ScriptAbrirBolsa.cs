using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAbrirBolsa : MonoBehaviour
{
    public DialogueViewer1 viewer;


    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "GrabVolumeSmallRight" || other.gameObject.name == "GrabVolumeBig")
        {            
            viewer.ChangeNode();
            Destroy(gameObject);
        }
    }
}
