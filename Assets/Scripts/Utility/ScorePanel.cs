using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{ 
    public Text enemiesKilled;
    public Text deaths;
    public Text totalTime;
    public Text totalScore;

    public Button mainMenu;

    private int deathValue = 1000;
    private int killValue = 1250;

    private float score;
    private string minutes;
    private string seconds;

    private GameManager gm;
    void Start()
    {
        mainMenu.onClick.AddListener(MenuButton);
        gm = Toolbox.GetInstance().GetGameManager();
        UpdatePanel();
    }

    void UpdatePanel()
    {
        enemiesKilled.text = /*"Enemies Killed" + "\n" + */gm.enemiesKilled.ToString();
        deaths.text =/* "Deaths" + "\n" + */gm.timesPlayerDied.ToString();

        minutes = ((int)gm.totalTimeValue / 60).ToString();
        seconds = (gm.totalTimeValue % 60).ToString("f1");

        totalTime.text = /*"Total Time" + "\n" +*/ minutes + ":" + seconds;

        score = Mathf.Round(((gm.enemiesKilled * killValue) - (gm.timesPlayerDied * deathValue)) - gm.totalTimeValue);
       
        totalScore.text = score.ToString();
    }

    public void MenuButton()
    {
        gm.RestartGame();
    }
}
