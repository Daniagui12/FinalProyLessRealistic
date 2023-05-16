using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionDialNeopuff : MonoBehaviour
{
    bool yaPaso = false;
    DialogueViewer1 viewer;
    private float tiempoNecesario = 1.5f;
    float transcurrido;
    bool usarValoresRealesPICO = false;
    int contador = 0;

    bool debeCambiarSegunCantidad = false;
    int cantidad = 0;

    private void Awake()
    {
        viewer = FindObjectOfType<DialogueViewer1>();
    }

    private void Update()
    {
        if (viewer.lastNodeTitle == "Paso PICO" || viewer.lastNodeTitle == "Paso PICO inicial")
        {
            print("ESTA ACA 1");
            gameObject.GetComponent<BoxCollider>().enabled = true;

            if (viewer.lastNodeTitle == "Paso PICO")
            {
                if (!usarValoresRealesPICO) yaPaso = false;
                usarValoresRealesPICO = true;
            }
            else
            {
                usarValoresRealesPICO = false;
            }
        }
        else if(viewer.lastNodeTitle == "Paso preparar blender" || viewer.lastNodeTitle == "Realizar acciones de tutorial")
        {
            print("ESTA ACA 2");
            gameObject.GetComponent<BoxCollider>().enabled = true;

            usarValoresRealesPICO = true;
            yaPaso = true;
            contador = 1;
        }

        if (viewer.lastNodeTitle == "Paso hacer tutorial VPP con PICO")
        {
            debeCambiarSegunCantidad = true;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "coll_hands:b_l_index3" || other.gameObject.name == "hands_coll:b_r_index3")
        {
            if (usarValoresRealesPICO) GameObject.Find("Disp en T Neopuff").GetComponent<Animator>().SetTrigger("PICOSube");
            else
            {
                if (contador == 0)
                {
                    GameObject.Find("Disp en T Neopuff").GetComponent<Animator>().SetTrigger("PICOMal");
                    contador++;
                }
                else GameObject.Find("Disp en T Neopuff").GetComponent<Animator>().SetTrigger("PICOSube");
            }

            if (debeCambiarSegunCantidad)
            {
                cantidad++;
                if (cantidad == 2)
                {
                    cantidad++;
                    debeCambiarSegunCantidad = false;
                    viewer.ChangeNode();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "coll_hands:b_l_index3" || other.gameObject.name == "hands_coll:b_r_index3")
        {
            GameObject.Find("Disp en T Neopuff").GetComponent<Animator>().SetTrigger("PICOBaja");

        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "coll_hands:b_l_index3" || other.gameObject.name == "hands_coll:b_r_index3")
        {
            transcurrido += Time.fixedDeltaTime;
            if (transcurrido > tiempoNecesario && !yaPaso)
            {
                yaPaso = true;
                viewer.ChangeNode();
            }

        }

    }


}
