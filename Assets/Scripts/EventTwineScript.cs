using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EventTwineScript : MonoBehaviour
{
    Audio inicio;
    private void Awake()
    {
        inicio = FindObjectOfType<Audio>();
    }
    public Text textoFC;
    public GameObject panelReloj;
    public GameObject llantoBebe;
    public GameObject panelInicial;
    public GameObject panelPreguntas;
    public GameObject panelNo;
    public GameObject panelBebe;
    public GameObject panelRiesgo;
    public GameObject panelLiquido;
    public GameObject panelEdad;
    public GameObject panelSiguiente;
    public GameObject panelPreparacion;
    public GameObject panelCronometro;
    public GameObject panelFase1;
    public GameObject panelBuenTono;
    public GameObject panelNada;
    public GameObject panelFinalCasoBase;
    public GameObject panelNoBuenTono;
    public GameObject panelApnea;
    public GameObject panelPresenta;
    public GameObject panelNoPresenta;
    public GameObject panelCianosis;
    public GameObject panelNoCianosis;
    public GameObject panelPosicionViaAerea;
    public GameObject panelAtenconPosterior;
    public GameObject panelUsarVPP;
    public GameObject panelMayor100;
    public GameObject panelMenor100;
    public GameObject panelVentilacion;
    public GameObject panelAumento;
    public GameObject panelDisminuyo;
    public GameObject panelAumentoMayor100;
    public GameObject panelIntubar;

    // Start is called before the first frame update
    public void onClickPreguntas()
    {
        print("entered node");
        panelPreguntas.SetActive(true);
        panelInicial.SetActive(false);        
    }

    public void onClickNo()
    {
        print("entered node");
        panelNo.SetActive(true);
        panelInicial.SetActive(false);
    }

    public void onClickOk()
    {
        print("entered node");
        panelPreguntas.SetActive(true);
        panelNo.SetActive(false);
    }

    public void onClickBebe()
    {
        print("entered node");
        panelBebe.SetActive(true);
        panelPreguntas.SetActive(false);
    }

    public void onClickRiesgo()
    {
        print("entered node");
        panelRiesgo.SetActive(true);
        panelPreguntas.SetActive(false);
    }

    public void onClickEdad()
    {
        print("entered node");
        panelEdad.SetActive(true);
        panelPreguntas.SetActive(false);
    }

    public void onClickLiquido()
    {
        print("entered node");
        panelLiquido.SetActive(true);
        panelPreguntas.SetActive(false);
    }

    public void onClickSaleBebe()
    {
        print("entered node");
        panelBebe.SetActive(false);
        panelPreguntas.SetActive(true);
    }

    public void onClickSaleRiesgo()
    {
        print("entered node");
        panelRiesgo.SetActive(false);
        panelPreguntas.SetActive(true);
    }

    public void onClickSaleEdad()
    {
        print("entered node");
        panelEdad.SetActive(false);
        panelPreguntas.SetActive(true);
    }

    public void onClickSaleLiquido()
    {
        print("entered node");
        panelLiquido.SetActive(false);
        panelPreguntas.SetActive(true);
    }

    public void onClickSaleNoBebe()
    {
        print("entered node");
        panelBebe.SetActive(false);
        panelSiguiente.SetActive(true);
    }

    public void onClickSaleNoRiesgo()
    {
        print("entered node");
        panelRiesgo.SetActive(false);
        panelSiguiente.SetActive(true);
    }

    public void onClickSaleNoEdad()
    {
        print("entered node");
        panelEdad.SetActive(false);
        panelSiguiente.SetActive(true);
    }

    public void onClickSaleNoLiquido()
    {
        print("entered node");
        panelLiquido.SetActive(false);
        panelSiguiente.SetActive(true);
    }

    public void onClickMasPreguntas()
    {
        print("entered node");
        panelPreguntas.SetActive(true);
        panelSiguiente.SetActive(false);
    }

    public void onClickPreparacion()
    {
        print("entered node");
        panelPreparacion.SetActive(true);
        panelSiguiente.SetActive(false);
    }

    public void onClickCronometro()
    {
        print("entered node");
        panelCronometro.SetActive(true);
        panelPreparacion.SetActive(false);
    }

    public void onClickFase1Inicia()
    {
        print("entered node");
        GameObject.Find("LipSyncTargets").transform.position = new Vector3(30.62f, 2.5f, -49.33f);
        GameObject.Find("LipSyncTargets").transform.rotation = Quaternion.Euler(0f, 88.1f, 0f);
        panelReloj.SetActive(true);
        panelFase1.SetActive(true);
        //Inicia el tiempo a correr
        panelCronometro.SetActive(false);
        //Se eliminan los outlines de los objetos
        GameObject[] lista = GameObject.FindGameObjectsWithTag("Outline");
        foreach (var gObj in lista)
        {
            Destroy(gObj);
        }
        llantoBebe.SetActive(true);
        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/DialogoFase1")));
        GameObject.Find("EGC").GetComponentInChildren<VideoPlayer>().enabled = true;
    }

    public void onClickSiRespira()
    {
        print("entered node");
        panelBuenTono.SetActive(true);
        panelFase1.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/DialogoRespira")));
    }

    public void onClickHaceProceso()
    {
        print("entered node");
        panelFinalCasoBase.SetActive(true);
        panelFase1.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/DialogoFin1")));
    }

    public void onClickFinCasoBase1()
    {
        print("entered node");
        panelFinalCasoBase.SetActive(true);
        panelNada.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/DialogoFin1")));
    }

    public void onClickNoHaceNada()
    {
        print("entered node");
        panelNada.SetActive(true);
        panelFase1.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/DialogoNada")));
    }

    public void onClickNoRespira()
    {
        print("entered node");
        panelNoBuenTono.SetActive(true);
        panelFase1.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/DialogoNo")));
    }

    public void onClickApnea()
    {
        print("entered node");
        panelApnea.SetActive(true);
        panelNoBuenTono.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/DialogoPreguntaApnea")));
    }

    public void onClickSiApnea()
    {
        print("entered node");
        panelPresenta.SetActive(true);
        panelApnea.SetActive(false);
        textoFC.text = "FC 70 lpm";
        GameObject.Find("EGC").GetComponentInChildren<VideoPlayer>().playbackSpeed = 1.5f;
        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Presenta")));
    }

    public void onClickNoApnea()
    {
        print("entered node");
        panelNoPresenta.SetActive(true);
        panelApnea.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/No presenta")));
    }

    public void onClickSiCianosis()
    {
        print("entered node");
        panelCianosis.SetActive(true);
        panelNoPresenta.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Cianosis")));
    }

    public void onClickNoCianosis()
    {
        print("entered node");
        panelNoCianosis.SetActive(true);
        panelNoPresenta.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/No cianosis")));
    }

    public void onClickRevisarDeNuevo()
    {
        print("entered node");
        panelCianosis.SetActive(true);
        panelNoCianosis.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Cianosis")));
    }

    public void onClickPosicionarVia()
    {
        print("entered node");
        panelPosicionViaAerea.SetActive(true);
        panelCianosis.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Posicion via aerea")));
    }

    public void onClickAtencionPosterior()
    {
        print("entered node");
        panelAtenconPosterior.SetActive(true);
        panelPosicionViaAerea.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Atencion Posterior")));
    }

    public void onClickUsarVPP()
    {
        print("entered node");
        panelUsarVPP.SetActive(true);
        panelPresenta.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Usar VPP")));
    }

    public void onClickMayor100()
    {
        print("entered node");
        panelMayor100.SetActive(true);
        panelUsarVPP.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Mayor 100")));
    }

    public void onClickTerminarMayor100()
    {
        print("entered node");
        panelAtenconPosterior.SetActive(true);
        panelMayor100.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Atencion Posterior")));
    }

    public void onClickMenor100()
    {
        print("entered node");
        panelMenor100.SetActive(true);
        panelUsarVPP.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Menor 100")));
    }

    public void onClickEjecutarVentilacion()
    {
        print("entered node");
        panelVentilacion.SetActive(true);
        panelMenor100.SetActive(false);
        textoFC.text = "FC 110 lpm";
        GameObject.Find("EGC").GetComponentInChildren<VideoPlayer>().playbackSpeed = 1f;
        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Ventilacion")));
    }

    public void onClickAumento()
    {
        print("entered node");
        panelAumento.SetActive(true);
        panelVentilacion.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Aumento")));
    }

    public void onClickAumentoMayor100()
    {
        print("entered node");
        panelAumentoMayor100.SetActive(true);
        panelAumento.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Aumento Mayor 100")));
    }

    public void onClickAumentoMenor100()
    {
        print("entered node");
        panelUsarVPP.SetActive(true);
        panelAumento.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Usar VPP")));
    }

    public void onClickFinAumentoMayor100()
    {
        print("entered node");
        panelAtenconPosterior.SetActive(true);
        panelAumentoMayor100.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Atencion Posterior")));
    }

    public void onClickDisminuyo()
    {
        print("entered node");
        panelDisminuyo.SetActive(true);
        panelVentilacion.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Disminuyo")));
    }

    public void onClickIntubar()
    {
        print("entered node");
        panelIntubar.SetActive(true);
        panelDisminuyo.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Intubar")));
    }

    public void onClickFinIntubar()
    {
        print("entered node");
        panelAtenconPosterior.SetActive(true);
        panelIntubar.SetActive(false);

        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Atencion Posterior")));
    }

    IEnumerator Explicar(AudioClip audio)
    {
        //El audio debe desabilitarse para que pueda iniciarse despues
        inicio.audioData.enabled = false;
        inicio.audioData.clip = audio;
        //El ovrlypsinc tambien debe deshabilitarse para que el lipsync inicie nuevamente
        inicio.ovrLipsync.enabled = false;
        //Cuando se habilita el lipsync y el audio, el personaje inicia a hablar de nuevo
        inicio.ovrLipsync.enabled = true;
        inicio.audioData.enabled = true;
        yield return "listo";
    }
}
