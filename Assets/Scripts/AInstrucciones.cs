using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class AInstrucciones : MonoBehaviour
{
    public TextMeshProUGUI texto;

    Audio inicio;

    public GameObject controlIzq;
    public GameObject controlDer;
    public GameObject derecha;
    public GameObject izquierda;

    public GameObject locomotion;

    public GameObject joystickIzq;
    public GameObject joystickDer;

    public Material outline;
    public Material normal;

    public GameObject video;
    public GameObject triggerTeleport;
    public GameObject botonX;

    public GameObject inicioCaso;


    int contador = 0;
    public bool sePresionoBoton=true;

    CambioMaterial scriptCambio;
    

    private void Awake()
    {
        inicio = FindObjectOfType<Audio>();
        scriptCambio = FindObjectOfType<CambioMaterial>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name=="Hospital")StartCoroutine(Explicacion());
        else
        {
            gameObject.SetActive(false);
            inicioCaso.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<OVRPlayerController>().EnableRotation = true;
            controlDer.SetActive(false);
            derecha.SetActive(true);

            controlIzq.SetActive(false);
            izquierda.SetActive(true);
        }
    }

    IEnumerator Explicacion()
    {
        for (int i = 0; i < scriptCambio.objetosRequeridos.Count; i++)
        {
            print(scriptCambio.objetosRequeridos[i]);
        }

        yield return new WaitForSeconds(0.1f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = false;
        

        if (!inicio.seDebenIgnorarIntrucciones)
        {
            controlDer.SetActive(true);
            derecha.SetActive(false);

            controlIzq.SetActive(true);
            izquierda.SetActive(false);

            contador++;
            texto.text = "Antes de iniciar te explicaremos los controles necesarios para el movimiento. Durante el tutorial no podrás moverte. El movimiento se habilitará hasta el final de esta explicación.";
            inicio.audioData.enabled = false;
            inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Instruccion1");
            inicio.ovrLipsync.enabled = false;
            inicio.ovrLipsync.enabled = true;
            inicio.audioData.enabled = true;
            yield return new WaitForSeconds(inicio.audioData.clip.length);

            texto.text = "En tus manos, podrás ver un modelo de los controles.\n\nDebes estar pendiente de ellos mientras te damos las indicaciones ya que los botones se irán alumbrando cuando se mencionen.";
            inicio.audioData.enabled = false;
            inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Instruccion2");
            inicio.ovrLipsync.enabled = false;
            inicio.ovrLipsync.enabled = true;
            inicio.audioData.enabled = true;
            yield return new WaitForSeconds(inicio.audioData.clip.length);

            joystickIzq.GetComponent<SkinnedMeshRenderer>().material = outline;
            texto.text = "Con el joystick del control de la mano izquierda puedes moverte. Mira el control y verás que un botón se está alumbrando.\n\nEste es el botón con el que podrás recorrer el lugar.";
            inicio.audioData.enabled = false;
            inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Instruccion3");
            inicio.ovrLipsync.enabled = false;
            inicio.ovrLipsync.enabled = true;
            inicio.audioData.enabled = true;
            yield return new WaitForSeconds(inicio.audioData.clip.length);
            yield return new WaitForSeconds(3.5f);

            joystickDer.GetComponent<SkinnedMeshRenderer>().material = outline;
            joystickIzq.GetComponent<SkinnedMeshRenderer>().material = normal;
            texto.text = "Si quieres rotar puedes hacerlo de dos maneras:\n\nPuedes rotar girando tu cuerpo en la vida real o con el joystick de la mano derecha.";
            inicio.audioData.enabled = false;
            inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Instruccion4");
            inicio.ovrLipsync.enabled = false;
            inicio.ovrLipsync.enabled = true;
            inicio.audioData.enabled = true;
            yield return new WaitForSeconds(inicio.audioData.clip.length);
            yield return new WaitForSeconds(3.5f);

            joystickDer.GetComponent<SkinnedMeshRenderer>().material = normal;
            triggerTeleport.GetComponent<SkinnedMeshRenderer>().material = outline;
            texto.text = "ADVERTENCIA: ES POSIBLE QUE SE PUEDAN EXPERIMENTAR MAREOS AL MOVERSE CON EL JOYSTICK. \n\nPor esto, una alternativa es usar la teletransportación.";
            inicio.audioData.enabled = false;
            inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/InstruccionTeleport1");
            inicio.ovrLipsync.enabled = false;
            inicio.ovrLipsync.enabled = true;
            inicio.audioData.enabled = true;
            yield return new WaitForSeconds(inicio.audioData.clip.length);

            texto.text = "Para teletransportarte debes usar el control izquierdo.";
            video.SetActive(true);
            video.GetComponentInChildren<VideoPlayer>().clip = Resources.Load<VideoClip>("IndicacionBotonTeleTransportacion");
            inicio.audioData.enabled = false;
            inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/InstruccionTeleport2");
            inicio.ovrLipsync.enabled = false;
            inicio.ovrLipsync.enabled = true;
            inicio.audioData.enabled = true;
            yield return new WaitForSeconds(inicio.audioData.clip.length);
            yield return new WaitForSeconds(3.5f);

            botonX.GetComponent<SkinnedMeshRenderer>().material = outline;
            triggerTeleport.GetComponent<SkinnedMeshRenderer>().material = normal;
            texto.text = "Oprime el botón de atrás del control IZQUIERDO, apunta, mantén presionado y oprime X para teletransportarte.";
            video.GetComponentInChildren<VideoPlayer>().clip = Resources.Load<VideoClip>("IndicacionBotonX");
            inicio.audioData.enabled = false;
            inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/InstruccionTeleport3");
            inicio.ovrLipsync.enabled = false;
            inicio.ovrLipsync.enabled = true;
            inicio.audioData.enabled = true;
            yield return new WaitForSeconds(inicio.audioData.clip.length);
            yield return new WaitForSeconds(3.5f);

            texto.text = "Intenta hacer esto en caso de presentar mareos.";
            video.GetComponentInChildren<VideoPlayer>().clip = Resources.Load<VideoClip>("TeleportExterno");
            inicio.audioData.enabled = false;
            inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/InstruccionTeleportExterno");
            inicio.ovrLipsync.enabled = false;
            inicio.ovrLipsync.enabled = true;
            inicio.audioData.enabled = true;
            yield return new WaitForSeconds(inicio.audioData.clip.length);

            video.SetActive(false);
            joystickIzq.GetComponent<SkinnedMeshRenderer>().material = outline;
            joystickDer.GetComponent<SkinnedMeshRenderer>().material = outline;
            texto.text = "Podrás moverte cuando se desactive esta pantalla.";
            inicio.audioData.enabled = false;
            inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Instruccion5");
            inicio.ovrLipsync.enabled = false;
            inicio.ovrLipsync.enabled = true;
            inicio.audioData.enabled = true;
            yield return new WaitForSeconds(inicio.audioData.clip.length);
        }        

        gameObject.SetActive(false);
        inicioCaso.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled=true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<OVRPlayerController>().EnableRotation = true;
        locomotion.SetActive(true);
        controlDer.SetActive(false);
        derecha.SetActive(true);
        
        controlIzq.SetActive(false);
        izquierda.SetActive(true);
        joystickIzq.GetComponent<SkinnedMeshRenderer>().material = normal;
        joystickDer.GetComponent<SkinnedMeshRenderer>().material = normal;
        triggerTeleport.GetComponent<SkinnedMeshRenderer>().material = normal;
        botonX.GetComponent<SkinnedMeshRenderer>().material = normal;

        inicio.audioData.enabled = false;
        inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Instruccion6");
        inicio.ovrLipsync.enabled = false;
        inicio.ovrLipsync.enabled = true;
        inicio.audioData.enabled = true;
        yield return new WaitForSeconds(inicio.audioData.clip.length);
    }

    public IEnumerator Explicar(AudioClip audio)
    {
        inicio.audioData.enabled = false;
        inicio.audioData.clip = audio;
        inicio.ovrLipsync.enabled = false;
        inicio.ovrLipsync.enabled = true;
        inicio.audioData.enabled = true;
        yield return "listo";
        yield return new WaitForSeconds(inicio.audioData.clip.length);
    }
}
