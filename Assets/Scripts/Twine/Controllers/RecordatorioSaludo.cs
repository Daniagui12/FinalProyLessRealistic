using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordatorioSaludo : MonoBehaviour
{
    Inicio inicio;

    private void Awake()
    {
        inicio = FindObjectOfType<Inicio>();
    }

    void Start()
    {
        InvokeRepeating("RecordarActividad", 70.0f, 60.0f);
    }

    void RecordarActividad()
    {
        if (inicio != null) StartCoroutine(inicio.Explicar(Resources.Load<AudioClip>("Audios Voz Real/RecordatorioSaludoMadre")));
    }
}
