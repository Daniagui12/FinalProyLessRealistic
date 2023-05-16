using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ControllerBaby : MonoBehaviour
{
    public bool nuevaPosicionDispRA = false;


    public bool bolsaEnContactoConBebe = false;

    public DialogueViewer1 viewer;
    bool permitido = false;
    public Material materialCianosis;
    public Material materialPiel;
    public Material materialGorrito;

    private float tiempoNecesario = 4.0f;
    private float tiempoNecesarioVentilacion = 6.0f;
    private float transcurridoSecado30Seg;
    float transcurrido;
    float transcurrido1;
    float transcurrido2;
    float transcurrido3;
    float transcurrido4;
    float transcurrido5;

    float transcurridoSecado = 0.0f;
    float tiempoSecadoMinimoNecesario = 4.0f;

    public bool pasoPera = false;
    bool pasoSondaDeSuccion = false;
    public bool pasoBolsa = false;
    public bool pasoNeotee = false;
    public bool pasoNeopuff = false;
    bool pasoBebe = false;
    bool pasoGorro = false;
    public bool pasoCalentador = false;
    bool pasoOlfateo = false;
    public bool pasoAgarrarDisp = false;
    bool procedimientosEnCalentador = false;

    public bool estaEnContacto=false;

    public bool seAspiroBocaBebe = false;
    public bool seAspiroNarizBebe = false;
    public bool seSecoBebe=false;
    public bool sePusoGorro = false;
    public bool seAspiroBebe = false;
    public bool seVentiloBebe = false;

    public bool debePasarAlPonerGorro=false;
    public bool debePasarAlPonerBolsa = false;

    public bool estaEnIntervencion = true;
    bool permiteMostrarVideos=true;


    bool usoToalla = false;
    bool usoPera = false;
    bool usoBolsa = false;
    bool usoNeotee = false;
    bool usoNeopuff = false;
    bool usoSonda = false;

    bool bebeMejora = false;
    bool puedeActivarAudio = true;

    public bool cianotico = false;

    public bool activarSecuenciaVPP = false;
    public bool toallaArropandoABebe = false;
    public bool toalla2ArropandoABebe = false;

    public bool puedePasarMantaGorro = false;
    public bool yaPasoGorro = false;
    public bool yaPasoManta = false;

    public bool usoEstetoscopio = false;

    public bool pasoPinza = false;
    public bool pasoEstetoscopio = false;

    public bool posicionDispFlujoLibre = false;
    public bool seLigo = false;

    //Valores para retroalimentacion en casos sin guia
    public bool seco15 = false;
    public bool seco30 = false;
    public bool ordenoPinzamientoATiempo=false;
    public bool ligoATiempo = true;
    public bool aspiro30 = false;
    public bool llevoACalentador = false;
    public bool ventilo30 = false;
    public bool regresoMadre = false;

    public bool nuevaPosicionToalla = false;

    public void PermiteDebePasarAlPonerGorro()
    {
        debePasarAlPonerGorro= true;
    }

    public void PermiteDebePasarAlPonerBolsa(string pasar)
    {
        debePasarAlPonerBolsa = bool.Parse(pasar);
    }

    public void ReajustarPosicionToalla()
    {
        nuevaPosicionToalla = true;
    }


    public void ImpedirMostrarVideo()
    {
        permiteMostrarVideos = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bolsa autoinflable" && !pasoBolsa && estaEnIntervencion && puedeActivarAudio)
        {
            bolsaEnContactoConBebe = true;
        }
        else if (other.gameObject.name == "Estetoscopio")
        {
            usoEstetoscopio = true;
            if (FindObjectOfType<ControllerTwine>() != null) FindObjectOfType<ControllerTwine>().usoEstetoscopio = true;
            GameObject.Find("CanvasFCBebeEnMama").transform.GetChild(0).gameObject.SetActive(true);
            if (!bebeMejora)
            {
                int random = Random.Range(90, 95);
                GameObject.Find("FrecuenciaEstetoscopio").GetComponent<TextMeshProUGUI>().text = "FC: " + random + " lat/min";
                GameObject.Find("FrecuenciaEstetoscopioBebeEnMama").GetComponent<TextMeshProUGUI>().text = "FC: " + random + " lat/min";
            }
            else if (bebeMejora)
            {
                int randomBien = Random.Range(135, 145);
                GameObject.Find("FrecuenciaEstetoscopioBebeEnMama").GetComponent<TextMeshProUGUI>().text = "FC: " + randomBien + " lat/min";
                GameObject.Find("FrecuenciaEstetoscopio").GetComponent<TextMeshProUGUI>().text = "FC: " + randomBien + " lat/min";
            }

            if (pasoEstetoscopio) viewer.ChangeNode();

        }
        else if (other.gameObject.name == "Pinza" && pasoPinza)
        {
            seLigo = true;
            if (FindObjectOfType<ControllerTwine>() != null) FindObjectOfType<ControllerTwine>().seLigo = true;
            if (GameObject.Find("PinzaBebe") != null) GameObject.Find("PinzaBebe").transform.GetChild(0).gameObject.SetActive(true);
            Destroy(other.gameObject);
            viewer.ChangeNode();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Estetoscopio")
        {
            usoEstetoscopio = true;
            if (FindObjectOfType<ControllerTwine>() != null) FindObjectOfType<ControllerTwine>().usoEstetoscopio = true;
            GameObject.Find("FrecuenciaEstetoscopio").GetComponent<TextMeshProUGUI>().text = "FC: --- lat/min";
            GameObject.Find("CanvasFCBebeEnMama").transform.GetChild(0).gameObject.SetActive(false);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if((other.gameObject.name == "Gorro" || other.gameObject.name == "Gorro1") && pasoGorro)
        {
            if (viewer.lastNodeTitle == "Paso antes de pinzamiento")
            {
                sePusoGorro = true;
                if (FindObjectOfType<ControllerTwine>() != null) FindObjectOfType<ControllerTwine>().sePusoGorro = true;
                GameObject.Find("GorroBebé").transform.GetChild(0).gameObject.SetActive(true);
                if ((toallaArropandoABebe || toalla2ArropandoABebe) && puedePasarMantaGorro && !yaPasoGorro)
                {
                    sePusoGorro = false;
                    yaPasoGorro = true;
                    viewer.ChangeNode();
                }
            }
            if (viewer.lastNodeTitle != "Paso antes de pinzamiento")
            {
                sePusoGorro = true;
                if (FindObjectOfType<ControllerTwine>() != null) FindObjectOfType<ControllerTwine>().sePusoGorro = true;
                GameObject.Find("GorroBebé").transform.GetChild(0).gameObject.SetActive(true);
            }
            if (debePasarAlPonerGorro) viewer.ChangeNode();
            Destroy(GameObject.Find("Gorro"));
            Destroy(GameObject.Find("Gorro1"));
            
        }

        if ((other.gameObject.name == "Toalla") || (other.gameObject.name == "Toalla2"))
        {
            if (viewer.lastNodeTitle == "Paso antes de pinzamiento")
            {
                if (sePusoGorro && puedePasarMantaGorro && !yaPasoManta)
                {
                    toallaArropandoABebe = false;
                    toalla2ArropandoABebe = false;
                    yaPasoManta = true;
                    viewer.ChangeNode();
                }
            }
            
        }

        if ((other.gameObject.name == "Toalla" || other.gameObject.name == "Toalla2" || other.gameObject.name == "Toalla3") && (pasoPera || pasoBolsa))
        {
            seSecoBebe = true;
        }

        else if ((other.gameObject.name == "Toalla" || other.gameObject.name == "Toalla2") && permitido)
        {            
            estaEnContacto = true;


            transcurridoSecado += Time.fixedDeltaTime;
            if (transcurridoSecado >= tiempoSecadoMinimoNecesario)
            {
                seSecoBebe = true;
            }
            
        }
        if (other.gameObject.name == "Toalla")
        {
            if ((GameObject.Find("ControllerTwine").GetComponent<ControllerTwine>().casoBase || GameObject.Find("ControllerTwine").GetComponent<ControllerTwine>().enContactoConMadre) && (other.gameObject.GetComponent<OVRGrabbable>()!=null && !other.gameObject.GetComponent<OVRGrabbable>().isGrabbed))
            {
                if (!toalla2ArropandoABebe)
                {
                    toallaArropandoABebe = true;
                    GameObject.FindGameObjectWithTag("Toalla").GetComponent<MeshRenderer>().enabled = false;
                    GameObject.FindGameObjectWithTag("Toalla").transform.GetChild(0).gameObject.SetActive(false);
                    GameObject.FindGameObjectWithTag("Toalla").transform.localPosition = new Vector3(-49.86514f, 2.421448f, -20.60479f);
                    GameObject.FindGameObjectWithTag("Toalla").transform.localRotation = Quaternion.Euler(83.96101f, -82.98f, -83.277f);

                    if (!pasoCalentador || !procedimientosEnCalentador)
                    {
                        if (!nuevaPosicionToalla)
                        {
                            GameObject.Find("ToallaEstatica").transform.position = new Vector3(20.51092f, 2.495184f, -50.57784f);
                            GameObject.Find("ToallaEstatica").transform.rotation = Quaternion.Euler(-13.696f, 3.828f, 0.094f);
                        }
                        else
                        {
                            GameObject.Find("ToallaEstatica").transform.position = new Vector3(20.563f, 2.503f, -50.609f);
                            GameObject.Find("ToallaEstatica").transform.rotation = Quaternion.Euler(-19.104f, 0f, 0f);
                        }
                    }
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Toalla").transform.localPosition = new Vector3(-50.325f, 2.14f, -21.371f);
                    GameObject.FindGameObjectWithTag("Toalla").transform.localRotation = Quaternion.Euler(-90f, 0f, 90f);
                }

            }
            else if(!GameObject.Find("ControllerTwine").GetComponent<ControllerTwine>().casoBase && !other.gameObject.GetComponent<OVRGrabbable>().isGrabbed)
            {
                GameObject.FindGameObjectWithTag("Toalla").transform.localPosition = new Vector3(-50.325f, 2.14f, -21.371f);
                GameObject.FindGameObjectWithTag("Toalla").transform.localRotation = Quaternion.Euler(-90f, 0f, 90f);
            }
        } 
        if (other.gameObject.name == "Toalla2")
        {
            if ((GameObject.Find("ControllerTwine").GetComponent<ControllerTwine>().casoBase || GameObject.Find("ControllerTwine").GetComponent<ControllerTwine>().enContactoConMadre) && !other.gameObject.GetComponent<OVRGrabbable>().isGrabbed)
            {
                if(!toallaArropandoABebe)
                {
                    toalla2ArropandoABebe = true;
                    GameObject.Find("Toalla2").GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find("Toalla2").transform.localPosition = new Vector3(-49.86514f, 2.421448f, -20.60479f);
                    GameObject.Find("Toalla2").transform.localRotation = Quaternion.Euler(83.96101f, -82.98f, -83.277f);

                    if (!nuevaPosicionToalla)
                    {
                        GameObject.Find("ToallaEstatica").transform.position = new Vector3(20.51092f, 2.495184f, -50.57784f);
                        GameObject.Find("ToallaEstatica").transform.rotation = Quaternion.Euler(-13.696f, 3.828f, 0.094f);
                    }
                    else
                    {
                        GameObject.Find("ToallaEstatica").transform.position = new Vector3(20.563f, 2.503f, -50.609f);
                        GameObject.Find("ToallaEstatica").transform.rotation = Quaternion.Euler(-19.104f, 0f, 0f);
                    }
                }
                else
                {
                    other.gameObject.transform.localPosition = new Vector3(-50.325f, 2.14f, -21.582f);
                    other.gameObject.transform.localRotation = Quaternion.Euler(-90f, 0f, 90f);
                }

            }
            else if (!GameObject.Find("ControllerTwine").GetComponent<ControllerTwine>().casoBase && !other.gameObject.GetComponent<OVRGrabbable>().isGrabbed)
            {
                other.gameObject.transform.localPosition = new Vector3(-50.325f, 2.14f, -21.582f);
                other.gameObject.transform.localRotation = Quaternion.Euler(-90f, 0f, 90f);
            }
        }

        else if (other.gameObject.name == "Perilla De Goma" && pasoPera && pasoCalentador)
        {
            seAspiroBebe = true;            
        }
        else if (other.gameObject.name == "Perilla De Goma" && pasoPera)
        {
            seAspiroBebe = true;
        }
        
        else if (other.gameObject.name == "Sonda de succión" && pasoSondaDeSuccion)
        {
            seAspiroBebe = true;
            
        }
        
        else if ((other.gameObject.name == "Bolsa autoinflable" || other.gameObject.name == "Bolsa autoinflable1") && pasoBolsa)
        {
            bolsaEnContactoConBebe = true;
            seVentiloBebe = true;

            if (activarSecuenciaVPP)
            {
                activarSecuenciaVPP = false;
                ReproducirCadenciaVPP();
            }

            if(GameObject.FindGameObjectWithTag("Bolsa").GetComponent<OVRGrabbable>()!=null) Destroy(GameObject.FindGameObjectWithTag("Bolsa").GetComponent<OVRGrabbable>());
            if (GameObject.FindGameObjectWithTag("Bolsa").GetComponent<Rigidbody>() != null) Destroy(GameObject.FindGameObjectWithTag("Bolsa").GetComponent<Rigidbody>());
            if(GameObject.Find("SeNecesitaBolsa")!=null) GameObject.Find("SeNecesitaBolsa").transform.GetChild(0).gameObject.SetActive(false);
            if (!FindObjectOfType<ControllerTwine>().esCasoReanimacion)
            {
                GameObject.FindGameObjectWithTag("Bolsa").transform.localPosition = new Vector3(-49.3966f, 2.27f, -23.1457f);
                GameObject.FindGameObjectWithTag("Bolsa").transform.localRotation = Quaternion.Euler(-189f, 87.99999f, -174.5f);
            }
            else
            {
                nuevaPosicionDispRA = false;
                GameObject.FindGameObjectWithTag("Bolsa").transform.localPosition = new Vector3(15.81f, 17.97f, 1.1f);
                GameObject.FindGameObjectWithTag("Bolsa").transform.localRotation = Quaternion.Euler(-4.744f, -85.745f, -90.562f);

                if (GameObject.Find("ConexionABolsa").transform.GetChild(0).gameObject.activeInHierarchy) GameObject.Find("ConexionABolsa").transform.GetChild(0).gameObject.SetActive(true);
                if (GameObject.Find("CableStartOriginal").transform.GetChild(0).gameObject.activeInHierarchy) GameObject.Find("CableStartOriginal").transform.GetChild(0).gameObject.SetActive(true);

            }

            if (debePasarAlPonerBolsa) viewer.ChangeNode();
        }

        else if (other.gameObject.name == "Dispositivo en T Neotee" && pasoNeotee)
        {
            seVentiloBebe = true;
            nuevaPosicionDispRA = false;
            if (GameObject.Find("Neotee_Dispositivo_T").GetComponent<OVRGrabbable>() != null) Destroy(GameObject.Find("Neotee_Dispositivo_T").GetComponent<OVRGrabbable>());
            if (GameObject.Find("Neotee_Dispositivo_T").GetComponent<Rigidbody>() != null) Destroy(GameObject.Find("Neotee_Dispositivo_T").GetComponent<Rigidbody>());

            GameObject.Find("Neotee_Dispositivo_T").transform.localPosition = new Vector3(1.475f, -0.835f, -0.272f);
            GameObject.Find("Neotee_Dispositivo_T").transform.localRotation = Quaternion.Euler(-90f, -180f, -85.7749f);

            if (activarSecuenciaVPP)
            {
                activarSecuenciaVPP = false;
                ReproducirCadenciaVPP();
            }

            if (debePasarAlPonerBolsa) viewer.ChangeNode();
        }
        
        else if (other.gameObject.name == "Pieza en T Neopuff" && pasoNeopuff)
        {
            seVentiloBebe = true;
            nuevaPosicionDispRA = false;
            if (GameObject.Find("Pieza en T Neopuff").GetComponent<OVRGrabbable>() != null) Destroy(GameObject.Find("Pieza en T Neopuff").GetComponent<OVRGrabbable>());
            if (GameObject.Find("Pieza en T Neopuff").GetComponent<Rigidbody>() != null) Destroy(GameObject.Find("Pieza en T Neopuff").GetComponent<Rigidbody>());

            GameObject.Find("Pieza en T Neopuff").transform.localPosition = new Vector3(1.63f, -0.69f, -0.01f);
            GameObject.Find("Pieza en T Neopuff").transform.localRotation = Quaternion.Euler(-17f, -90f, -90f);

            if (activarSecuenciaVPP)
            {
                activarSecuenciaVPP = false;
                ReproducirCadenciaVPP();
            }

            if (debePasarAlPonerBolsa) viewer.ChangeNode();
        }
        

        // TENER EN CUENTA PARA LOS CASOS DONDE DEBE HACER LA ACCION
        if ((other.gameObject.name == "Perilla De Goma") && !seAspiroBocaBebe)
        {
            if (viewer.lastNodeTitle == "Finalizar Video limpiar secreciones")
            {
                transcurrido += Time.fixedDeltaTime;
                if (transcurrido > tiempoNecesario && !usoPera)
                {
                    usoPera = true;
                    viewer.ChangeNode();
                }
            }
        }

        if ((other.gameObject.name == "Perilla De Goma") && !seAspiroBebe)
        {
            if (viewer.lastNodeTitle == "Finalizar Video limpiar secreciones caso ventilacion")
            {
                transcurrido += Time.fixedDeltaTime;
                if (transcurrido > tiempoNecesario && !usoPera)
                {
                    usoPera = true;
                    viewer.ChangeNode();
                }
            }
        }

        else if ((other.gameObject.name == "Sonda de succión" && !seAspiroBebe))
        {
            if (viewer.lastNodeTitle == "Finalizar Video limpiar secreciones")
            {
                transcurrido3 += Time.fixedDeltaTime;
                if (transcurrido3 > tiempoNecesario && !usoSonda)
                {
                    usoSonda = true;
                    viewer.ChangeNode();
                }
            }
        }
        else if ((other.gameObject.name == "Sonda de succión" && !seAspiroBebe))
        {
            if (viewer.lastNodeTitle == "Finalizar retroalimentacion limpiar secreciones MR SOPA")
            {
                transcurrido5 += Time.fixedDeltaTime;
                if (transcurrido5 > tiempoNecesario && !usoSonda)
                {
                    usoSonda = true;
                    viewer.ChangeNode();
                }
            }
        }
        else if (other.gameObject.name == "Bolsa autoinflable" && !seVentiloBebe)
        {
            if (viewer.lastNodeTitle == "Finalizar Video bolsa")
            {
                transcurrido1 += Time.fixedDeltaTime;
                if (transcurrido1 > tiempoNecesarioVentilacion && !usoBolsa)
                {
                    usoBolsa = true;
                    viewer.ChangeNode();
                }
            }
        }
        else if (other.gameObject.name == "Bolsa autoinflable" && !seVentiloBebe)
        {
            bolsaEnContactoConBebe = true;
            
            if (viewer.lastNodeTitle == "Finalizar Video bolsa expandir")
            {
                transcurrido4 += Time.fixedDeltaTime;
                if (transcurrido4 > tiempoNecesario && !usoBolsa)
                {
                    usoBolsa = true;
                    viewer.ChangeNode();
                }
            }
        }
        else if (other.gameObject.name == "Dispositivo en T Neotee" && !seVentiloBebe)
        {
            if (viewer.lastNodeTitle == "Finalizar Video neotee")
            {
                transcurrido1 += Time.fixedDeltaTime;
                if (transcurrido1 > tiempoNecesario && !usoNeotee)
                {
                    usoNeotee = true;
                    viewer.ChangeNode();
                }
            }
        }
        else if (other.gameObject.name == "Pieza en T Neopuff" && !seVentiloBebe)
        {
            if (viewer.lastNodeTitle == "Finalizar Video neopuff")
            {
                transcurrido1 += Time.fixedDeltaTime;
                if (transcurrido1 > tiempoNecesario && !usoNeopuff)
                {
                    usoNeopuff = true;
                    viewer.ChangeNode();
                }
            }
        }
        else if ((other.gameObject.name == "Toalla" || other.gameObject.name == "Toalla2") && !seSecoBebe)
        {
            if (viewer.lastNodeTitle == "Finalizar Video Caso Basico" || viewer.lastNodeTitle == "Audio retroalimentacion secado2")
            {
                print("esta haceidi proces");
                transcurrido2 += Time.fixedDeltaTime;
                if (transcurrido2 > tiempoNecesario)
                {
                    viewer.ChangeNode();
                }
            }

            //if (viewer.lastNodeTitle == "Audio retroalimentacion secado2" || viewer.lastNodeTitle == "Finalizar Video limpiar secreciones")
            if (viewer.lastNodeTitle == "Audio retroalimentacion secado2")
            {
                transcurridoSecado30Seg += Time.fixedDeltaTime;
                if (transcurridoSecado30Seg > tiempoNecesario)
                {
                    viewer.ChangeNode();
                }
            }
        }

        if(other.gameObject.name == "Bolsa autoinflable")
        {
            bolsaEnContactoConBebe = true;
        }

    }


    void ReproducirAudioNoNecesitarEquipo()
    {
        GameObject.Find("B20_Ch_01_Avatar").GetComponent<AudioSource>().enabled = false;
        GameObject.Find("B20_Ch_01_Avatar").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/NoSeNecesitaEseEquipo");
        GameObject.Find("B20_Ch_01_Avatar").GetComponent<AudioSource>().enabled = true;
    }

    void ReproducirCadenciaVPP()
    {
        GameObject.Find("B20_Ch_01_Avatar").GetComponent<AudioSource>().enabled = false;
        GameObject.Find("B20_Ch_01_Avatar").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/CadenciaVPP");
        GameObject.Find("B20_Ch_01_Avatar").GetComponent<AudioSource>().enabled = true;
    }


    public void agregarScriptToalla()
    {
        GameObject.Find("Toalla").AddComponent<DragObject>();
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }
    

    public void agregarScriptGorro()
    {
        GameObject.Find("Gorro").AddComponent<DragObject>();
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }

    public void agregarScriptPera()
    {
        GameObject.Find("Pera De Goma").AddComponent<DragObject>();
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject.GetComponent<DragObject>());
        }
    }

    public void agregarScriptBebe()
    {
        GameObject.Find("baby").AddComponent<DragObject>();
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
    }

    public void agregarScriptBolsa()
    {
        GameObject.Find("Bolsa autoinflable").AddComponent<DragObject>();
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }

    public bool RealizoProcedimiento()
    {
        return false;
    }

    public void cambiarMaterialCianosis()
    {
        Debug.Log("INTENTA CAMBIAR MATERIAL");
        if (SceneManager.GetActiveScene().name != "Hospital") gameObject.GetComponent<MeshRenderer>().material = materialCianosis;
        else
        {
            Material[] mats = gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().materials;
            mats[0] = materialCianosis;
            gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().materials = mats;
            //gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = materialCianosis;             
        }

    }

    public void cambiarMaterialPiel()
    {
        if (SceneManager.GetActiveScene().name != "Hospital") gameObject.GetComponent<MeshRenderer>().material = materialPiel;
        else
        {
            Material[] mats = gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().materials;
            mats[0] = materialPiel;
            gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().materials = mats;
            //gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = materialPiel;            
        }
    }

    public void estarEnIntervencion(string duranteProceso)
    {
        estaEnIntervencion = bool.Parse(duranteProceso);
    }

    public void DebeActivarSecuenciaVPP(string secuencia)
    {
        activarSecuenciaVPP = bool.Parse(secuencia);
    }

    public void ImpedirMostrarVideoPeraEnCalentador(string puede)
    {
        pasoCalentador = bool.Parse(puede);
    }

    public void cambiarAPasoPera()
    {
        if(pasoPera) pasoPera = false;
        else pasoPera = true;
    }

    public void cambiarAPasoBebe()
    {
        if (pasoBebe) pasoBebe = false;
        else pasoBebe = true;
    }

    public void cambiarAPasoBolsa()
    {
        if (pasoBolsa) pasoBolsa = false;
        else pasoBolsa = true;
    }

    public void cambiarAPasoToalla()
    {
        if (permitido) permitido = false;
        else permitido = true;
    }

    public void cambiarAPasoGorro()
    {
        if (pasoGorro) pasoGorro = false;
        else pasoGorro = true;
    }

    public void cambiarSeSecoBebe()
    {
        seSecoBebe = false;
        transcurridoSecado = 0.0f;
    }

    public void cambiarSeAspiroBebe()
    {
        seAspiroBebe = false;
    }
    

    public void cambiarSeVentiloBebe()
    {
        seVentiloBebe = false;
    }

    
    public void cambiarAPasoSonda(string pasoSondaBool)
    {
        if (bool.Parse(pasoSondaBool)) pasoSondaDeSuccion = true;
        else pasoSondaDeSuccion = false;
    }

    public void cambiarAPasoPinza(string pasoPinzaBool)
    {
        if (bool.Parse(pasoPinzaBool)) pasoPinza = true;
        else pasoPinza = false;
    }

    public void cambiarAPasoEstetoscopio(string pasoEstetoscopioBool)
    {
        if (bool.Parse(pasoEstetoscopioBool)) pasoEstetoscopio = true;
        else pasoEstetoscopio = false;
    }

    public void cambiarAPasoNeotee(string pasoNeoteeBool)
    {
        if (bool.Parse(pasoNeoteeBool)) pasoNeotee = true;
        else pasoNeotee = false;
    }

    public void cambiarAPasoNeopuff(string pasoNeopuffBool)
    {
        if (bool.Parse(pasoNeopuffBool)) pasoNeopuff = true;
        else pasoNeopuff = false;
    }

    public void cambiarAPasoOlfateo(string pasoOlfateoBool)
    {
        if (bool.Parse(pasoOlfateoBool)) pasoOlfateo = true;
        else pasoOlfateo = false;
    }

    public void cambiarAPasoAgarrarDispositivo(string pasoDisBool)
    {
        pasoAgarrarDisp = bool.Parse(pasoDisBool);
    }

    public void EliminarScriptDrag()
    {
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject.GetComponent<DragObject>());
        }
    }

    public void permitirMovimiento()
    {
        if (SceneManager.GetActiveScene().name != "Hospital")
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Destroy(GameObject.Find("Calentador").GetComponent<BoxCollider>());
        }
    }

    IEnumerator procesoScript()
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Destroy(GameObject.Find("TriggerCalentador").GetComponent<BoxCollider>());
        Destroy(gameObject.GetComponent<Rigidbody>());
        yield return new WaitForSeconds(0.5f);
        gameObject.AddComponent<Rigidbody>();
        yield return new WaitForSeconds(0.5f);        
    }

    public void habilitarMovimientoBebe()
    {
        if(SceneManager.GetActiveScene().name=="Hospital")
        {
            GameObject.Find("Baby").GetComponent<Rigidbody>().isKinematic = false;
            GameObject.Find("Baby").GetComponent<OVRGrabbable>().allowOffhandGrab = true;
        }
    }


    //Activar animaciones

    public void ActivarTriggerAnimacion(string nombre)
    {
        gameObject.GetComponent<Animator>().SetTrigger(nombre);
    }

    public void BebeMejora()
    {
        bebeMejora = true;
    }

    public void DesactivarAudio()
    {
        puedeActivarAudio = false;
    }

    public void BebeCianotico(string estaCianotico)
    {
        cianotico = bool.Parse(estaCianotico);
    }

    public void DesacomodarBolsa()
    {
        if (GameObject.FindGameObjectWithTag("Bolsa").GetComponent<Rigidbody>() == null)
        {
            GameObject.FindGameObjectWithTag("Bolsa").AddComponent<Rigidbody>();
            GameObject.FindGameObjectWithTag("Bolsa").AddComponent<OVRGrabbable>().Initialize(new Collider[1] { GameObject.FindGameObjectWithTag("Bolsa").GetComponent<BoxCollider>() });
            GameObject.FindGameObjectWithTag("Bolsa").transform.localPosition = new Vector3(-49.49747f, 2.095052f, -22.95666f);
            GameObject.FindGameObjectWithTag("Bolsa").transform.localRotation = Quaternion.Euler(-90f, 0f, 180f);
        }

    }

    public void AcomodarBolsaNuevaPosicion(string boolPos)
    {
        if (GameObject.FindGameObjectWithTag("Bolsa").GetComponent<Rigidbody>() == null)
        {
            GameObject.FindGameObjectWithTag("Bolsa").AddComponent<Rigidbody>();
            GameObject.FindGameObjectWithTag("Bolsa").AddComponent<OVRGrabbable>().Initialize(new Collider[1] { GameObject.FindGameObjectWithTag("Bolsa").GetComponent<BoxCollider>() });
            
        }
        nuevaPosicionDispRA = bool.Parse(boolPos);
    }

    public void DesacomodarNeotee()
    {
        if (GameObject.Find("Neotee_Dispositivo_T").GetComponent<Rigidbody>() == null)
        {
            GameObject.Find("Neotee_Dispositivo_T").AddComponent<Rigidbody>();
            GameObject.Find("Neotee_Dispositivo_T").AddComponent<OVRGrabbable>().Initialize(new Collider[1] { GameObject.Find("Neotee_Dispositivo_T").GetComponent<BoxCollider>() });
            GameObject.Find("Neotee_Dispositivo_T").transform.localPosition = new Vector3(1.194407f, -1.010849f, -0.1979523f);
            GameObject.Find("Neotee_Dispositivo_T").transform.localRotation = Quaternion.Euler(-90f, 0f, 94.957f);

        }

    }

    public void AcomodarNeoteeNuevaPosicion(string boolPos)
    {
        if (GameObject.Find("Neotee_Dispositivo_T").GetComponent<Rigidbody>() == null)
        {
            GameObject.Find("Neotee_Dispositivo_T").AddComponent<Rigidbody>();
            GameObject.Find("Neotee_Dispositivo_T").AddComponent<OVRGrabbable>().Initialize(new Collider[1] { GameObject.Find("Neotee_Dispositivo_T").GetComponent<BoxCollider>() });
            
        }
        nuevaPosicionDispRA = bool.Parse(boolPos);
    }

    public void DesacomodarNeopuff()
    {
        if (GameObject.Find("Pieza en T Neopuff").GetComponent<Rigidbody>() == null)
        {
            GameObject.Find("Pieza en T Neopuff").AddComponent<Rigidbody>();
            GameObject.Find("Pieza en T Neopuff").AddComponent<OVRGrabbable>().Initialize(new Collider[1] { GameObject.Find("Pieza en T Neopuff").GetComponent<MeshCollider>() });
            GameObject.Find("Pieza en T Neopuff").transform.localPosition = new Vector3(1.673f, -0.96395f, 0.050385f);
            GameObject.Find("Pieza en T Neopuff").transform.localRotation = Quaternion.Euler(-180f, -270f, -180f);
        }

    }

    public void AcomodarNeopuffNuevaPosicion(string boolPos)
    {
        if (GameObject.Find("Pieza en T Neopuff").GetComponent<Rigidbody>() == null)
        {
            GameObject.Find("Pieza en T Neopuff").AddComponent<Rigidbody>();
            GameObject.Find("Pieza en T Neopuff").AddComponent<OVRGrabbable>().Initialize(new Collider[1] { GameObject.Find("Pieza en T Neopuff").GetComponent<MeshCollider>() });

        }
        nuevaPosicionDispRA = bool.Parse(boolPos);
    }

    public void ActivarPartesCuerpo()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        gameObject.transform.GetChild(3).gameObject.SetActive(true);
    }

    public void pasarNodo()
    {
        viewer.ChangeNode();
    }

    public void Reescalar()
    {
        gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void Reescalar111()
    {
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void AcomodarColliderACara()
    {
        gameObject.GetComponent<BoxCollider>().center = new Vector3(0.04764365f, 0.02479527f, -0.005268493f);
        gameObject.GetComponent<BoxCollider>().size = new Vector3(0.08603698f, 0.05080393f, 0.06721225f);
    }

    public void CambiarAProcedimientosEnCalentador()
    {
        procedimientosEnCalentador = true;
    }

    public void CambiarPosicionOriginalDispositivoVentilacion()
    {
        nuevaPosicionDispRA = false;
    }

    public void PosicionFlujoLibre(string pos)
    {
        posicionDispFlujoLibre = bool.Parse(pos);
    }

    public bool valorAValidar(string boolAValidar)
    {
        return (bool)this.GetType().GetField(boolAValidar).GetValue(this);
    }

}
