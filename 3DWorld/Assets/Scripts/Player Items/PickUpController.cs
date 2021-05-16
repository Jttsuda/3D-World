using UnityEngine;
using UnityEngine.EventSystems;

public class PickUpController : MonoBehaviour
{
    public BowScript bowScript;
    public Rigidbody rb;
    public BoxCollider coll;
    private Transform player, LeftEquippedContainer, RightEquippedContainer;
    public float pickUpRange;
    public bool equipped;

    private Inventory inventory;
    public GameObject itemButton;


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
        LeftEquippedContainer = GameObject.FindGameObjectWithTag("LeftHandEquipped").transform;
        RightEquippedContainer = GameObject.FindGameObjectWithTag("RightHandEquipped").transform;
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
                GameObject equippedIcon = Instantiate(itemButton, inventory.slots[i].transform, false);

                // Check if Currently Holding Item
                if (!inventory.HandsFull)
                {
                    //TEST
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(equippedIcon);

                    equipped = true;
                    inventory.HandsFull = true;
                    rb.isKinematic = true;
                    coll.isTrigger = true;

                    if (transform.CompareTag("OldBow"))
                    {
                        transform.SetParent(LeftEquippedContainer);
                        transform.localRotation = Quaternion.Euler(new Vector3(357.985352f, 346.495453f, 2.36139822f));
                    }
                    else if (transform.CompareTag("OldAxe"))
                    {
                        transform.SetParent(RightEquippedContainer);
                        transform.localRotation = Quaternion.Euler(new Vector3(14.5f, 298f, 268.5f));
                    }
                    else if (transform.CompareTag("OldSword"))
                    {
                        transform.SetParent(RightEquippedContainer);
                        transform.localRotation = Quaternion.Euler(new Vector3(20.23f, 298.23f, 272.13f));
                    }

                    transform.localPosition = Vector3.zero;
                    if (bowScript != null)
                        bowScript.enabled = true;

                }
                else if (inventory.HandsFull)
                    Destroy(gameObject);

                break;
            }
        }

    }


}
