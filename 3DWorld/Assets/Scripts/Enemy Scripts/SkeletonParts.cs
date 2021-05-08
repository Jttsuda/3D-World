using UnityEngine;

public class SkeletonParts : MonoBehaviour
{
    public SkeletonEnemy enemyHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            enemyHealth.currentHealth -= 30f;
        }
    }


}
