using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public List<string> listaNueva = new List<string>();
    public List<string> listaCorrectos = new List<string>();
    public List<string> listaIncorrectos = new List<string>();

    public string dispositivoVentilacionSeleccionado;
    public GameObject particlesCalentador;
    public GameObject particlesMama;
    public Material seleccionado;
    public Material incorrectoMat;
    public Material normal;
    Inicio inicio;
    public bool casoGuiado;
    public bool seDebenIgnorarIntrucciones;
    public GameObject flechaBio;
    public GameObject video;
    public GameObject indicador;
    public Animator puertaSala;
    public Animator puertaAnimator;
    
    private JsonObject listaAudios;
    private List<string> lista;

    public GameObject personajeNPC;
    public AudioSource audioData;
    public OVRLipSyncContext ovrLipsync;
    public AudioClip segundaParte;
    CambioMaterial scriptCambio;
    ActividadPreguntas scriptclick;
    public GameObject boton;
    public GameObject pantalla;
    public GameObject pantallaRetro;
    public GameObject preguntas;    
    public GameObject pantalla2;
    public GameObject tv3;
    public GameObject botonContinuar;
    public GameObject canvas;
    
    public AudioClip retroVNR;
    public AudioClip retroVR;
    public AudioClip retroVN;
    public AudioClip retroNR;
    public AudioClip retroV;
    public AudioClip retroN;

    public TextAsset jsonFile;
    public string tipoCaso;

    public string casoSeleccionado;

    // Start is called before the first frame update
    private void Awake()
    {
        inicio= FindObjectOfType<Inicio>();
        scriptCambio = FindObjectOfType<CambioMaterial>();
        scriptclick = FindObjectOfType<ActividadPreguntas>();
    }

    private void LeerAudiosCaso()
    {
        
        if (jsonFile != null)
        {
            listaAudios = JsonUtility.FromJson<JsonObject>(jsonFile.text);
            lista = new List<string>();
            for(int i= 0;i<listaAudios.audios.Length; i++)
            {
                lista.Add(listaAudios.audios[i].source);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LeerAudiosCaso();
            if (GameObject.Find("RecordatorioInicioPadre")!=null) Destroy(GameObject.Find("RecordatorioInicioPadre"));
            Destroy(gameObject.GetComponent<BoxCollider>());
            Destroy(GameObject.Find("SalaBioseguridadTrigger"));
            Destroy(GameObject.Find("RecordatorioAntesCaso"));
            StartCoroutine(IniciarDialogoDoctor());
            puertaSala.SetTrigger("Cerrar");
            Destroy(GameObject.Find("Particle System Preguntas"));
        }
    }

    IEnumerator IniciarDialogoDoctor()
    {
        if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Habla");
        for (int i=0; i<lista.Count;i++)
        {
            audioData.enabled = false;
            audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/" + lista[i]);
            ovrLipsync.enabled = false;
            ovrLipsync.enabled = true;
            audioData.enabled = true;
            yield return new WaitForSeconds(audioData.clip.length);
        }

        if (!seDebenIgnorarIntrucciones)
        {
            video.SetActive(true);
            audioData.enabled = false;
            audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/IndicacionesBoton1");
            ovrLipsync.enabled = false;
            ovrLipsync.enabled = true;
            audioData.enabled = true;
            yield return new WaitForSeconds(audioData.clip.length);

            audioData.enabled = false;
            audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/IndicacionesBoton2");
            ovrLipsync.enabled = false;
            ovrLipsync.enabled = true;
            audioData.enabled = true;
            yield return new WaitForSeconds(audioData.clip.length);
            video.SetActive(false);
        }

        if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Idle");
        preguntas.SetActive(true);
        pantalla.SetActive(false);
        boton.SetActive(false);
        botonContinuar.AddComponent(typeof(ActividadPreguntas));
        if(GameObject.Find("RecordatorioBoton")!=null) Destroy(GameObject.Find("RecordatorioBoton"));
        if (GameObject.Find("CanvasLaserInicio") != null)  Destroy(GameObject.Find("CanvasLaserInicio"));
    }

    private IEnumerator CorrutinaHablar(string audio)
    {
        audioData.enabled = false;
        audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/"+ audio);
        ovrLipsync.enabled = false;
        ovrLipsync.enabled = true;
        audioData.enabled = true;
        yield return new WaitForSeconds(audioData.clip.length);
    }

    public void Hablar(string audio)
    {
        StartCoroutine(CorrutinaHablar(audio));
    }

    public void definirDispositivoVentilacion(string dispositivo)
    {
        dispositivoVentilacionSeleccionado = dispositivo;
    }

    public void EstablecerCasoSeleccionado(string nombre)
    {
        casoSeleccionado = nombre;
    }

    public void EstablecerDispositivo(string nombre)
    {
        dispositivoVentilacionSeleccionado = nombre;
    }

}

