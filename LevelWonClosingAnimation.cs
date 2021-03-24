using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelWonClosingAnimation : MonoBehaviour
{

    Color lightGreenAnimationColor = new Color(0.1568f, .7725f, .0901f, 1);
    Color darkGreenAnimationColor = new Color(0.0980f, 0.4352f, 0.0549f, 1);

    Color lightGreyTextColor = new Color(0.7547f, 0.7547f, 0.7547f, 1);
    Color darkGreyTextColor = new Color(0.1960f, 0.1960f, 0.1960f, 1);

    private void Start()
    {
        if (PlayerPrefs.GetString("selectedMode", "Light") == "Dark")
        {
            GetComponent<Image>().color = darkGreenAnimationColor;
            GetComponentsInChildren<Text>()[0].color = lightGreyTextColor;
            GetComponentsInChildren<Text>()[1].color = lightGreyTextColor;
        }

        else
        {
            GetComponent<Image>().color = lightGreenAnimationColor;
            GetComponentInChildren<Text>().color = darkGreyTextColor;
        }

    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }
}
