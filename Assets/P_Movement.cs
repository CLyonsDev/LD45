using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Movement : MonoBehaviour
{
    public static P_Movement PlayerInstance;

    private Rigidbody rb;
    public FloatReference moveSpeed;

    private P_MouseLook mouseLookScript;
    private bool canMove = true;
    //private float speed = 5f;

    // Start is called before the first frame update
    void Awake()
    {
        mouseLookScript = GetComponentInChildren<P_MouseLook>();
        rb = GetComponent<Rigidbody>();
        PlayerInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 forward = transform.forward * v;
            Vector3 right = transform.right * h;
            Vector3 final = (forward + right);
            final = Vector3.ClampMagnitude(final, 1f);

            rb.MovePosition(transform.position + (final * moveSpeed.Value * Time.deltaTime));
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    public void MovePlayerToLocation(Transform pos, bool alsoSetRotation = false)
    {
        rb.transform.position = pos.position;

        if(alsoSetRotation)
        {
            mouseLookScript.EndLookAtTarget();
            rb.MoveRotation(pos.rotation);
        }
    }

    public void LockCamera()
    {
        mouseLookScript.SetMouseEnabled(false);
    }

    public void UnlockCamera()
    {
        mouseLookScript.SetMouseEnabled(true);
    }

    public void ForcePlayerLookat(Transform target)
    {
        mouseLookScript.LookAtTarget(target);
    }

    public void EndPlayerLookat()
    {
        mouseLookScript.EndLookAtTarget();
    }

    public void EndPlayerLookatNoReturn()
    {
        mouseLookScript.EndLookatNoReturn();
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
