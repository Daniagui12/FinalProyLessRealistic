using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRoot : MonoBehaviour
{

    [SerializeField]
    GameObject parentObject;

    [SerializeField]
    bool spawn, snapFirst, snapLast;

    // Update is called once per frame
    void Update()
    {

        if (spawn)
        {
            Spawn();

            spawn = false;
        }



    }


    public void Spawn()
    {
        
        for (int x = 0; x < 5; x++)
        {

            GameObject tmp;

            tmp = parentObject.transform.GetChild(x).transform.gameObject;

            if (x == 0)
            {
                Destroy(tmp.GetComponent<CharacterJoint>());
                if (snapFirst)
                {
                    tmp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }
            else
            {
                tmp.GetComponent<CharacterJoint>().connectedBody = parentObject.transform.Find((parentObject.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
            }

            if (snapLast)
            {
                parentObject.transform.Find((parentObject.transform.childCount).ToString()).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}