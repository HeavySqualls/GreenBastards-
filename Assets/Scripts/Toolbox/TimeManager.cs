using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    public float currentTime;

    private float startTime;
    private bool isTrackTime;

    void Start()
    {
        isTrackTime = false;
    }

    void Update()
    {
        TrackTime();
    }

    private void TrackTime()
    {
        if (isTrackTime)
        {
            currentTime = startTime += Time.deltaTime;
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
        currentTime = 0;
    }
}
