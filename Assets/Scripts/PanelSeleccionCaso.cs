using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PanelSeleccionCaso : MonoBehaviour
{
    public TextMeshProUGUI textoInicial;

    public GameObject cuerpoTextoInicial;
    public GameObject cuerpoPreguntaInicial;
    public GameObject botonMinutoOro;
    public GameObject botonReanimacionAvanzada;

    public GameObject botonVerTutoriales;
    public GameObject botonOmitirTutoriales;

    public GameObject ejercicioSinAyuda;
    public GameObject ejercicioGuiado;

    public GameObject btnCasoCompleto;
    public GameObject btnCasoParteReanimacion;

    public GameObject btnFlujo1;
    public GameObject btnFlujo2;
    public GameObject btnFlujo3;
    public GameObject btnFlujo6;
    public GameObject btnFlujo8;
    public GameObject btnFlujoRandom;

    public GameObject btnFlujo1RA;
    public GameObject btnFlujo2RA;
    public GameObject btnFlujo3RA;

    public GameObject btnFlujoAutoinflable;
    public GameObject btnFlujoNeotee;
    public GameObject btnFlujoNeopuff;

    public GameObject btnAtras;

    Audio inicio;
    public Inicio scriptInicio;

    public GameObject video;
    bool estaEnTutorial = true;

    private void Awake()
    {
        inicio = FindObjectOfType<Audio>();
    }

    private void Start()
    {        
        if (SceneManager.GetActiveScene().name == "Hospital") StartCoroutine(DialogoInicial());
                
    }

    IEnumerator DialogoInicial()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = false;
        inicio.audioData.enabled = false;
        inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/InstruccionPanelInicial");
        inicio.ovrLipsync.enabled = false;
        inicio.ovrLipsync.enabled = true;
        inicio.audioData.enabled = true;
        yield return new WaitForSeconds(inicio.audioData.clip.length);

        if (estaEnTutorial)
        {
            video.SetActive(true);
            textoInicial.text = "Oprime el botón de atrás del control derecho para seleccionar una opción.";
            inicio.audioData.enabled = false;
            inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/InstruccionPanelInicial2");
            inicio.ovrLipsync.enabled = false;
            inicio.ovrLipsync.enabled = true;
            inicio.audioData.enabled = true;
            yield return new WaitForSeconds(inicio.audioData.clip.length);

            if (estaEnTutorial)
            {
                video.SetActive(false);
                Destroy(cuerpoTextoInicial);
                cuerpoPreguntaInicial.SetActive(true);
                botonMinutoOro.SetActive(true);
                botonReanimacionAvanzada.SetActive(true);
                Destroy(GameObject.Find("BtnSaltarEtapa"));
            }
        }
    }

    public void SaltarEtapaInicial()
    {
        Destroy(GameObject.Find("BtnSaltarEtapa"));
        estaEnTutorial = false;
        video.SetActive(false);
        Destroy(cuerpoTextoInicial);
        cuerpoPreguntaInicial.SetActive(true);
        botonMinutoOro.SetActive(true);
        botonReanimacionAvanzada.SetActive(true);
        inicio.audioData.clip = null;
    }

    public void ElegirCasoMinutoOro()
    {
        scriptInicio.SeleccionarTipoCaso(true, false);
        HabilitarBotonesParaCasosSinAyuda();
        btnAtras.SetActive(true);
    }

    public void ElegirCasoReanimación()
    {
        scriptInicio.SeleccionarTipoCaso(false, true);
        HabilitarBotonesParaCasosSinAyuda();
        btnAtras.SetActive(true);
    }

    public void ElegirCasoGuiado()
    {
        inicio.casoGuiado = true;
        HabilitarBotonesSaltarCaso();
    }

    public void ElegirCasoSinAyuda()
    {
        inicio.casoGuiado = false;
        HabilitarBotonesSaltarCaso();

    }

    public void HabilitarBotonesSaltarCaso()
    {
        cuerpoPreguntaInicial.GetComponent<TextMeshProUGUI>().text = "¿Quieres hacer todo el ejercicio o saltarte al proceso de reanimación?";
        botonMinutoOro.SetActive(false);
        botonReanimacionAvanzada.SetActive(false);
        ejercicioGuiado.SetActive(false);
        ejercicioSinAyuda.SetActive(false);

        btnCasoCompleto.SetActive(true);
        btnCasoParteReanimacion.SetActive(true);
    }

    public void elegirCaso1()
    {
        inicio.EstablecerCasoSeleccionado("casoBebeNaceBien");
        HabilitarBotones();
        //GameObject.Find("InstruccionesPadre").transform.GetChild(0).gameObject.SetActive(true);
        //GameObject.Find("PanelSeleccion").SetActive(false);
    }

    public void elegirCaso2()
    {
        inicio.EstablecerCasoSeleccionado("casoBebeMejora");
        HabilitarBotones();
        //GameObject.Find("InstruccionesPadre").transform.GetChild(0).gameObject.SetActive(true);
        //GameObject.Find("PanelSeleccion").SetActive(false);
    }

    public void elegirCaso3()
    {
        inicio.EstablecerCasoSeleccionado("casoBebeNoMejora");
        HabilitarBotones();
        //GameObject.Find("InstruccionesPadre").transform.GetChild(0).gameObject.SetActive(true);
        //GameObject.Find("PanelSeleccion").SetActive(false);
    }
       

    public void elegirCaso6()
    {
        inicio.EstablecerCasoSeleccionado("casoBebeSinRespirarMejoraConSecado");
        HabilitarBotones();
        //GameObject.Find("InstruccionesPadre").transform.GetChild(0).gameObject.SetActive(true);
        //GameObject.Find("PanelSeleccion").SetActive(false);
    }

    public void elegirCaso8()
    {
        inicio.EstablecerCasoSeleccionado("casoBebeCianoticoSinRespirarMejoraConAspiracion");
        HabilitarBotones();
        //GameObject.Find("InstruccionesPadre").transform.GetChild(0).gameObject.SetActive(true);
        //GameObject.Find("PanelSeleccion").SetActive(false);
    }

    public void elegirCaso1RA()
    {
        inicio.EstablecerCasoSeleccionado("casoBebeSinRespirarMejora");
        HabilitarBotones();
    }

    public void elegirCaso2RA()
    {
        inicio.EstablecerCasoSeleccionado("casoBebeSinRespirarNoMejora");
        HabilitarBotones();
    }

    public void elegirCaso3RA()
    {
        inicio.EstablecerCasoSeleccionado("casoBebeConDificultadRespiratoria");
        HabilitarBotones();
    }


    public void elegirCasoAutoinflable()
    {
        inicio.EstablecerDispositivo("Bolsa autoinflable");
        HabilitarBotonesRA();
    }

    public void elegirCasoNeotee()
    {
        inicio.EstablecerDispositivo("Neotee");
        HabilitarBotonesRA();
    }

    public void elegirCasoNeopuff()
    {
        inicio.EstablecerDispositivo("Neopuff");
        HabilitarBotonesRA();
    }

    public void elegirCasoRandom()
    {
        inicio.EstablecerCasoSeleccionado("random");
        HabilitarBotones();
        //GameObject.Find("InstruccionesPadre").transform.GetChild(0).gameObject.SetActive(true);
        //GameObject.Find("PanelSeleccion").SetActive(false);
    }


    public void DecisionSaltarCaso()
    {
        scriptInicio.SeleccionarSaltarCaso(true);
        
        botonMinutoOro.SetActive(false);
        botonReanimacionAvanzada.SetActive(false);
        ejercicioGuiado.SetActive(false);
        ejercicioSinAyuda.SetActive(false);

        btnCasoCompleto.SetActive(false);
        btnCasoParteReanimacion.SetActive(false);

        //inicio.seDebenIgnorarIntrucciones = true;

        if (scriptInicio.minutoOro)
        {
            cuerpoPreguntaInicial.GetComponent<TextMeshProUGUI>().text = "Seleccione el caso específico que quiere realizar";
            //SE AGREGA ESTO TEMPORALMENTE
            btnFlujo1.SetActive(true);
            btnFlujo2.SetActive(true);
            //btnFlujo3.SetActive(true);
            btnFlujo6.SetActive(true);
            btnFlujo8.SetActive(true);
            btnFlujoRandom.SetActive(true);
        }
        else
        {
            cuerpoPreguntaInicial.GetComponent<TextMeshProUGUI>().text = "Seleccione el dispositivo con el que quiere practicar";
            /**
            btnFlujo1RA.SetActive(true);
            btnFlujo2RA.SetActive(true);
            btnFlujo3RA.SetActive(true);
            btnFlujo4RA.SetActive(true);
            btnFlujo5RA.SetActive(true);
            **/
            btnFlujoAutoinflable.SetActive(true);
            btnFlujoNeotee.SetActive(true);
            btnFlujoNeopuff.SetActive(true);
        }        

        // SE DESACTIVA ESTO TEMPORALMENTE GameObject.Find("InstruccionesPadre").transform.GetChild(0).gameObject.SetActive(true);
        // SE DESACTIVA ESTO TEMPORALMENTE GameObject.Find("PanelSeleccion").SetActive(false);

    }

    public void DecisionCasoCompleto()
    {
        scriptInicio.SeleccionarSaltarCaso(false);
        
        botonMinutoOro.SetActive(false);
        botonReanimacionAvanzada.SetActive(false);
        ejercicioGuiado.SetActive(false);
        ejercicioSinAyuda.SetActive(false);

        btnCasoCompleto.SetActive(false);
        btnCasoParteReanimacion.SetActive(false);

        if (scriptInicio.minutoOro)
        {
            cuerpoPreguntaInicial.GetComponent<TextMeshProUGUI>().text = "Seleccione el caso específico que quiere realizar";
            //SE AGREGA ESTO TEMPORALMENTE
            btnFlujo1.SetActive(true);
            btnFlujo2.SetActive(true);
            //btnFlujo3.SetActive(true);
            btnFlujo6.SetActive(true);
            btnFlujo8.SetActive(true);
            btnFlujoRandom.SetActive(true);
        }
        else
        {
            cuerpoPreguntaInicial.GetComponent<TextMeshProUGUI>().text = "Seleccione el dispositivo con el que quiere practicar";
            /**
            btnFlujo1RA.SetActive(true);
            btnFlujo2RA.SetActive(true);
            btnFlujo3RA.SetActive(true);
            btnFlujo4RA.SetActive(true);
            btnFlujo5RA.SetActive(true);
            **/
            btnFlujoAutoinflable.SetActive(true);
            btnFlujoNeotee.SetActive(true);
            btnFlujoNeopuff.SetActive(true);
        }
                    

        // SE DESACTIVA ESTO TEMPORALMENTE GameObject.Find("InstruccionesPadre").transform.GetChild(0).gameObject.SetActive(true);
        // SE DESACTIVA ESTO TEMPORALMENTE GameObject.Find("PanelSeleccion").SetActive(false);

    }

    public void HabilitarBotones()
    {
        cuerpoPreguntaInicial.GetComponent<TextMeshProUGUI>().text = "¿Quieres ver tutoriales de los controles durante la simulación?";
        botonMinutoOro.SetActive(false);
        botonReanimacionAvanzada.SetActive(false);
        ejercicioGuiado.SetActive(false);
        ejercicioSinAyuda.SetActive(false);
        btnCasoCompleto.SetActive(false);
        btnCasoParteReanimacion.SetActive(false);

        if (scriptInicio.minutoOro)
        {
            btnFlujo1.SetActive(false);
            btnFlujo2.SetActive(false);
            //btnFlujo3.SetActive(false);
            btnFlujo6.SetActive(false);
            btnFlujo8.SetActive(false);
            btnFlujoRandom.SetActive(false);
        }
        else
        {
            btnFlujo1RA.SetActive(false);
            btnFlujo2RA.SetActive(false);
            btnFlujo3RA.SetActive(false);            
        }
        
        botonOmitirTutoriales.SetActive(true);
        botonVerTutoriales.SetActive(true);
    }

    public void HabilitarBotonesRA()
    {
        cuerpoPreguntaInicial.GetComponent<TextMeshProUGUI>().text = "Seleccione el caso específico que quiere realizar";
        botonMinutoOro.SetActive(false);
        botonReanimacionAvanzada.SetActive(false);
        ejercicioGuiado.SetActive(false);
        ejercicioSinAyuda.SetActive(false);
        btnCasoCompleto.SetActive(false);
        btnCasoParteReanimacion.SetActive(false);


        btnFlujo1RA.SetActive(true);
        btnFlujo2RA.SetActive(true);
        if(inicio.dispositivoVentilacionSeleccionado!= "Bolsa autoinflable") btnFlujo3RA.SetActive(true);

        btnFlujoAutoinflable.SetActive(false);
        btnFlujoNeotee.SetActive(false);
        btnFlujoNeopuff.SetActive(false);

        btnAtras.SetActive(true);
    }

    public void HabilitarBotonesParaCasosSinAyuda()
    {
        cuerpoPreguntaInicial.GetComponent<TextMeshProUGUI>().text = "¿Quieres que la simulación sea guiada\npara aprender o que sea sin ayudas?";
        botonMinutoOro.SetActive(false);
        botonReanimacionAvanzada.SetActive(false);
        botonOmitirTutoriales.SetActive(false);
        botonVerTutoriales.SetActive(false);

        ejercicioGuiado.SetActive(true);
        if(scriptInicio.minutoOro) ejercicioSinAyuda.SetActive(true);
    }

    public void NoIgnorarInstrucciones()
    {
        inicio.seDebenIgnorarIntrucciones = false;
        GameObject.Find("InstruccionesPadre").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("InstruccionesPadre").transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("InstruccionesPadre").transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("InstruccionesPadre").transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("InstruccionesPadre").transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("InstruccionesPadre").transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("PanelSeleccion").SetActive(false);
    }

    public void IgnorarInstrucciones()
    {
        inicio.seDebenIgnorarIntrucciones = true;
        GameObject.Find("InstruccionesPadre").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("PanelSeleccion").SetActive(false);
        
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


    public void atras()
    {
        if (ejercicioGuiado.activeInHierarchy)
        {
            cuerpoPreguntaInicial.GetComponent<TextMeshProUGUI>().text = "¿Con qué tipo de caso quieres practicar?";
            botonMinutoOro.SetActive(true);
            botonReanimacionAvanzada.SetActive(true);
            btnAtras.SetActive(false);
            ejercicioGuiado.SetActive(false);
            ejercicioSinAyuda.SetActive(false);
        }
        else if (btnCasoCompleto.activeInHierarchy)
        {
            HabilitarBotonesParaCasosSinAyuda();
            btnCasoCompleto.SetActive(false);
            btnCasoParteReanimacion.SetActive(false);
            btnAtras.SetActive(true);
        }
        else if (btnFlujoRandom.activeInHierarchy)
        {
            HabilitarBotonesSaltarCaso();
            if (scriptInicio.minutoOro)
            {
                btnFlujo1.SetActive(false);
                btnFlujo2.SetActive(false);
                //btnFlujo3.SetActive(false);
                btnFlujo6.SetActive(false);
                btnFlujo8.SetActive(false);
                btnFlujoRandom.SetActive(false);
            }
        }
        else if (btnFlujo1RA.activeInHierarchy)
        {
            cuerpoPreguntaInicial.GetComponent<TextMeshProUGUI>().text = "Seleccione el dispositivo con el que quiere practicar";

            btnFlujoAutoinflable.SetActive(true);
            btnFlujoNeotee.SetActive(true);
            btnFlujoNeopuff.SetActive(true);

            btnFlujo1RA.SetActive(false);
            btnFlujo2RA.SetActive(false);
            btnFlujo3RA.SetActive(false);

        }

        else if (btnFlujoAutoinflable.activeInHierarchy)
        {
            HabilitarBotonesSaltarCaso();
            btnFlujoAutoinflable.SetActive(false);
            btnFlujoNeotee.SetActive(false);
            btnFlujoNeopuff.SetActive(false);

        }
        else if (botonOmitirTutoriales.activeInHierarchy)
        {
            cuerpoPreguntaInicial.GetComponent<TextMeshProUGUI>().text = "Seleccione el caso específico que quiere realizar";
            botonMinutoOro.SetActive(false);
            botonReanimacionAvanzada.SetActive(false);
            ejercicioGuiado.SetActive(false);
            ejercicioSinAyuda.SetActive(false);

            btnCasoCompleto.SetActive(false);
            btnCasoParteReanimacion.SetActive(false);

            if (scriptInicio.minutoOro)
            {
                //SE AGREGA ESTO TEMPORALMENTE
                btnFlujo1.SetActive(true);
                btnFlujo2.SetActive(true);
                //btnFlujo3.SetActive(true);
                btnFlujo6.SetActive(true);
                btnFlujo8.SetActive(true);
                btnFlujoRandom.SetActive(true);
            }
            else
            {
                btnFlujo1RA.SetActive(true);
                btnFlujo2RA.SetActive(true);
                if (inicio.dispositivoVentilacionSeleccionado != "Bolsa autoinflable") btnFlujo3RA.SetActive(true);
            }


            botonOmitirTutoriales.SetActive(false);
            botonVerTutoriales.SetActive(false);
        }
        
    }
}
