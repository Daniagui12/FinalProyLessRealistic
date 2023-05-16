using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSecado : MonoBehaviour
{
    Inicio inicio;
    ControllerBaby controllerBaby;

    private void Awake()
    {
        inicio = FindObjectOfType<Inicio>();
        controllerBaby = FindObjectOfType<ControllerBaby>();
    }

    void Start()
    {
        InvokeRepeating("RecordarActividad", 10.0f, 10.0f);
    }

    void RecordarActividad()
    {
        if(!controllerBaby.estaEnContacto) StartCoroutine(inicio.Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/RecordatorioIniciarSecado")));
    }
}
