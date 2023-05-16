using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicio : MonoBehaviour
{
    public bool saltarCaso = false;
    public GameObject temporizador;
    public MoveCharWithAnimation doctor;
    public AudioClip audioInicial;
    Audio inicio;
    public bool inicioCaso;
    public GameObject elementosBioseguridad;
    public bool hizoClick;
    public Material materialGuantes;
    public Animator puerta;
    public GameObject izquierda;

    public GameObject flecha1;
    public GameObject flecha2;

    public bool minutoOro = false;
    public bool reanimacionAvanzada = false;
    Audio scriptAudio;
    public GameObject salaminutoOro;
    public GameObject salaReanimacion;

    private void Awake()
    {
        inicio = FindObjectOfType<Audio>();
        scriptAudio = FindObjectOfType<Audio>();
    }


    void Start()
    {
        InvokeRepeating("RecordarActividad", 30.0f, 15.0f);
        hizoClick = false;
    }

    public void SeleccionarTipoCaso(bool minO, bool reanimacion)
    {
        minutoOro = minO;
        reanimacionAvanzada = reanimacion;
    }

    public void SeleccionarSaltarCaso(bool boolSaltar)
    {
        saltarCaso = boolSaltar;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InicializarEjercicio();
        }
    }

    public void InicializarEjercicio()
    {
        if (minutoOro)
        {
            salaminutoOro.SetActive(true);

            scriptAudio.jsonFile = Resources.Load<TextAsset>("Caso Minuto de Oro/ArchivoGeneral");
            scriptAudio.tipoCaso = "Minuto de oro";
            salaReanimacion.SetActive(false);
            Destroy(GameObject.Find("SalaReanimacionPermanente"));
        }
        else if (reanimacionAvanzada)
        {
            salaReanimacion.SetActive(true);
            //Se desactivan las mesas del minuto de oro
            Destroy(GameObject.Find("MesaMinutoOro"));

            scriptAudio.jsonFile = Resources.Load<TextAsset>("Caso Reanimacion/ArchivoGeneralReanimacionCaso2");
            scriptAudio.tipoCaso = "Reanimacion";

        }

        if (!saltarCaso)
        {
            doctor.StartMove();
            elementosBioseguridad.SetActive(true);
            StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/InicioCasoAudio")));

            GameObject.Find("RecordatorioInicioPadre").transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            puerta.SetTrigger("Abrir");
            GameObject.Find("MessageRecordatorio").SetActive(false);
            StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/DialogoSaltarAReanimacion")));
            doctor.setPathBase(GameObject.Find("Path base Inicial Caso 2"));
            doctor.StartMove();
            Destroy(GameObject.Find("SalaBioseguridadTrigger"));
            Destroy(GameObject.Find("Particle System Preguntas"));
            Destroy(GameObject.Find("Environment").GetComponent<BoxCollider>());
            GameObject.Find("hands:Rhand").GetComponent<SkinnedMeshRenderer>().material = materialGuantes;
            izquierda.GetComponent<SkinnedMeshRenderer>().material = materialGuantes;
            flecha1.SetActive(true);
            flecha2.SetActive(true);
        }
        Destroy(gameObject.GetComponent<BoxCollider>());
        inicioCaso = true;
        Destroy(GameObject.Find("Particle System"));
        Destroy(GameObject.Find("Path base Inicial"));
    }

    void RecordarActividad()
    {
        if(!inicioCaso)
        {
            StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/RecordatorioInicio")));
        }
    }

    public IEnumerator Explicar(AudioClip audio)
    {
        if(!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) doctor.transform.gameObject.GetComponent<Animator>().SetTrigger("Habla");
        //El audio debe desabilitarse para que pueda iniciarse despues
        inicio.audioData.enabled = false;
        inicio.audioData.clip = audio;
        //El ovrlypsinc tambien debe deshabilitarse para que el lipsync inicie nuevamente
        inicio.ovrLipsync.enabled = false;
        //Cuando se habilita el lipsync y el audio, el personaje inicia a hablar de nuevo
        inicio.ovrLipsync.enabled = true;
        inicio.audioData.enabled = true;
        yield return "listo";
        yield return new WaitForSeconds(inicio.audioData.clip.length);
        if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) doctor.transform.gameObject.GetComponent<Animator>().SetTrigger("Idle");
    }

    void OnGUI()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
