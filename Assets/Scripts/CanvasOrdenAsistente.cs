using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CanvasOrdenAsistente : MonoBehaviour
{
    public DialogueViewer1 viewer;
    ControllerBaby baby;

    private void Awake()
    {
        viewer = FindObjectOfType<DialogueViewer1>();
        baby = FindObjectOfType<ControllerBaby>();
    }

    // Start is called before the first frame update
    public void OrdenarColocarSondaDeSuccion()
    {
        viewer.ChangeNode();
    }

    public void OrdenarColocarOximetro()
    {
        viewer.ChangeNode();  
    }

}
