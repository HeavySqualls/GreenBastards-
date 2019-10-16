using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Space]
    [Header("Player Score:")]
    public int timesPlayerDied = 0;
    public int bulletsCollected = 0;
    public int enemiesKilled = 0;
    public float totalTime = 0;

    [Space]
    [Header("Scene Management:")]
    private int currentLevelIndex;
    private Scene currentLevelName;
    private bool isLastLevel = false;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetGameManager();
        Toolbox.GetInstance().GetTimeManager().ResetTimerManager();
    }

    void Start()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        currentLevelName = SceneManager.GetActiveScene();
    }

    void Update()
    {
        
    }


    // ------ SCORE MANAGEMENT ------ //


    public void RecieveScore(int _timesDied, int _bulletsCol, int _enemiesKilled)
    {
        timesPlayerDied += _timesDied;
        bulletsCollected += _bulletsCol;
        enemiesKilled += _enemiesKilled;
    }

    public void RecieveLevelTime(float _time)
    {
        totalTime += _time;
    }


    // ------ SCENE MANAGEMENT ------ //


    public void NextOnClick()
    {
        if (isLastLevel)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentLevelIndex + 1);
        }
    }


    // ------ TOOLBOX REFERENCE RESET ------ //


    private void ResetGameManager()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        currentLevelName = SceneManager.GetActiveScene();

    }
}
