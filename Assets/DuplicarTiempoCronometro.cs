using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicarTiempoCronometro : MonoBehaviour
{
    public TextMesh text;
    public TextMesh cronometro;

    void Update()
    {
        text.text = cronometro.text;   
    }
}
