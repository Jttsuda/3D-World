using UnityEngine;

public class SkeletonEnemy : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            animator.SetTrigger("Die");
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                Invoke(nameof(DestroyObject), 1f);

            }

        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }


}
