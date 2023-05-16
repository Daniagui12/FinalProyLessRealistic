using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ControladorTipoCaso : MonoBehaviour
{
    public bool minutoOro=false;
    public bool reanimacionAvanzada = false;
    Audio scriptAudio;
    public GameObject salaminutoOro;
    public GameObject salaReanimacion;

    public GameObject video;
    public GameObject imagen;
    public TextMeshProUGUI texto;
    public GameObject particle;
    public GameObject fondo;

    public GameObject flecha;
    public GameObject flecha2;
    public GameObject flecha3;
    public GameObject flecha4;

    

    CambioMaterial scriptCambio;

    public GameObject controlIzq;
    public GameObject controlDer;
    public GameObject derecha;
    public GameObject izquierda;
    public GameObject handTriggerD;
    public GameObject handTriggerI;
    public GameObject botonA;
    public Material resaltado;
    public Material normal;

    bool puertaCerrada = false;
    Inicio scriptInicio;

    public int puedeCerrarsePuerta=0;

    private void Awake()
    {
        scriptCambio = FindObjectOfType<CambioMaterial>();
        scriptAudio = FindObjectOfType<Audio>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {            
            puedeCerrarsePuerta++;
            FindObjectOfType<Inicio>().inicioCaso = true;
            
        }
        else if(other.gameObject.name=="Crocs")
        {
            puedeCerrarsePuerta++;
        }
    }

    private void Update()
    {
        if (puedeCerrarsePuerta == 2 && !puertaCerrada)
        {
            puertaCerrada = true;
            scriptAudio.puertaAnimator.SetTrigger("Cerrar");

            if (SceneManager.GetActiveScene().name == "Hospital") StartCoroutine(Explicar());
            Destroy(gameObject.GetComponent<BoxCollider>());
            Destroy(GameObject.Find("RecordatorioSalaPadre"));
            Destroy(flecha3);
            Destroy(flecha4);
            //StartCoroutine(Recordatorio());
        }
    }

    IEnumerator Explicar()
    {
        yield return new WaitForSeconds(0.1f);
        if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Habla");
        scriptCambio.CargarArchivoJsonEquipos();
        GameObject.Find("BtnSiguienteEquipos").AddComponent(typeof(ActividadEquipos));

        if (!scriptAudio.seDebenIgnorarIntrucciones)
        {
            GameObject[] lista = GameObject.FindGameObjectsWithTag("ActividadEquipos");
            foreach (var gObj in lista)
            {
                gObj.AddComponent(typeof(ActividadEquipos));
            }
            
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/2 Actividad Equipos11SinInstrucciones2");
            scriptAudio.ovrLipsync.enabled = false;
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            yield return new WaitForSeconds(scriptAudio.audioData.clip.length);

            GameObject.Find("FlechaSeleccionEquipos").transform.GetChild(0).gameObject.SetActive(true);
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/IndicacionAcercarseMesa");
            scriptAudio.ovrLipsync.enabled = false;
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            yield return new WaitForSeconds(scriptAudio.audioData.clip.length);
            yield return new WaitForSeconds(2f);
            if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Idle");

            scriptAudio.pantalla2.SetActive(true);
            GameObject.Find("CanvasSaltarEtapa").transform.GetChild(0).gameObject.SetActive(false);

            controlDer.SetActive(true);
            derecha.SetActive(false);
            controlIzq.SetActive(true);
            izquierda.SetActive(false);
            
            texto.text = "Revisa los modelos de los controles en tus manos.";
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/InstruccionSeleccion1");
            scriptAudio.ovrLipsync.enabled = false;
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            yield return new WaitForSeconds(scriptAudio.audioData.clip.length);

            texto.text = "Puedes agarrar y seleccionar los elementos después del tutorial.";
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/InstruccionSeleccion2");
            scriptAudio.ovrLipsync.enabled = false;
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            yield return new WaitForSeconds(scriptAudio.audioData.clip.length);

            video.SetActive(true);
            texto.text = "Toma los objetos oprimiendo uno de estos botones.";
            handTriggerD.GetComponent<SkinnedMeshRenderer>().material = resaltado;
            handTriggerI.GetComponent<SkinnedMeshRenderer>().material = resaltado;
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/InstruccionEquipos");
            scriptAudio.ovrLipsync.enabled = false;
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            yield return new WaitForSeconds(scriptAudio.audioData.clip.length);

            texto.text = "Agarra los objetos y oprime A para seleccionarlos";
            video.SetActive(false);
            imagen.SetActive(true);
            handTriggerD.GetComponent<SkinnedMeshRenderer>().material = normal;
            handTriggerI.GetComponent<SkinnedMeshRenderer>().material = normal;
            botonA.GetComponent<SkinnedMeshRenderer>().material = resaltado;
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Seleccion1");
            scriptAudio.ovrLipsync.enabled = false;
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            yield return new WaitForSeconds(scriptAudio.audioData.clip.length);
            yield return new WaitForSeconds(3f);

            texto.text = "Puedes ir practicando la selección de equipos";
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/ActivanManosInstruccion");
            scriptAudio.ovrLipsync.enabled = false;
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            yield return new WaitForSeconds(scriptAudio.audioData.clip.length);

            controlDer.SetActive(false);
            controlIzq.SetActive(false);
            derecha.SetActive(true);
            izquierda.SetActive(true);
            video.SetActive(true);
            imagen.SetActive(false);
            texto.text = "Los elementos seleccionados tienen un contorno naranja.";
            video.GetComponentInChildren<VideoPlayer>().clip = Resources.Load<VideoClip>("VideoSeleccion3");
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Seleccion4");
            scriptAudio.ovrLipsync.enabled = false;
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            yield return new WaitForSeconds(scriptAudio.audioData.clip.length);
            yield return new WaitForSeconds(2f);

            video.SetActive(false);
            texto.text = "Los elementos seleccionados incorrectos tienen un contorno rojo.";
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Seleccion4Incorrectos");
            scriptAudio.ovrLipsync.enabled = false;
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            yield return new WaitForSeconds(scriptAudio.audioData.clip.length);

            video.SetActive(true);
            texto.text = "Puedes quitar la selección agarrando el objeto y oprimiendo 'A'.";
            video.GetComponentInChildren<VideoPlayer>().clip = Resources.Load<VideoClip>("VideoSeleccion4");
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Seleccion5");
            scriptAudio.ovrLipsync.enabled = false;
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            yield return new WaitForSeconds(scriptAudio.audioData.clip.length);

            texto.text = "Selecciona los elementos y ten en cuenta el tablero para saber de tus aciertos o errores.";
            video.SetActive(false);
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Seleccion6");
            scriptAudio.ovrLipsync.enabled = false;
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            yield return new WaitForSeconds(scriptAudio.audioData.clip.length);

            texto.text = "Podrás continuar cuando selecciones lo necesario.";
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Seleccion7");
            scriptAudio.ovrLipsync.enabled = false;
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            yield return new WaitForSeconds(scriptAudio.audioData.clip.length);

            botonA.GetComponent<SkinnedMeshRenderer>().material = normal;
            
            GameObject.Find("CanvasSaltarEtapa").transform.GetChild(0).gameObject.SetActive(true);
            
            texto.text = "Puedes iniciar la selección de tu equipo.";
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/InstruccionSeleccion5");
            scriptAudio.ovrLipsync.enabled = false;
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            yield return new WaitForSeconds(scriptAudio.audioData.clip.length);
        }
        else
        {
            scriptAudio.pantalla2.SetActive(true);
            GameObject.Find("FlechaSeleccionEquipos").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("CanvasSaltarEtapa").transform.GetChild(0).gameObject.SetActive(false);
            GameObject[] lista = GameObject.FindGameObjectsWithTag("ActividadEquipos");
            foreach (var gObj in lista)
            {
                gObj.AddComponent(typeof(ActividadEquipos));
            }

            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/2 Actividad Equipos11SinInstrucciones2");
            scriptAudio.ovrLipsync.enabled = false;
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            yield return new WaitForSeconds(scriptAudio.audioData.clip.length);
            GameObject.Find("CanvasSaltarEtapa").transform.GetChild(0).gameObject.SetActive(true);

            if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Idle");

        }        
        video.SetActive(false);
        texto.text = "";
        Destroy(particle);
        GameObject.Find("CanvasSaltarEtapa1").transform.GetChild(0).gameObject.SetActive(true);
        fondo.transform.GetChild(0).gameObject.SetActive(false);

        InvokeRepeating("RecordarActividad", 10.0f, 50.0f);
    }

    void RecordarActividad()
    {
        if (scriptAudio.listaNueva.Count == 0 && GameObject.Find("CanvasTwine")==null)
        {
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/RecodatorioSeleccionarEquipo");
            scriptAudio.ovrLipsync.enabled = false;
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
        }
    }
}
