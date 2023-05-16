using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public MoveCharWithAnimation doctor;

    // Update is called once per frame
    void Start()
    {
        doctor.StartMove();
    }
}
