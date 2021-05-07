using UnityEngine;
using UnityEngine.Animations.Rigging;


public class animationController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isAimingHash;

    // Aiming Animation Rigging
    public Rig rigLayer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // Increases Performance
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isAimingHash = Animator.StringToHash("isAiming");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isrunning = animator.GetBool(isRunningHash);
        bool isAiming = animator.GetBool(isAimingHash);

        float forwardPressed = Input.GetAxisRaw("Vertical");
        float rightPressed = Input.GetAxisRaw("Horizontal");
        bool runPressed = Input.GetButton("Sprint");


        // Walking
        if (forwardPressed != 0 || rightPressed != 0)
        {
            animator.SetBool(isWalkingHash, true);
        } else
        {
            animator.SetBool(isWalkingHash, false);
        }


        // Sprinting
        if (!isrunning && isWalking && runPressed)
            animator.SetBool(isRunningHash, true);
        if ((isrunning && !runPressed) || (forwardPressed == 0 && rightPressed == 0))
            animator.SetBool(isRunningHash, false);


        // Jumping
        if (Input.GetButtonDown("Jump"))
            animator.SetTrigger("Jump");
        if (Input.GetButtonUp("Jump"))
            animator.ResetTrigger("Jump");


        // Aiming Bow
        if (Input.GetAxis("Aim") == 1)
        {
            animator.SetTrigger("Aiming");
            animator.SetBool(isAimingHash, true);

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("AimingIdle"))
            {
                if (rigLayer.weight < 1f)
                    rigLayer.weight += 3f * Time.deltaTime;
            }


        }
        if (Input.GetAxis("Aim") == -1 && isAiming == true)
        {
            animator.ResetTrigger("Aiming");
            animator.SetBool(isAimingHash, false);

            rigLayer.weight = 0;
        }


    }


}
