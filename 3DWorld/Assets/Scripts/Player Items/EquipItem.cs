using UnityEngine;

public class EquipItem : MonoBehaviour
{
    public GameObject item;
    public LeftHandContainer leftHandFull;
    public GameObject equippedContainer;

    private void Start()
    {
        equippedContainer = GameObject.FindGameObjectWithTag("LeftHandEquipped");
        leftHandFull = equippedContainer.GetComponent<LeftHandContainer>();
    }


    public void Equip()
    {
        if (!leftHandFull.LeftHandFull)
        {
            leftHandFull.LeftHandFull = true;

            GameObject equippedItem = Instantiate(item, equippedContainer.transform.position, Quaternion.identity);
            equippedItem.transform.SetParent(equippedContainer.transform);
            equippedItem.transform.localPosition = Vector3.zero;
            equippedItem.transform.localRotation = Quaternion.Euler(new Vector3(4.36f, -11.74f, 2.8f));

            Rigidbody rb = equippedItem.transform.GetComponent<Rigidbody>();
            BoxCollider coll = equippedItem.transform.GetComponent<BoxCollider>();

            rb.isKinematic = true;
            coll.isTrigger = true;

            PickUpController heldItem = equippedItem.transform.GetComponent<PickUpController>();
            heldItem.equipped = true;

        }


    }
}
