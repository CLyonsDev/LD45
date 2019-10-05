using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCameraOnEvent : MonoBehaviour
{
    public void LockCamera()
    {
        P_Movement.PlayerInstance.LockCamera();
    }

    public void UnlockCamera()
    {
        P_Movement.PlayerInstance.UnlockCamera();
    }
}
