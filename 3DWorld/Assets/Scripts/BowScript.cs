using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    public Camera cam;

    //public KeyCode fireButton;

    public Transform spawn;
    public Rigidbody arrowObj;

    //draw, release, cancel
    void Update()
    {

        if (Input.GetButtonDown("Fire1") && Input.GetButton("Aim"))
        {
            //release
            Rigidbody go = Instantiate(arrowObj, spawn.position, Quaternion.identity) as Rigidbody;
            go.velocity = transform.forward * -40f;
            //go.velocity = cam.transform.forward * 40f;
            //Rigidbody arrow = Instantiate(arrowObj, spawn.position, Quaternion.identity) as Rigidbody;
            //arrow.AddForce(spawn.forward * _charge, ForceMode.Impulse);
            //_charge = 0;

        }


    }
}
