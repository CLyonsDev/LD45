using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimedGameEvent
{
    public bool waitForEventBeforeProgressing = false;
    public float delayBeforeRaise;  // How long after the previous event is raised (in seconds) before this event is raised as well? 
    public GameEvent gameEvent;     // The event that is raised (this can call anything from a scene change to a minor update)
    public AudioClip clip;          // May not use this, but could be a good way to trigger each scene's audio instead of having one massive clip.
}

//TODO: Consider resetting game timer to 0 every time an event is called? Not sure yet.

public class StoryTeller : MonoBehaviour
{
    public List<TimedGameEvent> StoryEvents;

    private AudioSource storytellerAudioSource;

    [SerializeField]
    private float gameTimer = 0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        storytellerAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;

        if(StoryEvents.Count > 0)
        {
            if (gameTimer >= StoryEvents[0].delayBeforeRaise && StoryEvents[0].waitForEventBeforeProgressing == false)
            {
                RaiseTimedGameEvent(0);
            }
        }
    }

    public void ProgressStory()
    {
        if(StoryEvents[0].waitForEventBeforeProgressing == true)
            RaiseTimedGameEvent(0);
    }

    private void RaiseTimedGameEvent(int index)
    {
        TimedGameEvent e = StoryEvents[index];

        if (e.gameEvent != null)
            e.gameEvent.Raise();
        if(e.clip != null)
            storytellerAudioSource.PlayOneShot(e.clip);

        gameTimer = 0.0f;
        StoryEvents.RemoveAt(index);
        
    }
}
