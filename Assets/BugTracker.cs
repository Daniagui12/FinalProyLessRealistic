using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

public class BugTracker : MonoBehaviour
{
    public GameObject marcador;
    public GameObject cuadroTexto;
    public static int contador=0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            contador++;
            Caller();
            print("SE ESTA REPORTANDO UN BUG");
            Instantiate(marcador, GameObject.FindGameObjectWithTag("Player").transform.TransformPoint(Vector3.forward), marcador.transform.rotation);
            Instantiate(cuadroTexto, GameObject.FindGameObjectWithTag("Player").transform.TransformPoint(Vector3.forward *6), cuadroTexto.transform.rotation);

        }
    }

    public void Caller()
    {
        
        string imagePath =  Application.dataPath + "/Resources/BugTracker/BUG-N"+contador+".png";
        StartCoroutine(Screenshot(imagePath));            

    }


    IEnumerator Screenshot(string imagePath)
    {
        yield return new WaitForEndOfFrame();
        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);

        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();
        
        byte[] imageBytes = screenImage.EncodeToPNG();

        System.IO.File.WriteAllBytes(imagePath, imageBytes);

    }

    public void ImprimirComentario()
    {
        WriteString(GameObject.FindGameObjectWithTag("input").GetComponent<TMP_InputField>().text);
        print(GameObject.FindGameObjectWithTag("input").GetComponent<TMP_InputField>().text);
        Destroy(GameObject.FindGameObjectWithTag("input").transform.parent.parent.gameObject);
    }

    static void WriteString(string texto)
    {
        string path = "Assets/Resources/BugTracker/test.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(contador + ". COMENTARIO: " + texto + " EN EL MOMENTO: " + IdentificarMomentoSimulacion());
        writer.Close();

        //Re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset(path);
        TextAsset asset = Resources.Load<TextAsset>("BugTracker/test");

        //Print the text from the file
        Debug.Log(asset.text);
    }

    static void ReadString()
    {
        string path = "Assets/Resources/test.txt";

        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

    public static string IdentificarMomentoSimulacion()
    {
        if (GameObject.Find("Particle System") != null) return "Pasillo";
        else if (GameObject.Find("Particle System") == null && GameObject.Find("Preguntas") == null && GameObject.Find("ColliderLavamanos") != null) return "Antes de preguntas";
        else if (GameObject.Find("Preguntas") != null) return "Preguntas";
        else if (GameObject.Find("ColliderLavamanosHijo") != null) return "Lavada de manos";
        else if (GameObject.Find("ColliderLavamanosHijo") == null) return "Elección de bioseguridad";
        else if (GameObject.Find("CanvasTwine") != null) return FindObjectOfType<DialogueViewer1>().lastNodeTitle;
        else return "Eleccion del equipo";
    }
}
