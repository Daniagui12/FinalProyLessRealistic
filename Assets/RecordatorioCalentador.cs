using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordatorioCalentador : MonoBehaviour
{
    Inicio inicio;

    private void Awake()
    {
        inicio = FindObjectOfType<Inicio>();
    }

    void Start()
    {
        InvokeRepeating("RecordarActividad", 25.0f, 15.0f);
    }

    void RecordarActividad()
    {
        StartCoroutine(inicio.Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/RecordatorioCalentador")));
    }
}
