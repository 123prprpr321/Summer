using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class TimelineStart : MonoBehaviour
{
    public TimelineAsset timeline;
    private bool hasTriggered = false;//只允许触发一次
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            Debug.Log(123);
            PlayTimeline();
            hasTriggered = true; // 设置已触发标记为true
        }
    }
    private void PlayTimeline()
    {
        var timelineController = GetComponent<TimelineController>();
        if (timelineController != null)
        {
            Debug.Log(123);
            timelineController.PlayTimeline(timeline);
        }
    }
}
