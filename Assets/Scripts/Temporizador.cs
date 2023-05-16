using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Temporizador : MonoBehaviour
{
    public TextMeshProUGUI texto;
    public float timeRemaining = 5;

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            texto.text = ""+ Mathf.FloorToInt(timeRemaining % 60);
        }
    }
}