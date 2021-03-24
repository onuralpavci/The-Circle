using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public void PauseGame()
    {
        if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
        {
            FindObjectOfType<AudioManager>().Play("ButtonSound");
        }

        if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
            FindObjectOfType<AudioManager>().Pause("GameMusic2");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
        {
            FindObjectOfType<AudioManager>().Play("ButtonSound");
        }

        if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
            FindObjectOfType<AudioManager>().PlayWithFadeIn("GameMusic2", 0.5f);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
        {
            FindObjectOfType<AudioManager>().Play("ButtonSound");
        }

        SceneManager.LoadScene(0);
        Time.timeScale = 1f;

        if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
            FindObjectOfType<AudioManager>().PlayWithFadeIn("GameMusic", 1f);
    }
}
