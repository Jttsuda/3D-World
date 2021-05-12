using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject item;
    private Transform player;
    public GameObject equippedContainer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        equippedContainer = GameObject.FindGameObjectWithTag("LeftHandEquipped");
    }


    public void Drop()
    {

        // Check if Player is Holding Item

        foreach (Transform child in equippedContainer.transform)
        {
            if (item.CompareTag(equippedContainer.transform.GetChild(0).tag))
            {
                Destroy(child.gameObject);
            }
        }


        Instantiate(item, player.position+(player.forward*2), Quaternion.identity);

    }

}
