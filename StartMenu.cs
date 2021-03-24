using UnityEngine;
using UnityEngine.UI;
using DanielLochner.Assets.SimpleScrollSnap;

public class StartMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public Text vehicleSelectedText;
    public Text circleSelectedText;
    public GameObject mainMenuClosingAnim;
    public SimpleScrollSnap vehiclesScroll;
    public SimpleScrollSnap circlesScroll;

    public GameObject adManager;

    public void StartGame()
    {
        if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
        {
            FindObjectOfType<AudioManager>().Play("ButtonSound");
        }

        if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
        {
            FindObjectOfType<AudioManager>().PauseWithFadeOut("GameMusic", 1f);
            FindObjectOfType<AudioManager>().PlayWithFadeIn("GameMusic2", 2f);
        }

        PlayerPrefs.SetInt("isChallange", 0);

        mainMenuClosingAnim.SetActive(true);
    }

    public void StartChallange()
    {
        if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
        {
            FindObjectOfType<AudioManager>().Play("ButtonSound");
        }

        if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
        {
            FindObjectOfType<AudioManager>().PauseWithFadeOut("GameMusic", 1f);
            FindObjectOfType<AudioManager>().PlayWithFadeIn("GameMusic2", 2f);
        }

        PlayerPrefs.SetInt("isChallange", 1);

        mainMenuClosingAnim.SetActive(true);
    }

    public void Start()
    {
        InvokeRepeating("PlayIntersitialAd", 30f, 90f);
        

        if (PlayerPrefs.GetInt("HelicopterIsBought", 0) == 1)
        {
            GameObject.FindGameObjectWithTag("Helicopter").GetComponent<Button>().interactable = true;
            Destroy(GameObject.FindGameObjectWithTag("HelicopterBuyButton"));
        }
           
        if (PlayerPrefs.GetInt("JetIsBought", 0) == 1)
        {
            GameObject.FindGameObjectWithTag("Jet").GetComponent<Button>().interactable = true;
            Destroy(GameObject.FindGameObjectWithTag("JetBuyButton"));
        }

        if (PlayerPrefs.GetInt("RocketIsBought", 0) == 1)
        {
            GameObject.FindGameObjectWithTag("Rocket").GetComponent<Button>().interactable = true;
            Destroy(GameObject.FindGameObjectWithTag("RocketBuyButton"));
        }

        if (PlayerPrefs.GetInt("StealthIsBought", 0) == 1)
        {
            GameObject.FindGameObjectWithTag("Stealth").GetComponent<Button>().interactable = true;
            Destroy(GameObject.FindGameObjectWithTag("StealthBuyButton"));
        }

        if (PlayerPrefs.GetInt("SwordIsBought", 0) == 1)
        {
            GameObject.FindGameObjectWithTag("Sword").GetComponent<Button>().interactable = true;
            Destroy(GameObject.FindGameObjectWithTag("SwordBuyButton"));
        }


        if (PlayerPrefs.GetInt("circleSize", 10) >= 400)
        {
            Destroy(GameObject.FindGameObjectWithTag("CircleBuyButton"));
        }

        if( PlayerPrefs.GetInt("level" , 1) >= 10)
        {
            GameObject.FindGameObjectWithTag("Hat").GetComponent<Button>().interactable = true;
            Destroy(GameObject.FindGameObjectWithTag("HatReachLevelText"));
        }

        if( PlayerPrefs.GetInt("level", 1) >= 20)
        {
            GameObject.FindGameObjectWithTag("Two").GetComponent<Button>().interactable = true;
            Destroy(GameObject.FindGameObjectWithTag("TwoReachLevelText"));
        }

        if (PlayerPrefs.GetInt("level", 1) >= 30)
        {
            GameObject.FindGameObjectWithTag("Glasses").GetComponent<Button>().interactable = true;
            Destroy(GameObject.FindGameObjectWithTag("GlassesReachLevelText"));
        }

        if (GameObject.FindGameObjectWithTag(PlayerPrefs.GetString("selectedVehicle", "Tank")) != null)
        {
            vehicleSelectedText.transform.SetParent( GameObject.FindGameObjectWithTag(PlayerPrefs.GetString("selectedVehicle", "Tank")).transform );
            vehicleSelectedText.transform.position = GameObject.FindGameObjectWithTag(PlayerPrefs.GetString("selectedVehicle", "Tank")).transform.TransformPoint(Vector3.up * 60);
        }

        if (GameObject.FindGameObjectWithTag(PlayerPrefs.GetString("selectedCircle", "Orange")) != null)
        {
            circleSelectedText.transform.SetParent( GameObject.FindGameObjectWithTag(PlayerPrefs.GetString("selectedCircle", "Orange")).transform);
            circleSelectedText.transform.position = GameObject.FindGameObjectWithTag(PlayerPrefs.GetString("selectedCircle", "Orange")).transform.TransformPoint(Vector3.up * 60);
        }

        Invoke("OpenSelectedVehicleInScroll" , 0.5f);
        Invoke("OpenSelectedCircleInScroll", 0.5f);
    }

    public int FindSelectedVehicleIndex()
    {
        for (int i = 0; i < vehiclesScroll.NumberOfPanels; i++)
        {
            if (vehiclesScroll.Panels[i].CompareTag(PlayerPrefs.GetString("selectedVehicle", "Tank") + "Panel") )
            {
                return i;
            }
        }
        //Error - Selected Vehicle Could Not Found
        return -1;
    }

    public int FindSelectedCircleIndex()
    {
        for (int i = 0; i < circlesScroll.NumberOfPanels; i++)
        {
            if (circlesScroll.Panels[i].CompareTag(PlayerPrefs.GetString("selectedCircle", "Orange") + "Panel"))
            {
                return i;
            }
        }
        return -1;
    }

    void OpenSelectedVehicleInScroll()
    {
        vehiclesScroll.GoToPanel(FindSelectedVehicleIndex());
    }

    void OpenSelectedCircleInScroll()
    {
        circlesScroll.GoToPanel(FindSelectedCircleIndex());
    }

    public void OpenSettings()
    {
        if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
        {
            FindObjectOfType<AudioManager>().Play("ButtonSound");
        }

        settingsMenu.SetActive(true);

        if( PlayerPrefs.GetString("selectedMode" , "Light") == "Dark")
        {
            GameObject.FindGameObjectWithTag("LightModeButton").GetComponent<Image>().color = new Color32(200, 200, 200, 128);
            GameObject.FindGameObjectWithTag("DarkModeButton").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            GameObject.FindGameObjectWithTag("LightModeButton").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            GameObject.FindGameObjectWithTag("DarkModeButton").GetComponent<Image>().color = new Color32(200, 200, 200, 128);
        }


        if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
        {
            GameObject.FindGameObjectWithTag("MusicButton").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            GameObject.FindGameObjectWithTag("MusicButton").GetComponent<Image>().color = new Color32(200, 200, 200, 128);
        }

        if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
        {
            GameObject.FindGameObjectWithTag("SoundButton").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            GameObject.FindGameObjectWithTag("SoundButton").GetComponent<Image>().color = new Color32(200, 200, 200, 128);
        }

        if( PlayerPrefs.GetString("SelectedController" , "joystick") == "arrows")
        {
            GameObject.FindGameObjectWithTag("ArrowControllerButton").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            GameObject.FindGameObjectWithTag("JoystickControllerButton").GetComponent<Image>().color = new Color32(200, 200, 200, 128);
        }
        else
        {
            GameObject.FindGameObjectWithTag("JoystickControllerButton").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            GameObject.FindGameObjectWithTag("ArrowControllerButton").GetComponent<Image>().color = new Color32(200, 200, 200, 128);
        }
    }

    public void PlayIntersitialAd()
    {
        adManager.GetComponent<AdManager>().PlayIntersitialAd();
    }
}
