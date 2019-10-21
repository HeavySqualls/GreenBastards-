using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Space]
    [Header("Player Score:")]
    public int timesPlayerDied = 0;
    public int bulletsCollected = 0;
    public int enemiesKilled = 0;
    public float totalTimeValue = 0;

    [Space]
    [Header("Scene Management:")]
    private int currentLevelIndex;
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
    }

    void Start()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
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
        totalTimeValue += _time;
    }


    // ------ SCENE MANAGEMENT ------ //


    public void LevelComplete()
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

    public void RestartLevel(bool _ismanualReset)
    {
        if (!_ismanualReset)
            timesPlayerDied++;

        SceneManager.LoadScene(currentLevelIndex);
    }

    public void RestartGame()
    {
        timesPlayerDied = 0;
        totalTimeValue = 0;
        enemiesKilled = 0;
        bulletsCollected = 0;
        totalTimeValue = 0;

        SceneManager.LoadScene(0);
    }


    // ------ TOOLBOX REFERENCE RESET ------ //

    private void ResetGameManager()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
