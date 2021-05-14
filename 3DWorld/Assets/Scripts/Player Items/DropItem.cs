using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject item;
    private Transform player;
    public GameObject LeftEquippedContainer;
    public GameObject RightEquippedContainer;
    public Inventory handsFull;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        handsFull = player.GetComponent<Inventory>();

        LeftEquippedContainer = GameObject.FindGameObjectWithTag("LeftHandEquipped");
        RightEquippedContainer = GameObject.FindGameObjectWithTag("RightHandEquipped");

    }

    public void Drop()
    {
        // Check if Player is Holding Item
        foreach (Transform child in LeftEquippedContainer.transform)
        {
            if (item.CompareTag(LeftEquippedContainer.transform.GetChild(0).tag))
            {
                Destroy(child.gameObject);
                handsFull.HandsFull = false;
            }
        }
        foreach (Transform child in RightEquippedContainer.transform)
        {
            if (item.CompareTag(RightEquippedContainer.transform.GetChild(0).tag))
            {
                Destroy(child.gameObject);
                handsFull.HandsFull = false;
            }
        }

        Instantiate(item, player.position+(player.forward*2), Quaternion.identity);
    }

}
