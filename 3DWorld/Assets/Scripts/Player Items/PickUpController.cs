using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public BowScript bowScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, equippedContainer, Cam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;


    private void Start()
    {
        //Setup
        if (!equipped)
        {
            bowScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            bowScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //Check if Player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetButtonDown("Interact") && !slotFull) PickUp();

        //Drop if equipped and "Q" is pressed
        if (equipped && Input.GetButtonDown("Drop")) Drop();
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //Make weapon a child of the BowContainer and move it to default position
        transform.SetParent(equippedContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make RigidBody Kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //Enable Script
        bowScript.enabled = true;
    }
    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //Make RigidBody not Kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
        rb.AddForce(Cam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(Cam.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //Disable Script
        bowScript.enabled = false;
    }
}