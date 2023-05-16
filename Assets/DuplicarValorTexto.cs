using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DuplicarValorTexto : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI valorACopiar;

    void Update()
    {
        text.text = valorACopiar.text;
    }
}
