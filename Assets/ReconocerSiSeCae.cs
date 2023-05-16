using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReconocerSiSeCae : MonoBehaviour
{
    public GameObject Toalla;
    public GameObject ToallaAdicional;
    public GameObject Bolsa;
    public GameObject Pera;
    public GameObject Estetoscopio;
    public GameObject Gorro;
    public GameObject GorroCalentador;
    public GameObject PinzaCalentador;
    public GameObject pinzaCasoRA;
    public GameObject bebe;
    public GameObject bebeCalentador;
    public GameObject estetoscopioMesaMO;
    public GameObject BolsaMesaMO;
    public GameObject pinzaMesa;

    public GameObject peraPreparacion;
    public GameObject bolsaPreparacion;
    public GameObject mascaritaPreparacion;

    public GameObject toalla;
    public GameObject bolsa;
    public GameObject pera;
    public GameObject estetoscopio;
    public GameObject gorro;
    public GameObject reloj;
    public GameObject mascarita;

    public GameObject relojRA;
    public GameObject pinzaRA;
    public GameObject jeringaRA;
    public GameObject jeringa2RA;
    public GameObject tijerasRA;
    public GameObject mascaritaRA;
    public GameObject adrenalinaRA;
    public GameObject toallaRA;
    public GameObject laringoscopio;
    public GameObject hoja1laringoscopio;
    public GameObject hoja2laringoscopio;
    public GameObject suero;
    public GameObject termometro;
    public GameObject tubo;
    public GameObject gorroRA;
    public GameObject bolsaRA;
    public GameObject dispositivoT;
    public GameObject dispositivoTNeotee;
    public GameObject tuboOretraqueal;
    public GameObject cateter;

    public GameObject neoteeCaso;

    public GameObject toallaCalentador;
    public GameObject peraCalentador;

    public GameObject pinza;

    public GameObject bolsaPreparacionRA;
    public GameObject mangeraPreparacionRA;
    public GameObject mascaritaPreparacionRA;

    public GameObject bolsaPreparacionAnimacionRA;

    public GameObject neoteePreparacionRA;
    public GameObject mangueraPreparacionRANeotee;
    public GameObject mascaritaNeoteePreparacionRA;

    public GameObject mangueraPreparacionRANeopuff;
    public GameObject mangueraMascaraPreparacionRANeopuff;
    public GameObject mascaritaNeopuffPreparacionRA;
    public GameObject mangueraConectadaNeopuff;
    public GameObject mangueraNuevaConectadaNeopuff;

    public GameObject GorroNeopuff;
    public GameObject frascoYodopodivona;
    public GameObject ampollaVitaminaK;
    public GameObject bolsaGlobulosRojos;
    public GameObject cintaMetrica;

    public GameObject cateterMesa;
    public GameObject jeringaMesa;
    public GameObject laringoMesa;
    public GameObject AdrenalinaMesa;

    int cant = 0;

    void Update()
    {
        //MIRAR A USUARIO
        Vector3 targetPostition = new Vector3(GameObject.Find("OVRPlayerController").transform.position.x,
                                       GameObject.Find("DoctoraGuia").transform.localPosition.y,
                                       GameObject.Find("OVRPlayerController").transform.position.z);
        if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking)
        {
            if ((FindObjectOfType<ControllerTwine>() != null && !FindObjectOfType<ControllerTwine>().impideMirarObjetivo) || FindObjectOfType<ControllerTwine>() == null) 
                GameObject.Find("DoctoraGuia").transform.LookAt(targetPostition);

            GameObject.Find("InputType_Sample").transform.localPosition = new Vector3(0f, 0f, 0f);
        }
        

        //OBJETOS CAYENDOSE
        if (bebe!=null && bebe.GetComponent<OVRGrabbable>() != null)
        {
            if ((bebe.transform.localPosition.z > 21 || bebe.transform.localPosition.x < -12.59 || bebe.transform.localPosition.x > -12.54 || bebe.transform.localPosition.y < 7.13) && !bebe.GetComponent<OVRGrabbable>().isGrabbed)
            {
                bebe.transform.localPosition = new Vector3(-15.196f, 7.518951f, 21.25324f);
                bebe.transform.localRotation = Quaternion.Euler(-192.483f, 179.56f, -2.119995f);
            }
        }
        if (bebeCalentador != null && bebeCalentador.GetComponent<OVRGrabbable>() != null)
        {
            if ((bebeCalentador.transform.localPosition.z > 21.5 || bebeCalentador.transform.localPosition.y > 7.946) && !bebeCalentador.GetComponent<OVRGrabbable>().isGrabbed)
            {
                bebeCalentador.transform.localPosition = new Vector3(-12.6092f, 7.6316f, 21.0193f);
                bebeCalentador.transform.localRotation = Quaternion.Euler(-199.268f, 180.721f, -173.357f);
            }
        }

        if (neoteeCaso != null && neoteeCaso.GetComponent<OVRGrabbable>() != null)
        {
            if (FindObjectOfType<ControllerBaby>() != null && FindObjectOfType<ControllerBaby>().nuevaPosicionDispRA)
            {
                if (neoteeCaso != null && (neoteeCaso.transform.localPosition.y < -1.5 || neoteeCaso.transform.localPosition.x != 1.475 || neoteeCaso.transform.localPosition.z != -0.272) && !neoteeCaso.GetComponent<OVRGrabbable>().isGrabbed)
                {
                    neoteeCaso.transform.localPosition = new Vector3(1.475f, -0.835f, -0.272f);
                    neoteeCaso.transform.localRotation = Quaternion.Euler(-90f, -180f, -85.7749f);
                    if (GameObject.Find("ConexionANeotee")!=null && GameObject.Find("ConexionANeotee").transform.GetChild(0).gameObject.activeInHierarchy) GameObject.Find("ConexionANeotee").transform.GetChild(0).gameObject.SetActive(false);
                    if (GameObject.Find("CableStartOriginalN")!=null && GameObject.Find("CableStartOriginalN").transform.GetChild(0).gameObject.activeInHierarchy) GameObject.Find("CableStartOriginalN").transform.GetChild(0).gameObject.SetActive(false);
                    if (GameObject.Find("Dispositivo en T Neotee") != null) GameObject.Find("Dispositivo en T Neotee").GetComponent<BoxCollider>().isTrigger = true;
                }
                if (neoteeCaso != null && neoteeCaso.GetComponent<OVRGrabbable>().isGrabbed)
                {
                    if (GameObject.Find("Dispositivo en T Neotee") != null) GameObject.Find("Dispositivo en T Neotee").GetComponent<BoxCollider>().isTrigger = false;
                    if (FindObjectOfType<ControllerBaby>().pasoAgarrarDisp == true && cant==0)
                    {
                        FindObjectOfType<DialogueViewer1>().ChangeNode();
                        cant = 1;
                    }
                    if (GameObject.Find("ConexionANeotee").transform.GetChild(0).gameObject.activeInHierarchy) GameObject.Find("ConexionANeotee").transform.GetChild(0).gameObject.SetActive(true);
                    if (GameObject.Find("CableStartOriginalN").transform.GetChild(0).gameObject.activeInHierarchy) GameObject.Find("CableStartOriginalN").transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            else
            {
                if (neoteeCaso != null)
                {
                    if (neoteeCaso.GetComponent<OVRGrabbable>() != null && (neoteeCaso.transform.localPosition.y < -1.5 || neoteeCaso.transform.localPosition.x != 1.194407 || neoteeCaso.transform.localPosition.z != -0.1979523) && !neoteeCaso.GetComponent<OVRGrabbable>().isGrabbed)
                    {
                        neoteeCaso.transform.localPosition = new Vector3(1.194407f, -1.010849f, -0.1979523f);
                        neoteeCaso.transform.localRotation = Quaternion.Euler(-90f, 0f, 94.957f);
                        if (GameObject.Find("Dispositivo en T Neotee") != null) GameObject.Find("Dispositivo en T Neotee").GetComponent<BoxCollider>().isTrigger = true;
                    }

                    if (neoteeCaso.GetComponent<OVRGrabbable>() != null)
                    {
                        if (neoteeCaso != null && neoteeCaso.GetComponent<OVRGrabbable>().isGrabbed)
                        {
                            if (GameObject.Find("SeNecesitaNeotee") != null) GameObject.Find("SeNecesitaNeotee").transform.GetChild(0).gameObject.SetActive(false);
                            if (GameObject.Find("Dispositivo en T Neotee") != null) GameObject.Find("Dispositivo en T Neotee").GetComponent<BoxCollider>().isTrigger = false;
                        }
                    }
                }
            }
        }
        else if (neoteeCaso != null && neoteeCaso.GetComponent<OVRGrabbable>() == null)
        {
            if (FindObjectOfType<ControllerBaby>() != null && FindObjectOfType<ControllerBaby>().posicionDispFlujoLibre)
            {
                if ((neoteeCaso.transform.localPosition.y < -1.5 || neoteeCaso.transform.localPosition.x != 1.61 || neoteeCaso.transform.localPosition.z != -0.006))
                {
                    neoteeCaso.transform.localPosition = new Vector3(1.475f, -0.801f, -0.272f);
                    neoteeCaso.transform.localRotation = Quaternion.Euler(-90f, -180f, -85.7749f);

                }
            }
        }


        //SELECCION DE EQUIPOS Reanimacion avanzada
        if (relojRA != null)
        {
            if ((relojRA.transform.localPosition.y < 1.5 || relojRA.transform.localPosition.x != -46.09 || relojRA.transform.localPosition.z != -21.098) && !relojRA.GetComponent<OVRGrabbable>().isGrabbed)
            {
                relojRA.transform.localPosition = new Vector3(-46.09f, 2.187f, -21.098f);
                relojRA.transform.localRotation = Quaternion.Euler(-90f, 0f, -90f);
            }
            if ((pinzaRA.transform.localPosition.y < 1.5 || pinzaRA.transform.localPosition.x != -46.017 || pinzaRA.transform.localPosition.z != -21.383) && !pinzaRA.GetComponent<OVRGrabbable>().isGrabbed)
            {
                pinzaRA.transform.localPosition = new Vector3(-46.017f, 2.175f, -21.383f);
                pinzaRA.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            }
            if (jeringaRA != null && (jeringaRA.transform.localPosition.y < 1.5 || jeringaRA.transform.localPosition.x != -46.372 || jeringaRA.transform.localPosition.z != -21.008) && !jeringaRA.GetComponent<OVRGrabbable>().isGrabbed)
            {
                jeringaRA.transform.localPosition = new Vector3(-46.372f, 2.179f, -21.008f);
                jeringaRA.transform.localRotation = Quaternion.Euler(270f, 90f, 0f);
            }
            if (jeringa2RA != null && (jeringa2RA.transform.localPosition.y < 1.5 || jeringa2RA.transform.localPosition.x != -46.3706 || jeringa2RA.transform.localPosition.z != -21.2597) && !jeringa2RA.GetComponent<OVRGrabbable>().isGrabbed)
            {
                jeringa2RA.transform.localPosition = new Vector3(-46.3706f, 2.1791f, -21.2597f);
                jeringa2RA.transform.localRotation = Quaternion.Euler(-90f, 0f, 90f);
            }
            if (tijerasRA != null && (tijerasRA.transform.localPosition.y < 1.5 || tijerasRA.transform.localPosition.x != -46.232 || tijerasRA.transform.localPosition.z != -21.395) && !tijerasRA.GetComponent<OVRGrabbable>().isGrabbed)
            {
                tijerasRA.transform.localPosition = new Vector3(-46.232f, 2.182f, -21.395f);
                tijerasRA.transform.localRotation = Quaternion.Euler(90f, 0f, -180f);
            }
            if (mascaritaRA != null && (mascaritaRA.transform.localPosition.y < 1.5 || mascaritaRA.transform.localPosition.x != -46.183 || mascaritaRA.transform.localPosition.z != -20.867) && !mascaritaRA.GetComponent<OVRGrabbable>().isGrabbed)
            {
                mascaritaRA.transform.localPosition = new Vector3(-46.183f, 2.151f, -20.867f);
                mascaritaRA.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            }
            if (adrenalinaRA != null && (adrenalinaRA.transform.localPosition.y < 1.5 || adrenalinaRA.transform.localPosition.x != -46.368 || adrenalinaRA.transform.localPosition.z != -20.719) && !adrenalinaRA.GetComponent<OVRGrabbable>().isGrabbed)
            {
                adrenalinaRA.transform.localPosition = new Vector3(-46.368f, 2.173f, -20.719f);
                adrenalinaRA.transform.localRotation = Quaternion.Euler(-90f, 0f, 90f);
            }
            if (toallaRA != null && (toallaRA.transform.localPosition.y < 1.5 || toallaRA.transform.localPosition.x != -46.137 || toallaRA.transform.localPosition.z != -20.575) && !toallaRA.GetComponent<OVRGrabbable>().isGrabbed)
            {
                toallaRA.transform.localPosition = new Vector3(-46.137f, 2.232f, -20.575f);
                toallaRA.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            }

            if (laringoscopio != null && (laringoscopio.transform.localPosition.y < 1.5 || laringoscopio.transform.localPosition.x != -46.226 || laringoscopio.transform.localPosition.z != -20.334) && !laringoscopio.GetComponent<OVRGrabbable>().isGrabbed)
            {
                laringoscopio.transform.localPosition = new Vector3(-46.226f, 2.183f, -20.334f);
                laringoscopio.transform.localRotation = Quaternion.Euler(0f, -130f, 0f);

            }
            if (hoja1laringoscopio != null && (hoja1laringoscopio.transform.localPosition.y < 1.5 || hoja1laringoscopio.transform.localPosition.x != -46.327 || hoja1laringoscopio.transform.localPosition.z != -20.299) && !hoja1laringoscopio.GetComponent<OVRGrabbable>().isGrabbed)
            {
                hoja1laringoscopio.transform.localPosition = new Vector3(-46.327f, 1.809f, -20.299f);
                hoja1laringoscopio.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);

            }
            if (hoja2laringoscopio != null && (hoja2laringoscopio.transform.localPosition.y < 1.5 || hoja2laringoscopio.transform.localPosition.x != -46.358 || hoja2laringoscopio.transform.localPosition.z != -20.403) && !hoja2laringoscopio.GetComponent<OVRGrabbable>().isGrabbed)
            {
                hoja2laringoscopio.transform.localPosition = new Vector3(-46.358f, 1.896f, -20.403f);
                hoja2laringoscopio.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);

            }

            if (suero != null && (suero.transform.localPosition.y < 1.5 || suero.transform.localPosition.x != -46.13901 || suero.transform.localPosition.z != -19.177) && !suero.GetComponent<OVRGrabbable>().isGrabbed)
            {
                suero.transform.localPosition = new Vector3(-46.13901f, 2.199f, -19.177f);
                suero.transform.localRotation = Quaternion.Euler(90f, 180f, -90f);
            }
            if (termometro != null && (termometro.transform.localPosition.y < 1.5 || termometro.transform.localPosition.x != -46.26212 || termometro.transform.localPosition.z != -18.85155) && !termometro.GetComponent<OVRGrabbable>().isGrabbed)
            {
                termometro.transform.localPosition = new Vector3(-46.26212f, 2.230424f, -18.85155f);
                termometro.transform.localRotation = Quaternion.Euler(0f, 180f, 180f);
            }
            if (tuboOretraqueal != null && (tuboOretraqueal.transform.localPosition.y < 1.5 || tuboOretraqueal.transform.localPosition.x != -46.404 || tuboOretraqueal.transform.localPosition.z != -20.52) && !tuboOretraqueal.GetComponent<OVRGrabbable>().isGrabbed)
            {
                tuboOretraqueal.transform.localPosition = new Vector3(-46.404f, 2.204005f, -20.52f);
                tuboOretraqueal.transform.localRotation = Quaternion.Euler(90f, 90f, 0f);
            }
            if (cateter != null && (cateter.transform.localPosition.y < 1.5 || cateter.transform.localPosition.x != -45.954 || cateter.transform.localPosition.z != -18.89) && !cateter.GetComponent<OVRGrabbable>().isGrabbed)
            {
                cateter.transform.localPosition = new Vector3(-45.954f, 2.176f, -18.89f);
                cateter.transform.localRotation = Quaternion.Euler(-180f, 0f, 0f);
            }
            if (tubo != null && (tubo.transform.localPosition.y < 1.5 || tubo.transform.localPosition.x != -46.372 || tubo.transform.localPosition.z != -19.067) && !tubo.GetComponent<OVRGrabbable>().isGrabbed)
            {
                tubo.transform.localPosition = new Vector3(-46.372f, 2.184f, -19.067f);
                tubo.transform.localRotation = Quaternion.Euler(270f, 90f, 0f);
            }
            if (gorroRA != null && (gorroRA.transform.localPosition.y < 1.5 || gorroRA.transform.localPosition.x != -46.135 || gorroRA.transform.localPosition.z != -18.702) && !gorroRA.GetComponent<OVRGrabbable>().isGrabbed)
            {
                gorroRA.transform.localPosition = new Vector3(-46.135f, 2.241f, -18.702f);
                gorroRA.transform.localRotation = Quaternion.Euler(-45f, 90f, 0f);

            }

            if (bolsaRA != null && (bolsaRA.transform.localPosition.y < 1.5 || bolsaRA.transform.localPosition.x != -46.374 || bolsaRA.transform.localPosition.z != -18.442) && !bolsaRA.GetComponent<OVRGrabbable>().isGrabbed)
            {
                
                bolsaRA.transform.localPosition = new Vector3(-46.374f, 2.211f, -18.442f);
                bolsaRA.transform.localRotation = Quaternion.Euler(-90f, 0f, 90f);

            }
            
                       

            if (dispositivoT != null && (dispositivoT.transform.localPosition.y < 1.5 || dispositivoT.transform.localPosition.x != -46.187 || dispositivoT.transform.localPosition.z != -18.39215) && !dispositivoT.GetComponent<OVRGrabbable>().isGrabbed)
            {
                dispositivoT.transform.localPosition = new Vector3(-46.187f, 2.255f, -18.39215f);
                dispositivoT.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);

            }
            if (dispositivoTNeotee != null && (dispositivoTNeotee.transform.localPosition.y < 1.5 || dispositivoTNeotee.transform.localPosition.x != -46.17101 || dispositivoTNeotee.transform.localPosition.z != -18.19866) && !dispositivoTNeotee.GetComponent<OVRGrabbable>().isGrabbed)
            {
                dispositivoTNeotee.transform.localPosition = new Vector3(-46.17101f, 2.198f, -18.19866f);
                dispositivoTNeotee.transform.localRotation = Quaternion.Euler(-90f, 0f, 180f);

            }

            if (frascoYodopodivona != null && (frascoYodopodivona.transform.localPosition.y < 1.5 || frascoYodopodivona.transform.localPosition.x != -46.37 || frascoYodopodivona.transform.localPosition.z != -21.401) && !frascoYodopodivona.GetComponent<OVRGrabbable>().isGrabbed)
            {
                frascoYodopodivona.transform.localPosition = new Vector3(-46.37f, 2.169f, -21.401f);
                frascoYodopodivona.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);

            }

            if (ampollaVitaminaK != null && (ampollaVitaminaK.transform.localPosition.y < 1.5 || ampollaVitaminaK.transform.localPosition.x != -46.28 || ampollaVitaminaK.transform.localPosition.z != -18.004) && !ampollaVitaminaK.GetComponent<OVRGrabbable>().isGrabbed)
            {
                ampollaVitaminaK.transform.localPosition = new Vector3(-46.28f, 2.165199f, -18.004f);
                ampollaVitaminaK.transform.localRotation = Quaternion.Euler(-90f, 0f, 90f);

            }

            if (bolsaGlobulosRojos != null && (bolsaGlobulosRojos.transform.localPosition.y < 1.5 || bolsaGlobulosRojos.transform.localPosition.x != -46.132 || bolsaGlobulosRojos.transform.localPosition.z != -21.613) && !bolsaGlobulosRojos.GetComponent<OVRGrabbable>().isGrabbed)
            {
                bolsaGlobulosRojos.transform.localPosition = new Vector3(-46.132f, 2.199f, -21.613f);
                bolsaGlobulosRojos.transform.localRotation = Quaternion.Euler(90f, 180f, -90f);

            }

            if (cintaMetrica != null && (cintaMetrica.transform.localPosition.y < 1.5 || cintaMetrica.transform.localPosition.x != -46.435 || cintaMetrica.transform.localPosition.z != -19.092) && !cintaMetrica.GetComponent<OVRGrabbable>().isGrabbed)
            {
                cintaMetrica.transform.localPosition = new Vector3(-46.435f, 2.182f, -19.092f);
                cintaMetrica.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            }


        }


        //ELEMENTOS PARA SELECCIONAR EN MINUTO DE ORO
        if (toalla != null)
        {
            if ((toalla.transform.localPosition.x != -46.217 || toalla.transform.localPosition.y < 1.5 || toalla.transform.localPosition.z != -19.32) && !toalla.GetComponent<OVRGrabbable>().isGrabbed)
            {
                toalla.transform.localPosition = new Vector3(-46.217f, 2.15f, -19.32f);
                toalla.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            }
            if ((bolsa.transform.localPosition.y < 1.5 || bolsa.transform.localPosition.x != -46.34 || bolsa.transform.localPosition.z != -19.047) && !bolsa.GetComponent<OVRGrabbable>().isGrabbed)
            {
                bolsa.transform.localPosition = new Vector3(-46.34f, 2.168f, -19.047f);
                bolsa.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
            }
            if ((pera.transform.localPosition.y < 1.5 || pera.transform.localPosition.x != -46.233 || pera.transform.localPosition.z != -18.692) && !pera.GetComponent<OVRGrabbable>().isGrabbed)
            {
                pera.transform.localPosition = new Vector3(-46.233f, 2.128f, -18.692f);
                pera.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            }
            if ((estetoscopio.transform.localPosition.y < 1.5 || estetoscopio.transform.localPosition.x != -46.441 || estetoscopio.transform.localPosition.z != -18.618) && !estetoscopio.GetComponent<OVRGrabbable>().isGrabbed)
            {
                estetoscopio.transform.localPosition = new Vector3(-46.441f, 2.122f, -18.618f);
                estetoscopio.transform.localRotation = Quaternion.Euler(-90f, 0f, -180f);
            }
            if ((gorro.transform.localPosition.y < 1.5 || gorro.transform.localPosition.x != -46.104 || gorro.transform.localPosition.z != -18.42) && !gorro.GetComponent<OVRGrabbable>().isGrabbed)
            {
                gorro.transform.localPosition = new Vector3(-46.104f, 2.154f, -18.42f);
                gorro.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            }
            if ((reloj.transform.localPosition.y < 1.5 || reloj.transform.localPosition.x != -46.451 || reloj.transform.localPosition.z != -18.36) && !reloj.GetComponent<OVRGrabbable>().isGrabbed)
            {
                reloj.transform.localPosition = new Vector3(-46.451f, 2.176f, -18.36f);
                reloj.transform.localRotation = Quaternion.Euler(-90f, 0f, -90f);
            }
            if ((mascarita.transform.localPosition.y < 1.5 || mascarita.transform.localPosition.x != -46.51092 || mascarita.transform.localPosition.z != -18.8938) && !mascarita.GetComponent<OVRGrabbable>().isGrabbed)
            {
                mascarita.transform.localPosition = new Vector3(-46.51092f, 2.101941f, -18.8938f);
                mascarita.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            }
            if ((pinza.transform.localPosition.y < 1.5 || pinza.transform.localPosition.x != -46.468 || pinza.transform.localPosition.z != -19.064) && !pinza.GetComponent<OVRGrabbable>().isGrabbed)
            {
                pinza.transform.localPosition = new Vector3(-46.468f, 2.095f, -19.064f);
                pinza.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            }
        }

        //ELEMENTOS PARA MINUTO DE ORO
        if (Toalla != null)
        {
            if (Toalla.GetComponent<OVRGrabbable>() != null)
            {
                if (Toalla.GetComponent<OVRGrabbable>().isGrabbed)
                {
                    if (GameObject.Find("SeNecesitaToallaMO") != null) GameObject.Find("SeNecesitaToallaMO").transform.GetChild(0).gameObject.SetActive(false);

                    if (!Toalla.GetComponent<MeshRenderer>().enabled) GameObject.FindGameObjectWithTag("Toalla").GetComponent<MeshRenderer>().enabled = true;
                    if (!Toalla.transform.GetChild(0).gameObject.activeInHierarchy) GameObject.FindGameObjectWithTag("Toalla").transform.GetChild(0).gameObject.SetActive(true);

                    if (FindObjectOfType<ControllerBaby>()!=null && FindObjectOfType<ControllerBaby>().toallaArropandoABebe)
                    {
                        GameObject.Find("ToallaEstatica").transform.position = new Vector3(100f, 2.4868f, -50.5233f);
                        FindObjectOfType<ControllerBaby>().toallaArropandoABebe = false;
                    }
                }

                //si la toalla se solto fuera del bebé
                if (!Toalla.GetComponent<OVRGrabbable>().isGrabbed && (GameObject.Find("ToallaEstatica").transform.position == new Vector3(100f, 2.4868f, -50.5233f) || (FindObjectOfType<ControllerBaby>() != null && FindObjectOfType<ControllerBaby>().toalla2ArropandoABebe)))
                {
                    Toalla.transform.localPosition = new Vector3(-50.325f, 2.14f, -21.371f);
                    Toalla.transform.localRotation = Quaternion.Euler(-90f, 0f, 90f);
                }
            }
        }

        if (ToallaAdicional != null)
        {
            if (ToallaAdicional.GetComponent<OVRGrabbable>() != null)
            {
                //TOALLA 2 DE LA MESA
                if (ToallaAdicional.GetComponent<OVRGrabbable>().isGrabbed)
                {
                    if (GameObject.Find("SeNecesitaToallaMO") != null) GameObject.Find("SeNecesitaToallaMO").transform.GetChild(0).gameObject.SetActive(false);

                    if (!ToallaAdicional.GetComponent<MeshRenderer>().enabled) ToallaAdicional.GetComponent<MeshRenderer>().enabled = true;

                    if (FindObjectOfType<ControllerBaby>() != null && FindObjectOfType<ControllerBaby>().toalla2ArropandoABebe)
                    {
                        GameObject.Find("ToallaEstatica").transform.position = new Vector3(100f, 2.4868f, -50.5233f);
                        FindObjectOfType<ControllerBaby>().toalla2ArropandoABebe = false;
                    }
                }

                //si la toalla se solto fuera del bebé
                if (!ToallaAdicional.GetComponent<OVRGrabbable>().isGrabbed && (GameObject.Find("ToallaEstatica").transform.position == new Vector3(100f, 2.4868f, -50.5233f) || (FindObjectOfType<ControllerBaby>() != null && FindObjectOfType<ControllerBaby>().toallaArropandoABebe)))
                {
                    ToallaAdicional.transform.localPosition = new Vector3(-50.325f, 2.14f, -21.582f);
                    ToallaAdicional.transform.localRotation = Quaternion.Euler(-90f, 0f, 90f);
                }
            }
        }

        if (Pera != null)
        {

            if (Pera.GetComponent<OVRGrabbable>().isGrabbed)
            {
                if (GameObject.Find("SeNecesitaPera") != null) GameObject.Find("SeNecesitaPera").transform.GetChild(0).gameObject.SetActive(false);
            }

            if ((Pera.transform.localPosition.y < 1.5 || Pera.transform.localPosition.z != -21.19301 || Pera.transform.localPosition.x != -50.48903) && !Pera.GetComponent<OVRGrabbable>().isGrabbed)
            {
                Pera.transform.localPosition = new Vector3(-50.48903f, 2.082591f, -21.19301f);
                Pera.transform.localRotation = Quaternion.Euler(-90f, 0f, 90f);
            }
        }

        if (Gorro != null)
        {
            if (Gorro.GetComponent<OVRGrabbable>() != null && Gorro.GetComponent<OVRGrabbable>().isGrabbed)
            {
                if (GameObject.Find("SeNecesitaGorro") != null) GameObject.Find("SeNecesitaGorro").transform.GetChild(0).gameObject.SetActive(false);
            }

            if ((Gorro.transform.localPosition.y < 1.5 || Gorro.transform.localPosition.x != -50.687 || Gorro.transform.localPosition.z != -21.484) && !Gorro.GetComponent<OVRGrabbable>().isGrabbed)
            {
                Gorro.transform.localPosition = new Vector3(-50.687f, 2.161f, -21.484f);
                Gorro.transform.localRotation = Quaternion.Euler(-52.145f, 20.609f, -1.157f);
            }
        }

        if (Bolsa != null)
        {
            if (Bolsa.GetComponent<OVRGrabbable>() != null)
            {
                if (Bolsa.GetComponent<OVRGrabbable>().isGrabbed && GameObject.Find("SeNecesitaBolsa") != null) GameObject.Find("SeNecesitaBolsa").transform.GetChild(0).gameObject.SetActive(false);

                if ((Bolsa.transform.localPosition.y < 1.5 || Bolsa.transform.localPosition.x != -49.49747 || Bolsa.transform.localPosition.z != -22.95666) && Bolsa.GetComponent<OVRGrabbable>() != null)
                {
                    if (!Bolsa.GetComponent<OVRGrabbable>().isGrabbed)
                    {
                        Bolsa.transform.localPosition = new Vector3(-49.49747f, 2.095052f, -22.95666f);
                        Bolsa.transform.localRotation = Quaternion.Euler(-90f, 0f, 180f);
                    }
                }

            }

        }

        if (BolsaMesaMO != null)
        {
            if (BolsaMesaMO.GetComponent<OVRGrabbable>() != null)
            {

                if ((BolsaMesaMO.transform.localPosition.y < 1.5 || BolsaMesaMO.transform.localPosition.x != -50.55955 || BolsaMesaMO.transform.localPosition.z != -22.03315) && BolsaMesaMO.GetComponent<OVRGrabbable>() != null)
                {
                    if (!BolsaMesaMO.GetComponent<OVRGrabbable>().isGrabbed)
                    {
                        BolsaMesaMO.transform.localPosition = new Vector3(-50.55955f, 2.144004f, -22.03315f);
                        BolsaMesaMO.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
                    }
                }

            }
        }

        if (pinzaMesa != null)
        {
            if ((pinzaMesa.transform.localPosition.y < 1.5 || pinzaMesa.transform.localPosition.x != -50.319 || pinzaMesa.transform.localPosition.z != -21.8) && !pinzaMesa.GetComponent<OVRGrabbable>().isGrabbed)
            {
                pinzaMesa.transform.localPosition = new Vector3(-50.319f, 2.137f, -21.8f);
                pinzaMesa.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }

        if (Estetoscopio != null)
        {


            if ((Estetoscopio.transform.localPosition.y < 1.5 || Estetoscopio.transform.localPosition.x != -49.90002 || Estetoscopio.transform.localPosition.z != -23.27801) && !Estetoscopio.GetComponent<OVRGrabbable>().isGrabbed)
            {
                Estetoscopio.transform.localPosition = new Vector3(-49.90002f, 2.069557f, -23.27801f);
                Estetoscopio.transform.localRotation = Quaternion.Euler(-89.32201f, -50.726f, 46.485f);
            }

            if (Estetoscopio.GetComponent<OVRGrabbable>().isGrabbed)
            {
                if (GameObject.Find("SeNecesitaEstetoscopioCalentador") != null) GameObject.Find("SeNecesitaEstetoscopioCalentador").transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        if (estetoscopioMesaMO)
        {
            if ((estetoscopioMesaMO.transform.localPosition.y < 1.5 || estetoscopioMesaMO.transform.localPosition.x != -50.566 || estetoscopioMesaMO.transform.localPosition.z != -21.818) && !estetoscopioMesaMO.GetComponent<OVRGrabbable>().isGrabbed)
            {

                estetoscopioMesaMO.transform.localPosition = new Vector3(-50.566f, 2.112f, -21.818f);
                estetoscopioMesaMO.transform.localRotation = Quaternion.Euler(-89.32201f, -50.758f, -131.278f);
            }

            if (estetoscopioMesaMO.GetComponent<OVRGrabbable>().isGrabbed)
            {
                if (GameObject.Find("SeNecesitaEstetoscopioMesa") != null) GameObject.Find("SeNecesitaEstetoscopioMesa").transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        if (GorroCalentador != null)
        {
            if ((GorroCalentador.transform.localPosition.y < 1.5 || GorroCalentador.transform.localPosition.x != -49.844 || GorroCalentador.transform.localPosition.z != -23.065) && !GorroCalentador.GetComponent<OVRGrabbable>().isGrabbed)
            {
                GorroCalentador.transform.localPosition = new Vector3(-49.844f, 2.155f, -23.065f);
                GorroCalentador.transform.localRotation = Quaternion.Euler(-52.145f, 20.609f, -1.157f);
            }

            if (GorroCalentador.GetComponent<OVRGrabbable>().isGrabbed)
            {
                if (GameObject.Find("SeNecesitaGorroCalentador") != null) GameObject.Find("SeNecesitaGorroCalentador").transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        if (GorroNeopuff != null)
        {
            if ((GorroNeopuff.transform.localPosition.y < 1.5 || GorroNeopuff.transform.localPosition.x != -49.84402 || GorroNeopuff.transform.localPosition.z != -23.468) && !GorroNeopuff.GetComponent<OVRGrabbable>().isGrabbed)
            {
                GorroNeopuff.transform.localPosition = new Vector3(-49.84402f, 2.155f, -23.468f);
                GorroNeopuff.transform.localRotation = Quaternion.Euler(-52.145f, 20.609f, -1.157f);
            }
        }

        if (PinzaCalentador != null)
        {
            if ((PinzaCalentador.transform.localPosition.y < 1.5 || PinzaCalentador.transform.localPosition.x != -49.86 || PinzaCalentador.transform.localPosition.z != -22.897) && !PinzaCalentador.GetComponent<OVRGrabbable>().isGrabbed)
            {
                PinzaCalentador.transform.localPosition = new Vector3(-49.86f, 2.1f, -22.897f);
                PinzaCalentador.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }

        if (pinzaCasoRA != null)
        {
            if ((pinzaCasoRA.transform.localPosition.y < 1.5 || pinzaCasoRA.transform.localPosition.x != -49.293 || pinzaCasoRA.transform.localPosition.z != -23.063) && !pinzaCasoRA.GetComponent<OVRGrabbable>().isGrabbed)
            {
                pinzaCasoRA.transform.localPosition = new Vector3(-49.293f, 2.1f, -23.063f);
                pinzaCasoRA.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            }
        }

        //ELEMENTOS PREPARACIÓN MINUTO DE ORO
        if (peraPreparacion!=null)
        {
            if ((peraPreparacion.transform.localPosition.y != 2.11 || peraPreparacion.transform.localPosition.x != -50.489 || peraPreparacion.transform.localPosition.z != -21.193) && !peraPreparacion.GetComponent<OVRGrabbable>().isGrabbed)
            {
                peraPreparacion.transform.localPosition = new Vector3(-50.489f, 2.11f, -21.193f);
                peraPreparacion.transform.localRotation = Quaternion.Euler(-90f, 0f, 90f);
            }
        }
        if (bolsaPreparacion!=null)
        {
            if ((bolsaPreparacion.transform.localPosition.y != 2.144 || bolsaPreparacion.transform.localPosition.x != -50.43 || bolsaPreparacion.transform.localPosition.z != -21.438) && !bolsaPreparacion.GetComponent<OVRGrabbable>().isGrabbed)
            {
                bolsaPreparacion.transform.localPosition = new Vector3(-50.43f, 2.144f, -21.438f);
                bolsaPreparacion.transform.localRotation = Quaternion.Euler(-90f, 0f, -180f);
            }
        }

        if (mascaritaPreparacion != null)
        {
            if ((mascaritaPreparacion.transform.localPosition.y != 2.1 || mascaritaPreparacion.transform.localPosition.x != -50.431 || mascaritaPreparacion.transform.localPosition.z != -21.676) && !mascaritaPreparacion.GetComponent<OVRGrabbable>().isGrabbed)
            {
                mascaritaPreparacion.transform.localPosition = new Vector3(-50.431f, 2.1f, -21.676f);
                mascaritaPreparacion.transform.localRotation = Quaternion.Euler(-177.489f, -1.525879f, -180f);
            }
        }

        //ELEMENTOS DEL CALENTADOR

        if (toallaCalentador!=null && (toallaCalentador.transform.localPosition.y < 2.07 || toallaCalentador.transform.localPosition.x != 23.491 || toallaCalentador.transform.localPosition.z!= -50.21) && !toallaCalentador.GetComponent<OVRGrabbable>().isGrabbed)
        {
            toallaCalentador.transform.localPosition = new Vector3(23.491f, 2.098f, -50.21f);
            toallaCalentador.transform.localRotation = Quaternion.Euler(90f, 0f,180f);
        }
        if (peraCalentador != null)
        {
            if ((peraCalentador.transform.localPosition.y < 2.03 || peraCalentador.transform.localPosition.x != 23.43164 || peraCalentador.transform.localPosition.z!= -50.48645) && !peraCalentador.GetComponent<OVRGrabbable>().isGrabbed)
            {
                peraCalentador.transform.localPosition = new Vector3(23.43164f, 2.095f, -50.48645f);
                peraCalentador.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            }
            if(peraCalentador.GetComponent<OVRGrabbable>().isGrabbed)
            {
                if (GameObject.Find("SeNecesitaPeraCalentador") != null) GameObject.Find("SeNecesitaPeraCalentador").transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        if (bolsaPreparacionRA != null)
        {
            if ((bolsaPreparacionRA.transform.localPosition.y < 1.5 || bolsaPreparacionRA.transform.localPosition.x != 13.48002 || bolsaPreparacionRA.transform.localPosition.z != -0.4400015) && !bolsaPreparacionRA.GetComponent<OVRGrabbable>().isGrabbed)
            {
                bolsaPreparacionRA.transform.localPosition = new Vector3(13.48002f, 15.95f, -0.4400015f);
                bolsaPreparacionRA.transform.localRotation = Quaternion.Euler(0f, 0f, -180f);
            }
        }

        if (mascaritaPreparacionRA != null)
        {
            if (mascaritaPreparacionRA.GetComponent<OVRGrabbable>()!=null)
            {
                if ((mascaritaPreparacionRA.transform.localPosition.y < 1.5 || mascaritaPreparacionRA.transform.localPosition.x != 14.03 || mascaritaPreparacionRA.transform.localPosition.z != -2.023686) && !mascaritaPreparacionRA.GetComponent<OVRGrabbable>().isGrabbed)
                {
                    mascaritaPreparacionRA.transform.localPosition = new Vector3(14.03f, 18.77004f, -2.023686f);
                    mascaritaPreparacionRA.transform.localRotation = Quaternion.Euler(0f, 90f, 90f);
                }
            }
        }

        if (mangeraPreparacionRA != null)
        {
            if ((mangeraPreparacionRA.transform.localPosition.y < 1.5 || mangeraPreparacionRA.transform.localPosition.x != 16.11098 || mangeraPreparacionRA.transform.localPosition.z != -1.204998) && !mangeraPreparacionRA.GetComponent<OVRGrabbable>().isGrabbed)
            {
                mangeraPreparacionRA.transform.localPosition = new Vector3(16.11098f, 16.339f, -1.204998f);
                mangeraPreparacionRA.transform.localRotation = Quaternion.Euler(0f, 90f, 90f);
            }
        }

        if (bolsaPreparacionAnimacionRA != null && bolsaPreparacionAnimacionRA.GetComponent<OVRGrabbable>() != null)
        {
            if (FindObjectOfType<ControllerBaby>() != null && FindObjectOfType<ControllerBaby>().nuevaPosicionDispRA)
            {
                if (bolsaPreparacionAnimacionRA != null && (bolsaPreparacionAnimacionRA.transform.localPosition.y < 1.5 || bolsaPreparacionAnimacionRA.transform.localPosition.x != 15.31 || bolsaPreparacionAnimacionRA.transform.localPosition.z != 0.94) && !bolsaPreparacionAnimacionRA.GetComponent<OVRGrabbable>().isGrabbed)
                {
                    /**
                    bolsaPreparacionAnimacionRA.transform.localPosition = new Vector3(15.31f, 16.21f, 0.94f);
                    bolsaPreparacionAnimacionRA.transform.localRotation = Quaternion.Euler(-3.062f, -86.192f, -114.146f);
                    **/
                    bolsaPreparacionAnimacionRA.transform.localPosition = new Vector3(15.81f, 17.97f, 1.1f);
                    bolsaPreparacionAnimacionRA.transform.localRotation = Quaternion.Euler(-4.744f, -85.745f, -90.562f);
                    if (GameObject.Find("ConexionABolsa").transform.GetChild(0).gameObject.activeInHierarchy) GameObject.Find("ConexionABolsa").transform.GetChild(0).gameObject.SetActive(false);
                    if (GameObject.Find("CableStartOriginal").transform.GetChild(0).gameObject.activeInHierarchy) GameObject.Find("CableStartOriginal").transform.GetChild(0).gameObject.SetActive(false);
                }
                if (bolsaPreparacionAnimacionRA != null && bolsaPreparacionAnimacionRA.GetComponent<OVRGrabbable>().isGrabbed)
                {
                    if (FindObjectOfType<ControllerBaby>().pasoAgarrarDisp == true && cant == 0)
                    {
                        FindObjectOfType<DialogueViewer1>().ChangeNode();
                        cant = 1;
                    }
                    if (GameObject.Find("ConexionABolsa").transform.GetChild(0).gameObject.activeInHierarchy) GameObject.Find("ConexionABolsa").transform.GetChild(0).gameObject.SetActive(true);
                    if (GameObject.Find("CableStartOriginal").transform.GetChild(0).gameObject.activeInHierarchy) GameObject.Find("CableStartOriginal").transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            else
            {
                if (bolsaPreparacionAnimacionRA != null)
                {
                    if (bolsaPreparacionAnimacionRA.GetComponent<OVRGrabbable>() != null && (bolsaPreparacionAnimacionRA.transform.localPosition.y < 1.5 || bolsaPreparacionAnimacionRA.transform.localPosition.x != 14.05301 || bolsaPreparacionAnimacionRA.transform.localPosition.z != -1.529162) && !bolsaPreparacionAnimacionRA.GetComponent<OVRGrabbable>().isGrabbed)
                    {
                        bolsaPreparacionAnimacionRA.transform.localPosition = new Vector3(14.05301f, 15.09545f, -1.529162f);
                        bolsaPreparacionAnimacionRA.transform.localRotation = Quaternion.Euler(0.385f, -0.561f, -185.131f);
                    }

                    if (bolsaPreparacionAnimacionRA.GetComponent<OVRGrabbable>() != null && bolsaPreparacionAnimacionRA.GetComponent<OVRGrabbable>().isGrabbed)
                    {
                        if (GameObject.Find("SeNecesitaBolsa") != null) GameObject.Find("SeNecesitaBolsa").transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
        }

        //PREPARACION DEL NEOTEE

        if (neoteePreparacionRA != null)
        {
            if ((neoteePreparacionRA.transform.localPosition.y < 1.5 || neoteePreparacionRA.transform.localPosition.x != 12.88236 || neoteePreparacionRA.transform.localPosition.z != -1.219998) && !neoteePreparacionRA.GetComponent<OVRGrabbable>().isGrabbed)
            {
                neoteePreparacionRA.transform.localPosition = new Vector3(12.88236f, 15.43594f, -1.219998f);
                neoteePreparacionRA.transform.localRotation = Quaternion.Euler(0f, 0f, -180f);
            }
        }

        if (mascaritaNeoteePreparacionRA != null)
        {
            if ((mascaritaNeoteePreparacionRA.transform.localPosition.y < 1.5 || mascaritaNeoteePreparacionRA.transform.localPosition.x != 14.03 || mascaritaNeoteePreparacionRA.transform.localPosition.z != -2.023686) && !mascaritaNeoteePreparacionRA.GetComponent<OVRGrabbable>().isGrabbed)
            {
                mascaritaNeoteePreparacionRA.transform.localPosition = new Vector3(14.03f, 18.77004f, -2.023686f);
                mascaritaNeoteePreparacionRA.transform.localRotation = Quaternion.Euler(0f, 90f, 90f);
            }
        }

        if (mangueraPreparacionRANeotee != null)
        {
            if ((mangueraPreparacionRANeotee.transform.localPosition.y < 1.5 || mangueraPreparacionRANeotee.transform.localPosition.x != 16.17001 || mangueraPreparacionRANeotee.transform.localPosition.z != -1.204998) && !mangueraPreparacionRANeotee.GetComponent<OVRGrabbable>().isGrabbed)
            {
                mangueraPreparacionRANeotee.transform.localPosition = new Vector3(16.17001f, 17f, -1.204998f);
                mangueraPreparacionRANeotee.transform.localRotation = Quaternion.Euler(0f, 90f, 90f);
            }
        }

        if (mascaritaNeopuffPreparacionRA != null)
        {
            if ((mascaritaNeopuffPreparacionRA.transform.localPosition.y < 1.5 || mascaritaNeopuffPreparacionRA.transform.localPosition.x != 14.03 || mascaritaNeopuffPreparacionRA.transform.localPosition.z != -2.023686) && !mascaritaNeopuffPreparacionRA.GetComponent<OVRGrabbable>().isGrabbed)
            {
                mascaritaNeopuffPreparacionRA.transform.localPosition = new Vector3(14.03f, 18.77004f, -2.023686f);
                mascaritaNeopuffPreparacionRA.transform.localRotation = Quaternion.Euler(0f, 90f, 90f);
            }
        }

        if (mangueraPreparacionRANeopuff != null)
        {
            if ((mangueraPreparacionRANeopuff.transform.localPosition.y < 1.5 || mangueraPreparacionRANeopuff.transform.localPosition.x != 16.17001 || mangueraPreparacionRANeopuff.transform.localPosition.z != -1.204998) && !mangueraPreparacionRANeopuff.GetComponent<OVRGrabbable>().isGrabbed)
            {
                mangueraPreparacionRANeopuff.transform.localPosition = new Vector3(16.17001f, 17f, -1.204998f);
                mangueraPreparacionRANeopuff.transform.localRotation = Quaternion.Euler(0f, 90f, 90f);
            }
        }

        if (mangueraMascaraPreparacionRANeopuff != null)
        {
            if ((mangueraMascaraPreparacionRANeopuff.transform.localPosition.y < 1.5 || mangueraMascaraPreparacionRANeopuff.transform.localPosition.x != 1.683 || mangueraMascaraPreparacionRANeopuff.transform.localPosition.z != 0.050385) && !mangueraMascaraPreparacionRANeopuff.GetComponent<OVRGrabbable>().isGrabbed)
            {
                mangueraMascaraPreparacionRANeopuff.transform.localPosition = new Vector3(1.683f, -0.96395f, 0.050385f);
                mangueraMascaraPreparacionRANeopuff.transform.localRotation = Quaternion.Euler(-180f, -270f, -180f);
            }
        }


        if (mangueraConectadaNeopuff != null)
        {
            if ((mangueraConectadaNeopuff.transform.localPosition.y < 1.5 || mangueraConectadaNeopuff.transform.localPosition.x != 1.673 || mangueraConectadaNeopuff.transform.localPosition.z != 0.050385) && !mangueraConectadaNeopuff.GetComponent<OVRGrabbable>().isGrabbed)
            {
                if (!GameObject.Find("ManoTutorial").transform.GetChild(0).gameObject.activeInHierarchy)
                {
                    if(GameObject.Find("ExtremoConectado")!=null) GameObject.Find("ExtremoConectado").transform.GetChild(0).gameObject.SetActive(false);
                    mangueraConectadaNeopuff.transform.localPosition = new Vector3(1.673f, -0.96395f, 0.050385f);
                    mangueraConectadaNeopuff.transform.localRotation = Quaternion.Euler(-180f, -270f, -180f);
                }
                else
                {
                    if (GameObject.Find("ExtremoConectado") != null) GameObject.Find("ExtremoConectado").transform.GetChild(0).gameObject.SetActive(true);
                    mangueraConectadaNeopuff.SetActive(false);
                }
            }
            else if(mangueraConectadaNeopuff.GetComponent<OVRGrabbable>().isGrabbed)
            {
                if (GameObject.Find("ExtremoConectado") != null) GameObject.Find("ExtremoConectado").transform.GetChild(0).gameObject.SetActive(true);
                if (GameObject.Find("ManoTutorial").transform.GetChild(0).gameObject.activeInHierarchy)
                {
                    Destroy(mangueraConectadaNeopuff);
                }
            }
            if(GameObject.Find("ManoTutorial").transform.GetChild(0).gameObject.activeInHierarchy) if (GameObject.Find("ExtremoConectado") != null) GameObject.Find("ExtremoConectado").transform.GetChild(0).gameObject.SetActive(true);
        }
                
        if (mangueraNuevaConectadaNeopuff != null && mangueraNuevaConectadaNeopuff.GetComponent<OVRGrabbable>() != null)
        {
            if (FindObjectOfType<ControllerBaby>() != null && FindObjectOfType<ControllerBaby>().nuevaPosicionDispRA)
            {
                if (mangueraNuevaConectadaNeopuff != null && (mangueraNuevaConectadaNeopuff.transform.localPosition.y < -1.5 || mangueraNuevaConectadaNeopuff.transform.localPosition.x != 1.63 || mangueraNuevaConectadaNeopuff.transform.localPosition.z != -0.01) && !mangueraNuevaConectadaNeopuff.GetComponent<OVRGrabbable>().isGrabbed)
                {
                    //COLOCAR LO MISMO QUE DEL BEBE
                    mangueraNuevaConectadaNeopuff.transform.localPosition = new Vector3(1.63f, -0.69f, -0.01f);
                    mangueraNuevaConectadaNeopuff.transform.localRotation = Quaternion.Euler(-17f, -90f, -90f);

                }
                if (mangueraNuevaConectadaNeopuff != null && mangueraNuevaConectadaNeopuff.GetComponent<OVRGrabbable>().isGrabbed)
                {
                    if (FindObjectOfType<ControllerBaby>().pasoAgarrarDisp == true && cant == 0)
                    {
                        FindObjectOfType<DialogueViewer1>().ChangeNode();
                        cant = 1;
                    }
                }
            }
            else
            {
                if ((mangueraNuevaConectadaNeopuff.transform.localPosition.y < 1.5 || mangueraNuevaConectadaNeopuff.transform.localPosition.x != 1.673 || mangueraNuevaConectadaNeopuff.transform.localPosition.z != 0.050385) && !mangueraNuevaConectadaNeopuff.GetComponent<OVRGrabbable>().isGrabbed)
                {
                    if (GameObject.Find("ExtremoConectado") != null) GameObject.Find("ExtremoConectado").transform.GetChild(0).gameObject.SetActive(false);
                    mangueraNuevaConectadaNeopuff.transform.localPosition = new Vector3(1.673f, -0.96395f, 0.050385f);
                    mangueraNuevaConectadaNeopuff.transform.localRotation = Quaternion.Euler(-180f, -270f, -180f);

                }
                else if (mangueraNuevaConectadaNeopuff.GetComponent<OVRGrabbable>().isGrabbed)
                {
                    if (GameObject.Find("ExtremoConectado") != null) GameObject.Find("ExtremoConectado").transform.GetChild(0).gameObject.SetActive(true);

                    if (GameObject.Find("SeNecesitaNeopuff") != null) GameObject.Find("SeNecesitaNeopuff").transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
        else if(mangueraNuevaConectadaNeopuff != null && mangueraNuevaConectadaNeopuff.GetComponent<OVRGrabbable>() == null)
        {
            if (FindObjectOfType<ControllerBaby>() != null && FindObjectOfType<ControllerBaby>().posicionDispFlujoLibre)
            {
                if ((mangueraNuevaConectadaNeopuff.transform.localPosition.y < -1.5 || mangueraNuevaConectadaNeopuff.transform.localPosition.x != 1.585 || mangueraNuevaConectadaNeopuff.transform.localPosition.z != -0.001))
                {
                    mangueraNuevaConectadaNeopuff.transform.localPosition = new Vector3(1.585f, -0.679f, -0.001f);
                    mangueraNuevaConectadaNeopuff.transform.localRotation = Quaternion.Euler(-17f, -90f, -90f);

                }
            }
            else if(FindObjectOfType<ControllerBaby>() == null)
            {
                mangueraNuevaConectadaNeopuff.transform.localPosition = new Vector3(1.673f, -0.96395f, 0.050385f);
                mangueraNuevaConectadaNeopuff.transform.localRotation = Quaternion.Euler(-180f, -270f, -180f);
            }
        }

        if (cateterMesa != null && (cateterMesa.transform.localPosition.y < 1.5 || cateterMesa.transform.localPosition.x != 0.339 || cateterMesa.transform.localPosition.z != 5.31) && !cateterMesa.GetComponent<OVRGrabbable>().isGrabbed)
        {
            cateterMesa.transform.localPosition = new Vector3(0.339f, 2.084f, 5.31f);
            cateterMesa.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
        }

        if (jeringaMesa != null && (jeringaMesa.transform.localPosition.y < 1.5 || jeringaMesa.transform.localPosition.x != 0.1942253 || jeringaMesa.transform.localPosition.z != 4.994) && !jeringaMesa.GetComponent<OVRGrabbable>().isGrabbed)
        {
            jeringaMesa.transform.localPosition = new Vector3(0.1942253f, 2.082f, 4.994f);
            jeringaMesa.transform.localRotation = Quaternion.Euler(270f, 0f, -90f);
        }

        if (laringoMesa != null && (laringoMesa.transform.localPosition.y < 1.5 || laringoMesa.transform.localPosition.x != 0.413 || laringoMesa.transform.localPosition.z != 5.112) && !laringoMesa.GetComponent<OVRGrabbable>().isGrabbed)
        {
            laringoMesa.transform.localPosition = new Vector3(0.413f, 2.069f, 5.112f);
            laringoMesa.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if (AdrenalinaMesa != null && (AdrenalinaMesa.transform.localPosition.y < 1.5 || AdrenalinaMesa.transform.localPosition.x != 0.273 || AdrenalinaMesa.transform.localPosition.z != 4.989) && !AdrenalinaMesa.GetComponent<OVRGrabbable>().isGrabbed)
        {
            AdrenalinaMesa.transform.localPosition = new Vector3(0.273f, 2.069f, 4.989f);
            AdrenalinaMesa.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
        }

    }


}
