﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoAlejarseAspirado : MonoBehaviour
{
    Audio inicio;
    private void Awake()
    {
        inicio = FindObjectOfType<Audio>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inicio.audioData.enabled = false;
            inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/NoAlejarseDeMadreAspirado");
            inicio.ovrLipsync.enabled = false;
            inicio.ovrLipsync.enabled = true;
            inicio.audioData.enabled = true;
        }
    }
}
