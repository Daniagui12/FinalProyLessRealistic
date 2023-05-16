using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    // Start is called before the first frame update
    public void ReiniciarSimulacion()
    {
        SceneManager.LoadScene("Hospital");   
    }
}
