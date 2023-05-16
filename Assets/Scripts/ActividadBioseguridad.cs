using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActividadBioseguridad : MonoBehaviour
{
    public TextMeshProUGUI texto;
    public bool seLavoManos;
    Inicio inicio;    
    public bool puedeRegresar;
    public MoveCharWithAnimation doctor;
    public Animator puerta;
    public GameObject izquierda;

    public bool entraronManos = false;

    bool puedeSeleccionarlo = true;
    float transcurrido = 0.0f;

    private void Awake()
    {
        inicio = FindObjectOfType<Inicio>();
    }
    
    void Start()
    {
        seLavoManos = false;
        puedeRegresar = false;
        InvokeRepeating("RecordarActividad", 20.0f, 35.0f);
    }

    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Bioseguridad").Length==0 && !puedeRegresar)
        {            
            puerta.SetTrigger("Abrir");
            StartCoroutine(inicio.Explicar(Resources.Load<AudioClip>("Audios Voz Real/DialogoSiguienteFaseContinuar")));
            doctor.setPathBase(GameObject.Find("Path base Sala"));
            doctor.StartMove();            
            puedeRegresar = true;
            GameObject.Find("RecordatorioSalaPadre").transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((SceneManager.GetActiveScene().name == "HospitalPC" || SceneManager.GetActiveScene().name == "HospitalActualizado") && other.gameObject.tag == "Player")
        {
            GameObject.Find("Lavamanos").transform.GetChild(0).gameObject.SetActive(true);
        }
        else if(SceneManager.GetActiveScene().name == "Hospital" && (other.gameObject.name == "GrabVolumeSmallRight" || other.gameObject.name == "GrabVolumeSmallLeft") && gameObject.tag=="Bioseguridad")
        {
            if (puedeSeleccionarlo)
            {
                puedeSeleccionarlo = false;
                seleccionarImplemento();
            }
            else
            {
                puedeSeleccionarlo = true;
            }
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if ((SceneManager.GetActiveScene().name == "HospitalPC" || SceneManager.GetActiveScene().name == "HospitalActualizado") && other.gameObject.tag == "Player")
        {
            GameObject.Find("Lavamanos").transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (SceneManager.GetActiveScene().name=="Hospital" && (other.gameObject.name == "GrabVolumeSmallRight" || other.gameObject.name == "GrabVolumeBig") && gameObject.name == "ColliderLavamanosHijo")
        {            
            GameObject.Find("ColliderLavamanosHijo").GetComponent<ActividadBioseguridad>().entraronManos = true;
            if (GameObject.Find("ColliderAntesLavamanosHijo") != null)
            {
                ActividadLavadaManos lavamanos = GameObject.Find("ColliderAntesLavamanosHijo").GetComponent<ActividadLavadaManos>();
                if (lavamanos.texto != null) lavamanos.texto.text = "";
            }
            
            inicio.temporizador.SetActive(true);

            transcurrido += Time.fixedDeltaTime;
            if (transcurrido > 4.0f)
            {                
                if (!seLavoManos)
                {
                    lavarManos();
                }
            }
            
        }
        else if ((SceneManager.GetActiveScene().name == "HospitalPC" || SceneManager.GetActiveScene().name == "HospitalActualizado") && other.gameObject.tag == "Player")
        {
            transcurrido += Time.fixedDeltaTime;
            if (transcurrido > 4.0f)
            {
                if (!seLavoManos)
                {
                    
                    lavarManos();
                }
            }
        }
    }

    private void lavarManos()
    {
        Debug.Log("Se lavó las manos");
        seLavoManos = true;
        GameObject.Find("CanvasTV").gameObject.transform.GetChild(0).gameObject.SetActive(true);
        Destroy(inicio.temporizador);
        StartCoroutine(inicio.Explicar(Resources.Load<AudioClip>("Audios Voz Real/DialogoTrasLavadaManos1")));
        inicio.hizoClick = true;
    }

    private void OnMouseDown()
    {
        seleccionarImplemento();
    }

    void seleccionarImplemento()
    {
        if (GameObject.Find("ColliderLavamanosHijo") != null && gameObject.name != "ColliderLavamanosHijo")
        {
            if (GameObject.Find("ColliderLavamanosHijo").GetComponent<ActividadBioseguridad>().seLavoManos)
            {
                if (gameObject.name == "Guantes" && SceneManager.GetActiveScene().name=="Hospital")
                {
                    Debug.Log("CAMBIAN DE MATERIAL LAS MANOS");

                    GameObject.Find("hands:Rhand").GetComponent<SkinnedMeshRenderer>().material = inicio.materialGuantes;
                    izquierda.GetComponent<SkinnedMeshRenderer>().material = inicio.materialGuantes;
                }
                inicio.hizoClick = true;
                GameObject.Find(gameObject.name + "Txt").GetComponent<TextMeshProUGUI>().text = "Listo";
                GameObject.Find(gameObject.name + "Txt").GetComponent<TextMeshProUGUI>().color = Color.green;
                Destroy(gameObject);
            }
            else
            {
                inicio.hizoClick = true;
                StartCoroutine(inicio.Explicar(Resources.Load<AudioClip>("Audios Voz Real/DialogoNoLavadaManos")));
            }
            
        }
    }



    void RecordarActividad()
    {
        if (GameObject.FindGameObjectsWithTag("Bioseguridad").Length > 0 && gameObject.name== "ColliderLavamanosHijo")
        {
            if(!seLavoManos)
            {
                StartCoroutine(inicio.Explicar(Resources.Load<AudioClip>("Audios Voz Real/RecordatorioLavadoManos")));
            }
            else if (inicio.hizoClick)
            {
                inicio.hizoClick = false;
            }
            else
            {
                StartCoroutine(inicio.Explicar(Resources.Load<AudioClip>("Audios Voz Real/RecordatorioBioseguridad")));
            }
        }
    }
}
