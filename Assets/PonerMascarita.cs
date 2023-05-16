using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PonerMascarita : MonoBehaviour
{
    public DialogueViewer1 viewer;
    public bool pasoMascarita = false;

    private void Awake()
    {
        viewer = FindObjectOfType<DialogueViewer1>();
    }

    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        print("ENTRA OBJECTO " + other.gameObject.name);
        if (other.gameObject.name == "MascaritaP")
        {
            viewer = FindObjectOfType<DialogueViewer1>();
            Destroy(GameObject.Find("MascaritaP"));
           viewer.ChangeNode();

        }
        else if(other.gameObject.name == "Mascara neonatal recién nacido a términoP" && pasoMascarita)
        {
            viewer = FindObjectOfType<DialogueViewer1>();

            if (GameObject.Find("ControllerTwine") != null) GameObject.Find("ControllerTwine").GetComponent<ControllerTwine>().pusoMascaritaTutorial = true;
            GameObject.Find("Bolsa autoinflableP").transform.GetChild(1).transform.gameObject.SetActive(true);
            viewer.ChangeNode();
            Destroy(GameObject.Find("Mascara neonatal recién nacido a términoP"));
            
        }
    }

    public void PasoPonerMascarita()
    {
        pasoMascarita = true;
    }

    public void ActivarAnimacion(string trigger)
    {
        gameObject.GetComponent<Animator>().SetTrigger(trigger);
    }

    public void cambiarPasoMascarita(string puede)
    {
        pasoMascarita = bool.Parse(puede);
    }
}
