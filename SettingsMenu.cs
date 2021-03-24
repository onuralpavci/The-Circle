using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsMenu;

    public Sprite circleBackgroundLight;
    public Sprite circleBackgroundDark;
    public Camera mainCamera;
    public GameObject mainPanel;
    Color lightGreenColor = new Color(0.1568f, .7725f, .0901f, 1);
    Color lightGreyColor = new Color(0.9547f, 0.9547f, 0.9547f, 1f);

    Color darkGreenColor = new Color(0.0980f, 0.4352f, 0.0549f, 1);
    Color darkGreyColor = new Color(0.2641f, 0.2641f, 0.2641f, 1f); 

    public GameObject vehicles;
    public GameObject circles;
    public Text theCircleText;

    public void PlayPauseMusic()
    {
        if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
        {
            FindObjectOfType<AudioManager>().Play("ButtonSound");
        }

        if ( PlayerPrefs.GetInt("MusicEnabled" , 1) == 1)
        {
            FindObjectOfType<AudioManager>().Pause("GameMusic");
            PlayerPrefs.SetInt("MusicEnabled", 0);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("GameMusic");
            PlayerPrefs.SetInt("MusicEnabled", 1);
        }
    }

    public void PlayPauseSound()
    {
        if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
        {
            FindObjectOfType<AudioManager>().Play("ButtonSound");
        }

        if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
        {
            PlayerPrefs.SetInt("SoundEnabled", 0);
        }
        else
        {
            PlayerPrefs.SetInt("SoundEnabled", 1);
        }
    }


    public void LightMode()
    {
        Component[] circleTexts = circles.GetComponentsInChildren<Text>();
        foreach (Text text in circleTexts)
            text.color = darkGreyColor;

        Component[] vehicleTexts = vehicles.GetComponentsInChildren<Text>();
        foreach (Text text in vehicleTexts)
            text.color = darkGreyColor;


        theCircleText.color = new Color(.196f, .196f, .196f, 1f);

        PlayerPrefs.SetString("selectedMode", "Light");

        mainPanel.GetComponent<Image>().sprite = circleBackgroundLight;

        Component[] mainPanelTexts = mainPanel.GetComponentsInChildren<Text>();
        foreach (Text text in mainPanelTexts)
            text.color = darkGreyColor;

        mainCamera.backgroundColor = lightGreenColor;

        GameObject.FindGameObjectWithTag("LightModeButton").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        GameObject.FindGameObjectWithTag("DarkModeButton").GetComponent<Image>().color = new Color32(200, 200, 200, 128);
    }

    public void DarkMode()
    {
        Component[] circleTexts = circles.GetComponentsInChildren<Text>();
        foreach (Text text in circleTexts)
            text.color = lightGreyColor;

        Component[] vehicleTexts = vehicles.GetComponentsInChildren<Text>();
        foreach (Text text in vehicleTexts)
            text.color = lightGreyColor;

        theCircleText.color = new Color(1f, 1f, 1f, 1f);

        PlayerPrefs.SetString("selectedMode", "Dark");

        mainPanel.GetComponent<Image>().sprite = circleBackgroundDark;

        Component[] mainPanelTexts = mainPanel.GetComponentsInChildren<Text>();
        foreach (Text text in mainPanelTexts)
            text.color = lightGreyColor;

        mainCamera.backgroundColor = darkGreenColor;

        GameObject.FindGameObjectWithTag("LightModeButton").GetComponent<Image>().color = new Color32(200, 200, 200, 128);
        GameObject.FindGameObjectWithTag("DarkModeButton").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void BackToMainMenu()
    {
        if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
        {
            FindObjectOfType<AudioManager>().Play("ButtonSound");
        }

        settingsMenu.SetActive(false);
    }

}
