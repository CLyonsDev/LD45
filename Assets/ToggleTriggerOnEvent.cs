using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTriggerOnEvent : MonoBehaviour
{
    public Collider trigger;

    public void ToggleTrigger()
    {
        trigger.enabled = !trigger.enabled;
    }
}
