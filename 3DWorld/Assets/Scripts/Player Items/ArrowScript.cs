using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    Rigidbody myBody;
    private readonly float lifeTimer = 2f;
    private float timer;
    private bool hitSomething = false;


    private void Start()
    {
        myBody = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(myBody.velocity);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= lifeTimer)
            Destroy(gameObject);
        if (!hitSomething)
            transform.rotation = Quaternion.LookRotation(myBody.velocity);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Arrow") && !collision.collider.CompareTag("Player"))
        {
            hitSomething = true;
            myBody.transform.parent = collision.collider.transform;
            Stick();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            hitSomething = true;
            myBody.transform.parent = other.transform;
            Stick();

        }
    }

    private void Stick()
    {
        myBody.constraints = RigidbodyConstraints.FreezeAll;
    }

}
