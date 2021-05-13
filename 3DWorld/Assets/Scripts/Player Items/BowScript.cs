using UnityEngine;
using UnityEngine.UI;

public class BowScript : MonoBehaviour
{
    private Camera cam;
    public Transform spawn;
    public Rigidbody arrowObj;

    // Range is Optional
    private readonly float range = 100f;
    private RectTransform crosshair;
    private Image uiDot;
    private SimpleCrosshair script;

    private int gapIncrement = 40;


    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        crosshair = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<RectTransform>();
        uiDot = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Image>();
        script = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<SimpleCrosshair>();
    }


    void Update()
    {

        if (Input.GetAxis("Aim") == 1)
        {
            uiDot.enabled = true;
            if (Physics.Raycast(spawn.position, cam.transform.forward, out RaycastHit hit, range))
            {
                if(!hit.transform.root.CompareTag("Arrow"))
                {
                    crosshair.position = cam.WorldToScreenPoint(hit.point);
                    if (gapIncrement > 5)
                    {
                        gapIncrement -= 35;
                        script.SetGap(gapIncrement, true);
                    }
                }
            }
            else
            {
                crosshair.position = new Vector3(Screen.width * 0.5f - 7, Screen.height * 0.5f - 7, 0);
                if (gapIncrement < 40)
                {
                    gapIncrement += 35;
                    script.SetGap(gapIncrement, true);

                }
            }
        }
        else if (!Input.GetButton("Aim") && uiDot.enabled == true)
        {
            uiDot.enabled = false;
        }

        if (Input.GetButtonDown("Fire1") && Input.GetAxis("Aim") == 1)
            Shoot();

    }

    void Shoot()
    {
        Rigidbody go = Instantiate(arrowObj, spawn.position, Quaternion.identity);
        go.velocity = cam.transform.forward * 40f;
    }


}
