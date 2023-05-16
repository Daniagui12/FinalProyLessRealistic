using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToallaFisicas : MonoBehaviour
{
    Vector3 vector;
    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            vector= gameObject.GetComponent<Rigidbody>().worldCenterOfMass;
            if(vector.y <= 2.0f)
            {
                var test = gameObject.GetComponent<Rigidbody>().constraints;
                test = RigidbodyConstraints.FreezePositionY;
                gameObject.GetComponent<Rigidbody>().constraints = test;
            }
        }
    }
}
