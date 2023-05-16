using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActividadEquipos : MonoBehaviour
{
    CambioMaterial scriptCambio;
    Audio inicio;
    Inicio script;
    public bool fueSeleccionado = false;

    private void Awake()
    {
        inicio = FindObjectOfType<Audio>();
        scriptCambio = FindObjectOfType<CambioMaterial>();
        script = FindObjectOfType<Inicio>();
    }

    private void Update()
    {
        if (gameObject.GetComponent<OVRGrabbable>().isGrabbed)
        {
            if (GameObject.Find("NombreHerramientaActual") != null) GameObject.Find("NombreHerramientaActual").GetComponent<TextMeshProUGUI>().text = gameObject.name;
        }
    }

    private void OnMouseDown()
    {
        print("ESTE SE ESTA OPRIMIENDO!! " + gameObject.name);
        //Esto debería cambiarse por compresa caliente
        if (gameObject.name == "Toalla") StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Toalla")));
        if (gameObject.name != "Boton agregar" && gameObject.name != "BtnSiguienteEquipos")
        {
            bool hayAlgunoActivo = false;
            GameObject[] lista = GameObject.FindGameObjectsWithTag("informacion");
            foreach (var gObj in lista)
            {
                if(gObj.name!= gameObject.name && gObj.activeInHierarchy)
                {
                    gObj.SetActive(false);
                }                
            }

            GameObject.Find("NombreHerramientaActual").GetComponent<TextMeshProUGUI>().text= gameObject.name;
            gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = gameObject.name;
            if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy) gameObject.transform.GetChild(0).gameObject.SetActive(false);
            else gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (gameObject.name== "BtnSiguienteEquipos")
        {
            validarElementosPreparados();
        }
        else
        {
            AgregarEquipo();
        }
    }

    public void AgregarEquipo()
    {
        print("SE DEBE AGREGAR ALGO-!!!!");
        if(SceneManager.GetActiveScene().name=="HospitalPC") scriptCambio.objetosSeleccionados.Add(gameObject.transform.parent.gameObject.transform.parent.name);
        /**else scriptCambio.objetosSeleccionados.Add(GameObject.Find("NombreHerramientaActual").GetComponent<TextMeshProUGUI>().text);**/

        if (scriptCambio.objetosSeleccionados.Count == 1) GameObject.Find("BtnSiguienteEquipos").AddComponent(typeof(ActividadEquipos));
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void validarElementosPreparados()
    {
        GameObject.Find("RevisarElementosSeleccionados").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FondoNegro").transform.GetChild(0).gameObject.SetActive(false);
        Destroy(GameObject.Find("BotonDerecho"));
        Destroy(GameObject.Find("BotonIzquierdo"));
        if (GameObject.Find("ControladorSimulacion") != null) GameObject.Find("ControladorSimulacion").SetActive(false);
        if (GameObject.Find("SeleccionEquipo") != null) GameObject.Find("SeleccionEquipo").SetActive(false);
        if (GameObject.Find("Letrero") != null) GameObject.Find("Letrero").SetActive(false);
        if (GameObject.Find("VideoEquipos") != null) GameObject.Find("VideoEquipos").SetActive(false);
        if (GameObject.Find("CanvasSaltarEtapa") != null) GameObject.Find("CanvasSaltarEtapa").SetActive(false);
        if (GameObject.Find("Particle System equipo") != null) GameObject.Find("Particle System equipo").SetActive(false);
        GameObject.Find("FlechaSeleccionEquipos").transform.GetChild(0).gameObject.SetActive(false);

        StartCoroutine(scriptCambio.EliminarNoNecesarios());

       
        GameObject[] lista = GameObject.FindGameObjectsWithTag("ActividadEquipos");
        foreach (var gObj in lista)
        {
            Destroy(gObj.GetComponent<ActividadEquipos>());
            gObj.AddComponent(typeof(RetroalimentacionDispositivos));
        }

        GameObject[] lista2 = GameObject.FindGameObjectsWithTag("informacion");
        foreach (var gObj in lista2)
        {
            if (gObj.name != gameObject.name && gObj.activeInHierarchy)
            {
                gObj.SetActive(false);
            }
        }

        Destroy(gameObject.GetComponent<ActividadEquipos>());

        inicio.personajeNPC.SetActive(true);

        string casoActual = "TwineCasosN3/1BebeNaceSano";
                

        if (inicio.tipoCaso.Equals("Minuto de oro"))
        {
            StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/RetroalimentacionEquiposOculus1")));
            inicio.particlesMama.SetActive(true);

            if (inicio.casoGuiado)
            {
                if(inicio.casoSeleccionado== "casoBebeNaceBien") casoActual= "TwineCasosN3/1BebeNaceSano";
                else if (inicio.casoSeleccionado == "casoBebeMejora") casoActual = "TwineCasosN3/2BebeCianoticoSinRespirarMejora";
                else if (inicio.casoSeleccionado == "casoBebeNoMejora") casoActual = "TwineCasosN3/2BebeCianoticoSinRespirarNoMejora";                
                else if (inicio.casoSeleccionado == "casoBebeSinRespirarMejoraConSecado") casoActual = "TwineCasosN3/3BebeSinRespirarMejoraConSecado";
                else if (inicio.casoSeleccionado == "casoBebeCianoticoSinRespirarMejoraConAspiracion") casoActual = "TwineCasosN3/2BebeCianoticoSinRespirarMejoraConAspiracion";
                else
                {
                    List<string> flujos = new List<string>();
                    flujos.Add("TwineCasosN3/1BebeNaceSano");
                    flujos.Add("TwineCasosN3/2BebeCianoticoSinRespirarMejora");
                    flujos.Add("TwineCasosN3/3BebeSinRespirarMejoraConSecado");
                    flujos.Add("TwineCasosN3/2BebeCianoticoSinRespirarMejoraConAspiracion");

                    int randomIndex = Random.Range(0, flujos.Count);
                    string randomName = flujos[randomIndex];
                    casoActual = randomName;
                }

            }
            else
            {
                if (inicio.casoSeleccionado == "casoBebeNaceBien") casoActual = "TwineCasosSinDialogos/1BebeNaceSano";
                else if (inicio.casoSeleccionado == "casoBebeMejora") casoActual = "TwineCasosSinDialogos/2BebeCianoticoSinRespirarMejora";
                else if (inicio.casoSeleccionado == "casoBebeNoMejora") casoActual = "TwineCasosSinDialogos/2BebeCianoticoSinRespirarNoMejora";
                else if (inicio.casoSeleccionado == "casoBebeSinRespirarMejoraConSecado") casoActual = "TwineCasosSinDialogos/3BebeSinRespirarMejoraConSecado";
                else if (inicio.casoSeleccionado == "casoBebeCianoticoSinRespirarMejoraConAspiracion") casoActual = "TwineCasosSinDialogos/2BebeCianoticoSinRespirarMejoraConAspiracion";
                else
                {
                    List<string> flujos = new List<string>();
                    flujos.Add("TwineCasosSinDialogos/1BebeNaceSano");
                    flujos.Add("TwineCasosSinDialogos/2BebeCianoticoSinRespirarMejora");
                    flujos.Add("TwineCasosSinDialogos/3BebeSinRespirarMejoraConSecado");
                    flujos.Add("TwineCasosSinDialogos/2BebeCianoticoSinRespirarMejoraConAspiracion");

                    int randomIndex = Random.Range(0, flujos.Count);
                    string randomName = flujos[randomIndex];
                    casoActual = randomName;
                }
            }
            GameObject.Find("DialogController").GetComponent<DialogueController1>().twineText = Resources.Load<TextAsset>(casoActual);

            if (SceneManager.GetActiveScene().name == "HospitalPC" || SceneManager.GetActiveScene().name == "HospitalActualizado")
            {
                inicio.canvas.transform.position = new Vector3(20.55f, 3.49f, -54.07f);
                inicio.tv3.transform.position = new Vector3(20.56f, 2.25f, -54.19f);
            }

            script.doctor.setPathBase(GameObject.Find("Path base Camilla"));

        }
        else
        {
            StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/Dialogos/RetroalimentacionEquiposOculus1RA")));
            inicio.particlesCalentador.SetActive(true);
            if (inicio.dispositivoVentilacionSeleccionado == "Bolsa autoinflable")
            {
                if (inicio.casoSeleccionado == "casoBebeSinRespirarMejora") casoActual = "TwineReanimacionAvanzada/1BebeMejoraAutoinflable";
                else if (inicio.casoSeleccionado == "casoBebeSinRespirarNoMejora") casoActual = "TwineReanimacionAvanzada/1BebeNoMejoraAutoinflable";
                            
            }
            if (inicio.dispositivoVentilacionSeleccionado == "Neotee")
            {
                if (inicio.casoSeleccionado == "casoBebeSinRespirarMejora") casoActual = "TwineReanimacionAvanzada/2BebeMejoraNeotee";
                else if (inicio.casoSeleccionado == "casoBebeSinRespirarNoMejora") casoActual = "TwineReanimacionAvanzada/2BebeNoMejoraNeotee";
                else if (inicio.casoSeleccionado == "casoBebeConDificultadRespiratoria") casoActual = "TwineReanimacionAvanzada/2BebeDificultadNeotee";

            }
            if (inicio.dispositivoVentilacionSeleccionado == "Neopuff")
            {
                if (inicio.casoSeleccionado == "casoBebeSinRespirarMejora") casoActual = "TwineReanimacionAvanzada/3BebeMejoraNeopuff";
                else if (inicio.casoSeleccionado == "casoBebeSinRespirarNoMejora") casoActual = "TwineReanimacionAvanzada/3BebeNoMejoraNeopuff";
                else if (inicio.casoSeleccionado == "casoBebeConDificultadRespiratoria") casoActual = "TwineReanimacionAvanzada/3BebeDificultadNeopuff";
                               

            }

            GameObject.Find("DialogController").GetComponent<DialogueController1>().twineText = Resources.Load<TextAsset>(casoActual);

            script.doctor.setPathBase(GameObject.Find("Path base Camilla"));
        }
        script.doctor.StartMove();
        inicio.canvas.SetActive(true);
        inicio.tv3.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().name == "Hospital" && (other.gameObject.name == "GrabVolumeSmallRight" || other.gameObject.name == "GrabVolumeSmallLeft") && gameObject.name != "Boton agregar" && gameObject.name != "BtnSiguienteEquipos")
        {
            GameObject.Find("NombreHerramientaActual").GetComponent<TextMeshProUGUI>().text = gameObject.name;
        } 
    }


    IEnumerator Explicar(AudioClip audio)
    {
        if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Habla");
        //El audio debe desabilitarse para que pueda iniciarse despues
        inicio.audioData.enabled = false;
        inicio.audioData.clip = audio;
        //El ovrlypsinc tambien debe deshabilitarse para que el lipsync inicie nuevamente
        inicio.ovrLipsync.enabled = false;
        //Cuando se habilita el lipsync y el audio, el personaje inicia a hablar de nuevo
        inicio.ovrLipsync.enabled = true;
        inicio.audioData.enabled = true;
        yield return "listo";
        if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Idle");
    }

    
}
