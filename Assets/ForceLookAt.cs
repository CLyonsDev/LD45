using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceLookAt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ForcePlayerLookAtThis()
    {
        P_Movement.PlayerInstance.ForcePlayerLookat(this.transform);
    }

    public void StopPlayerLookAtThis()
    {
        P_Movement.PlayerInstance.EndPlayerLookat();
    }

    public void StopPlayerLookAtNoReturn()
    {
        P_Movement.PlayerInstance.EndPlayerLookatNoReturn();
    }
}
