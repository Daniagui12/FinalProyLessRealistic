using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAspirar : MonoBehaviour
{
    Inicio inicio;

    private void Awake()
    {
        inicio = FindObjectOfType<Inicio>();
    }

    void Start()
    {
        InvokeRepeating("RecordarActividad", 30.0f, 25.0f);
    }

    void RecordarActividad()
    {
        StartCoroutine(inicio.Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/ControllerParaAspirar")));
    }
}
