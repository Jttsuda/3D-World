using UnityEngine;

public class SkeletonEnemy : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    Animator animator;


    //Skeleton Physics
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundDistance = 0.4f;
    [SerializeField]
    private LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Free Fall: y = 1/2g * t^2
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


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
