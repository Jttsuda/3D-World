using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private float speed = 6f;
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private float jumpHeight = 1.5f;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundDistance = 0.4f;
    [SerializeField]
    private LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public Transform cam;


    public float turnSmoothTime = .01f;
    float turnSmoothVelocity;

    private void Start()
    {
    // Disable Mouse Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

        // Moving The Player
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Sprinting
            if (Input.GetButtonDown("Sprint") && isGrounded)
            {
                speed = 20f;
            }
            if (Input.GetButtonUp("Sprint"))
            {
                speed = 6f;
            }

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // Jumping Equation
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


    }
}
