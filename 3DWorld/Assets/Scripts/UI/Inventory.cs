using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;

    // Switching Weapons
    private Transform LeftEquippedContainer;
    private Transform RightEquippedContainer;
    private bool switchWeaponPressed = false;
    private int nextWeapon;

    // Implement This
    public bool HandsFull = false;


    private void Start()
    {
        LeftEquippedContainer = GameObject.FindGameObjectWithTag("LeftHandEquipped").transform;
        RightEquippedContainer = GameObject.FindGameObjectWithTag("RightHandEquipped").transform;
    }


    private void Update()
    {
        // Switching Equipped Item
        if (Input.GetAxis("D-Pad-X") != 0 && !switchWeaponPressed) SwitchEquipped();
        else if (Input.GetAxis("D-Pad-X") == 0) switchWeaponPressed = false;
    }


    private void SwitchEquipped()
    {
        switchWeaponPressed = true;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount > 0)
            {
                GameObject itemInSlot = slots[i].GetComponentInChildren<EquipItem>().item;
                bool leftHandMatch = LeftEquippedContainer.childCount > 0 && itemInSlot.CompareTag(LeftEquippedContainer.GetChild(0).tag);
                bool rightHandMatch = RightEquippedContainer.childCount > 0 && itemInSlot.CompareTag(RightEquippedContainer.GetChild(0).tag);
                if (leftHandMatch || rightHandMatch)
                {
                    if (Input.GetAxis("D-Pad-X") == -1) nextWeapon = i - 1;
                    else if (Input.GetAxis("D-Pad-X") == 1) nextWeapon = i + 1;
                    break;
                }
                else if (LeftEquippedContainer.childCount == 0 && RightEquippedContainer.childCount == 0)
                {
                    nextWeapon = i;
                    break;
                }

            }
        }


        Debug.Log(nextWeapon);

        // Check if "nextWeapon" Index exists
        if (nextWeapon >= 0 && nextWeapon < slots.Length && slots[nextWeapon].transform.childCount > 0)
        {
            foreach (Transform child in LeftEquippedContainer) Destroy(child.gameObject);
            foreach (Transform child in RightEquippedContainer) Destroy(child.gameObject);

            // Get nextWeapon GameObject and Equip
            EquipItem newWeapon = slots[nextWeapon].GetComponentInChildren<EquipItem>();
            Transform equippedItem = Instantiate(newWeapon.item, LeftEquippedContainer.position, Quaternion.identity).transform;


            // Place IF-Statement here to check what Item is being equipped
            if (equippedItem.CompareTag("OldBow"))
            {
                equippedItem.SetParent(LeftEquippedContainer);
                equippedItem.localPosition = Vector3.zero;
                equippedItem.localRotation = Quaternion.Euler(new Vector3(4.36f, -11.74f, 2.8f));

            }
            else if (equippedItem.CompareTag("OldAxe"))
            {
                equippedItem.SetParent(RightEquippedContainer);
                equippedItem.localPosition = Vector3.zero;
                equippedItem.localRotation = Quaternion.Euler(new Vector3(14.5f, 298f, 268.5f));

            }

            equippedItem.GetComponent<Rigidbody>().isKinematic = true;
            equippedItem.GetComponent<BoxCollider>().isTrigger = true;


            // Marking Item as Equipped in "PickUpController" so that you can't pick up the item you're currently holding
            PickUpController heldItem = equippedItem.GetComponent<PickUpController>();
            heldItem.equipped = true;


        }


    }
}
