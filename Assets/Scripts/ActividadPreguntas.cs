using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActividadPreguntas : MonoBehaviour
{
    private JsonObject preguntas;
    public TextMesh p1;
    public TextMesh p2;
    public TextMesh p3;
    public TextMesh p4;
    public int contador = 0;
    public int maxCont = 0;

    Audio scriptAudio;
    CambioMaterial scriptCambio;
    ValidacionPreguntas scriptValidacion;

    public bool pregunta1 = false;
    public bool pregunta2 = false;
    public bool pregunta3 = false;
    public bool pregunta4 = false;

    private bool aviso=false;

    public GameObject flecha;

    private void Update()
    {
        if (gameObject.name == "BtnSiguiente")
        {
            if (!aviso && gameObject.GetComponent<ActividadPreguntas>().pregunta4 == true && gameObject.GetComponent<ActividadPreguntas>().pregunta3 == true && gameObject.GetComponent<ActividadPreguntas>().pregunta2 == true && gameObject.GetComponent<ActividadPreguntas>().pregunta1 == true)
            {
                aviso = true;
                StartCoroutine(Recordatorio());                
            }
        }
    }

    private void Awake()
    {
        scriptAudio = FindObjectOfType<Audio>();
        scriptCambio = FindObjectOfType<CambioMaterial>();
        scriptValidacion = FindObjectOfType<ValidacionPreguntas>();

    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Hospital" || SceneManager.GetActiveScene().name == "Hospital - copia")
        {
            GameObject.Find("ButtonNext").GetComponent<Button>().onClick.AddListener(validarPreguntasSeguir);
        }

        if (scriptAudio.jsonFile != null)
        {
            preguntas = JsonUtility.FromJson<JsonObject>(scriptAudio.jsonFile.text);
            print(preguntas.preguntas.Length);
            float temp1 = preguntas.preguntas.Length;
            float temp2 = 4;
            temp2 = temp1 / temp2;
            maxCont = Mathf.CeilToInt(temp2);
            if (preguntas != null)
            {
                if (preguntas.preguntas.Length > 3)
                {
                    if (p1 != null) p1.text = preguntas.preguntas[0].pregunta;
                    if (p2 != null) p2.text = preguntas.preguntas[1].pregunta;
                    if (p3 != null) p3.text = preguntas.preguntas[2].pregunta;
                    if (p4 != null) p4.text = preguntas.preguntas[3].pregunta;
                }
                else
                {
                    actualizar();
                }
            }
        }

    }

    public void responderPreguntas()
    {

        if (gameObject.name == "Boton abajo")
        {
            siguientePreguntas();
        }
        else if (gameObject.name == "Boton arriba")
        {
            anteriorPreguntas();
        }
        else if (gameObject.name == "BtnSiguiente")
        {
            validarPreguntasSeguir();

        }
        else
        {
            print("ESTA ENTRANDO");
            PreguntasIndividuales p = Array.Find(preguntas.preguntas, pregunta => pregunta.pregunta == gameObject.transform.GetComponentInChildren<TextMesh>().text);
            StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/" + p.source), gameObject.name));
            if (p.pregunta == "¿Cuál es la edad gestacional?") GameObject.Find("BtnSiguiente").GetComponent<ActividadPreguntas>().pregunta1 = true;
            if (p.pregunta == "¿Cuántos bebés son?") GameObject.Find("BtnSiguiente").GetComponent<ActividadPreguntas>().pregunta2 = true;
            if (p.pregunta == "¿Hay factores de riesgo materno? ") GameObject.Find("BtnSiguiente").GetComponent<ActividadPreguntas>().pregunta3 = true;
            if (p.pregunta == "Características del líquido amniótico") GameObject.Find("BtnSiguiente").GetComponent<ActividadPreguntas>().pregunta4 = true;
        }
    }

    public void validarPreguntasSeguir()
    {
        if (pregunta1 == true && pregunta2 == true && pregunta3 == true && pregunta4 == true)
        {
            StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/DialogoInicioBioseguridad"), gameObject.name));

            scriptAudio.pantallaRetro.SetActive(false);
            scriptAudio.preguntas.SetActive(false);
            Destroy(scriptAudio.botonContinuar.GetComponent<ActividadPreguntas>());
            Destroy(GameObject.Find("FlechaPreguntas"));
            
            GameObject[] lista = GameObject.FindGameObjectsWithTag("Bioseguridad");
            foreach (var gObj in lista)
            {
                gObj.AddComponent(typeof(ActividadBioseguridad));
            }
            
            scriptAudio.pantalla.SetActive(true);

            if (SceneManager.GetActiveScene().name != "Hospital")
            {
                GameObject.Find("ColliderLavamanos").transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (SceneManager.GetActiveScene().name == "Hospital")
            {
                scriptAudio.flechaBio.SetActive(true);
                GameObject.Find("ColliderAntesLavamanos").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("ColliderLavamanos").transform.GetChild(0).gameObject.SetActive(true);
                Destroy(scriptAudio.botonContinuar.transform.GetChild(0).gameObject);

                GameObject[] canvas = GameObject.FindGameObjectsWithTag("CanvasLaser");
                foreach (var gObjCanvas in canvas)
                {
                    Destroy(gObjCanvas);
                }
            }
        }
        else
        {
            scriptAudio.preguntas.SetActive(false);
            scriptAudio.pantallaRetro.SetActive(true);
            AudioClip audio1 = null;
            AudioClip audio2 = null;
            AudioClip audio3 = null;
            AudioClip audio4 = null;

            if (pregunta1 == false)
            {
                audio1 = Resources.Load<AudioClip>("Audios Voz Real/Pregunta1");
                
            }
            if (pregunta2 == false)
            {
                audio2 = Resources.Load<AudioClip>("Audios Voz Real/Pregunta2");
                
            }
            if (pregunta3 == false)
            {
                audio3 = Resources.Load<AudioClip>("Audios Voz Real/Pregunta3");
                
            }
            if (pregunta4 == false)
            {
                audio4 = Resources.Load<AudioClip>("Audios Voz Real/Pregunta4");                
            }

            if (scriptAudio.tipoCaso == "Minuto de oro") StartCoroutine(ExplicarTodo(audio1, audio2, audio3, audio4));
            else StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/DialogoPreguntasIncompletas"), "incompleto")); ;
        }
    }

    // Start is called before the first frame update
    private void OnMouseDown()
    {
        responderPreguntas();
    }

    IEnumerator Explicar(AudioClip audio, string name)
    {
        if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Habla");
        //El audio debe desabilitarse para que pueda iniciarse despues
        scriptAudio.audioData.enabled = false;
        scriptAudio.audioData.clip = audio;
        //El ovrlypsinc tambien debe deshabilitarse para que el lipsync inicie nuevamente
        scriptAudio.ovrLipsync.enabled = false;
        //Cuando se habilita el lipsync y el audio, el personaje inicia a hablar de nuevo
        scriptAudio.ovrLipsync.enabled = true;
        scriptAudio.audioData.enabled = true;
        yield return new WaitForSeconds(audio.length);
        if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Idle");
    }


    IEnumerator Recordatorio()
    {
        yield return new WaitForSeconds(10f);
        if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Habla");
        GameObject.Find("Pregunta1").GetComponent<ActividadPreguntas>().flecha.SetActive(true);
        //El audio debe desabilitarse para que pueda iniciarse despues
        scriptAudio.audioData.enabled = false;
        scriptAudio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/DialogoRecordatorioBotonPreguntas");
        //El ovrlypsinc tambien debe deshabilitarse para que el lipsync inicie nuevamente
        scriptAudio.ovrLipsync.enabled = false;
        //Cuando se habilita el lipsync y el audio, el personaje inicia a hablar de nuevo
        scriptAudio.ovrLipsync.enabled = true;
        scriptAudio.audioData.enabled = true;
        yield return new WaitForSeconds(scriptAudio.audioData.clip.length);
        if (!GameObject.Find("DoctoraGuia").GetComponent<MoveCharWithAnimation>().walking) GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Idle");
    }


    IEnumerator ExplicarTodo(AudioClip audio1, AudioClip audio2, AudioClip audio3, AudioClip audio4)
    {
        GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Habla");
        if (audio1 != null)
        {
            //El audio debe desabilitarse para que pueda iniciarse despues
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = audio1;
            //El ovrlypsinc tambien debe deshabilitarse para que el lipsync inicie nuevamente
            scriptAudio.ovrLipsync.enabled = false;
            //Cuando se habilita el lipsync y el audio, el personaje inicia a hablar de nuevo
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            yield return new WaitForSeconds(audio1.length);
            yield return "listo";
        }
        if (audio2 != null)
        {
            //El audio debe desabilitarse para que pueda iniciarse despues
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = audio2;
            //El ovrlypsinc tambien debe deshabilitarse para que el lipsync inicie nuevamente
            scriptAudio.ovrLipsync.enabled = false;
            //Cuando se habilita el lipsync y el audio, el personaje inicia a hablar de nuevo
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            yield return new WaitForSeconds(audio2.length);
            yield return "listo";
        }
        if (audio3 != null)
        {
            //El audio debe desabilitarse para que pueda iniciarse despues
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = audio3;
            //El ovrlypsinc tambien debe deshabilitarse para que el lipsync inicie nuevamente
            scriptAudio.ovrLipsync.enabled = false;
            //Cuando se habilita el lipsync y el audio, el personaje inicia a hablar de nuevo
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            yield return new WaitForSeconds(audio3.length);
            yield return "listo";
        }
        if (audio4 != null)
        {
            //El audio debe desabilitarse para que pueda iniciarse despues
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = audio4;
            //El ovrlypsinc tambien debe deshabilitarse para que el lipsync inicie nuevamente
            scriptAudio.ovrLipsync.enabled = false;
            //Cuando se habilita el lipsync y el audio, el personaje inicia a hablar de nuevo
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            yield return new WaitForSeconds(audio4.length);
            yield return "listo";
        }
        GameObject.Find("DoctoraGuia").GetComponent<Animator>().SetTrigger("Idle");
        scriptAudio.preguntas.SetActive(true);
        scriptAudio.pantallaRetro.SetActive(false);
    }

    void actualizar()
    {
        if (preguntas.preguntas.Length - (contador * 4) > 3)
        {
            p1.text = preguntas.preguntas[(contador * 4) + 0].pregunta;
            p2.text = preguntas.preguntas[(contador * 4) + 1].pregunta;
            p3.text = preguntas.preguntas[(contador * 4) + 2].pregunta;
            p4.text = preguntas.preguntas[(contador * 4) + 3].pregunta;
        }
        else
        {
            if (preguntas.preguntas.Length - (contador * 4) == 1)
            {
                p1.text = preguntas.preguntas[(contador * 4) + 0].pregunta;
                p2.text = "";
                p3.text = "";
                p4.text = "";
            }
            else if (preguntas.preguntas.Length - (contador * 4) == 2)
            {
                p1.text = preguntas.preguntas[(contador * 4) + 0].pregunta;
                p2.text = preguntas.preguntas[(contador * 4) + 1].pregunta;
                p3.text = "";
                p4.text = "";
            }
            else
            {
                p1.text = preguntas.preguntas[(contador * 4) + 0].pregunta;
                p2.text = preguntas.preguntas[(contador * 4) + 1].pregunta;
                p3.text = preguntas.preguntas[(contador * 4) + 2].pregunta;
                p4.text = "";
            }
        }

    }

    public void siguientePreguntas()
    {
        print("siguiente");
        if (contador + 1 < maxCont)
        {
            print("se cambia al siguiente");
            contador += 1;
            GameObject.Find("Boton arriba").GetComponentInChildren<ActividadPreguntas>().contador = contador;
            actualizar();
        }
    }

    public void anteriorPreguntas()
    {
        print("anterior");
        if (contador - 1 > -1)
        {
            print("se cambia al anterior");
            contador -= 1;
            GameObject.Find("Boton abajo").GetComponentInChildren<ActividadPreguntas>().contador = contador;
            actualizar();
        }

    }

    public void playAudio(string source)
    {
        PreguntasIndividuales p = Array.Find(preguntas.preguntas, pregunta => pregunta.pregunta == source);
        StartCoroutine(Explicar(Resources.Load<AudioClip>("Audios Voz Real/" + p.source), gameObject.name));
    }
}
