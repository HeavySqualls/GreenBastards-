using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button mainMenu;
    public Button close;
    public Button restartLevel;
    public GameObject pausePanel;

    private bool isGamePaused;

    private GameManager gm;

    void Start()
    {
        gm = Toolbox.GetInstance().GetGameManager();

        mainMenu.onClick.AddListener(LoadMenu);
        close.onClick.AddListener(DisablePauseMenu);
        restartLevel.onClick.AddListener(ResetLevel);

        DisablePauseMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGamePaused)
        {
            EnablePauseMenu();
            isGamePaused = true;
        }
    }

    void LoadMenu()
    {
        Time.timeScale = 1;
        gm.RestartGame();
    }

    void ResetLevel()
    {
        DisablePauseMenu();
        gm.RestartLevel(true);
    }

    void EnablePauseMenu()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    void DisablePauseMenu()
    {
        pausePanel.SetActive(false);
        isGamePaused = false;
        Time.timeScale = 1;
    }
}
