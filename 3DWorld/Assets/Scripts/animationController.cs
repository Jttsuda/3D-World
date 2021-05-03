using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class animationController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isAimingHash;

    //TEST
    public Rig rigLayer;
    public RigBuilder warriorRigs;


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
        if (isrunning && (forwardPressed == 0 || rightPressed == 0) && !runPressed)
            animator.SetBool(isRunningHash, false);


        // Jumping
        if (Input.GetButtonDown("Jump"))
            animator.SetTrigger("Jump");
        if (Input.GetButtonUp("Jump"))
            animator.ResetTrigger("Jump");


        // Aiming Bow
        if (Input.GetButton("Aim"))
        {
            animator.SetTrigger("Aiming");
            animator.SetBool(isAimingHash, true);

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("AimingIdle"))
            {
                //warriorRigs.enabled = true;
                if (rigLayer.weight < 1f)
                    rigLayer.weight += 4 * Time.deltaTime;
            }


        }
        if (!Input.GetButton("Aim") && isAiming == true)
        {
            animator.ResetTrigger("Aiming");
            animator.SetBool(isAimingHash, false);

            rigLayer.weight = 0;
            //warriorRigs.enabled = false;
        }


    }


}
