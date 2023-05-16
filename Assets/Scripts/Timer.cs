using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text tiempoText;
    public float segundos = 0.0f;
    public int minutos = 0;
    public int horas = 0;

    public bool LlegoMinuto=false;
    public bool terminar = false;
    DialogueViewer1 viewer;

    private void Awake()
    {
        viewer = FindObjectOfType<DialogueViewer1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!terminar)
        {
            segundos = segundos + 1 * Time.deltaTime;
            int t = int.Parse(segundos.ToString("f0"));

            if (t == 60)
            {
                segundos = 0.0f;

                if (minutos + 1 == 60)
                {
                    minutos = 0;
                    horas++;
                }
                else
                {
                    minutos++;
                }
            }

            tiempoText.text = horas.ToString("00") + ":" + minutos.ToString("00") + ":" + t.ToString("00");
            if (tiempoText.text == "00:01:00")
            {
                LlegoMinuto = true;                
            }

            if (viewer != null)
            {
                if (viewer.lastNodeTitle == "Calentar y secar" && minutos>0)
                {
                    viewer.LoadResponses();
                }
            }

            if (viewer.lastNode.tags.Contains("END"))
            {
                terminar = true;
            }
        }
    } 
}
