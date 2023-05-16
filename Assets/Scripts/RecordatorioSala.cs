using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordatorioSala : MonoBehaviour
{
    private Inicio inicio;
    public GameObject flecha;
    private void Awake()
    {
        inicio = FindObjectOfType<Inicio>();
    }
    void Start()
    {
        InvokeRepeating("RecordarActividad", 25.0f, 20.0f);
    }

    void RecordarActividad()
    {
        flecha.SetActive(true);
        StartCoroutine(inicio.Explicar(Resources.Load<AudioClip>("Audios Voz Real/RecordatorioEntrarASala2")));
    }
}
