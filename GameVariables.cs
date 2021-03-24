using UnityEngine;
using UnityEngine.UI;

public class GameVariables : MonoBehaviour
{
    public Text levelText;
    public Text coinText;
    public Text timeText;
    public Text scoreText;
    public RectTransform joystick;

    Color lightGreyTextColor = new Color(0.9547f, 0.9547f, 0.9547f, 1);
    Color darkGreyTextColor = new Color(0.1960f, 0.1960f, 0.1960f, 1);

    private void Start()
    {

        if( PlayerPrefs.GetInt("isChallange" , 0) == 1)
        {
            if (PlayerPrefs.GetInt("level", 1) < 10)
                levelText.text = "Level : 0" + PlayerPrefs.GetInt("level", 1);
            else
                levelText.text = "Level : " + PlayerPrefs.GetInt("level", 1);
        }

        else
        {
            scoreText.text = "0";
        }

        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<RectTransform>();
        if (PlayerPrefs.GetString("selectedJoystick", "Left") == "Left")
        {
            joystick.anchorMin = Vector2.zero;
            joystick.anchorMax = Vector2.zero;
            joystick.anchoredPosition = new Vector2(0 , 0);
            
        }

        else
        {
            joystick.anchorMin = Vector2.zero;
            joystick.anchorMax = Vector2.zero;
            joystick.anchoredPosition = new Vector2(0, 0);
        }

        if (PlayerPrefs.GetString("selectedMode", "Light") == "Dark")
        {
            if (PlayerPrefs.GetInt("isChallange", 0) == 0)
            {
                scoreText.color = lightGreyTextColor;
                coinText.color = lightGreyTextColor;
            }
            else
            {
                timeText.color = lightGreyTextColor;
                coinText.color = lightGreyTextColor;
                levelText.color = lightGreyTextColor;
            }
        }

        else
        {
            if (PlayerPrefs.GetInt("isChallange", 0) == 0)
            {
                scoreText.color = darkGreyTextColor;
                coinText.color = darkGreyTextColor;
            }
            else
            {
                timeText.color = darkGreyTextColor;
                coinText.color = darkGreyTextColor;
                levelText.color = darkGreyTextColor;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = PlayerPrefs.GetInt("coin", 0).ToString();

        if (timeText != null && MyTime.timeLeft >= 0)
            timeText.text = Mathf.Round(MyTime.timeLeft).ToString();

        else if (PlayerPrefs.GetInt("isChallange", 0) == 0)
            scoreText.text = EnemyWaveController.highScore.ToString();

    }
}
