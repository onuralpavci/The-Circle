using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = this.GetComponent<Button>();
        button.onClick.AddListener(LoadFirstScene);

    }

    void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }
}
