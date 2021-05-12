using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public BowScript bowScript;
    public Rigidbody rb;
    public BoxCollider coll;
    private Transform player, equippedContainer, Cam;
    public float pickUpRange;
    public bool equipped;
    public static bool slotFull;


    //NEW CODE
    private Inventory inventory;
    public GameObject itemButton;


    private void Start()
    {
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

        player = GameObject.FindGameObjectWithTag("Player").transform;
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        equippedContainer = GameObject.FindGameObjectWithTag("LeftHandEquipped").transform;
        Cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }


    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;

        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetButtonDown("Interact")) PickUp();
        if (equipped && Input.GetButtonDown("Drop")) Drop();
    }


    private void PickUp()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.slots[i].transform, false);

                // Player Not Currently Holding Anything
                if (!slotFull)
                {
                    equipped = true;
                    slotFull = true;
                    transform.SetParent(equippedContainer);
                    transform.localPosition = Vector3.zero;

                    //transform.localRotation = Quaternion.Euler(Vector3.zero);
                    transform.localRotation = Quaternion.Euler(new Vector3(4.36f, -11.74f, 2.8f));
                    rb.isKinematic = true;
                    coll.isTrigger = true;
                    bowScript.enabled = true;
                }
                else if (slotFull)
                {
                    Destroy(gameObject);
                }

                break;
            }
        }

    }


    private void Drop()
    {
        equipped = false;
        slotFull = false;
        transform.SetParent(null);
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(Cam.forward * 2, ForceMode.Impulse);
        rb.AddForce(Cam.up * 2, ForceMode.Impulse);

        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        bowScript.enabled = false;
    }
}
