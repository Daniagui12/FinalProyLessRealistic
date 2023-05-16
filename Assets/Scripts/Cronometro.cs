using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Cronometro : MonoBehaviour
{
    public TextMesh time;
    public string timerMinutes;
    public string timerSeconds;
    public string timerSeconds100;

    private float startTime;
    private float stopTime;
    private float timerTime;
    private bool isRunning = false;
    bool entro = false;

    ControllerTwine controller;
    DialogueViewer1 viewer;

    public bool estaDetenido = false;

    void Awake()
    {
        controller = FindObjectOfType<ControllerTwine>();
        viewer = FindObjectOfType<DialogueViewer1>();
    }

    // Use this for initialization
    void Start()
    {
        TimerStart();
    }

    public void TimerStart()
    {
        if (!isRunning)
        {
            print("START");
            isRunning = true;
            startTime = Time.time;
        }
    }

    public void TimerStop()
    {
        if (isRunning)
        {
            print("STOP");
            isRunning = false;
            stopTime = timerTime;
            
        }
    }

    public void TimerReset()
    {
        print("RESET");
        stopTime = 0;
        isRunning = false;
        timerMinutes = timerSeconds = timerSeconds100 = "00";
    }


    // Update is called once per frame
    void Update()
    {
        timerTime = stopTime + (Time.time - startTime);
        int minutesInt = (int)timerTime / 60;
        int secondsInt = (int)timerTime % 60;
        int seconds100Int = (int)(Mathf.Floor((timerTime - (secondsInt + minutesInt * 60)) * 100));

        if (isRunning)
        {
            timerMinutes = (minutesInt < 10) ? "0" + minutesInt : minutesInt.ToString();
            timerSeconds = (secondsInt < 10) ? "0" + secondsInt : secondsInt.ToString();
            timerSeconds100 = (seconds100Int < 10) ? "0" + seconds100Int : seconds100Int.ToString();

            time.text = timerMinutes +":"+ timerSeconds;
        }

        if (!controller.esCasoSinAyuda)
        {
            if (!controller.esCasoReanimacion)
            {
                if (secondsInt == 14 && minutesInt == 0)
                {
                    if (!controller.casoBase) controller.CambiarColorSemaforo("amarillo");
                }
                else if (secondsInt == 15 && minutesInt == 0 && !entro)
                {
                    if (!controller.casoBase)
                    {
                        controller.CambiarColorSemaforo("rojo");
                        TimerStop();
                    }
                    entro = true;
                    viewer.ChangeNode();
                }



                if (time.text == "00:29")
                {
                    controller.CambiarColorSemaforo("amarillo");
                    entro = false;
                }
                else if (time.text == "00:30" && !entro)
                {
                    controller.CambiarColorSemaforo("rojo");
                    TimerStop();
                    entro = true;
                    viewer.ChangeNode();
                }
            }
            else
            {
                if (secondsInt == 20 && minutesInt == 0 && !entro)
                {
                    entro = true;
                    viewer.ChangeNode();
                }

                if (time.text == "00:29")
                {
                    controller.CambiarColorSemaforo("amarillo");
                    entro = false;
                }

                else if (time.text == "00:30" && !entro)
                {
                    //SOLO QUIERO DETENER EL CRONOMETRO ACÁ
                    controller.CambiarColorSemaforo("rojo");
                    TimerStop();
                    entro = true;
                }

                if (!controller.casoQuejido)
                {
                    if (time.text == "00:34")
                    {
                        controller.CambiarColorSemaforo("amarillo");
                        entro = false;
                    }
                    else if (time.text == "00:35" && !entro)
                    {
                        controller.CambiarColorSemaforo("rojo");
                        TimerStop();
                        entro = true;
                        viewer.ChangeNode();
                    }
                }
            }


            if (controller.casoBase || controller.casoBaseMejoraTrasAspirar || controller.casoBase2)
            {
                if (time.text == "00:44" && minutesInt == 0)
                {
                    controller.CambiarColorSemaforo("amarillo");
                    entro = false;
                }
                if (time.text == "00:45" && minutesInt == 0 && !entro)
                {
                    controller.CambiarColorSemaforo("rojo");
                    TimerStop();
                    entro = true;
                    viewer.ChangeNode();
                }
                if (time.text == "00:59")
                {
                    entro = false;
                }
                else if (time.text == "01:00" && !entro)
                {
                    entro = true;
                    viewer.LoadResponses();
                    GameObject.Find("FlechaPinzamiento").transform.GetChild(0).gameObject.SetActive(true);
                }

                else if (time.text == "02:14" && !controller.seCortoCordon)
                {
                    controller.CambiarColorSemaforo("amarillo");
                    entro = false;
                }
                else if (time.text == "02:15" && !entro && !controller.seCortoCordon)
                {
                    controller.CambiarColorSemaforo("rojo");
                    TimerStop();
                    entro = true;
                    controller.EmpezarCronometro();
                    viewer.DestroyResponses();
                }
                else if (time.text == "02:16" && !controller.seCortoCordon)
                {
                    viewer.LoadResponses();
                }
                else if (time.text == "03:04" && !controller.seCortoCordon)
                {
                    controller.CambiarColorSemaforo("amarillo");
                    entro = false;
                }
                else if (time.text == "03:05" && !entro && !controller.seCortoCordon)
                {
                    controller.CambiarColorSemaforo("rojo");
                    TimerStop();
                    entro = true;
                    controller.EmpezarCronometro();
                    viewer.DestroyResponses();
                }
                else if (time.text == "03:06" && !controller.seCortoCordon)
                {
                    viewer.LoadResponses();
                }
            }
            else
            {
                if (!controller.esCasoReanimacion)
                {
                    if (time.text == "00:59")
                    {
                        controller.CambiarColorSemaforo("amarillo");
                        entro = false;
                    }
                    else if (time.text == "01:00" && !entro)
                    {
                        controller.CambiarColorSemaforo("rojo");
                        TimerStop();
                        entro = true;
                        viewer.ChangeNode();
                    }
                }
            }
        }
        else
        {
            if (!controller.esCasoReanimacion)
            {
                if (secondsInt == 15 && minutesInt == 0 && !entro)
                {
                    entro = true;
                    viewer.ChangeNode();
                }



                if (time.text == "00:29")
                {
                    entro = false;
                }
                else if (time.text == "00:30" && !entro)
                {
                    entro = true;
                    viewer.ChangeNode();
                }
            }
            else
            {
                if (secondsInt == 20 && minutesInt == 0 && !entro)
                {
                    entro = true;
                    viewer.ChangeNode();
                }

                if (time.text == "00:29")
                {
                    entro = false;
                }

                else if (time.text == "00:30" && !entro)
                {
                    entro = true;
                }

                if (!controller.casoQuejido)
                {
                    if (time.text == "00:34")
                    {
                        entro = false;
                    }
                    else if (time.text == "00:35" && !entro)
                    {
                        entro = true;
                        viewer.ChangeNode();
                    }
                }
            }


            if (controller.casoBase || controller.casoBaseMejoraTrasAspirar || controller.casoBase2)
            {
                if (time.text == "00:44" && minutesInt == 0)
                {
                    entro = false;
                }
                if (time.text == "00:45" && minutesInt == 0 && !entro && viewer.lastNodeTitle== "Paso Gorro")
                {
                    viewer.ChangeNode();
                }
                if (time.text == "00:59")
                {
                    entro = false;
                }

                else if (minutesInt >0 && minutesInt <4 && !entro && viewer.lastNodeTitle == "Paso Gorro")
                {
                    entro = true;
                    viewer.ChangeNode();
                }

                else if (time.text == "01:00" && !entro && viewer.lastNodeTitle == "Ordenar pinzamiento habitual del cordón umbilical")
                {
                    entro = true;
                    viewer.LoadResponses();
                    GameObject.Find("FlechaPinzamiento").transform.GetChild(0).gameObject.SetActive(true);
                }

                else if (time.text == "02:14" && !controller.seCortoCordon && viewer.lastNodeTitle == "Ordenar pinzamiento habitual del cordón umbilical")
                {
                    entro = false;
                }
                else if (time.text == "02:15" && !entro && !controller.seCortoCordon && viewer.lastNodeTitle == "Ordenar pinzamiento habitual del cordón umbilical")
                {
                    entro = true;
                }
                else if (time.text == "02:16" && !controller.seCortoCordon && viewer.lastNodeTitle == "Ordenar pinzamiento habitual del cordón umbilical")
                {
                    viewer.LoadResponses();
                }
                else if (time.text == "03:04" && !controller.seCortoCordon && viewer.lastNodeTitle == "Ordenar pinzamiento habitual del cordón umbilical")
                {
                    entro = false;
                }
                else if (time.text == "03:05" && !entro && !controller.seCortoCordon && viewer.lastNodeTitle == "Ordenar pinzamiento habitual del cordón umbilical")
                {
                    entro = true;
                }
                else if (time.text == "03:06" && !controller.seCortoCordon && viewer.lastNodeTitle == "Ordenar pinzamiento habitual del cordón umbilical")
                {
                    viewer.LoadResponses();
                }

            }
        }

        if(controller.casoQuejido)
        {
            /**
            if (time.text == "00:59")
            {
                entro = false;
            }
            if (time.text == "01:00" && !entro)
            {
                viewer.LoadResponses();
                entro = true;
                GameObject.Find("FlechaPinzamiento").transform.GetChild(0).gameObject.SetActive(true);
            }

            else if (time.text == "01:14" && !controller.seCortoCordon)
            {
                controller.CambiarColorSemaforo("amarillo");
                entro = false;
            }
            else if (time.text == "01:15" && !entro && !controller.seCortoCordon)
            {
                controller.ExplicarOcultandoRespuestas("DialogoDemoraCortaCordon");
                controller.CambiarColorSemaforo("rojo");
                TimerStop();
                entro = true;
            }
            **/
        }
        

        if (viewer.lastNode.tags!= null && viewer.lastNode.tags.Contains("END"))
        {
            TimerStop();
            controller.CambiarColorSemaforo("rojo");
        }


    }
}
