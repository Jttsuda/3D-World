using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    public CinemachineFreeLook thirdPersonCamera;
    public CinemachineFreeLook AimingCamera;

    private bool thirdPerson = true;

    private void Update()
    {
        if (Input.GetButtonDown("Aim") || Input.GetButtonUp("Aim"))
        {
            SwitchPriority();
        }
    }


    private void SwitchPriority()
    {
        if (thirdPerson)
        {
            thirdPersonCamera.Priority = 0;
            AimingCamera.Priority = 1;

            thirdPersonCamera.m_RecenterToTargetHeading.m_enabled = true;
        }
        else
        {
            thirdPersonCamera.Priority = 1;
            AimingCamera.Priority = 0;

            thirdPersonCamera.m_RecenterToTargetHeading.m_enabled = false;
        }
        thirdPerson = !thirdPerson;
    }
}