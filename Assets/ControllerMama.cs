using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerMama : MonoBehaviour
{
    public DialogueViewer1 viewer;
    bool pasoFinal = false;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.name == "baby") && pasoFinal)
        {
            Destroy(GameObject.Find("Baby").GetComponent<OVRGrabbable>());
            Destroy(GameObject.Find("Baby").GetComponent<Rigidbody>());
            Destroy(GameObject.Find("Baby").GetComponent<BoxCollider>());

            GameObject.Find("Baby").transform.localPosition = new Vector3(-12.68f, 7.606f, 20.983f);
            GameObject.Find("Baby").transform.localRotation = Quaternion.Euler(-204.575f, 179.946f, -173.979f);
            other.gameObject.GetComponent<ControllerBaby>().ActivarTriggerAnimacion("AbajoRespirando");
            viewer.ChangeNode();
            if (other.gameObject.GetComponent<DragObject>() != null && SceneManager.GetActiveScene().name!="Hospital")
            {
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            }
        }
        else if(GameObject.Find("CanvasTwine")!=null && (other.gameObject.name == "baby" && (GameObject.Find("CanvasTwine").GetComponent<DialogueViewer1>().lastNodeTitle == "Trasladar al bebé con madre")))
        {
            Destroy(GameObject.Find("Baby").GetComponent<OVRGrabbable>());
            Destroy(GameObject.Find("Baby").GetComponent<Rigidbody>());
            Destroy(GameObject.Find("Baby").GetComponent<BoxCollider>());
            other.gameObject.GetComponent<ControllerBaby>().Reescalar111();
            other.gameObject.GetComponent<ControllerBaby>().ActivarTriggerAnimacion("Amamantar");
            GameObject.Find("B20_Ch_01_Avatar").GetComponent<Animator>().SetTrigger("Amamantar");

            GameObject.Find("Baby").transform.localPosition = new Vector3(-12.6092f, 7.6316f, 21.0193f);
            GameObject.Find("Baby").transform.localRotation = Quaternion.Euler(-199.268f, 180.721f, -173.357f);
            viewer.ChangeNode();
            if (other.gameObject.GetComponent<DragObject>() != null && SceneManager.GetActiveScene().name != "Hospital")
            {
                other.gameObject.transform.localRotation = Quaternion.Euler(-90f, 90f, 0f);
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }

    }


    public void cambiarAPasoFinal()
    {
        if (pasoFinal) pasoFinal = false;
        else pasoFinal = true;
    }
}
