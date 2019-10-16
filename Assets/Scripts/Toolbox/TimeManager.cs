using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [Space]
    [Header("Time Stats:")]
    public string minutes;
    public string seconds;
    private float startTime;
    private float currentTime;

    [Space]
    [Header("Time Status:")]
    public bool isCountDownTime;
    private bool isCountDownStarted;
    public GameObject gameOverPanel;

    [Space]
    [Header("Time References:")]
    public GameObject startText;
    public Text time;

    public float StartCountDownTimer(float levelTime)
    {
        if (!isCountDownStarted)
        {
            Debug.Log("Time has started!");
            time.enabled = true;
            isCountDownStarted = true;
            isCountDownTime = true;
            startTime = levelTime;
        }
        return startTime;
    }

    private void TrackTime()
    {
        if (isCountDownTime)
        {
            startText.SetActive(false);
            currentTime = startTime -= Time.deltaTime;

            minutes = ((int)currentTime / 60).ToString();
            seconds = (currentTime % 60).ToString("f1");

            time.text = minutes + ":" + seconds;

            if (currentTime <= 0)
            {
                StopCountDownTimer();
            }
        }
    }

    public void StopCountDownTimer()
    {
        isCountDownTime = false;
        time.enabled = false;
        isCountDownStarted = false;
    }
}
