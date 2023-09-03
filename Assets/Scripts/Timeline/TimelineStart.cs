using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class TimelineStart : MonoBehaviour
{
    public TimelineAsset timeline;
    private bool hasTriggered = false;//ֻ������һ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            Debug.Log(123);
            PlayTimeline();
            hasTriggered = true; // �����Ѵ������Ϊtrue
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
