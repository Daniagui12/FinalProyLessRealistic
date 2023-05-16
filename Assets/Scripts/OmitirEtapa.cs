using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class OmitirEtapa : MonoBehaviour
{

    public GameObject video;
    public GameObject imagen;
    public TextMeshProUGUI texto;
    public GameObject fondo;
    Audio inicio;

    public bool click = false;
    private void Awake()
    {
        
        inicio = FindObjectOfType<Audio>();
        
    }


    public void OmitirEtapaSeleccion()
    {
        GameObject.Find("BtnSiguienteEquipos").GetComponent<ActividadEquipos>().validarElementosPreparados();
        if (inicio.tipoCaso == "Minuto de oro") inicio.particlesMama.SetActive(true);
        else
        {
            inicio.particlesCalentador.SetActive(true);
            inicio.particlesMama.SetActive(false);
        }
        GameObject.Find("CanvasSaltarEtapa").SetActive(false);
    }

    public void RecordarTutorial()
    {
        StartCoroutine(tutorial());
    }

    IEnumerator tutorial()
    {
        fondo.transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("BtnRepetirIntruccion").GetComponent<Image>().enabled=false;
        GameObject.Find("BtnRepetirInstrucciones").GetComponent<TextMeshProUGUI>().text = "";
        video.SetActive(true);
        texto.text = "Agarra los objetos oprimiendo uno de estos botones.";
        video.GetComponentInChildren<VideoPlayer>().clip = Resources.Load<VideoClip>("com.oculus.UnitySample-20210629-173606_Trim (2)");
        inicio.audioData.enabled = false;
        inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/InstruccionEquipos");
        inicio.ovrLipsync.enabled = false;
        inicio.ovrLipsync.enabled = true;
        inicio.audioData.enabled = true;
        yield return new WaitForSeconds(inicio.audioData.clip.length);

        texto.text = "Mientras sostienes un objeto, oprime A para seleccionarlo";
        video.SetActive(false);
        imagen.SetActive(true);
        inicio.audioData.enabled = false;
        inicio.audioData.clip = Resources.Load<AudioClip>("Audios Voz Real/Dialogos/Seleccion1");
        inicio.ovrLipsync.enabled = false;
        inicio.ovrLipsync.enabled = true;
        inicio.audioData.enabled = true;
        yield return new WaitForSeconds(inicio.audioData.clip.length);
        yield return new WaitForSeconds(1f);
        
        imagen.SetActive(false);
        GameObject.Find("BtnRepetirIntruccion").GetComponent<Image>().enabled = true;
        GameObject.Find("BtnRepetirInstrucciones").GetComponent<TextMeshProUGUI>().text = "Recordar tutorial";
        fondo.transform.GetChild(0).gameObject.SetActive(false);

    }
}
