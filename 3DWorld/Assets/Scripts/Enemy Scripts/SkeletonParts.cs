using UnityEngine;

public class SkeletonParts : MonoBehaviour
{
    public SkeletonEnemy enemyHealth;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Arrow")
        {
            enemyHealth.currentHealth -= 30f;
            Debug.Log(enemyHealth.currentHealth);
        }
    }


}
