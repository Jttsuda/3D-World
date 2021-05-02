using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    public Camera cam;
    public GameObject player;

    //public KeyCode fireButton;

    public Transform spawn;
    public Rigidbody arrowObj;

    //TEST - Range is Optional
    public float damage = 10f;
    public float range = 100f;


    //draw, release, cancel
    void Update()
    {

        if (Input.GetButtonDown("Fire1") && Input.GetButton("Aim"))
        {

            Shoot();

            //release
            //Rigidbody go = Instantiate(arrowObj, spawn.position, Quaternion.identity) as Rigidbody;
            //go.velocity = transform.forward * -160f;



            //go.velocity = player.transform.forward * 100f;
            //go.velocity = cam.transform.forward * 60f;
            //Rigidbody arrow = Instantiate(arrowObj, spawn.position, Quaternion.identity) as Rigidbody;
            //arrow.AddForce(spawn.forward * _charge, ForceMode.Impulse);
            //_charge = 0;

        }


    }

    void Shoot()
    {

        RaycastHit hit;
        if (Physics.Raycast(spawn.position, cam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);
            //Debug.Log(hit.transform.position);

        }
        Rigidbody go = Instantiate(arrowObj, spawn.position, Quaternion.identity) as Rigidbody;
        go.velocity = cam.transform.forward * 150f;

    }



}
