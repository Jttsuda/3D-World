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

    public float turnSmoothTime = .01f;
    float turnSmoothVelocity;

    public Transform cam;

    // Aiming Movement
    public Transform playerBody;
    public float aimSensitivity = 100f;

    private void Start()
    {
        // Cap FPS
/*        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 144;*/

        // Disable Mouse Cursor
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;


        // Free Fall: y = 1/2g * t^2
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        // Player Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1 && !Input.GetButton("Aim"))
        {
            MoveThirdPerson(direction);
        }
        else if (Input.GetButton("Aim"))
        {
            AimingMovement(horizontal, vertical);
        }


        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Sprinting
        if (Input.GetButtonDown("Sprint") && isGrounded)
            speed = 18f;
        if (Input.GetButtonUp("Sprint"))
            speed = 6f;

    }


    private void MoveThirdPerson(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(moveDir.normalized * speed * Time.deltaTime);
    }

    private void AimingMovement(float horizontal, float vertical)
    {
        float mouseX = Input.GetAxis("Mouse X") * aimSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * aimSensitivity * Time.deltaTime;
        playerBody.Rotate(Vector3.up * mouseX);

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);
    }

}
