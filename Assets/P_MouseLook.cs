using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_MouseLook : MonoBehaviour
{
    private Rigidbody body;
    private Camera cam;

    [SerializeField]
    private Vector2 sensitivity = new Vector2(2, 2);

    private Vector2 mouseClampVertical = new Vector2(-35f, 35f);
    public float desiredMouseY = 0;
    private float mouseLerpRate = 15f;

    [SerializeField]
    private bool mouseLookEnabled = true;

    private Transform lookatTarget;
    private Quaternion prevLook;

    private void Awake()
    {
        body = transform.root.GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(mouseLookEnabled == true)
        {
            MouseLookLogic();
        }else if(lookatTarget != null)
        {
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation,
                Quaternion.LookRotation(lookatTarget.position - cam.transform.position),
                10 * Time.deltaTime);
        }
    }

    private void MouseLookLogic()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        desiredMouseY += v;
        desiredMouseY = Mathf.Clamp(desiredMouseY, mouseClampVertical.x, mouseClampVertical.y);
        //Debug.Log(v);

        body.MoveRotation(Quaternion.Euler(0, body.transform.eulerAngles.y + (h * sensitivity.x), 0));

        //cam.transform.localRotation =
        cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation, Quaternion.Euler(-desiredMouseY * sensitivity.y, 0, 0), mouseLerpRate * Time.deltaTime);
    }

    public void LookAtTarget(Transform target)
    {
        if(mouseLookEnabled == true)
        {
            SetMouseEnabled(false);
            prevLook = cam.transform.rotation;
            lookatTarget = target;
        }
    }

    public void EndLookAtTarget()
    {
        if(prevLook != Quaternion.identity)
            StartCoroutine(ReturnFromLookat());
    }

    // ominous name...
    public void EndLookatNoReturn()
    {
        lookatTarget = null;
    }

    private IEnumerator ReturnFromLookat()
    {
        lookatTarget = null;

        while (true)
        {
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation,
                prevLook,
                10 * Time.deltaTime);

            if(cam.transform.rotation == prevLook)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        prevLook = Quaternion.identity;
        //cam.transform.localRotation = Quaternion.identity;
        SetMouseEnabled(true);
    }

    public void SetMouseEnabled(bool isMouseEnabled)
    {
        mouseLookEnabled = isMouseEnabled;
    }
}
