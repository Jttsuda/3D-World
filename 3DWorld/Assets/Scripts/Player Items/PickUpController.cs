using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public BowScript bowScript;
    public Rigidbody rb;
    public BoxCollider coll;
    private Transform player, equippedContainer;
    public float pickUpRange;
    public bool equipped;

    private Inventory inventory;
    public GameObject itemButton;
    public LeftHandContainer leftHandFull;

    private void Start()
    {
        if (!equipped)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
            if (bowScript != null)
                bowScript.enabled = false;
        }
        if (equipped)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            if (bowScript != null)
                bowScript.enabled = true;
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        equippedContainer = GameObject.FindGameObjectWithTag("LeftHandEquipped").transform;
        leftHandFull = equippedContainer.GetComponent<LeftHandContainer>();
    }

    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetButtonDown("Interact")) PickUp();
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
                if (!leftHandFull.LeftHandFull)
                {
                    equipped = true;
                    leftHandFull.LeftHandFull = true;
                    transform.SetParent(equippedContainer);
                    transform.localPosition = Vector3.zero;

                    //transform.localRotation = Quaternion.Euler(Vector3.zero);
                    transform.localRotation = Quaternion.Euler(new Vector3(4.36f, -11.74f, 2.8f));
                    rb.isKinematic = true;
                    coll.isTrigger = true;

                    if (bowScript != null)
                    {
                        bowScript.enabled = true;

                    }
                }
                else if (leftHandFull.LeftHandFull)
                {
                    Destroy(gameObject);
                }

                break;
            }
        }

    }


}
