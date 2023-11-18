using System;
using UnityEngine;
using UnityEngine.Events;


public class AnimationEventReceiver : MonoBehaviour
{
    public event Action<AnimationEvent> AnimationStart;
    public event Action<AnimationEvent> AnimationEnd;

    public void OnAnimationStart(AnimationEvent animationEvent)
    {
        AnimationStart?.Invoke(animationEvent);
    }

    public void OnAnimationEnd(AnimationEvent animationEvent)
    {
        AnimationEnd?.Invoke(animationEvent);
    }
}
