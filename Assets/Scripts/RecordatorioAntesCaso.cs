using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordatorioAntesCaso : MonoBehaviour
{
    private Inicio inicio;
    private void Awake()
    {
        inicio = FindObjectOfType<Inicio>();
    }
    void Start()
    {
        InvokeRepeating("RecordarActividad", 15.0f, 20.0f);
    }

    void RecordarActividad()
    {
        StartCoroutine(inicio.Explicar(Resources.Load<AudioClip>("Audios Voz Real/RecordatorioIniciarPreguntas")));
    }
}
