using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Countdown : MonoBehaviour
{

    public float countdownTime = 3f;
    public float startTime = 0;
    public bool countdownStarted;
    public bool countdownFinished = false;


    public Image countDownImage;
    public TMP_Text countDownText;

    private void Awake()
    {
        countDownImage = GetComponent<Image>();
        countDownText = GetComponentInChildren<TMP_Text>();
    }

    void Start()
    {
        startTime = countdownTime;
        countdownFinished = false;
    }

    void Update()
    {
        if (startTime >= 0 && countdownStarted)
        {
            startTime -= Time.deltaTime;
            countDownText.text = Mathf.Ceil(startTime).ToString();
            countDownImage.fillAmount = 1f - (startTime / countdownTime);
        }
        else
        {
            transform.parent.gameObject.SetActive(false);
            countdownStarted = false;
            countdownFinished = true;
        }
    }

    public void CountDownStart(float dura)
    {
        startTime = countdownTime;
        countdownStarted = true;
        countdownFinished = false;
        countdownTime = dura;
    }

    public bool CountDownEnd()
    {
        return countdownFinished == true;
    }
}
