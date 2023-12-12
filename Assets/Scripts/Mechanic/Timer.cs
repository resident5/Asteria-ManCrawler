using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float time;
    public float duration;

    public Image fillImage;

    public bool timerStarted;
    public bool timerFinished;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        fillImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= duration && timerStarted)
        {
            timerStarted = true;
            time += Time.deltaTime;
            fillImage.fillAmount = time / duration;
        }
        else
        {
            timerFinished = true;
            timerStarted = false;
        }
    }

    public void TimerStart(float dura)
    {
        time = 0;
        timerStarted = true;
        timerFinished = false;
        duration = dura;
    }

    public bool TimerEnded()
    {
        return timerFinished == true;
    }
}
