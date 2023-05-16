using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSecar : MonoBehaviour
{
    Inicio inicio;

    private void Awake()
    {
        inicio = FindObjectOfType<Inicio>();
    }

    void Start()
    {
        InvokeRepeating("RecordarActividad", 50.0f, 25.0f);
    }

    void RecordarActividad()
    {
        StartCoroutine(inicio.Explicar(Resources.Load<AudioClip>("Audios Voz Real/ControllerSecar")));
    }
}
