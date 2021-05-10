using UnityEngine;
using UnityEngine.AI;

public class SkeletonEnemy : MonoBehaviour
{
    Vector3 spawnLocation;
    Animator animator;
    public Transform player;
    public float maxHealth = 20f;
    public float currentHealth;
    public float agroRange = 20f;
    public float combatRange = 2f;
    int isWalkingHash;


    //Patrolling
    public NavMeshAgent agent;
    public LayerMask groundLayer;
    public Vector3 walkPoint;
    public float walkPointRange = 10f;
    public float rotationSpeed = 3f;
    bool walkPointSet;
    float randomZ;
    float randomX;


    private void Awake()
    {
        player = GameObject.Find("Third Person Player").transform;
        spawnLocation = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Skeleton Death
        if (currentHealth <= 0)
        {
            animator.SetBool(isWalkingHash, false);
            animator.SetTrigger("Die");
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
                Invoke(nameof(DestroyObject), 1f);
        }
    }

    private void FixedUpdate()
    {
        // Moving Skeleton
        Vector3 distanceToPlayer = player.position - transform.position;

        if (distanceToPlayer.magnitude > agroRange && currentHealth > 0) Patrolling();
        else if (distanceToPlayer.magnitude <= agroRange && distanceToPlayer.magnitude > combatRange && currentHealth > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(distanceToPlayer), Time.deltaTime * rotationSpeed * 2f);
            agent.SetDestination(player.position);
            animator.SetBool(isWalkingHash, true);
        }
        else if (distanceToPlayer.magnitude > agroRange || distanceToPlayer.magnitude <= combatRange)
        {
            animator.SetBool(isWalkingHash, false);
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }


    private void Patrolling()
    {
        Vector3 distanceToWalkPoint = walkPoint - transform.position;
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(distanceToWalkPoint), Time.deltaTime * rotationSpeed);
            agent.SetDestination(walkPoint);
            animator.SetBool(isWalkingHash, true);
        }
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            animator.SetBool(isWalkingHash, false);
        }
    }

    private void SearchWalkPoint()
    {
        randomZ = Random.Range(-walkPointRange, walkPointRange);
        randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(spawnLocation.x + randomX, spawnLocation.y, spawnLocation.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
            walkPointSet = true;
    }


}