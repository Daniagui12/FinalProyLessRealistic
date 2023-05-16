using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidacionPreguntas : MonoBehaviour
{
    private List<string> preguntasTotales;
    public List<string> preguntasRealizadas;
    public List<AudioClip> audiosRetroalimentacion;
    Audio scriptAudio;

    private void Awake()
    {
        scriptAudio = FindObjectOfType<Audio>();
    }

        // Start is called before the first frame update
        void Start()
    {
        preguntasTotales = new List<string>();

        preguntasTotales.Add("Pregunta1");
        preguntasTotales.Add("Pregunta2");
        preguntasTotales.Add("Pregunta3");
        preguntasTotales.Add("Pregunta4");

        preguntasRealizadas = new List<string>();
        audiosRetroalimentacion = new List<AudioClip>();
    }

    public IEnumerator CargarAudio()
    {

        for (int i = 0; i < preguntasTotales.Count; i++)
        {
            if (!preguntasRealizadas.Contains(preguntasTotales[i].ToString()))
            {

                print("SE DEBE REPRODUCIR LA PREGUNTA " + preguntasTotales[i].ToString());
                //El audio debe desabilitarse para que pueda iniciarse despues
                scriptAudio.audioData.enabled = false;
                scriptAudio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/" + preguntasTotales[i].ToString());
                //El ovrlypsinc tambien debe deshabilitarse para que el lipsync inicie nuevamente
                scriptAudio.ovrLipsync.enabled = false;
                //Cuando se habilita el lipsync y el audio, el personaje inicia a hablar de nuevo
                scriptAudio.ovrLipsync.enabled = true;
                scriptAudio.audioData.enabled = true;                
            }
            yield return "listo";
        }
        scriptAudio.pantalla2.SetActive(true);
        yield return "listo";
    }

    public void AgregarAudiosRetroalimentacion()
    {
        for (int i = 0; i < preguntasTotales.Count; i++)
        {
            if (!preguntasRealizadas.Contains(preguntasTotales[i].ToString()))
            {
                audiosRetroalimentacion.Add(Resources.Load<AudioClip>("Audios Voz Real/" + preguntasTotales[i].ToString()));                
            }
        }        
    }

    public IEnumerator PlayAudioSequentially()
    {
        
        //1.Loop through each AudioClip
        for (int i = 0; i < audiosRetroalimentacion.Count; i++)
        {
            //2.Assign current AudioClip to audiosource
            print("SE DEBE REPRODUCIR LA PREGUNTA " + preguntasTotales[i].ToString());
            //El audio debe desabilitarse para que pueda iniciarse despues
            scriptAudio.audioData.enabled = false;
            scriptAudio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/" + preguntasTotales[i].ToString());
            //El ovrlypsinc tambien debe deshabilitarse para que el lipsync inicie nuevamente
            scriptAudio.ovrLipsync.enabled = false;
            //Cuando se habilita el lipsync y el audio, el personaje inicia a hablar de nuevo
            scriptAudio.ovrLipsync.enabled = true;
            scriptAudio.audioData.enabled = true;
            while (scriptAudio.audioData.isPlaying)
            {
                print("esta sonando el audio");
                yield return null;
            }

            //5. Go back to #2 and play the next audio in the adClips array
        }
    }      

}
