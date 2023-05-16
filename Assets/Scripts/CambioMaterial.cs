using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CambioMaterial : MonoBehaviour
{
    private JsonObject objetos;

    public List<string> objetosRequeridos;
    public List<string> objetosSeleccionados;

    public Material bien;
    public Material mal;
    public Material blanco;
    public Material seleccionado;

    public int cantidadAciertos;
    public int cantidadFaltas;
    public int cantidadFallos;

    public TextMeshProUGUI aciertos;
    public TextMeshProUGUI fallos;

    Audio scriptAudio;

    private void Awake()
    {
        scriptAudio = FindObjectOfType<Audio>();
    }

    // Start is called before the first frame update
    public void CargarArchivoJsonEquipos()
    {
        if(scriptAudio.jsonFile!= null)
        {
            objetos = JsonUtility.FromJson<JsonObject>(scriptAudio.jsonFile.text);
            objetosRequeridos = new List<string>();

            for (int i = 0; i < objetos.objetos.Length; i++)
            {
                objetosRequeridos.Add(objetos.objetos[i].nombre);
            }

            objetosSeleccionados = new List<string>();
        }        

        cantidadAciertos = 0;
        cantidadFallos = 0;
        cantidadFaltas = 0;

    }

    // Update is called once per frame
    public IEnumerator EsperarEleccion()
    {
        if (objetosSeleccionados.Count > 0)
        {
            for (int i = 0; i < objetosSeleccionados.Count; i++)
            {
                if (objetosRequeridos.Contains(objetosSeleccionados[i].ToString()))
                {
                    cantidadAciertos++;
                    GameObject padre = GameObject.Find(objetosSeleccionados[i].ToString());
                    int cantHijos = padre.gameObject.transform.childCount;
                    for (int j = 0; j < cantHijos; j++)
                    {
                        if (padre.gameObject.transform.GetChild(j).name == "Outline")
                        {
                            if(padre.gameObject.transform.GetChild(j).GetComponent<MeshRenderer>()!=null) padre.gameObject.transform.GetChild(j).GetComponent<MeshRenderer>().material = bien;
                            else
                            {
                                Material[] mats = padre.gameObject.transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().materials;
                                mats[0] = bien;
                                padre.gameObject.transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().materials = mats;
                            }
                        }
                    }


                }
                else
                {
                    cantidadFallos++;
                    GameObject padre = GameObject.Find(objetosSeleccionados[i].ToString());
                    int cantHijos = padre.gameObject.transform.childCount;
                    for (int j = 0; j < cantHijos; j++)
                    {
                        if (padre.gameObject.transform.GetChild(j).name == "Outline")
                        {
                            if (padre.gameObject.transform.GetChild(j).GetComponent<MeshRenderer>() != null) padre.gameObject.transform.GetChild(j).GetComponent<MeshRenderer>().material = blanco;
                            else
                            {
                                Material[] mats = padre.gameObject.transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().materials;
                                mats[0] = bien;
                                padre.gameObject.transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().materials = mats;
                            }
                        }
                    }
                }


            }
        }

        for (int i = 0; i < objetosRequeridos.Count; i++)
        {
            if (!objetosSeleccionados.Contains(objetosRequeridos[i].ToString()))
            {
                cantidadFaltas++;
                GameObject padre = GameObject.Find(objetosRequeridos[i].ToString());
                int cantHijos = padre.gameObject.transform.childCount;
                for (int j = 0; j < cantHijos; j++)
                {
                    if (padre.gameObject.transform.GetChild(j).name == "Outline")
                    {
                        if (padre.gameObject.transform.GetChild(j).GetComponent<MeshRenderer>() != null) padre.gameObject.transform.GetChild(j).GetComponent<MeshRenderer>().material = bien;
                        else
                        {
                            Material[] mats = padre.gameObject.transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().materials;
                            mats[0] = bien;
                            padre.gameObject.transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().materials = mats;
                        }
                    }
                }


            }
        }

        aciertos.text = cantidadAciertos.ToString();
        fallos.text = cantidadFallos.ToString();

        yield return "listo";
    }

    public IEnumerator EliminarNoNecesarios()
    {
        GameObject[] lista = GameObject.FindGameObjectsWithTag("ActividadEquipos");
        foreach (var gObj in lista)
        {
            if (!objetosRequeridos.Contains(gObj.name))
            {
                int cantHijos = gObj.transform.childCount;
                for (int j = 0; j < cantHijos; j++)
                {
                    if (gObj.transform.GetChild(j).name == "Outline")
                    {
                        if (gObj.transform.GetChild(j).GetComponent<MeshRenderer>() != null) gObj.transform.GetChild(j).GetComponent<MeshRenderer>().material = scriptAudio.incorrectoMat;
                        else
                        {
                            Material[] mats = gObj.transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().materials;
                            mats[0] = scriptAudio.incorrectoMat;
                            gObj.transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().materials = mats;
                        }
                    }
                }
            }
            else
            {
                int cantHijos = gObj.transform.childCount;
                for (int j = 0; j < cantHijos; j++)
                {
                    if (gObj.transform.GetChild(j).name == "Outline")
                    {
                        Destroy(gObj.transform.GetChild(j).gameObject);
                    }
                }
            }
        }
        
     yield return "listo";
    }

}
