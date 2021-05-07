using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    Rigidbody myBody;
    private float lifeTimer = 2f;
    private float timer;
    private bool hitSomething = false;

    //TEST
    public float damage = 10f;

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
        if(collision.collider.tag != "Arrow" && collision.collider.tag != "Player")
        {
            hitSomething = true;
            Stick();

            // Make Arrow Child of Col Obj
            myBody.transform.parent = collision.collider.transform;
        }
    }

    private void Stick()
    {
        myBody.constraints = RigidbodyConstraints.FreezeAll;
    }

}
