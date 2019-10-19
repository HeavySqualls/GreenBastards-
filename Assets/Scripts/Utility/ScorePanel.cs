using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{ 
    public Text enemiesKilled;
    public Text deaths;
    public Text totalTime;
    public Text totalScore;

    private int deathValue = 500;
    private int killValue = 250;

    private float score;
    private string minutes;
    private string seconds;

    private GameManager gm;
    void Start()
    {
        gm = Toolbox.GetInstance().GetGameManager();
        UpdatePanel();
    }

    void UpdatePanel()
    {
        enemiesKilled.text = "Enemies Killed" + "\n" + gm.enemiesKilled;
        deaths.text = "Deaths" + "\n" + gm.timesPlayerDied;

        minutes = ((int)gm.totalTimeValue / 60).ToString();
        seconds = (gm.totalTimeValue % 60).ToString("f1");

        totalTime.text = "Total Time" + "\n" + minutes + ":" + seconds;

        score = Mathf.Round(((gm.enemiesKilled * killValue) - (gm.timesPlayerDied * deathValue)) - gm.totalTimeValue);
       
        totalScore.text = score.ToString();
    }

    public void MenuButton()
    {
        gm.RestartGame();
    }
}
