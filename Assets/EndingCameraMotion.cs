using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCameraMotion : MonoBehaviour
{
    private Camera cam;

    public float TargetCameraFoV = 10.0f;
    public AnimationCurve ZoomCurve;

    private float timer;

    // Start is called before the first frame update
    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    public void StartEndingCameraMotion()
    {
        //transform.parent.GetComponent<P_MouseLook>().SetMouseEnabled(false);
        StartCoroutine(SnapZoom());
    }

    private IEnumerator SnapZoom()
    {
        timer = 0.0f;

        while(true)
        {
            cam.fieldOfView = Mathf.Lerp(60, TargetCameraFoV, ZoomCurve.Evaluate(timer));

            timer += Time.deltaTime;

            if(cam.fieldOfView <= TargetCameraFoV)
            {
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        //P_Movement.PlayerInstance.EndPlayerLookatNoReturn();
        
    }
}
