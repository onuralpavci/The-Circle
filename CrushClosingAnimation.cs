using UnityEngine;
using UnityEngine.UI;
public class CrushClosingAnimation : MonoBehaviour
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
            GetComponentInChildren<Text>().color = lightGreyTextColor;
        }

        else
        {
            GetComponent<Image>().color = lightGreenAnimationColor;
            GetComponentInChildren<Text>().color = darkGreyTextColor;
        }
            
    }

    public void ShowScore()
    {
        if (PlayerPrefs.GetInt("isChallange", 0) == 0)
        {
            if (PlayerPrefs.GetInt("highestScore", 0) < EnemyWaveController.highScore)
            {
                GetComponentInChildren<Text>().text = "New High Score\n" + EnemyWaveController.highScore.ToString();
            }
            else
            {
                GetComponentInChildren<Text>().text = "Score: " + EnemyWaveController.highScore.ToString() + "\nHigh Score: " + PlayerPrefs.GetInt("highestScore", 0);
            }

        }
    }


}
