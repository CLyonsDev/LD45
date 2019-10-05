using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class SceneRoot : MonoBehaviour
{
    private List<Transform> props = new List<Transform>();

    //[Header("Scene start/stop game events.")]
    //public GameEventListener EventListener;

    [SerializeField]
    private bool sceneIsActive = false;

    [SerializeField]
    private bool overrideCamera = false;
    [SerializeField]
    private Camera camToOverride;


    private void Awake()
    {
        GrabAllPropChildren();

        if(overrideCamera == true)
            camToOverride = GetComponentInChildren<Camera>();

        if(sceneIsActive)
        {
            for (int i = 0; i < props.Count; i++)
            {
                Animator anim = props[i].GetComponent<Animator>();
                anim.SetTrigger("start_grown");
            }
        }
    }

    public void StartScene()
    {
        GrowProps();
        camToOverride.enabled = true;
    }

    public void StopScene()
    {
        ShrinkProps();
        camToOverride.enabled = false;
    }

    public void ToggleScene()
    {
        if(sceneIsActive)
        {
            StopScene();
        }
        else
        {
            StartScene();
        }

        sceneIsActive = !sceneIsActive;
    }

    private void GrowProps()
    {
        for (int i = 0; i < props.Count; i++)
        {
            Animator anim = props[i].GetComponent<Animator>();
            anim.speed = Random.Range(0.85f, 1.3f);
            anim.SetTrigger("grow");
        }
    }

    private void ShrinkProps()
    {
        for (int i = 0; i < props.Count; i++)
        {
            Animator anim = props[i].GetComponent<Animator>();
            anim.speed = Random.Range(0.95f, 1.4f);
            anim.SetTrigger("shrink");
        }
    }

    private void GrabAllPropChildren()
    {
        foreach (Transform c in this.transform)
        {
            if(c.CompareTag("Prop"))
            {
                props.Add(c);
            }
        }
    }
}
