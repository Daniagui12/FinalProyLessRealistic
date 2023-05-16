using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerIniciarCaso : MonoBehaviour
{
    Inicio inicio;

    private void Awake()
    {
        inicio = FindObjectOfType<Inicio>();
    }

    void Start()
    {
        InvokeRepeating("RecordarActividad", 10.0f, 10.0f);
    }

    void RecordarActividad()
    {
        StartCoroutine(inicio.Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/RecordatorioParaIniciarCaso")));
    }
}
