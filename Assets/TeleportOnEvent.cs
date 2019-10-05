using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnEvent : MonoBehaviour
{
    public void TeleportPlayer()
    {
        P_Movement.PlayerInstance.MovePlayerToLocation(this.transform, false);
    }

    public void SetPlayerIsKinematic(int overrideValue)
    {
        P_Movement.PlayerInstance.TogglePlayerIsKinematic(overrideValue);
    }
}
