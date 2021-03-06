using UnityEngine;
using UnityEngine.AI;

public class SkeletonEnemy : MonoBehaviour
{
    Vector3 spawnLocation;
    Animator animator;
    private Transform player;
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
    bool walkPointSet;
    float randomZ;
    float randomX;

    float walkTimer;

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
            Invoke(nameof(DestroyObject), 1f);
        }

        // Checking if Stuck
        if (walkTimer > 10f)
        {
            walkPointSet = false;
        }

        // Moving Skeleton
        Vector3 distanceToPlayer = player.position - transform.position;

        if (distanceToPlayer.magnitude > agroRange && currentHealth > 0) Patrolling();
        else if (distanceToPlayer.magnitude <= agroRange && distanceToPlayer.magnitude > combatRange && currentHealth > 0)
        {
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
            walkTimer += Time.deltaTime;
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
        {
            walkPointSet = true;
            walkTimer = 0f;

        }
    }


}