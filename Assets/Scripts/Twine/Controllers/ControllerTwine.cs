using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class ControllerTwine : MonoBehaviour
{
    Inicio script;
    Audio inicio;
    public DialogueViewer1 viewer;
    private Timer timer;
    public bool aTiempo = false;

    public bool debeReacomodarseGuia = false;

    public bool impideMirarObjetivo = false;
    public bool pasoAnimacionPera = false;
    public bool pasoAnimacionBolsa = false;

    public bool enContactoConMadre = false;

    private ControllerBaby controllerBaby;

    public Material LuzRoja;
    public Material LuzNaranja;
    public Material LuzVerde;
    public Material apagado;
    Cronometro cronometro;

    public bool seCortoCordon = false;
    public bool casoBase = false;
    public bool casoBase2 = false;
    public bool casoBaseMejoraTrasAspirar = false;
    public bool CasoBolsa = false;
    public bool casoQuejido = false;

    public bool esCasoSinAyuda = false;
    public bool esCasoReanimacion = false;

    //VALORES PARA IGUALAR CON LOS DEL BEBE
    public bool seco15 = false;
    public bool aspiro30 = false;
    public bool ordenoPinzamientoATiempo = false;
    public bool llevoACalentador = false;        
    public bool ventilo30 = false;
    public bool ligoATiempo = true;
    public bool regresoMadre = false;
    public bool sePusoGorro = false;
    public bool usoEstetoscopio = false;
    public bool seLigo = false;

    public bool terminoPasoPeraTutorial = false;
    public bool terminoPasoBolsaTutorial = false;
    public bool pusoMascaritaTutorial = false;

    private void Awake()
    {
        script = FindObjectOfType<Inicio>();
        inicio = FindObjectOfType<Audio>();

    }

    public void IdentificarComoCasoReanimacion()
    {
        esCasoReanimacion = true;
    }

    public void IdentificarComoCasoQuejido()
    {
        casoQuejido = true;
    }

    public void IdentificarComoCasoSinAyuda()
    {
        esCasoSinAyuda = true;
    }

    public void llegoAPasoBolsa()
    {
        CasoBolsa = true;
    }


    public void acomodarCanvasRetroalimentacion()
    {
        GameObject.Find("RetroalimentacionFinal").transform.position = inicio.canvas.transform.position;
        GameObject.Find("RetroalimentacionFinal").transform.rotation = inicio.canvas.transform.rotation;
        inicio.canvas.transform.position = new Vector3(200f, 3.49f, -54.07f);
    }

    /**
     * Puede usarse en dado caso que se cambie el canvas de lugar a la posición inicial en el tv sobre la madre
     * */
    public void ReacomodarCanvas()
    {
        inicio.canvas.transform.position = new Vector3(19.37f, 3.49f, -52.43f);
        inicio.canvas.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    public void EsCasoBase()
    {
        casoBase = true;
    }

    public void EsCasoBase2()
    {
        casoBase2 = true;
    }

    //PUEDE BORRARSE
    public void ActivarColliderParaDetenerBebe()
    {
        if (SceneManager.GetActiveScene().name != "Hospital") GameObject.Find("Back").GetComponent<BoxCollider>().enabled = true;
    }

    //PUEDE BORRARSE
    public void ActivarColliderCalentador()
    {
        if (SceneManager.GetActiveScene().name != "Hospital") GameObject.Find("TriggerCalentador").GetComponent<BoxCollider>().enabled = true;
    }

    public void eliminarScriptsCalentador()
    {
        Destroy(GameObject.Find("TriggerCalentador").GetComponent<BoxCollider>());
        Destroy(GameObject.Find("TriggerCalentador").GetComponent<UnirObjeto>());
    }

    public void EsCasoBaseMejoraTrasAspirar()
    {
        casoBase = true;
    }

    public IEnumerator ActivarAudioDetenerProcesoEnum()
    {
        GameObject.Find("InputType_Sample").GetComponent<AudioSource>().enabled = false;

        GameObject.Find("InputType_Sample").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/DetenerProceso");
        GameObject.Find("InputType_Sample").GetComponent<OVRLipSyncContext>().enabled = false;
        GameObject.Find("InputType_Sample").GetComponent<OVRLipSyncContext>().enabled = true;
        GameObject.Find("InputType_Sample").GetComponent<AudioSource>().enabled = true;

        yield return new WaitForSeconds(GameObject.Find("InputType_Sample").GetComponent<AudioSource>().clip.length);
        yield return "listo";

        viewer.ChangeNode();
    }

    public void ActivarAudioDetenerProceso()
    {
        StartCoroutine(ActivarAudioDetenerProcesoEnum());
    }

    public IEnumerator ActivarAudioEnum(string nombre)
    {
        yield return new WaitForSeconds(0.1f);
        if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Habla");
        GameObject.Find("InputType_Sample").GetComponent<AudioSource>().enabled = false;

        GameObject.Find("InputType_Sample").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/" + nombre);
        GameObject.Find("InputType_Sample").GetComponent<OVRLipSyncContext>().enabled = false;
        GameObject.Find("InputType_Sample").GetComponent<OVRLipSyncContext>().enabled = true;
        GameObject.Find("InputType_Sample").GetComponent<AudioSource>().enabled = true;
        yield return new WaitForSeconds(GameObject.Find("InputType_Sample").GetComponent<AudioSource>().clip.length);
        yield return "listo";
        if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Idle");
        yield return new WaitForSeconds(0.1f);
    }

    public void ActivarAudio(string nombre)
    {
        StartCoroutine(ActivarAudioEnum(nombre));
    }

    public void ActivarAudioMadre(string nombre)
    {
        StartCoroutine(ReproducirAudioMadre(nombre));
    }

    public IEnumerator OrdenarSaludoAMadreIEnum()
    {
        if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Habla");
        inicio.audioData.enabled = false;
        inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/InstruccionLaser");
        inicio.ovrLipsync.enabled = false;
        inicio.ovrLipsync.enabled = true;
        inicio.audioData.enabled = true;
        yield return new WaitForSeconds(inicio.audioData.clip.length);
        if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Idle");
        yield return new WaitForSeconds(5f);
        viewer.ChangeNode();
    }

    public void OrdenarSaludoAMadre()
    {
        StartCoroutine(OrdenarSaludoAMadreIEnum());
    }

    public IEnumerator ReproducirAudioMadre(string nombre)
    {
        GameObject.Find("B20_Ch_01_Avatar").GetComponent<Animator>().SetTrigger("Habla");

        GameObject.Find("B20_Ch_01_Avatar").GetComponent<AudioSource>().enabled = false;
        GameObject.Find("B20_Ch_01_Avatar").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/" + nombre);
        GameObject.Find("B20_Ch_01_Avatar").GetComponent<AudioSource>().enabled = true;

        yield return new WaitForSeconds(GameObject.Find("B20_Ch_01_Avatar").GetComponent<AudioSource>().clip.length);

        GameObject.Find("B20_Ch_01_Avatar").GetComponent<Animator>().SetTrigger("NoHabla");

        viewer.ChangeNode();
    }

    public void EliminarOutline()
    {
        GameObject[] lista = GameObject.FindGameObjectsWithTag("Outline");
        foreach (var gObj in lista)
        {
            Destroy(gObj);
        }
    }

    public void CambiarPosicionSemaforo()
    {
        GameObject.Find("Traffic Light").transform.position = new Vector3(22.425f, 3.09f, -52.33f);
        GameObject.Find("Traffic Light").transform.rotation = Quaternion.Euler(0f, 0f, -270f);
    }

    public void CambiarPosicionReloj()
    {
        GameObject.Find("CronometroPared").transform.position = new Vector3(23.19f, 3.088f, -52.464f);
        GameObject.Find("CronometroPared").transform.rotation = Quaternion.Euler(0f, -90f, 0f);
    }

    public void ActivarGameobject(string nombreObjeto)
    {
        if (GameObject.Find(nombreObjeto) != null) GameObject.Find(nombreObjeto).transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ActivarGameobjectByTag(string tag)
    {
        if (GameObject.FindGameObjectWithTag(tag) != null) GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).gameObject.SetActive(true);
    }


    public void ActivarGameobject2(string nombreObjeto)
    {
        GameObject.Find(nombreObjeto).transform.GetChild(1).gameObject.SetActive(true);
    }

    public void DesactivarGameobject(string nombreObjeto)
    {
        if (GameObject.Find(nombreObjeto) != null)
        {
            if (GameObject.Find(nombreObjeto).transform.childCount != 0) GameObject.Find(nombreObjeto).transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void DesactivarGameobject2(string nombreObjeto)
    {
        if (GameObject.Find(nombreObjeto) != null) GameObject.Find(nombreObjeto).transform.GetChild(1).gameObject.SetActive(false);
    }


    public void MoverCanvas()
    {
        GameObject.Find("CanvasTwine").transform.position = new Vector3(27.97f, 3.499f, -50.33f);
        GameObject.Find("CanvasTwine").transform.rotation = Quaternion.Euler(0f, 90f, 0f);
    }


    public void DestruirGameobject(string nombreObjeto)
    {
        if (GameObject.Find(nombreObjeto) != null) Destroy(GameObject.Find(nombreObjeto));
    }

    public void CambiarRutaDoctorACalentador()
    {
        script = FindObjectOfType<Inicio>();
        script.doctor.setPathBase(GameObject.Find("Path base Calentador"));
        script.doctor.StartMove();
    }

    public void CambiarRutaDoctorBIncubadora()
    {
        GameObject.Find("Doctora_B").GetComponent<MoveCharWithAnimation>().setPathBase(GameObject.Find("Path base incubadora"));
        GameObject.Find("Doctora_B").GetComponent<MoveCharWithAnimation>().StartMove();
    }


    public IEnumerator Explicar(string audio, bool cambiar)
    {
        yield return new WaitForSeconds(0.1f);
        if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Habla");
        //El audio debe desabilitarse para que pueda iniciarse despues
        inicio.audioData.enabled = false;
        inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/" + audio);
        //El ovrlypsinc tambien debe deshabilitarse para que el lipsync inicie nuevamente
        inicio.ovrLipsync.enabled = false;
        //Cuando se habilita el lipsync y el audio, el personaje inicia a hablar de nuevo
        inicio.ovrLipsync.enabled = true;
        inicio.audioData.enabled = true;
        yield return "listo";
        yield return new WaitForSeconds(inicio.audioData.clip.length);
        if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Idle");
        yield return new WaitForSeconds(0.1f);
        if (cambiar) viewer.ChangeNode();
        else viewer.LoadResponses();
    }


    public IEnumerator ExplicarConReloj(string audio)
    {
        cronometro = FindObjectOfType<Cronometro>();
        yield return new WaitForSeconds(0.1f);
        GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Habla");
        //El audio debe desabilitarse para que pueda iniciarse despues
        inicio.audioData.enabled = false;
        inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/" + audio);
        //El ovrlypsinc tambien debe deshabilitarse para que el lipsync inicie nuevamente
        inicio.ovrLipsync.enabled = false;
        //Cuando se habilita el lipsync y el audio, el personaje inicia a hablar de nuevo
        inicio.ovrLipsync.enabled = true;
        inicio.audioData.enabled = true;
        yield return "listo";
        yield return new WaitForSeconds(inicio.audioData.clip.length);
        GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Idle");
        yield return new WaitForSeconds(0.1f);
        viewer.LoadResponses();
        cronometro.TimerStart();
        CambiarColorSemaforo("verde");
    }

    public void EmpezarCronometro()
    {
        StartCoroutine(ExplicarConReloj("DialogoNoCortoCordon"));
    }

    public void ResponderSegunTiempo()
    {
        timer = FindObjectOfType<Timer>();
        if (timer.LlegoMinuto)
        {
            aTiempo = false;
            StartCoroutine(Explicar("DialogoTenerEnCuentaTiempo", true));
        }
        else
        {
            aTiempo = true;
            StartCoroutine(Explicar("PreguntaProcedimientoSecar", false));
        }
    }

    public void ResponderSegunAccion()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            StartCoroutine(Explicar("DialogoSiSecoNuevaVersion", true));
            DetenerLlanto();
        }
        else
        {
            StartCoroutine(Explicar("DialogoNoSeco", true));
        }
    }

    public void CambiarMaterialPielYSonidoSegunAccion()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        DetenerLlanto();
        if (controllerBaby.seSecoBebe)
        {
            controllerBaby.cambiarMaterialPiel();
            controllerBaby.seco15 = true;
            ActivarAudio("ContinuaSecando");
            GameObject.Find("Seco151").GetComponent<TextMeshProUGUI>().text = "Si";
        }
        else
        {
            ActivarAudio("RecordarSecado");
            controllerBaby.seco15 = false;
            GameObject.Find("Seco151").GetComponent<TextMeshProUGUI>().text = "No";
        }
    }

    public void AudioSecadoDificultadSegunAccion()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            controllerBaby.seco15 = true;
            ActivarAudio("ContinuaSecando");
            GameObject.Find("Seco151").GetComponent<TextMeshProUGUI>().text = "Si";
        }
        else
        {
            ActivarAudio("RecordarSecado");
            controllerBaby.seco15 = false;
            GameObject.Find("Seco151").GetComponent<TextMeshProUGUI>().text = "No";
        }
    }

    public void CambiarMaterialPielSegunAccion()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            controllerBaby.cambiarMaterialPiel();
        }
    }

    public void RecordarContinuarConSecado()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();

        if (controllerBaby.seSecoBebe)
        {
            controllerBaby.seco15 = true;
            GameObject.Find("Seco151").GetComponent<TextMeshProUGUI>().text = "Si";
        }
        else
        {
            controllerBaby.seco15 = false;
            GameObject.Find("Seco151").GetComponent<TextMeshProUGUI>().text = "No";
        }
    }

    public void ResponderSegunAccionCaso2()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            StartCoroutine(Explicar("DialogoSiSecoCaso2NuevaVersion", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoNoSecoCaso2", true));
        }
    }

    public void ResponderSegunAccionCaso2RA()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            StartCoroutine(Explicar("DialogoSiSecoCasoRA", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoNoSecoCasoRA", true));
        }
    }

    public void ResponderSegunAccionCaso2RAQuejido()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            StartCoroutine(Explicar("DialogoSiSecoCasoRAQuejido", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoNoSecoCasoRAQuejido", true));
        }
    }

    public void ResponderSegunAccionCasoBebeNaceSinRespirar()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            StartCoroutine(Explicar("DialogoSiSecoCasoBebeNoRespira", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoNoSecoCasoBebeNoRespira", true));
        }
    }

    public void ResponderSegunAccionCasoBebeNaceSinRespirarMejoraSecado()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "Si";
            StartCoroutine(Explicar("DialogoSiSecoCasoBebeNoRespiraMejora", true));
            controllerBaby.ActivarTriggerAnimacion("NacimientoLlorando");
            controllerBaby.ActivarTriggerAnimacion("RespirandoNormal");
            controllerBaby.ActivarTriggerAnimacion("Llanto");
            controllerBaby.cambiarMaterialPiel();
            ActivarGameobject("LlantoPadre");
            controllerBaby.cambiarAPasoGorro();
            controllerBaby.BebeMejora();
        }
        else
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "No";
            StartCoroutine(Explicar("DialogoNoSecoCasoBebeNoRespira", true));
        }
    }

    public void RegistraCasoBebeNaceSinRespirarMejoraSecado()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "Si";
            StartCoroutine(Explicar("DialogoEnBlanco", true));
            controllerBaby.seco30 = true;

        }
        else
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "No";
            controllerBaby.seco30 = false;
            //Esperar a que haga la accion para continuar-- Esto lo valida el script del bebe
        }
    }

    public void ResponderSegunAccionCaso2BebeConEsfuerzoRespiratorio()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            StartCoroutine(Explicar("DialogoSiSecoCasoEsfuerzoRespiratorio", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoNoSecoCasoEsfuerzoRespiratorio", true));
        }
    }

    public void ResponderSegunAccion2()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            StartCoroutine(Explicar("DialogoSiSeco2", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoNoSeco2", true));
        }
    }

    public void ResponderSegunAccion2Caso2()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seAspiroBebe)
        {
            if (!esCasoReanimacion) StartCoroutine(Explicar("DialogoSiAspiroNuevaVersion", true));
            else StartCoroutine(Explicar("CasoAspiroBienSonda", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoNoAspiro", true));
        }
    }

    public void ResponderSegunAccionCasoBebeMejoraConAspiracion()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seAspiroBebe && controllerBaby.seAspiroBocaBebe)
        {
            StartCoroutine(Explicar("DialogoSiAspiroNarizBocaBebeMejoro", true));
            controllerBaby.ActivarTriggerAnimacion("RespirandoNormal");
            controllerBaby.ActivarTriggerAnimacion("NacimientoLlorando");
            controllerBaby.cambiarMaterialPiel();
            controllerBaby.ActivarTriggerAnimacion("Llanto");
            ActivarGameobject("LlantoPadre");
            controllerBaby.cambiarAPasoGorro();
            controllerBaby.BebeMejora();
        }
        else if (controllerBaby.seAspiroBebe && !controllerBaby.seAspiroBocaBebe)
        {
            StartCoroutine(Explicar("DialogoIntentoAspirar", true));
        }
        else if (!controllerBaby.seAspiroBebe)
        {
            StartCoroutine(Explicar("DialogoNoAspiro", true));
        }
    }

    public void ResponderSegunAccionCasoBebeMejoraConAspiracionSinAyudas()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        GameObject.Find("Seco30").GetComponent<TextMeshProUGUI>().text = "Aspiró secreciones";
        if (controllerBaby.seAspiroBebe)
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "Si";
            StartCoroutine(Explicar("DialogoEnBlanco", true));
            controllerBaby.aspiro30 = true;
            aspiro30 = true;

        }
        else
        {
            controllerBaby.aspiro30 = false;
            aspiro30 = false;
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "No";
            //Debe hacerlo
        }
    }

    public void ResponderSegunAccionCaso3()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seAspiroBebe)
        {
            StartCoroutine(Explicar("BebeMejoroConAspiracion", true));
        }
        else
        {
            StartCoroutine(Explicar("BebeMejoroSinAspiracion", true));
        }
    }

    public void ResponderSegunAccion3()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.sePusoGorro)
        {
            StartCoroutine(Explicar("DialogoSiSeco3", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoNoSeco3", true));
        }
    }

    public void ResponderSegunAccion3Caso2()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();

        if (controllerBaby.seVentiloBebe)
        {
            controllerBaby.BebeCianotico("false");
            controllerBaby.ActivarTriggerAnimacion("RespirandoNormal");
            controllerBaby.ActivarTriggerAnimacion("ArribaRespirando");
            controllerBaby.ActivarTriggerAnimacion("Llanto");
            ActivarGameobject("LlantoPadre");
            controllerBaby.cambiarMaterialPiel();
            controllerBaby.BebeMejora();
            controllerBaby.cambiarAPasoGorro();
        }
        if (controllerBaby.seSecoBebe && controllerBaby.seAspiroBebe && controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("SecoAspiroVentilo", true));
        }
        else if (controllerBaby.seSecoBebe && controllerBaby.seAspiroBebe && !controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("SecoAspiro", true));
        }
        else if (controllerBaby.seSecoBebe && !controllerBaby.seAspiroBebe && controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("SecoVentilo", true));
        }
        else if (!controllerBaby.seSecoBebe && controllerBaby.seAspiroBebe && controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("AspiroVentilo", true));
        }
        else if (!controllerBaby.seSecoBebe && !controllerBaby.seAspiroBebe && controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("Ventilo", true));
        }
        else if (!controllerBaby.seSecoBebe && controllerBaby.seAspiroBebe && !controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("Aspiro", true));
        }
        else if (controllerBaby.seSecoBebe && !controllerBaby.seAspiroBebe && !controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("Seco", true));
        }
        /**
        if (controllerBaby.seVentiloBebe)
        {
            controllerBaby.BebeCianotico("false");
            StartCoroutine(Explicar("DialogoSiVentiloNuevaVersion", true));
            controllerBaby.ActivarTriggerAnimacion("RespirandoNormal");
            controllerBaby.ActivarTriggerAnimacion("ArribaRespirando");
            controllerBaby.ActivarTriggerAnimacion("Llanto");
            ActivarGameobject("LlantoPadre");
            controllerBaby.cambiarMaterialPiel();
        }
        else
        {
            StartCoroutine(Explicar("DialogoNoVentilo", true));
        }
        **/
    }

    public void ResponderSegunAccionCasoBebeNoMejora()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe && controllerBaby.seAspiroBebe && controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("SecoAspiroVentilo", true));
        }
        else if (controllerBaby.seSecoBebe && controllerBaby.seAspiroBebe && !controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("SecoAspiro", true));
        }
        else if (controllerBaby.seSecoBebe && !controllerBaby.seAspiroBebe && controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("SecoVentilo", true));
        }
        else if (!controllerBaby.seSecoBebe && controllerBaby.seAspiroBebe && controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("AspiroVentilo", true));
        }
        else if (!controllerBaby.seSecoBebe && !controllerBaby.seAspiroBebe && controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("Ventilo", true));
        }
        if (!controllerBaby.seSecoBebe && controllerBaby.seAspiroBebe && !controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("Aspiro", true));
        }
        else if (controllerBaby.seSecoBebe && !controllerBaby.seAspiroBebe && !controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("Seco", true));
        }
        /**
        if (controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("DialogoSiVentiloNuevaVersion", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoNoVentilo", true));
        }
        **/
    }

    public void ResponderSegunAccionCaso4()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("BebeMejoroConVentilacion", true));
        }
        else
        {
            StartCoroutine(Explicar("BebeMejoroSinVentilacion", true));
        }
    }

    public void ResponderSegunSiEstaATiempo()
    {
        if (aTiempo)
        {
            ActivarAudio("EsperarOrden");
        }
    }

    public void ActivarAudioConEspera(string nombre, string debeCambiar)
    {
        StartCoroutine(Explicar(nombre, bool.Parse(debeCambiar)));
    }

    public bool RequiereEspera()
    {
        return false;
    }

    public void CambiarColorSemaforo(string colorLuz)
    {
        MeshRenderer semaforo = GameObject.Find("Lights").GetComponent<MeshRenderer>();
        Material[] mat = semaforo.materials;

        if (colorLuz == "verde")
        {
            mat[2] = LuzVerde;
            mat[0] = apagado;
            mat[1] = apagado;
            GameObject.Find("Lights").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("Lights").transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find("Lights").transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (colorLuz == "amarillo")
        {
            mat[0] = LuzNaranja;
            mat[2] = apagado;
            mat[1] = apagado;
            GameObject.Find("Lights").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("Lights").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Lights").transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (colorLuz == "rojo")
        {
            mat[1] = LuzRoja;
            mat[0] = apagado;
            mat[2] = apagado;
            GameObject.Find("Lights").transform.GetChild(2).gameObject.SetActive(true);
            GameObject.Find("Lights").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Lights").transform.GetChild(1).gameObject.SetActive(false);
        }
        semaforo.materials = mat;
    }

    public void ReproduceVideoSinCambiarNodo(string nombre)
    {
        ActivarGameobject("Video");
        GameObject.Find("Video1").GetComponentInChildren<VideoPlayer>().clip = Resources.Load<VideoClip>(nombre);
    }

    public IEnumerator ReproduceVideo(string nombre)
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();

        GameObject.Find("Video1").GetComponentInChildren<VideoPlayer>().clip = Resources.Load<VideoClip>(nombre);

        yield return "listo";
        yield return new WaitForSeconds((float)GameObject.Find("Video1").GetComponentInChildren<VideoPlayer>().clip.length);

        viewer.ChangeNode();
    }

    public void ReproducirVideo(string nombre)
    {
        StartCoroutine(ReproduceVideo(nombre));
    }

    public IEnumerator ReproduceVideoReanimacion(string nombre)
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        GameObject.Find("Video1").GetComponentInChildren<VideoPlayer>().clip = Resources.Load<VideoClip>(nombre);
        yield return "listo";
        yield return new WaitForSeconds((float)GameObject.Find("Video1").GetComponentInChildren<VideoPlayer>().clip.length);

        viewer.ChangeNode();
    }


    public void ReproducirVideoSegunAccion()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        ActivarGameobject("Video");
        StartCoroutine(ReproduceVideo("VideoSecadoExplicacion"));
        ActivarAudio("DialogoVideoDurante");

    }

    public void ReproducirVideoSegunAccionBebeMejoraConSecado()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        ActivarGameobject("Video");
        StartCoroutine(ReproduceVideo("VideoSecadoExplicacion"));
        ActivarAudio("DialogoVideoDurante");

        if (controllerBaby.seSecoBebe)
        {
            DetenerLlanto();
        }

    }

    public void ReproducirVideoSegunAccionCaso2()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        ActivarAudio("DialogoVideoDuranteCaso2");
        ActivarGameobject("Video");
        StartCoroutine(ReproduceVideoReanimacion("VideoSondaExplicacion"));
    }

    public void ReproducirVideoSegunAccionCaso2Bolsa()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        ActivarAudio("DialogoVideoDuranteCaso2Bolsa");
        ActivarGameobject("Video");
        StartCoroutine(ReproduceVideoReanimacion("VideoBolsaExplicacion"));
    }


    public void ReproducirVideoSegunAccionCaso2Neotee()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        ActivarAudio("DialogoVideoDuranteCasoNeotee");
        ActivarGameobject("Video");
        StartCoroutine(ReproduceVideoReanimacion("VideoNeotee"));
    }


    public void ReproducirVideoSegunAccionCaso2Neopuff()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        ActivarAudio("DialogoVideoDuranteCasoNeopuff");
        ActivarGameobject("Video");
        StartCoroutine(ReproduceVideoReanimacion("VideoNeopuff"));
    }

    public IEnumerator ReproduceVideo2(string nombre)
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        ActivarGameobject("Video");
        GameObject.Find("Video1").GetComponentInChildren<VideoPlayer>().clip = Resources.Load<VideoClip>(nombre);
        yield return "listo";
        yield return new WaitForSeconds((float)GameObject.Find("Video1").GetComponentInChildren<VideoPlayer>().clip.length);
        DesactivarGameobject("Video");
        viewer.ChangeNode();
    }

    public void ReproducirVideo2(string nombre)
    {
        StartCoroutine(ReproduceVideo2(nombre));
    }

    public IEnumerator DejarLlorar()
    {
        for (int i = 0; i < 15; i++)
        {
            yield return new WaitForSeconds(1);
            if (GameObject.Find("Llanto").GetComponent<AudioSource>().volume > 0) GameObject.Find("Llanto").GetComponent<AudioSource>().volume -= 0.1f;
        }
    }

    public void DetenerLlanto()
    {
        StartCoroutine(DejarLlorar());
    }

    public void ReactivarCronometro()
    {
        cronometro = FindObjectOfType<Cronometro>();
        cronometro.TimerStart();
        CambiarColorSemaforo("verde");
    }

    public void DetenerCronometro()
    {
        cronometro = FindObjectOfType<Cronometro>();
        cronometro.TimerStop();
        CambiarColorSemaforo("rojo");
    }

    public void SeCortoCordon()
    {
        seCortoCordon = true;
    }

    IEnumerator Recordar()
    {
        yield return new WaitForSeconds(42f);
        if (!seCortoCordon)
        {
            inicio.audioData.enabled = false;
            inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/RecordatorioCortarCordon");
            //El ovrlypsinc tambien debe deshabilitarse para que el lipsync inicie nuevamente
            inicio.ovrLipsync.enabled = false;
            //Cuando se habilita el lipsync y el audio, el personaje inicia a hablar de nuevo
            inicio.ovrLipsync.enabled = true;
            inicio.audioData.enabled = true;
            yield return "listo";
        }
    }

    public void RecordarPinzamiento()
    {
        StartCoroutine(Recordar());
    }

    IEnumerator RecordarMover()
    {
        yield return new WaitForSeconds(42f);
        if (GameObject.Find("CanvasTwine").GetComponent<DialogueViewer1>().lastNodeTitle == "Ordenar pinzamiento habitual del cordón umbilical")
        {
            inicio.audioData.enabled = false;
            inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/RecordatorioMoverBebeCalentador");
            //El ovrlypsinc tambien debe deshabilitarse para que el lipsync inicie nuevamente
            inicio.ovrLipsync.enabled = false;
            //Cuando se habilita el lipsync y el audio, el personaje inicia a hablar de nuevo
            inicio.ovrLipsync.enabled = true;
            inicio.audioData.enabled = true;
            yield return "listo";
        }
    }

    public void RecordarMoverBebe()
    {
        StartCoroutine(RecordarMover());
    }

    /**
     * METODOS PARA LOS CASOS MAS DETALLADOS DONDE NO SE PUEDE CONTINUAR SIN HACER LA ACTIVIDAD
     **/
    public void ResponderAntesDeContinuarSegunSiSeco15Segundos()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            GameObject.Find("Seco151").GetComponent<TextMeshProUGUI>().text = "Si";
            StartCoroutine(Explicar("DialogoFinVideoSecadoVersionFacil", true));
        }
        else
        {
            GameObject.Find("Seco151").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("SecarParaContinuar");
            ActivarGameobject("ControllerParaSecar");
            ActivarGameobject("ControllerActividadSecado");
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Puede continuar cuando haya secado y estimulado al bebé";
        }
    }

    public void ReescribirInformacion(string objeto, string valor)
    {
        GameObject.Find(objeto).GetComponent<TextMeshProUGUI>().text = valor;
    }

    public void ResponderContinuarSegunSiSeco15Segundos()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else
        {
            DetenerLlanto();
            DesactivarGameobject("ControllerParaSecar");
            DesactivarGameobject("ControllerActividadSecado");
            StartCoroutine(Explicar("DialogoFinVideoSecadoVersionFacil", true));
        }
    }

    public void ResponderAntesDeContinuarSegunSiSeco30Segundos()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            ActivarGameobject2("LlantoPadre");
            StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else
        {
            ActivarAudio("SecarParaContinuar");
            ActivarGameobject("ControllerActividadSecado");
            ActivarGameobject("ControllerParaSecar");
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Puede continuar cuando haya secado y estimulado al bebé";
        }
    }

    public void ResponderSegunAccion30Segundos()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "Si";
            StartCoroutine(Explicar("DialogoSeco30Segundos", true));
            controllerBaby.cambiarMaterialPiel();
        }
        else
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "No";
            StartCoroutine(Explicar("DialogoNoSeco30Segundos", true));
        }
    }

    public void ResponderSegunAccion30SegundosSinAyudas()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "Si";
            controllerBaby.cambiarMaterialPiel();
            controllerBaby.seco30 = true;
        }
        else
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "No";
            controllerBaby.seco30 = false;
        }
    }

    public void ResponderContinuarSegunSiSeco30Segundos()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        controllerBaby.cambiarMaterialPiel();
        DesactivarGameobject("ControllerParaSecar");
        DesactivarGameobject("ControllerActividadSecado");
        StartCoroutine(Explicar("DialogoYaSeco2VersionFacil", true));


    }

    public void ResponderContinuarSegunSiSeco30SegundosBebeMejoraSecado()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        DesactivarGameobject("ControllerParaSecar");
        DesactivarGameobject("ControllerActividadSecado");
        StartCoroutine(Explicar("DialogoYaSeco2VersionFacil", true));
        controllerBaby.BebeMejora();


        if (!controllerBaby.seSecoBebe)
        {
            controllerBaby.cambiarMaterialPiel();
            controllerBaby.ActivarTriggerAnimacion("RespirandoNormal");
            controllerBaby.ActivarTriggerAnimacion("NacimientoLlorando");
            controllerBaby.ActivarTriggerAnimacion("Llanto");
            ActivarGameobject("LlantoPadre");
            controllerBaby.cambiarAPasoGorro();
        }

    }

    public void ResponderContinuarSegunSiAspiro30SegundosBebeMejoraAspiracion()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        DesactivarGameobject("ControllerParaAspirar");
        DesactivarGameobject("ControllerActividadAspirado");
        StartCoroutine(Explicar("DialogoYaAspiro2VersionFacil", true));
        controllerBaby.BebeMejora();


        if (!controllerBaby.seAspiroBocaBebe)
        {
            controllerBaby.cambiarMaterialPiel();
            controllerBaby.ActivarTriggerAnimacion("RespirandoNormal");
            controllerBaby.ActivarTriggerAnimacion("NacimientoLlorando");
            controllerBaby.ActivarTriggerAnimacion("Llanto");
            ActivarGameobject("LlantoPadre");
            controllerBaby.cambiarAPasoGorro();
        }

    }

    public void ResponderAntesDeContinuarSegunSiSecoPusoGorro55Segundos()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.sePusoGorro && (controllerBaby.toallaArropandoABebe || controllerBaby.toalla2ArropandoABebe))
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "Si";
            StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else if (!controllerBaby.sePusoGorro && (controllerBaby.toallaArropandoABebe || controllerBaby.toalla2ArropandoABebe))
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
            StartCoroutine(Explicar("DialogoIndicarColocarGorro", true));
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Puede continuar cuando le haya colocado el gorro al bebé";
        }
        else if (controllerBaby.sePusoGorro && !(controllerBaby.toallaArropandoABebe || controllerBaby.toalla2ArropandoABebe))
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "Si";
            StartCoroutine(Explicar("DialogoIndicarColocarManta", true));
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Puede continuar cuando le haya colocado la manta al bebé";
        }
        else
        {

            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudioConEspera("DialogoIndicarColocarGorroYMantaBebe", "true");
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Puede continuar cuando le haya colocado la manta y el gorro al bebé";
        }
    }

    public void RegistrarSiPusoGorroYManta()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.sePusoGorro && (controllerBaby.toallaArropandoABebe || controllerBaby.toalla2ArropandoABebe))
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "Si";
            controllerBaby.cambiarMaterialPiel();
        }
        else if (!controllerBaby.sePusoGorro && (controllerBaby.toallaArropandoABebe || controllerBaby.toalla2ArropandoABebe))
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
            controllerBaby.cambiarMaterialPiel();
        }
        else if (controllerBaby.sePusoGorro && !(controllerBaby.toallaArropandoABebe || controllerBaby.toalla2ArropandoABebe))
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "Si";
        }
        else
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
        }
    }

    public void ResponderContinuarSegunSiSecoPusoGorroManta()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.sePusoGorro && (controllerBaby.toallaArropandoABebe || controllerBaby.toalla2ArropandoABebe))
        {
            StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else
        {
            //ESPERAR
            controllerBaby.puedePasarMantaGorro = true;
        }
    }

    public void ResponderContinuarSegunSiSecoPusoGorro55Segundos()
    {
        StartCoroutine(Explicar("DialogoSiMantaYGorro", true));
    }

    //METODOS CASO EN EL QUE EL BEBE NACE MAL
    public void ResponderAntesDeContinuarSegunSiSeco15SegundosCasoBebeNacioMal()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            GameObject.Find("Seco151").GetComponent<TextMeshProUGUI>().text = "Si";
            if (!esCasoReanimacion) StartCoroutine(Explicar("DialogoFinVideoSecadoCasoBebeMalVersionFacil", true));
            else StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else
        {
            GameObject.Find("Seco151").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("SecarParaContinuar");
            ActivarGameobject("ControllerParaSecar");
            if (!esCasoReanimacion) ActivarGameobject("ControllerActividadSecado");
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Puede continuar cuando haya secado y estimulado al bebé";
        }
    }

    public void ResponderAntesDeContinuarSegunSiSeco30SegundosCasoBebeNacioDificultad()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "Si";
            if (!esCasoReanimacion) StartCoroutine(Explicar("DialogoFinVideoSecadoCasoBebeMalVersionFacil", true));
            else StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("SecarParaContinuar");
            ActivarGameobject("ControllerParaSecar");
            if (!esCasoReanimacion) ActivarGameobject("ControllerActividadSecado");
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Puede continuar cuando haya secado y estimulado al bebé";
        }
    }

    public void RegistrarSiSeco15SegundosCasoBebeNacioMalSinAyudas()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            GameObject.Find("Seco151").GetComponent<TextMeshProUGUI>().text = "Si";
            controllerBaby.seco15 = true;
            seco15 = true;
        }
        else
        {
            GameObject.Find("Seco151").GetComponent<TextMeshProUGUI>().text = "No";
            controllerBaby.seco15 = false;
            seco15 = false;
        }
    }

    public void ResponderAntesDeContinuarSegunSiSeco15SegundosCasoBebeNacioMalMejora()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            GameObject.Find("Seco151").GetComponent<TextMeshProUGUI>().text = "Si";
            if (!esCasoReanimacion) StartCoroutine(Explicar("DialogoFinVideoSecadoCasoBebeMalVersionFacilMejora", true));
            else StartCoroutine(Explicar("DialogoFinVideoSecadoCasoBebeMalVersionReanimacion", true));
        }
        else
        {
            GameObject.Find("Seco151").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("SecarParaContinuar");
            ActivarGameobject("ControllerParaSecar");
            if (!esCasoReanimacion) ActivarGameobject("ControllerActividadSecado");
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Puede continuar cuando haya secado y estimulado al bebé";
        }
    }

    public void ResponderContinuarSegunSiSeco15SegundosCasoBebeNacioMalMejora()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {

            StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else
        {
            if (!esCasoReanimacion) StartCoroutine(Explicar("DialogoFinVideoSecadoCasoBebeMalVersionFacilMejora", true));
            else StartCoroutine(Explicar("DialogoFinVideoSecadoCasoBebeMalVersionReanimacion", true));
            DesactivarGameobject("ControllerParaSecar");
            DesactivarGameobject("ControllerActividadSecado");
            controllerBaby.cambiarMaterialPiel();
            controllerBaby.ActivarTriggerAnimacion("RespirandoNormal");
            controllerBaby.ActivarTriggerAnimacion("NacimientoLlorando");
            controllerBaby.ActivarTriggerAnimacion("Llanto");
            ActivarGameobject("LlantoPadre");
        }
    }

    public void ResponderContinuarSegunSiSeco15SegundosCasoBebeNacioMal()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {

            if (!esCasoReanimacion) StartCoroutine(Explicar("DialogoEnBlanco", true));
            else StartCoroutine(Explicar("DialogoFinVideoSecadoCasoBebeMalVersionReanimacion", false));
        }
        else
        {
            if (!esCasoReanimacion) StartCoroutine(Explicar("DialogoFinVideoSecadoCasoBebeMalVersionFacil", true));
            else StartCoroutine(Explicar("DialogoFinVideoSecadoCasoBebeMalVersionReanimacion", false));
            DesactivarGameobject("ControllerParaSecar");
            DesactivarGameobject("ControllerActividadSecado");
        }
    }

    public void ResponderContinuarSegunSiSeco15SegundosCasoBebeNacioMalQuejido()
    {
        StartCoroutine(Explicar("DialogoFinVideoSecadoCasoBebeMalVersionReanimacionQuejido", false));
        DesactivarGameobject("ControllerParaSecar");
        DesactivarGameobject("ControllerActividadSecado");

    }

    public void ResponderAntesDeContinuarSegunSiAspiro30SegundosBebeMejoraVentilacion()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        GameObject.Find("Seco30").GetComponent<TextMeshProUGUI>().text = "Aspiró secreciones";
        if (controllerBaby.seAspiroBebe)
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "Si";
            StartCoroutine(Explicar("DialogoFinVideoSecreciones", true));
        }
        else
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("AspirarParaContinuar");
            ActivarGameobject("ControllerParaAspirar");
            if (!esCasoReanimacion) ActivarGameobject("ControllerActividadAspirado");
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Puede continuar cuando haya aspirado las secreciones del bebé";
        }
    }

    public void ResponderAntesDeContinuarSegunSiAspiro30Segundos()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        GameObject.Find("Seco30").GetComponent<TextMeshProUGUI>().text = "Aspiró secreciones";
        if (controllerBaby.seAspiroBocaBebe)
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "Si";
            StartCoroutine(Explicar("DialogoFinVideoSecreciones", true));
        }
        else
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("AspirarParaContinuar");
            ActivarGameobject("ControllerParaAspirar");
            if (!esCasoReanimacion) ActivarGameobject("ControllerActividadAspirado");
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Puede continuar cuando haya aspirado las secreciones del bebé";
        }
    }

    public void ResponderAntesDeContinuarSegunSiAspiro30SegundosBebeMejorara()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        GameObject.Find("Seco30").GetComponent<TextMeshProUGUI>().text = "Aspiró secreciones";
        if (controllerBaby.seAspiroBocaBebe)
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "Si";
            StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("AspirarParaContinuar");
            ActivarGameobject("ControllerParaAspirar");
            if (!esCasoReanimacion) ActivarGameobject("ControllerActividadAspirado");
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Puede continuar cuando haya aspirado las secreciones del bebé";
        }
    }

    public void ResponderContinuarSegunSiAspiro30SegundosBebeMejora()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seAspiroBocaBebe)
        {
            StartCoroutine(Explicar("DialogoEnBlanco", false));
            DetenerLlanto();
        }
        else
        {
            DesactivarGameobject("ControllerParaAspirar");
            DesactivarGameobject("ControllerActividadAspirado");
            StartCoroutine(Explicar("DialogoNoAspiroFinVideoSecrecionesBebeMejora", false));
            controllerBaby.ActivarTriggerAnimacion("RespirandoNormal");
            controllerBaby.ActivarTriggerAnimacion("AbajoRespirando");
            controllerBaby.cambiarMaterialPiel();
            controllerBaby.ActivarTriggerAnimacion("Llanto");
            ActivarGameobject("LlantoPadre");
            controllerBaby.cambiarAPasoGorro();
        }
    }

    public void ResponderContinuarSegunSiAspiro30Segundos()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seAspiroBebe)
        {
            StartCoroutine(Explicar("DialogoEnBlanco", false));
        }
        else
        {
            DesactivarGameobject("ControllerParaAspirar");
            DesactivarGameobject("ControllerActividadAspirado");
            StartCoroutine(Explicar("DialogoFinVideoSecreciones", false));
        }
    }

    public void ResponderAntesDeContinuarSegunSiVentilo60Segundos()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        GameObject.Find("PusoGorro60").GetComponent<TextMeshProUGUI>().text = "Realizó ventilación";
        if (controllerBaby.seVentiloBebe)
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "Si";
            StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("VentilarParaContinuar");
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación con la bolsa autoinflable.";
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación con la bolsa autoinflable.";
        }
    }

    public void RegistrarSiVentilo60Segundos()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        GameObject.Find("PusoGorro60").GetComponent<TextMeshProUGUI>().text = "Realizó ventilación";
        if (controllerBaby.seVentiloBebe)
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "Si";
            StartCoroutine(Explicar("DialogoEnBlanco", true));
            controllerBaby.ventilo30 = true;
            if(int.Parse(cronometro.timerMinutes) == 1 && int.Parse(cronometro.timerSeconds) >=0 && int.Parse(cronometro.timerSeconds) < 25) ventilo30 = true;
            else ventilo30 = false;
        }
        else
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
            controllerBaby.ventilo30 = false;
            ventilo30 = false;
        }
    }

    public void ResponderContinuarSegunSiVentilo60Segundos()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seVentiloBebe)
        {
            if (!controllerBaby.sePusoGorro) StartCoroutine(Explicar("DialogoBebeRespondioCaso4", true));
            else StartCoroutine(Explicar("DialogoBebeTieneGorroRespondioCaso4", true));
        }
        else
        {
            controllerBaby.cambiarAPasoGorro();
            controllerBaby.BebeCianotico("false");
            if (!controllerBaby.sePusoGorro) StartCoroutine(Explicar("DialogoBebeRespondioCaso4", true));
            else StartCoroutine(Explicar("DialogoBebeTieneGorroRespondioCaso4", true));
            controllerBaby.ActivarTriggerAnimacion("ArribaRespirando");
            controllerBaby.ActivarTriggerAnimacion("RespirandoNormal");
            controllerBaby.ActivarTriggerAnimacion("Llanto");
            ActivarGameobject("LlantoPadre");
            controllerBaby.BebeMejora();
            controllerBaby.cambiarMaterialPiel();
        }
    }

    public void BebeTienePuestoGorroMRSOPA()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (!controllerBaby.sePusoGorro) StartCoroutine(Explicar("DialogoBebeRespondioCaso4", true));
        else StartCoroutine(Explicar("DialogoBebeTieneGorroRespondioCaso4", true));


    }

    public void BebeTienePuestoGorroMRSOPARA()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (!controllerBaby.sePusoGorro) StartCoroutine(Explicar("DialogoEnBlanco", true));
        else StartCoroutine(Explicar("DialogoEnBlanco", true));


    }

    public void ResponderAntesDeContinuarSegunSiVentilo60SegundosNoRespondio()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        GameObject.Find("PusoGorro60").GetComponent<TextMeshProUGUI>().text = "Realizó ventilación";
        if (controllerBaby.seVentiloBebe)
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "Si";
            StartCoroutine(Explicar("DialogoBebeNoRespondio", true));
        }
        else
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("VentilarParaContinuar");
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación con la bolsa autoinflable.";
        }
    }

    public void ResponderContinuarSegunSiVentilo60SegundosNoRespondio()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("DialogoEnBlanco", false));
        }
        else
        {
            StartCoroutine(Explicar("DialogoBebeNoRespondio", false));
        }
    }
    public IEnumerator ExplicarCasoCompetencia(string audio)
    {

        //El audio debe desabilitarse para que pueda iniciarse despues
        inicio.audioData.enabled = false;
        inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/" + audio);
        //El ovrlypsinc tambien debe deshabilitarse para que el lipsync inicie nuevamente
        inicio.ovrLipsync.enabled = false;
        //Cuando se habilita el lipsync y el audio, el personaje inicia a hablar de nuevo
        inicio.ovrLipsync.enabled = true;
        inicio.audioData.enabled = true;
        yield return "listo";
        ActivarGameobject("Error");
        yield return new WaitForSeconds(inicio.audioData.clip.length);
        DesactivarGameobject("Error");

    }

    // CASO 2 SIN AYUDA

    public void ActivarHijos()
    {
        GameObject.Find("Estrellas").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Estrellas").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("Estrellas").transform.GetChild(2).gameObject.SetActive(true);
    }

    public void DesactivarHijos()
    {
        GameObject.Find("Estrellas").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Estrellas").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("Estrellas").transform.GetChild(2).gameObject.SetActive(false);
    }

    public void SeSeco15SegundosCasoBebeNacioMal()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            ActivarAudio("RetroalimentacionFinalEtapaCumplioTarea");
            GameObject.Find("Seco151").GetComponent<TextMeshProUGUI>().text = "Si";
            GameObject.Find("Punto1").GetComponent<Image>().sprite = Resources.Load<Sprite>("CartoonGUIPack/Resources/Sliced Elements/10Complete/Icon_BigStar1_light");
        }
        else
        {
            StartCoroutine(ExplicarCasoCompetencia("RetroalimentacionFinalEtapa"));
        }
    }

    public void SeSeco30Segundos()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            ActivarAudio("RetroalimentacionFinalEtapaCumplioTarea");
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "Si";
            GameObject.Find("Punto2").GetComponent<Image>().sprite = Resources.Load<Sprite>("CartoonGUIPack/Resources/Sliced Elements/10Complete/Icon_BigStar1_light");
        }
        else
        {
            StartCoroutine(ExplicarCasoCompetencia("RetroalimentacionFinalEtapa"));
        }
    }

    public void SeSeco60Segundos()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (GameObject.Find("GorroBebé") != null)
        {
            ActivarAudio("RetroalimentacionFinalEtapaCumplioTarea");
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "Si";
            GameObject.Find("Punto3").GetComponent<Image>().sprite = Resources.Load<Sprite>("CartoonGUIPack/Resources/Sliced Elements/10Complete/Icon_BigStar1_light");
        }
        else
        {
            StartCoroutine(ExplicarCasoCompetencia("RetroalimentacionFinalEtapa"));
        }
    }

    public void SeAspiro30Segundos()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();

        if (controllerBaby.seAspiroBebe)
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "Si";
        }
        else
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "No";
        }
    }

    public void SeVentilo60SegundosNoRespondio()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();

        if (controllerBaby.seVentiloBebe)
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "Si";
        }
        else
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
        }
    }

    public void acomodarTextos()
    {
        GameObject.Find("PusoGorro60").GetComponent<TextMeshProUGUI>().text = "Realizó ventilación";
        GameObject.Find("Seco30").GetComponent<TextMeshProUGUI>().text = "Aspiró secreciones";
    }

    public IEnumerator esperarSegundos()
    {
        yield return new WaitForSeconds(8f);
        viewer.ChangeNode();
    }

    public void EsperarSegundos()
    {
        StartCoroutine(esperarSegundos());
    }

    public void ActivarBoxColliderElemento(string nombre)
    {
        GameObject.Find(nombre).GetComponent<MeshCollider>().enabled = true;
    }

    public void InflarReservorio()
    {
        GameObject.Find("DispositivosVentilacionPreparar").transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.SetActive(true);
    }

    public void AparecerAutoinflableConMascara()
    {
        GameObject.Find("DispositivosVentilacionPreparar").transform.GetChild(0).transform.position = new Vector3(17.61f, -1000f, -35.02f);
        GameObject.Find("DispositivosVentilacionPreparar").transform.GetChild(4).gameObject.SetActive(true);
        GameObject.Find("DispositivosVentilacionPreparar").transform.GetChild(3).transform.position = new Vector3(200.61f, -1000f, -35.02f);

    }

    public IEnumerator esperarSegundos2()
    {
        yield return new WaitForSeconds(2f);
        viewer.ChangeNode();
    }

    public void EsperarSegundos2()
    {
        StartCoroutine(esperarSegundos2());
    }

    public void Desaparecerautoinflable()
    {
        GameObject.Find("DispositivosVentilacionPreparar").transform.GetChild(4).gameObject.SetActive(false);
    }

    public void ResponderAntesDeContinuarSegunSiAspiro30SegundosReanimacion()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        GameObject.Find("Seco30").GetComponent<TextMeshProUGUI>().text = "Aspiró secreciones";
        if (controllerBaby.seAspiroBebe)
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "Si";
            StartCoroutine(Explicar("DialogoFinVideoSecrecionesReanimacion", false));
        }
        else
        {
            GameObject.Find("Seco301").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("AspirarParaContinuar");
            ActivarGameobject("ControllerParaAspirar");
            if (!esCasoReanimacion) ActivarGameobject("ControllerActividadAspirado");
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Puede continuar cuando haya aspirado las secreciones del bebé";
        }
    }

    public void ResponderContinuarSegunSiAspiro30SegundosReanimacion()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seAspiroBebe)
        {
            StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else
        {
            DesactivarGameobject("ControllerParaAspirar");
            DesactivarGameobject("ControllerActividadAspirado");
            StartCoroutine(Explicar("DialogoFinVideoSecrecionesReanimacion", true));
        }
    }

    public void ResponderAntesDeContinuarSegunSiVentilo60SegundosNoRespondioRA()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        GameObject.Find("PusoGorro60").GetComponent<TextMeshProUGUI>().text = "Realizó ventilación";
        if (controllerBaby.seVentiloBebe)
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "Si";
            if (esCasoReanimacion) StartCoroutine(Explicar("DialogoBebeNoRespondioRA", true));
            else StartCoroutine(Explicar("DialogoBebeNoRespondioMO", true));
        }
        else
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("VentilarParaContinuar");
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación.";
        }
    }
    public void ResponderAntesDeContinuarSegunSiVentiloPICOCorrectamente()
    {
        ControllerBaby baby = FindObjectOfType<ControllerBaby>();
        ControladorPICO controlador = FindObjectOfType<ControladorPICO>();

        GameObject.Find("PusoGorro60").GetComponent<TextMeshProUGUI>().text = "Realizó ventilación";

        //Si se puso el dispositivo, se tapo el orificio y se realizo bien la ventilacion entonces lo felicita
        if (baby.seVentiloBebe && controlador.cantidadPICO > 4 && controlador.cantidadPICO < 11 && controlador.tapoHueco)
        {
            StartCoroutine(Explicar("DialogoEnBlanco", true));
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "Si";
        }
        //Hizo muy pocos ciclos de ventilacion
        else if (baby.seVentiloBebe && controlador.cantidadPICO > 0 && controlador.cantidadPICO <= 4 && controlador.tapoHueco)
        {
            ReiniciarValoresVentilacion();
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("VentilarPICOParaContinuar");
            baby.estaEnIntervencion = true;
            baby.pasoNeopuff = true;
            baby.pasoNeotee = true;
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación siguiendo la cadencia correctamente";
        }
        //Hizo muchos ciclos de ventilacion
        else if (baby.seVentiloBebe && controlador.cantidadPICO > 10 && controlador.tapoHueco)
        {
            ReiniciarValoresVentilacion();
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("VentilarPICOParaContinuar");
            baby.estaEnIntervencion = true;
            baby.pasoNeopuff = true;
            baby.pasoNeotee = true;
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación siguiendo la cadencia correctamente";
        }
        //No hizo nada pero lo intento
        else if (baby.seVentiloBebe && controlador.cantidadPICO == 0 && controlador.tapoHueco)
        {
            ReiniciarValoresVentilacion();
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("VentilarPICOParaContinuar");
            baby.estaEnIntervencion = true;
            baby.pasoNeopuff = true;
            baby.pasoNeotee = true;
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación siguiendo la cadencia correctamente";
        }
        //No hizo nada y no lo intento
        else if (baby.seVentiloBebe && controlador.cantidadPICO == 0 && !controlador.tapoHueco)
        {
            ReiniciarValoresVentilacion();
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("VentilarPICOParaContinuar");
            baby.estaEnIntervencion = true;
            baby.pasoNeopuff = true;
            baby.pasoNeotee = true;
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación siguiendo la cadencia correctamente";
        }
        //No puso ni siquiera el dispositivo de ventilacion
        else if (!baby.seVentiloBebe)
        {
            ReiniciarValoresVentilacion();
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("VentilarMascaraPICOParaContinuar");
            baby.estaEnIntervencion = true;
            baby.pasoNeopuff = true;
            baby.pasoNeotee = true;
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación siguiendo la cadencia correctamente";
        }
    }

    public void ResponderAntesDeContinuarSegunSiVentiloPICOCorrectamentePasoBlender()
    {
        ControllerBaby baby = FindObjectOfType<ControllerBaby>();
        ControladorPICO controlador = FindObjectOfType<ControladorPICO>();

        //Si se puso el dispositivo, se tapo el orificio y se realizo bien la ventilacion entonces lo felicita
        if (baby.seVentiloBebe && controlador.cantidadPICO > 10 && controlador.cantidadPICO < 21 && controlador.tapoHueco)
        {
            StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        //Hizo muy pocos ciclos de ventilacion
        else if (baby.seVentiloBebe && controlador.cantidadPICO > 0 && controlador.cantidadPICO <= 10 && controlador.tapoHueco)
        {
            ReiniciarValoresVentilacion();

            ActivarAudio("VentilarPICOParaContinuar");
            baby.estaEnIntervencion = true;
            baby.pasoNeopuff = true;
            baby.pasoNeotee = true;
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación siguiendo la cadencia correctamente";
        }
        //Hizo muchos ciclos de ventilacion
        else if (baby.seVentiloBebe && controlador.cantidadPICO >= 21 && controlador.tapoHueco)
        {
            ReiniciarValoresVentilacion();
            ActivarAudio("VentilarPICOParaContinuar");
            baby.estaEnIntervencion = true;
            baby.pasoNeopuff = true;
            baby.pasoNeotee = true;
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación siguiendo la cadencia correctamente";
        }
        //No hizo nada pero lo intento
        else if (baby.seVentiloBebe && controlador.cantidadPICO == 0 && controlador.tapoHueco)
        {
            ReiniciarValoresVentilacion();
            ActivarAudio("VentilarPICOParaContinuar");
            baby.estaEnIntervencion = true;
            baby.pasoNeopuff = true;
            baby.pasoNeotee = true;
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación siguiendo la cadencia correctamente";
        }
        //No hizo nada y no lo intento
        else if (baby.seVentiloBebe && controlador.cantidadPICO == 0 && !controlador.tapoHueco)
        {
            ReiniciarValoresVentilacion();
            ActivarAudio("VentilarPICOParaContinuar");
            baby.estaEnIntervencion = true;
            baby.pasoNeopuff = true;
            baby.pasoNeotee = true;
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación siguiendo la cadencia correctamente";
        }
        //No puso ni siquiera el dispositivo de ventilacion
        else if (!baby.seVentiloBebe)
        {
            ReiniciarValoresVentilacion();
            ActivarAudio("VentilarMascaraPICOParaContinuar");
            baby.estaEnIntervencion = true;
            baby.pasoNeopuff = true;
            baby.pasoNeotee = true;
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación siguiendo la cadencia correctamente";
        }
    }


    public void ResponderAntesDeContinuarSegunSiVentiloVPPBolsaCorrectamentePasoBlender()
    {
        ControllerBaby baby = FindObjectOfType<ControllerBaby>();
        ControladorVPP controlador = FindObjectOfType<ControladorVPP>();

        //Si se puso el dispositivo, se tapo el orificio y se realizo bien la ventilacion entonces lo felicita
        if (baby.seVentiloBebe && controlador.cantidadVPP > 10 && controlador.cantidadVPP < 21)
        {
            StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        //Hizo muy pocos ciclos de ventilacion
        else if (baby.seVentiloBebe && controlador.cantidadVPP > 0 && controlador.cantidadVPP <= 10 )
        {
            ReiniciarValoresVentilacionBolsa();

            ActivarAudio("VentilarPICOParaContinuar");
            baby.estaEnIntervencion = true;
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación siguiendo la cadencia correctamente";
        }
        //Hizo muchos ciclos de ventilacion
        else if (baby.seVentiloBebe && controlador.cantidadVPP >= 21 )
        {
            ReiniciarValoresVentilacionBolsa();
            ActivarAudio("VentilarPICOParaContinuar");
            baby.estaEnIntervencion = true;
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación siguiendo la cadencia correctamente";
        }
       else if (baby.seVentiloBebe && controlador.cantidadVPP == 0 )
        {
            ReiniciarValoresVentilacionBolsa();
            ActivarAudio("VentilarPICOParaContinuar");
            baby.estaEnIntervencion = true;
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación siguiendo la cadencia correctamente";
        }
        //No puso ni siquiera el dispositivo de ventilacion
        else if (!baby.seVentiloBebe)
        {
            ReiniciarValoresVentilacionBolsa();
            ActivarAudio("VentilarMascaraPICOParaContinuar");
            baby.estaEnIntervencion = true;
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación siguiendo la cadencia correctamente";
        }
    }

    public void ResponderAntesDeContinuarSegunSiVentiloVPPBolsaCorrectamente()
    {
        ControllerBaby baby = FindObjectOfType<ControllerBaby>();
        ControladorVPP controlador = FindObjectOfType<ControladorVPP>();

        GameObject.Find("PusoGorro60").GetComponent<TextMeshProUGUI>().text = "Realizó ventilación";

        //Si se puso el dispositivo, se tapo el orificio y se realizo bien la ventilacion entonces lo felicita
        if (baby.seVentiloBebe && controlador.cantidadVPP > 4 && controlador.cantidadVPP < 11 )
        {
            StartCoroutine(Explicar("DialogoEnBlanco", true));
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "Si";
        }
        //Hizo muy pocos ciclos de ventilacion
        else if (baby.seVentiloBebe && controlador.cantidadVPP > 0 && controlador.cantidadVPP <= 4)
        {
            ReiniciarValoresVentilacionBolsa();
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("VentilarPICOParaContinuar");
            baby.estaEnIntervencion = true;
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación siguiendo la cadencia correctamente";
        }
        //Hizo muchos ciclos de ventilacion
        else if (baby.seVentiloBebe && controlador.cantidadVPP > 10 )
        {
            ReiniciarValoresVentilacionBolsa();
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("VentilarPICOParaContinuar");
            baby.estaEnIntervencion = true;
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación siguiendo la cadencia correctamente";
        }
        //No hizo nada y no lo intento
        else if (baby.seVentiloBebe && controlador.cantidadVPP == 0 )
        {
            ReiniciarValoresVentilacionBolsa();
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("VentilarPICOParaContinuar");
            baby.estaEnIntervencion = true;
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación siguiendo la cadencia correctamente";
        }
        //No puso ni siquiera el dispositivo de ventilacion
        else if (!baby.seVentiloBebe)
        {
            ReiniciarValoresVentilacionBolsa();
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("VentilarMascaraPICOParaContinuar");
            baby.estaEnIntervencion = true;
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación siguiendo la cadencia correctamente";
        }
    }

    public void ResponderAntesDeContinuarSegunSiVentilo60SegundosNoRespondioRAQuejido()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        GameObject.Find("PusoGorro60").GetComponent<TextMeshProUGUI>().text = "Realizó ventilación";
        if (controllerBaby.seVentiloBebe)
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "Si";
            if (esCasoReanimacion) StartCoroutine(Explicar("DialogoBebeResponderaRAQuejido", true));
        }
        else
        {
            GameObject.Find("PusoGorro601").GetComponent<TextMeshProUGUI>().text = "No";
            ActivarAudio("PEEPParaContinuar");
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación.";
        }
    }

    public void ResponderContinuarSegunSiVentilo60SegundosNoRespondioRA()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seVentiloBebe)
        {
            if (esCasoReanimacion) StartCoroutine(Explicar("DialogoEnBlanco", false));
            else StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else
        {
            if (esCasoReanimacion) StartCoroutine(Explicar("DialogoBebeNoRespondioRA", false));
            else StartCoroutine(Explicar("DialogoBebeNoRespondioMO", true));
        }
    }

    public void ResponderContinuarSegunSiVentilo60SegundosNoRespondioRAQuejido()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seVentiloBebe)
        {
            if (esCasoReanimacion) StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else
        {
            if (esCasoReanimacion) StartCoroutine(Explicar("DialogoBebeResponderaRAQuejido", true));
        }
    }


    public IEnumerator EscribirValoresOximetroRandomCorrutina()
    {
        for (int i = 0; i < 15; i++)
        {
            int valorSaturacion = Random.Range(45, 52);
            GameObject.Find("Saturacion").GetComponent<TextMeshProUGUI>().text = "" + valorSaturacion;
            int valorFrecuencia = Random.Range(75, 78);
            yield return new WaitForSeconds(1f);
            GameObject.Find("Frecuencia").GetComponent<TextMeshProUGUI>().text = "" + valorFrecuencia;
        }

    }

    public void EscribirValoresOximetroRandom()
    {
        StartCoroutine(EscribirValoresOximetroRandomCorrutina());
    }

    public IEnumerator EscribirValoresOximetroRandomCorrutinaQuejido()
    {
        for (int i = 0; i < 15; i++)
        {
            int valorSaturacion = Random.Range(45, 55);
            GameObject.Find("Saturacion").GetComponent<TextMeshProUGUI>().text = "" + valorSaturacion;
            int valorFrecuencia = Random.Range(115, 120);
            yield return new WaitForSeconds(1f);
            GameObject.Find("Frecuencia").GetComponent<TextMeshProUGUI>().text = "" + valorFrecuencia;
        }

    }

    public void EscribirValoresOximetroRandomQuejido()
    {
        StartCoroutine(EscribirValoresOximetroRandomCorrutinaQuejido());
    }


    public void EscribirValoresOximetroEmpeoroVersion1()
    {
        int valorSaturacion = Random.Range(50, 58);
        GameObject.Find("Saturacion").GetComponent<TextMeshProUGUI>().text = "" + valorSaturacion;
        int valorFrecuencia = Random.Range(70, 78);
        GameObject.Find("Frecuencia").GetComponent<TextMeshProUGUI>().text = "" + valorFrecuencia;
    }

    public void EscribirValoresOximetro()
    {
        int valorSaturacion = Random.Range(60, 70);
        GameObject.Find("Saturacion").GetComponent<TextMeshProUGUI>().text = "" + valorSaturacion;
        int valorFrecuencia = Random.Range(80, 90);
        GameObject.Find("Frecuencia").GetComponent<TextMeshProUGUI>().text = "" + valorFrecuencia;
    }

    public void EscribirValoresOximetroEmpeoro()
    {
        int valorSaturacion = Random.Range(40, 58);
        GameObject.Find("Saturacion").GetComponent<TextMeshProUGUI>().text = "" + valorSaturacion;
        int valorFrecuencia = Random.Range(60, 78);
        GameObject.Find("Frecuencia").GetComponent<TextMeshProUGUI>().text = "" + valorFrecuencia;
    }

    public void EscribirValoresOximetroVaAMejorar()
    {
        int valorSaturacion = Random.Range(45, 52);
        GameObject.Find("Saturacion").GetComponent<TextMeshProUGUI>().text = "" + valorSaturacion;
        int valorFrecuencia = Random.Range(75, 78);
        GameObject.Find("Frecuencia").GetComponent<TextMeshProUGUI>().text = "" + valorFrecuencia;
    }

    public void EscribirValoresOximetroVaAMejorarQuejido()
    {
        int valorSaturacion = Random.Range(50, 55);
        GameObject.Find("Saturacion").GetComponent<TextMeshProUGUI>().text = "" + valorSaturacion;
        int valorFrecuencia = Random.Range(115, 120);
        GameObject.Find("Frecuencia").GetComponent<TextMeshProUGUI>().text = "" + valorFrecuencia;
    }

    public void EscribirValoresOximetroVaAMejorarQuejidoRango(string min, string max)
    {
        int valorSaturacion = Random.Range(int.Parse(min), int.Parse(max));
        GameObject.Find("Saturacion").GetComponent<TextMeshProUGUI>().text = "" + valorSaturacion;
    }

    public void EscribirValoresOximetroVaAMejorarOlfateo()
    {
        int valorSaturacion = Random.Range(53, 69);
        GameObject.Find("Saturacion").GetComponent<TextMeshProUGUI>().text = "" + valorSaturacion;
        int valorFrecuencia = Random.Range(79, 82);
        GameObject.Find("Frecuencia").GetComponent<TextMeshProUGUI>().text = "" + valorFrecuencia;
    }

    public void EscribirValoresOximetroNoMejoro()
    {
        int valorSaturacion = Random.Range(60, 65);
        GameObject.Find("Saturacion").GetComponent<TextMeshProUGUI>().text = "" + valorSaturacion;
        int valorFrecuencia = Random.Range(75, 80);
        GameObject.Find("Frecuencia").GetComponent<TextMeshProUGUI>().text = "" + valorFrecuencia;
    }

    public void EscribirValoresOximetroVaAMejorarVentilacion()
    {
        int valorSaturacion = Random.Range(70, 74);
        GameObject.Find("Saturacion").GetComponent<TextMeshProUGUI>().text = "" + valorSaturacion;
        int valorFrecuencia = Random.Range(83, 85);
        GameObject.Find("Frecuencia").GetComponent<TextMeshProUGUI>().text = "" + valorFrecuencia;
    }



    public void EscribirValoresOximetroVaAMejorarSuccion()
    {
        int valorSaturacion = Random.Range(74, 76);
        GameObject.Find("Saturacion").GetComponent<TextMeshProUGUI>().text = "" + valorSaturacion;
        int valorFrecuencia = Random.Range(87, 89);
        GameObject.Find("Frecuencia").GetComponent<TextMeshProUGUI>().text = "" + valorFrecuencia;
    }

    public IEnumerator EscribirValoresOximetroRandomSube()
    {
        for (int i = 0; i < 17; i++)
        {
            int valorSaturacion = Random.Range(77, 80);
            GameObject.Find("Saturacion").GetComponent<TextMeshProUGUI>().text = "" + valorSaturacion;
            int valorFrecuencia = 92 + i;
            yield return new WaitForSeconds(1f);
            GameObject.Find("Frecuencia").GetComponent<TextMeshProUGUI>().text = "" + valorFrecuencia;
        }
    }

    public void EscribirValoresOximetroRandomSubeMejora()
    {
        StartCoroutine(EscribirValoresOximetroRandomSube());
    }

    public IEnumerator EscribirValoresOximetroRandomSubeQuejido()
    {
        for (int i = 0; i < 17; i++)
        {
            int valorFrecuencia = Random.Range(115, 120);
            GameObject.Find("Frecuencia").GetComponent<TextMeshProUGUI>().text = "" + valorFrecuencia;
            
            int valorSaturacion = 78 + i;
            yield return new WaitForSeconds(1f);
            GameObject.Find("Saturacion").GetComponent<TextMeshProUGUI>().text = "" + valorSaturacion;
        }
    }

    public void EscribirValoresOximetroRandomSubeMejoraQuejido()
    {
        StartCoroutine(EscribirValoresOximetroRandomSubeQuejido());
    }

    public IEnumerator EscribirValoresOximetroRandomSubeQuejidoPEEP()
    {
        for (int i = 0; i < 8; i++)
        {
            int valorSaturacion = 70 + i;
            yield return new WaitForSeconds(2.3f);
            GameObject.Find("Saturacion").GetComponent<TextMeshProUGUI>().text = "" + valorSaturacion;
        }
    }

    public void EscribirValoresOximetroRandomSubeMejoraQuejidoPEEP()
    {
        StartCoroutine(EscribirValoresOximetroRandomSubeQuejidoPEEP());
    }

    public void EscribirValoresOximetroVaAMejorarVentilacion2()
    {
        int valorSaturacion = Random.Range(90, 95);
        GameObject.Find("Saturacion").GetComponent<TextMeshProUGUI>().text = "" + valorSaturacion;
        int valorFrecuencia = Random.Range(120, 135);
        GameObject.Find("Frecuencia").GetComponent<TextMeshProUGUI>().text = "" + valorFrecuencia;
    }


    public void ResponderSegunAccionCasoRA()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("DialogoSiVentiloNuevaVersion", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoNoVentilo", true));
        }
    }

    public void ResponderSegunAccionCasoRADispositivoT()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("DialogoSiVentiloNuevaVersionDisT", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoNoVentiloDisT", true));
        }
    }

    public void ResponderSegunAccionCasoRADispositivoTQuejido()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("DialogoSiVentiloNuevaVersionDisTQuejido", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoNoVentiloDisTQuejido", true));
        }
    }

    public void VerificarSiAcomodoDispositivo()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else
        {
            ActivarAudio("RecordarAcomodarDisp");
            controllerBaby.PermiteDebePasarAlPonerBolsa("true");
        }
    }

    public IEnumerator esperarSegundos30()
    {
        yield return new WaitForSeconds(30f);
        viewer.ChangeNode();
    }

    public void EsperarSegundos30()
    {
        StartCoroutine(esperarSegundos30());
    }

    public void ResponderRetroalimentacionVentilacionPosicionOlfateo()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("DialogoSiVentiloOlfateo", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoNoVentiloOlfateo", true));
        }
    }

    public void ResponderRetroalimentacionVentilacionPosicionOlfateoMejora()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("DialogoSiVentiloOlfateoMejora", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoNoVentiloOlfateoMejora", true));
        }
    }

    public void ResponderAntesDeContinuarSegunSiVentiloPosicionOlfateo()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("DialogoSuccion", true));
        }
        else
        {
            ActivarAudio("VentilarParaContinuar");
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación.";
        }
    }

    public void ResponderAntesDeContinuarSegunSiVentiloPosicionOlfateoMejora()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("DialogoSuccionMejora", true));
        }
        else
        {
            ActivarAudio("VentilarParaContinuar");
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Debes realizar la ventilación.";
        }
    }

    public void ResponderContinuarSegunSiVentiloPosicionOlfateoMejora()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoSuccionMejora", true));
        }
    }

    public void ResponderContinuarSegunSiVentiloPosicionOlfateo()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoSuccion", true));
        }
    }

    public void ResponderSegunAccionSondaMRSOPA()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seAspiroBebe)
        {
            StartCoroutine(Explicar("CasoAspiroBienSondaMRSOPA", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoNoAspiroMRSOPA", true));
        }
    }

    public void ResponderAntesDeContinuarSegunSiAspiroMRSOPA()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seAspiroBebe)
        {
            StartCoroutine(Explicar("DialogoBlender", true));
        }
        else
        {
            ActivarAudio("AspirarParaContinuar");
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Puede continuar cuando haya aspirado las secreciones del bebé";
        }
    }

    public void ResponderContinuarSegunSiAspiroMRSOPA()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seAspiroBebe)
        {
            StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoBlender", true));
        }
    }

    public void ResponderContinuarSegunSiAspiroMRSOPABlender()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seAspiroBebe)
        {
            StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoPresionBlender", true));
        }
    }

    public void ResponderAntesDeContinuarSegunSiAspiroMRSOPABlender()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seAspiroBebe)
        {
            StartCoroutine(Explicar("DialogoPresionBlender", true));
        }
        else
        {
            ActivarAudio("AspirarParaContinuar");
            GameObject.Find("Message").GetComponent<TextMeshProUGUI>().text = "Puede continuar cuando haya aspirado las secreciones del bebé";
        }
    }

    public void ResponderSegunAccionBolsaMRSOPA()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seVentiloBebe)
        {
            StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else
        {
            StartCoroutine(Explicar("DialogoBlender", true));
        }
    }

    //METODOS PARA GRAFO UNIFICADO

    public bool PuedeMostrarOpcionesParaPasarASiguienteNodo()
    {
        return false;
    }

    /**
     * Hace la división inicial entre minuto de oro con o sin guía y reanimación con ventilación
     **/
    public void DividirSegunCaso()
    {
        inicio = FindObjectOfType<Audio>();
        if (inicio.tipoCaso.Equals("Minuto de oro"))
        {
            viewer.ChangeNodeIndex(1);
        }
        else
        {
            viewer.ChangeNodeIndex(0);
        }
    }

    public void DarRetroalimentacionSegunAccion()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.seSecoBebe)
        {
            viewer.ChangeNodeIndex(1);
        }
        else
        {
            viewer.ChangeNodeIndex(0);
        }
    }

    public IEnumerator esperarTiempoSegundos(string segundos)
    {
        yield return new WaitForSeconds(float.Parse(segundos));
        viewer.ChangeNode();
    }

    public void EsperarTiempoSegundos(string segundos)
    {
        StartCoroutine(esperarTiempoSegundos(segundos));
    }

    public void ActivarAnimacion(string objeto, string trigger)
    {
        GameObject.Find(objeto).GetComponent<Animator>().SetTrigger(trigger);
    }

    public void ActivarNeotee()
    {
        //Destroy(GameObject.Find("NeoteePreparacion").transform.GetChild(1).gameObject);
        //GameObject.Find("ElementosReanimacionNeotee").transform.GetChild(0).gameObject.SetActive(true);
    }

    public void esPasoAnimacionPera(string paso)
    {
        pasoAnimacionPera = bool.Parse(paso);
    }

    public void esPasoAnimacionBolsa(string paso)
    {
        pasoAnimacionBolsa = bool.Parse(paso);
    }

    public void bebeEnContactoConMadre()
    {
        enContactoConMadre = true;
    }

    public void CortarCordonUmbilical()
    {
        cronometro = FindObjectOfType<Cronometro>();
        viewer = FindObjectOfType<DialogueViewer1>();
        if (int.Parse(cronometro.timerMinutes) > 0 && int.Parse(cronometro.timerMinutes) < 4)
        {
            StartCoroutine(Explicar("DialogoEnBlanco", true));
            FindObjectOfType<ControllerBaby>().ordenoPinzamientoATiempo = true;
        }
        else if (int.Parse(cronometro.timerMinutes) == 0)
        {
            DetenerCronometro();
            CambiarColorSemaforo("rojo");
            viewer.DestroyResponses();
            ActivarAudio("CortoCordonAntes");
            StartCoroutine(ExplicarConSemaforo("CortoCordonAntes"));
            FindObjectOfType<ControllerBaby>().ordenoPinzamientoATiempo = false;
        }
        else if (int.Parse(cronometro.timerMinutes) > 3)
        {
            DetenerCronometro();
            CambiarColorSemaforo("rojo");
            viewer.DestroyResponses();
            ActivarAudioConEspera("TardóCortarCordon", "true");
            FindObjectOfType<ControllerBaby>().ordenoPinzamientoATiempo = true;
        }
    }

    public void CortarCordonUmbilicalQuejido()
    {
        cronometro = FindObjectOfType<Cronometro>();
        viewer = FindObjectOfType<DialogueViewer1>();
        if (int.Parse(cronometro.timerMinutes) == 1)
        {
            StartCoroutine(Explicar("DialogoEnBlanco", true));
        }
        else if (int.Parse(cronometro.timerMinutes) == 0)
        {
            DetenerCronometro();
            CambiarColorSemaforo("rojo");
            CambiarColorSemaforo("rojo");
            viewer.DestroyResponses();
            StartCoroutine(ExplicarConSemaforo("CortoCordonAntesQuejido"));
        }
    }

    public IEnumerator ExplicarConSemaforo(string audio)
    {
        yield return new WaitForSeconds(0.1f);
        GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Habla");
        //El audio debe desabilitarse para que pueda iniciarse despues
        inicio.audioData.enabled = false;
        inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/" + audio);
        //El ovrlypsinc tambien debe deshabilitarse para que el lipsync inicie nuevamente
        inicio.ovrLipsync.enabled = false;
        //Cuando se habilita el lipsync y el audio, el personaje inicia a hablar de nuevo
        inicio.ovrLipsync.enabled = true;
        inicio.audioData.enabled = true;
        yield return "listo";
        yield return new WaitForSeconds(inicio.audioData.clip.length);
        GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Idle");
        yield return new WaitForSeconds(0.1f);
        ReactivarCronometro();
        CambiarColorSemaforo("verde");
    }

    public void ExplicarOcultandoRespuestas(string audio)
    {
        StartCoroutine(ExplicarOcultaRespuestas(audio));
    }

    IEnumerator ExplicarOcultaRespuestas(string audio)
    {
        yield return new WaitForSeconds(0.1f);
        FindObjectOfType<DialogueViewer1>().DestroyResponses();
        GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Habla");
        //El audio debe desabilitarse para que pueda iniciarse despues
        inicio.audioData.enabled = false;
        inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/" + audio);
        //El ovrlypsinc tambien debe deshabilitarse para que el lipsync inicie nuevamente
        inicio.ovrLipsync.enabled = false;
        //Cuando se habilita el lipsync y el audio, el personaje inicia a hablar de nuevo
        inicio.ovrLipsync.enabled = true;
        inicio.audioData.enabled = true;
        yield return "listo";
        yield return new WaitForSeconds(inicio.audioData.clip.length);
        GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Idle");
        yield return new WaitForSeconds(0.1f);
        FindObjectOfType<DialogueViewer1>().LoadResponses();
    }

    public void AcomodarToallas()
    {
        GameObject.Find("ToallaEstatica").transform.position = new Vector3(100f, 2.4868f, -50.5233f);
        GameObject.FindGameObjectWithTag("Toalla").transform.localPosition = new Vector3(-50.325f, 2.14f, -21.45f);
        GameObject.FindGameObjectWithTag("Toalla").transform.localRotation = Quaternion.Euler(-90f, 0f, 90f);

        GameObject.Find("Toalla2").transform.localPosition = new Vector3(-50.325f, 2.14f, -21.582f);
        GameObject.Find("Toalla2").transform.localRotation = Quaternion.Euler(-90f, 0f, 90f);
    }

    public void ArroparBebe()
    {
        GameObject.FindGameObjectWithTag("Toalla").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Toalla").transform.localPosition = new Vector3(-49.86514f, 2.421448f, -20.60479f);
        GameObject.FindGameObjectWithTag("Toalla").transform.localRotation = Quaternion.Euler(83.96101f, -82.98f, -83.277f);

        GameObject.Find("ToallaEstatica").transform.position = new Vector3(20.51092f, 2.495184f, -50.57784f);
        GameObject.Find("ToallaEstatica").transform.rotation = Quaternion.Euler(-13.696f, 3.828f, 0.094f);
    }

    public void AcomodarToallaFinalCasoBase()
    {
        GameObject.Find("ToallaEstatica").transform.position = new Vector3(20.563f, 2.503f, -50.609f);
        GameObject.Find("ToallaEstatica").transform.rotation = Quaternion.Euler(-19.104f, 0f, 0f);
        if (GameObject.Find("Toalla2") != null) if (!GameObject.Find("Toalla2").GetComponent<MeshRenderer>().enabled) Destroy(GameObject.Find("Toalla2").GetComponent<OVRGrabbable>());
        if (GameObject.FindGameObjectWithTag("Toalla") != null) if (!GameObject.FindGameObjectWithTag("Toalla").GetComponent<MeshRenderer>().enabled) Destroy(GameObject.FindGameObjectWithTag("Toalla").GetComponent<OVRGrabbable>());
    }

    public void AcomodarToallaFinalCasoBaseSinGuia()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.toallaArropandoABebe || controllerBaby.toalla2ArropandoABebe)
        {
            GameObject.Find("ToallaEstatica").transform.position = new Vector3(20.563f, 2.503f, -50.609f);
            GameObject.Find("ToallaEstatica").transform.rotation = Quaternion.Euler(-19.104f, 0f, 0f);
            if (GameObject.Find("Toalla2") != null) if (!GameObject.Find("Toalla2").GetComponent<MeshRenderer>().enabled) Destroy(GameObject.Find("Toalla2").GetComponent<OVRGrabbable>());
            if (GameObject.FindGameObjectWithTag("Toalla") != null) if (!GameObject.FindGameObjectWithTag("Toalla").GetComponent<MeshRenderer>().enabled) Destroy(GameObject.FindGameObjectWithTag("Toalla").GetComponent<OVRGrabbable>());
        }
    }

    public void PermiteDebePasarAlPonerGorro()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.sePusoGorro)
        {
            ActivarAudioConEspera("DialogoEnBlanco", "true");
        }
        else
        {
            controllerBaby.PermiteDebePasarAlPonerGorro();
        }
    }

    public void AudioAlPonerGorro()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.sePusoGorro)
        {
            ActivarAudioConEspera("DialogoEnBlanco", "true");
        }
        else
        {
            ActivarAudio("ContinuarLlevarMadre");
        }
    }

    public void ActivarSonda()
    {
        GameObject.Find("ElementosReanimacionCaso1").transform.GetChild(3).gameObject.SetActive(true);
        GameObject.Find("ElementosReanimacionCaso1").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("ElementosReanimacionCaso1").transform.GetChild(2).gameObject.SetActive(true);
        GameObject.Find("TipSondaAnimacion").transform.GetChild(0).gameObject.SetActive(false);
    }

    public void agregarOVRGRabbableBolsa()
    {
        GameObject.FindGameObjectWithTag("Bolsa").AddComponent<Rigidbody>();
        GameObject.Find("BolsaAutoinflableAnimacion").AddComponent<OVRGrabbable>().Initialize(new Collider[1] { GameObject.Find("BolsaAutoinflableAnimacion").GetComponent<BoxCollider>() });

    }

    public void DesactivarColliderFlujometro()
    {
        Destroy(GameObject.Find("Anim_BlenderDial").GetComponent<BoxCollider>());
    }

    public void DesactivarSonda()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        GameObject.Find("ElementosReanimacionCaso1").transform.GetChild(3).gameObject.SetActive(false);
        GameObject.Find("ElementosReanimacionCaso1").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("ElementosReanimacionCaso1").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("ElementosReanimacionCaso1").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("TipSondaAnimacion").transform.GetChild(0).gameObject.SetActive(true);
        controllerBaby.seAspiroBebe = true;
    }

    public void ImpedirQueSeMueva(string valor)
    {
        impideMirarObjetivo = bool.Parse(valor);
    }

    public void CambiarRutaDoctorGuiaBlender()
    {
        script = FindObjectOfType<Inicio>();
        script.doctor.setPathBase(GameObject.Find("Path base Blender"));
        script.doctor.StartMove();
    }

    public void CambiarRutaDoctorGuiaFin()
    {
        script = FindObjectOfType<Inicio>();
        script.doctor.setPathBase(GameObject.Find("Path base final"));
        script.doctor.StartMove();
    }

    public void ActivarColliderDial()
    {
        ActivarAnimacion("SK_NeoTee_B", "PEEP5a7");
        GameObject.Find("SK_NeoTee_B").GetComponent<Animator>().SetTrigger("PICOSube");
        GameObject.Find("SK_NeoTee_B").GetComponent<Animator>().SetTrigger("PICOBaja");
        GameObject.Find("SK_NeoTee_B").GetComponent<Animator>().SetTrigger("semaforo");
    }

    public void EvaluarUsoDispositivoTParte1()
    {
        ControllerBaby baby = FindObjectOfType<ControllerBaby>();
        ControladorPICO controlador = FindObjectOfType<ControladorPICO>();
        //Si se puso el dispositivo, se tapo el orificio y se realizo bien la ventilacion entonces lo felicita
        if (baby.seVentiloBebe && controlador.cantidadPICO > 4 && controlador.cantidadPICO < 11 && controlador.tapoHueco)
        {
            ActivarAudioConEspera("DialogoAplicoPICOCorrectamente", "true");
        }
        //Hizo muy pocos ciclos de ventilacion
        else if (baby.seVentiloBebe && controlador.cantidadPICO > 0 && controlador.cantidadPICO <= 4 && controlador.tapoHueco)
        {
            ActivarAudioConEspera("DialogoAplicoPICOLento", "true");
        }
        //Hizo muchos ciclos de ventilacion
        else if (baby.seVentiloBebe && controlador.cantidadPICO > 10 && controlador.tapoHueco)
        {
            ActivarAudioConEspera("DialogoAplicoPICORapido", "true");
        }
        //No hizo nada pero lo intento
        else if (baby.seVentiloBebe && controlador.cantidadPICO == 0 && controlador.tapoHueco)
        {
            ActivarAudioConEspera("DialogoIntentoAplicoPICO", "true");
        }
        //No hizo nada y no lo intento
        else if (baby.seVentiloBebe && controlador.cantidadPICO == 0 && !controlador.tapoHueco)
        {
            ActivarAudioConEspera("DialogoNoAplicoPICO", "true");
        }
        //No puso ni siquiera el dispositivo de ventilacion
        else if (!baby.seVentiloBebe)
        {
            ActivarAudioConEspera("DialogoSoloPusoDispositivo", "true");
        }
    }

    public void ReiniciarValoresVentilacion()
    {
        FindObjectOfType<ControladorPICO>().ResetearInformacion();
    }

    public void EvaluarUsoDispositivoTParte2En30Segundos()
    {
        ControllerBaby baby = FindObjectOfType<ControllerBaby>();
        ControladorPICO controlador = FindObjectOfType<ControladorPICO>();
        //Si se puso el dispositivo, se tapo el orificio y se realizo bien la ventilacion entonces lo felicita
        if (baby.seVentiloBebe && controlador.cantidadPICO > 10 && controlador.cantidadPICO < 21 && controlador.tapoHueco)
        {
            ActivarAudioConEspera("DialogoAplicoPICOCorrectamente", "true");
        }
        //Hizo muy pocos ciclos de ventilacion
        else if (baby.seVentiloBebe && controlador.cantidadPICO > 0 && controlador.cantidadPICO <= 10 && controlador.tapoHueco)
        {
            ActivarAudioConEspera("DialogoAplicoPICOLento", "true");
        }
        //Hizo muchos ciclos de ventilacion
        else if (baby.seVentiloBebe && controlador.cantidadPICO >= 21 && controlador.tapoHueco)
        {
            ActivarAudioConEspera("DialogoAplicoPICORapido", "true");
        }
        //No hizo nada pero lo intento
        else if (baby.seVentiloBebe && controlador.cantidadPICO == 0 && controlador.tapoHueco)
        {
            ActivarAudioConEspera("DialogoIntentoAplicoPICO", "true");
        }
        //No hizo nada y no lo intento
        else if (baby.seVentiloBebe && controlador.cantidadPICO == 0 && !controlador.tapoHueco)
        {
            ActivarAudioConEspera("DialogoNoAplicoPICO", "true");
        }
        //No puso ni siquiera el dispositivo de ventilacion
        else if (!baby.seVentiloBebe)
        {
            ActivarAudioConEspera("DialogoSoloPusoDispositivo", "true");
        }
    }

    public void EvaluarUsoBolsaParte1()
    {
        ControllerBaby baby = FindObjectOfType<ControllerBaby>();
        ControladorVPP controlador = FindObjectOfType<ControladorVPP>();
        //Si se puso el dispositivo, se tapo el orificio y se realizo bien la ventilacion entonces lo felicita
        if (baby.seVentiloBebe && controlador.cantidadVPP > 4 && controlador.cantidadVPP < 11)
        {
            ActivarAudioConEspera("DialogoAplicoVPPBolsaCorrectamente", "true");
        }
        //Hizo muy pocos ciclos de ventilacion
        else if (baby.seVentiloBebe && controlador.cantidadVPP > 0 && controlador.cantidadVPP <= 4)
        {
            ActivarAudioConEspera("DialogoAplicoVPPBolsaLento", "true");
        }
        //Hizo muchos ciclos de ventilacion
        else if (baby.seVentiloBebe && controlador.cantidadVPP > 10)
        {
            ActivarAudioConEspera("DialogoAplicoVPPBolsaRapido", "true");
        }
        //No hizo nada y no lo intento
        else if (baby.seVentiloBebe && controlador.cantidadVPP == 0)
        {
            ActivarAudioConEspera("DialogoNoAplicoVPPBolsa", "true");
        }
        //No puso ni siquiera el dispositivo de ventilacion
        else if (!baby.seVentiloBebe)
        {
            ActivarAudioConEspera("DialogoSoloPusoDispositivo", "true");
        }
    }

    public void ReiniciarValoresVentilacionBolsa()
    {
        FindObjectOfType<ControladorVPP>().ResetearInformacion();
    }

    public void EvaluarUsoBolsaParte2En30Segundos()
    {
        ControllerBaby baby = FindObjectOfType<ControllerBaby>();
        ControladorVPP controlador = FindObjectOfType<ControladorVPP>();
        //Si se puso el dispositivo, se tapo el orificio y se realizo bien la ventilacion entonces lo felicita
        if (baby.seVentiloBebe && controlador.cantidadVPP > 10 && controlador.cantidadVPP < 21 )
        {
            ActivarAudioConEspera("DialogoAplicoVPPBolsaCorrectamente", "true");
        }
        //Hizo muy pocos ciclos de ventilacion
        else if (baby.seVentiloBebe && controlador.cantidadVPP > 0 && controlador.cantidadVPP <= 10 )
        {
            ActivarAudioConEspera("DialogoAplicoVPPBolsaLento", "true");
        }
        //Hizo muchos ciclos de ventilacion
        else if (baby.seVentiloBebe && controlador.cantidadVPP >= 21)
        {
            ActivarAudioConEspera("DialogoAplicoVPPBolsaRapido", "true");
        }
        //No hizo nada y no lo intento
        else if (baby.seVentiloBebe && controlador.cantidadVPP == 0)
        {
            ActivarAudioConEspera("DialogoNoAplicoVPPBolsa", "true");
        }
        //No puso ni siquiera el dispositivo de ventilacion
        else if (!baby.seVentiloBebe)
        {
            ActivarAudioConEspera("DialogoSoloPusoDispositivo", "true");
        }
    }

    public void ActivarVideoRetroalimentacionCasoVentilacion(string valorAValidar, string videoRetroalimentacionBien, string audioRetroalimentacionBien, string mensajeBien, string mensajeMal, string videoRetroalimentacionMal, string audioRetroalimentacionMal)
    {
        ActivarGameobject("RetroalimentacionCasoSinGuia");
        if (ValorAValidar(valorAValidar))
        {
            ActivarGameobject("ImagenFondoCanvas");
            DesactivarGameobject2("ImagenFondoCanvas");
            ReproduceVideoSinCambiarNodo(videoRetroalimentacionBien);
            ActivarAudioConEspera(audioRetroalimentacionBien, "true");
            GameObject.Find("MessageRetroalimentacion").GetComponent<TextMeshProUGUI>().text = mensajeBien;

        }
        else
        {
            ActivarGameobject2("ImagenFondoCanvas");
            DesactivarGameobject("ImagenFondoCanvas");
            ReproduceVideoSinCambiarNodo(videoRetroalimentacionMal);
            ActivarAudioConEspera(audioRetroalimentacionMal, "true");
            GameObject.Find("MessageRetroalimentacion").GetComponent<TextMeshProUGUI>().text = mensajeMal;
        }
    }

    public bool ValorAValidar(string boolAValidar)
    {
        return (bool)this.GetType().GetField(boolAValidar).GetValue(this);
    }

    public void ActivarVideoRetroalimentacion(string valorAValidar, string videoRetroalimentacionBien, string audioRetroalimentacionBien, string mensajeBien, string mensajeMal, string videoRetroalimentacionMal, string audioRetroalimentacionMal)
    {
        ControllerBaby baby = FindObjectOfType<ControllerBaby>();
        
        ActivarGameobject("RetroalimentacionCasoSinGuia");
        if (baby.valorAValidar(valorAValidar))
        {
            ActivarGameobject("ImagenFondoCanvas");
            DesactivarGameobject2("ImagenFondoCanvas");
            ReproduceVideoSinCambiarNodo(videoRetroalimentacionBien);
            ActivarAudioConEspera(audioRetroalimentacionBien, "true");
            GameObject.Find("MessageRetroalimentacion").GetComponent<TextMeshProUGUI>().text = mensajeBien;

        }
        else
        {
            ActivarGameobject2("ImagenFondoCanvas");
            DesactivarGameobject("ImagenFondoCanvas");
            ReproduceVideoSinCambiarNodo(videoRetroalimentacionMal);
            ActivarAudioConEspera(audioRetroalimentacionMal, "true");
            GameObject.Find("MessageRetroalimentacion").GetComponent<TextMeshProUGUI>().text = mensajeMal;
        }
    }

    public void ActivarVideoRetroalimentacionCasoMantas()
    {
        controllerBaby = FindObjectOfType<ControllerBaby>();
        if (controllerBaby.toallaArropandoABebe || controllerBaby.toalla2ArropandoABebe)
        {
            string valor = "";
            if (controllerBaby.toallaArropandoABebe) valor = "toallaArropandoABebe";
            else if(controllerBaby.toalla2ArropandoABebe) valor = "toalla2ArropandoABebe";
            ActivarVideoRetroalimentacion(valor, "VideoBebeArropado", "AudioBebeArropado", "Dejaste al recién nacido arropado con la manta para optimizar la termorregulación", "No dejaste al recién nacido arropado con la manta para optimizar la termorregulación", "VideoBebeNoArropado", "AudioBebeNoArropado");
        }
        else
        {
            ActivarVideoRetroalimentacion("toallaArropandoABebe", "VideoBebeArropado", "AudioBebeArropado", "Dejaste al recién nacido arropado con la manta para optimizar la termorregulación", "No dejaste al recién nacido arropado con la manta para optimizar la termorregulación", "VideoBebeNoArropado", "AudioBebeNoArropado");
        }
    }
       

    IEnumerator CorrutinaLigatura()
    {
        FindObjectOfType<ControllerBaby>().ligoATiempo = true;
        yield return new WaitForSeconds(15f);

        if (!FindObjectOfType<ControllerBaby>().seLigo)
        {            
            ActivarAudio("RecordatorioLigadura");
            yield return new WaitForSeconds(15f);
            if (!FindObjectOfType<ControllerBaby>().seLigo) FindObjectOfType<ControllerBaby>().ligoATiempo = false;
        }
    }

    public void TomarTiempoLigadura()
    {
        StartCoroutine(CorrutinaLigatura());
    }

    IEnumerator CorrutinaLigaturaVentilacion()
    {
        ligoATiempo = true;
        if (esCasoSinAyuda) yield return new WaitForSeconds(20f);
        else yield return new WaitForSeconds(45f);

        if (!seLigo)
        {
            ActivarAudio("RecordatorioLigadura");
            if (!seLigo) ligoATiempo = false;
        }
    }

    public void TomarTiempoLigaduraVentilacion()
    {
        StartCoroutine(CorrutinaLigaturaVentilacion());
    }

    IEnumerator CorrutinaCordonPinzamientoInmediato()
    {
        if(int.Parse(cronometro.timerMinutes) ==0 && int.Parse(cronometro.timerSeconds) < 45) ordenoPinzamientoATiempo = true;
        else if (int.Parse(cronometro.timerMinutes) >= 0 && int.Parse(cronometro.timerSeconds) >= 45) ordenoPinzamientoATiempo=false;
        yield return new WaitForSeconds(10f);

        if (!seCortoCordon)
        {
            ActivarAudio("RecordatorioCordonInmediato");
            ordenoPinzamientoATiempo = false;
        }
    }

    public void TomarTiempoPinzamientoInmediato()
    {
        StartCoroutine(CorrutinaCordonPinzamientoInmediato());
    }


    IEnumerator CorrutinaTrasladarACalentador()
    {
        if (ordenoPinzamientoATiempo) llevoACalentador = true;
        if (!ordenoPinzamientoATiempo) llevoACalentador = false;
        yield return new WaitForSeconds(10f);

        if (!FindObjectOfType<ControllerBaby>().pasoBolsa)
        {
            llevoACalentador = false;
        }
    }

    public void TomarTiempoTrasladarACalentador()
    {
        StartCoroutine(CorrutinaTrasladarACalentador());
    }

    IEnumerator CorrutinaTrasladarAMadre()
    {
        regresoMadre = true;
        yield return new WaitForSeconds(15f);

        if (!FindObjectOfType<ControllerBaby>().nuevaPosicionToalla)
        {
            ActivarAudio("RecordatorioLlevarAMadre");
            regresoMadre = false;
        }
    }

    public void TomarTiempoTrasladarAMadre()
    {
        StartCoroutine(CorrutinaTrasladarAMadre());
    }

    public void LogicaGorroBebe()
    {
        if (sePusoGorro) GameObject.Find("GorroBebé").transform.GetChild(0).gameObject.SetActive(true);
        else GameObject.Find("GorroBebé").transform.GetChild(0).gameObject.SetActive(false);
    }

    IEnumerator CorrutinaActividad(string valorBool, string audio, string segundos)
    {
        yield return new WaitForSeconds(float.Parse(segundos));
        if (!ValorAValidar(valorBool))
        {            
            ActivarAudio(audio);
        }
    }

    public void TomarTiempoActividad(string valorBool, string audio, string segundos)
    {
        StartCoroutine(CorrutinaActividad(valorBool, audio, segundos));
    }



}

