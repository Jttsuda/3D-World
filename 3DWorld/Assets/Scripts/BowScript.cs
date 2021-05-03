using UnityEngine;
using UnityEngine.UI;

public class BowScript : MonoBehaviour
{
    public Camera cam;
    public Transform spawn;
    public Rigidbody arrowObj;

    // Range is Optional
    public float range = 70f;
    public RectTransform crosshair;
    public Image uiDot;


    void Update()
    {

        if (Input.GetButton("Aim"))
        {
            uiDot.enabled = true;
            RaycastHit hit;
            if (Physics.Raycast(spawn.position, cam.transform.forward, out hit, range))
            {
                if(hit.transform.root.tag != "Arrow")
                    crosshair.position = cam.WorldToScreenPoint(hit.point);
            }
            else
            {
                crosshair.position = new Vector3(Screen.width * 0.5f - 7, Screen.height * 0.5f - 7, 0);
            }
        }
        if (Input.GetButtonUp("Aim"))
        {
            uiDot.enabled = false;
        }

        if (Input.GetButtonDown("Fire1") && Input.GetButton("Aim"))
            Shoot();

    }

    void Shoot()
    {
        Rigidbody go = Instantiate(arrowObj, spawn.position, Quaternion.identity) as Rigidbody;
        go.velocity = cam.transform.forward * 40f;
    }


}
