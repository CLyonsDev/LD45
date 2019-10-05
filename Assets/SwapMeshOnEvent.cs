using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapMeshOnEvent : MonoBehaviour
{
    public Animator OldMesh;
    public Animator NewMesh;
    public EnemyAI RootAI;

    public void SwapMeshes()
    {
        RootAI.StopAI();

        OldMesh.SetTrigger("shrink");
        NewMesh.gameObject.SetActive(true);
        NewMesh.SetTrigger("grow");

        RootAI.InitAI();
        RootAI.SetAnimator(NewMesh);
    }
}
