using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCompleteEvent : MonoBehaviour
{
    public GameEvent eventToRaise;
    private bool wasRaised = false;

    private void OnTriggerEnter(Collider other)
    {
        if(wasRaised == false)
        {
            Debug.Log("Trigger Entered");
            eventToRaise.Raise();
            wasRaised = true;
        }
    }
}
