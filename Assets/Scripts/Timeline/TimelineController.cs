using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class TimelineController : MonoBehaviour
{
    public PlayableDirector director;

    public void PlayTimeline(TimelineAsset timeline)
    {
        director.Stop();
        director.Play(timeline);
    }
}
