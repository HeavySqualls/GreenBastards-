using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controlsPanel;

    private int currentLevelIndex;

    void Start()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (controlsPanel != null)
        {
            controlsPanel.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(currentLevelIndex + 1);
    }

    public void OpenSettings()
    {
        controlsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        controlsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
