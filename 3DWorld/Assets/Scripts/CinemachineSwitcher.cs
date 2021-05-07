using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    public CinemachineFreeLook thirdPersonCamera;
    public CinemachineFreeLook AimingCamera;

    private bool thirdPerson = true;

    private void Update()
    {
        if ((Input.GetAxis("Aim") == 1 && thirdPerson) || (Input.GetAxis("Aim") == -1 && !thirdPerson))
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

            Invoke("ThirdPersonCamFollow", 1.0f);
            //thirdPersonCamera.m_RecenterToTargetHeading.m_enabled = true;
        }
        else
        {
            thirdPersonCamera.Priority = 1;
            AimingCamera.Priority = 0;

            ThirdPersonCamFollow();
            //thirdPersonCamera.m_RecenterToTargetHeading.m_enabled = false;
        }
        thirdPerson = !thirdPerson;
    }

    private void ThirdPersonCamFollow()
    {
        thirdPersonCamera.m_RecenterToTargetHeading.m_enabled = !thirdPersonCamera.m_RecenterToTargetHeading.m_enabled;
    }
}
