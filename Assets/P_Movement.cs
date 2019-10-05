using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Movement : MonoBehaviour
{
    public static P_Movement PlayerInstance;

    private Rigidbody rb;
    public FloatReference moveSpeed;
    //private float speed = 5f;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PlayerInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward * v;
        Vector3 right = transform.right * h;
        Vector3 final = (forward + right);
        final = Vector3.ClampMagnitude(final, 1f);

        rb.MovePosition(transform.position + (final * moveSpeed.Value * Time.deltaTime));
    }

    public void MovePlayerToLocation(Transform pos, bool alsoSetRotation = false)
    {
        rb.transform.position = pos.position;

        if(alsoSetRotation)
        {
            GetComponentInChildren<P_MouseLook>().EndLookAtTarget();
            rb.MoveRotation(pos.rotation);
        }
    }

    public void ForcePlayerLookat(Transform target)
    {
        GetComponentInChildren<P_MouseLook>().LookAtTarget(target);
    }

    public void EndPlayerLookat()
    {
        GetComponentInChildren<P_MouseLook>().EndLookAtTarget();
    }

    public void EndPlayerLookatNoReturn()
    {
        GetComponent<P_MouseLook>().EndLookatNoReturn();
    }

    public void TogglePlayerIsKinematic(int overrideValue = -1)
    {
        if(overrideValue == -1)
            rb.isKinematic = !rb.isKinematic;
        else
        {
            if(overrideValue == 0)
            {
                rb.isKinematic = false;
            }
            else
            {
                rb.isKinematic = true;
            }
        }
    }
}
