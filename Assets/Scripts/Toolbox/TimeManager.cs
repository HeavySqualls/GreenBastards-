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
    private bool isTrackTime;

    [Space]
    [Header("Time References:")]
    public Text time;

    void Start()
    {
        time = GameObject.FindGameObjectWithTag("TimerUI").GetComponent<Text>();
        isTrackTime = false;

        minutes = "0";
        seconds = "0";
    }

    void Update()
    {
        TrackTime();
    }

    private void TrackTime()
    {
        if (isTrackTime)
        {
            Debug.Log("Time has started!");
            currentTime = startTime += Time.deltaTime;
            //currentTime = startTime -= Time.deltaTime; ---- For counting down

            minutes = ((int)currentTime / 60).ToString();
            seconds = (currentTime % 60).ToString("f1");

            time.text = minutes + ":" + seconds;
        }
    }

    public void StartTimeTracker()
    {
        isTrackTime = true;
        startTime = 0;
    }

    public void StopTimeTracker()
    {
        isTrackTime = false;
        Toolbox.GetInstance().GetGameManager().RecieveLevelTime(currentTime);
    }

    public void ResetTimerManager()
    {
        time = GameObject.FindGameObjectWithTag("TimerUI").GetComponent<Text>();
        isTrackTime = false;

        minutes = "0";
        seconds = "0";
    }
}
