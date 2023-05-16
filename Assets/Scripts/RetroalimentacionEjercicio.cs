using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RetroalimentacionEjercicio : MonoBehaviour
{
    public string timerMinutes;
    public string timerSeconds;
    public string timerSeconds100;

    private float startTime;
    private float stopTime;
    private float timerTime;
    private bool isRunning = false;
    bool entro = false;
    DialogueViewer1 viewer;
    Cronometro cronometro;

    private void Awake()
    {
        viewer = FindObjectOfType<DialogueViewer1>();
        cronometro = FindObjectOfType<Cronometro>();
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

            //Debug.Log(timerMinutes + ":" + timerSeconds);
            GameObject.Find("TiempoSimulacion1").GetComponent<TextMeshProUGUI>().text = timerMinutes + ":" + timerSeconds;
        }

        if(GameObject.Find("CanvasTwine")!=null && 
            GameObject.Find("CanvasTwine").GetComponent<DialogueViewer1>().lastNode.tags!=null && 
            GameObject.Find("CanvasTwine").GetComponent<DialogueViewer1>().lastNode.tags.Contains("END") || 
            cronometro.estaDetenido)
        //if(viewer.lastNode.tags != null && viewer.lastNode.tags.Contains("END"))
        {
            TimerStop();        
        }

    }


}
