using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_MotionTracker : MonoBehaviour
{
    private float trackPosInterval = 0.2f;
    private float trackRotInterval = 0.2f;
    //private float rewindLerpRate = 5f;
    private float lerpSpeed = 8;

    [SerializeField]
    private List<Vector3> positionHistory = new List<Vector3>();
    [SerializeField]
    private List<Quaternion> rotationHistory = new List<Quaternion>();

    public GameEvent EventToCallOnRewindComplete;

    /*private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            StartTracking();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            PerformRewind();
        }
    }*/

    public void StartTracking()
    {
        StartCoroutine(TrackMotion());
        StartCoroutine(TrackRotation());
    }

    private void StopTracking()
    {
        StopAllCoroutines();
    }

    public void PerformRewind()
    {
        StopTracking();
        P_Movement.PlayerInstance.LockCamera();
        StartCoroutine(RewindMotion());
    }

    private IEnumerator TrackMotion()
    {
        while(true)
        {
            positionHistory.Add(transform.position);

            yield return new WaitForSeconds(trackPosInterval);
        }
    }

    private IEnumerator TrackRotation()
    {
        while(true)
        {
            rotationHistory.Add(transform.rotation);

            yield return new WaitForSeconds(trackRotInterval);
        }
    }

    private IEnumerator RewindMotion()
    {
        Vector3 targetPos = positionHistory[positionHistory.Count - 1];
        Vector3 playerStartPos = transform.position;

        Quaternion targetRot = rotationHistory[rotationHistory.Count - 1];
        Quaternion playerStartRot = transform.rotation;

        float movePctComplete = 0;
        float rotPctComplete = 0;

        bool rotDone = false;
        bool moveDone = false;

        while(rotDone == false || moveDone == false)
        {
            if(movePctComplete < 1.0f)
            {
                transform.position = Vector3.Lerp(playerStartPos, targetPos, movePctComplete);
                movePctComplete += lerpSpeed * Time.fixedDeltaTime;
            }
            else
            {
                positionHistory.RemoveAt(positionHistory.Count - 1);

                if (positionHistory.Count > 0)
                {
                    playerStartPos = transform.position;
                    targetPos = positionHistory[positionHistory.Count - 1];
                    movePctComplete = 0;
                }
                else
                {
                    moveDone = true;
                    Debug.Log("MoveDone");
                }
            }

            if(rotPctComplete < 1.0f)
            {
                transform.rotation = Quaternion.Lerp(playerStartRot, targetRot, rotPctComplete);
                rotPctComplete += lerpSpeed * Time.deltaTime;
            }
            else
            {
                rotationHistory.RemoveAt(rotationHistory.Count - 1);

                if (rotationHistory.Count > 0)
                {
                    playerStartRot = transform.rotation;
                    targetRot = rotationHistory[rotationHistory.Count - 1];
                    rotPctComplete = 0;
                }
                else
                {
                    rotDone = true;
                    Debug.Log("RotDone");
                }
            }
            yield return new WaitForFixedUpdate();
        }

        positionHistory.Clear();
        rotationHistory.Clear();
        P_Movement.PlayerInstance.TogglePlayerIsKinematic(0);
        P_Movement.PlayerInstance.UnlockCamera();
        P_Movement.PlayerInstance.SetCanMove(true);
        EventToCallOnRewindComplete.Raise();
    }
}
