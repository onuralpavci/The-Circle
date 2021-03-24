using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = this.GetComponent<Button>();
        button.onClick.AddListener(LoadNextLevel);

    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
        {
            FindObjectOfType<AudioManager>().PauseWithFadeOut("GameMusic", 0.5f);
            FindObjectOfType<AudioManager>().PlayWithFadeIn("GameMusic2", 1.5f);
        }
    }
}
