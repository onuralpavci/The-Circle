using UnityEngine;
using UnityEngine.UI;

public class StartMenuVariables : MonoBehaviour
{
    public Text scoreText;
    public Text mainMenuCoinText;
    public Text circleFeatures;

    public Sprite circleBackgroundLight;
    public Sprite circleBackgroundDark;
    public Camera mainCamera;
    public GameObject mainPanel;

    Color lightGreenCameraColor = new Color(0.0980f, .7803f, .0588f, 1);
    Color lightGreyTextColor = new Color(0.9547f, 0.9547f, 0.9547f, 1f);

    Color darkGreenCameraColor = new Color(0.0549f, 0.4392f, 0.0313f, 1);
    Color darkGreyTextColor = new Color(0.2641f, 0.2641f, 0.2641f, 1f);

    public GameObject vehicles;
    public GameObject circles;
    public Text theCircleText;

    private void Start()
    {
        scoreText.text = "Best : " + PlayerPrefs.GetInt("highestScore", 0);
        GameObject.FindGameObjectWithTag("CircleBuyButton").GetComponentInChildren<Text>().text = (PlayerPrefs.GetInt("circleSize", 10) * 10).ToString();

        if (PlayerPrefs.GetString("selectedMode", "Light") == "Dark")
        {
            mainPanel.GetComponent<Image>().sprite = circleBackgroundDark;

            Component[] mainPanelTexts = mainPanel.GetComponentsInChildren<Text>();
            foreach (Text text in mainPanelTexts)
                text.color = lightGreyTextColor;

            mainCamera.backgroundColor = darkGreenCameraColor;

            Component[] circleTexts = circles.GetComponentsInChildren<Text>();
            foreach (Text text in circleTexts)
                text.color = lightGreyTextColor;

            Component[] vehicleTexts = vehicles.GetComponentsInChildren<Text>();
            foreach (Text text in vehicleTexts)
                text.color = lightGreyTextColor;

            theCircleText.color = new Color(1f, 1f, 1f, 1f);

        }
        else
        {
            mainPanel.GetComponent<Image>().sprite = circleBackgroundLight;

            Component[] mainPanelTexts = mainPanel.GetComponentsInChildren<Text>();
            foreach (Text text in mainPanelTexts)
                text.color = darkGreyTextColor;

            mainCamera.backgroundColor = lightGreenCameraColor;

            Component[] circleTexts = circles.GetComponentsInChildren<Text>();
            foreach (Text text in circleTexts)
                text.color = darkGreyTextColor;

            Component[] vehicleTexts = vehicles.GetComponentsInChildren<Text>();
            foreach (Text text in vehicleTexts)
                text.color = darkGreyTextColor;

            theCircleText.color = new Color(.196f, .196f, .196f, 1f);


        }
    }
    // Update is called once per frame
    void Update()
    {
        mainMenuCoinText.text = PlayerPrefs.GetInt("coin", 0).ToString();

        if (PlayerPrefs.GetInt("circleSize", 10) < 400)
        {
            circleFeatures.text = "Size : " + PlayerPrefs.GetInt("circleSize", 10).ToString();
        }
        else
        {
            circleFeatures.text = "Reached Max Size";
        }
        

    }
}
