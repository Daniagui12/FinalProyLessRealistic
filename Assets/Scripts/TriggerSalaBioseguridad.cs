using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSalaBioseguridad : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            Destroy(GameObject.Find("FlechaInicio"));
            Destroy(GameObject.Find("RecordatorioInicioPadre"));
            Destroy(gameObject.GetComponent<BoxCollider>());
            if(!FindObjectOfType<Inicio>().saltarCaso) GameObject.Find("RecordatorioAntesCaso").transform.GetChild(0).gameObject.SetActive(true);
            if(GameObject.Find("ColliderInicioCaso").GetComponent<BoxCollider>()!=null)
                FindObjectOfType<Inicio>().InicializarEjercicio();
        }
    }
}
