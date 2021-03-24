using UnityEngine;
using UnityEngine.UI;
public class StoreMenu : MonoBehaviour
{

    public GameObject vehicleSelectedText;
    public GameObject circleSelectedText;

    int price;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("startMoneyGiven", 0) != 1)
        {
            PlayerPrefs.SetInt("coin", 300);
            PlayerPrefs.SetInt("startMoneyGiven", 1);
        }
    }
    public void GetPrice( int price)
    {   
        this.price = price;
    }
    public void BuyVehicle(string tag)
    {
        if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
        {
            FindObjectOfType<AudioManager>().Play("ButtonSound");
        }

        if (price < PlayerPrefs.GetInt("coin", 0))
        {
            PlayerPrefs.SetInt(tag + "IsBought", 1);
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) - price);
            GameObject.FindGameObjectWithTag(tag).GetComponent<Button>().interactable = true;
            Destroy(GameObject.FindGameObjectWithTag(tag + "BuyButton"));

        }
    }

    public void BuySize( string tag)
    {   
        if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
        {
            FindObjectOfType<AudioManager>().Play("ButtonSound");
        }

        if (PlayerPrefs.GetInt("circleSize", 10) * 10 <= PlayerPrefs.GetInt("coin", 0))
        {
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) - PlayerPrefs.GetInt("circleSize", 10) * 10);
            PlayerPrefs.SetInt("circleSize", PlayerPrefs.GetInt("circleSize", 10) + 10);
        }

        if(PlayerPrefs.GetInt("circleSize", 10) >= 400)
        {
            Destroy(GameObject.FindGameObjectWithTag("CircleBuyButton"));
        }

        GameObject.FindGameObjectWithTag("CircleBuyButton").GetComponentInChildren<Text>().text = (PlayerPrefs.GetInt("circleSize", 10) * 10 ).ToString() ;
    }

    public void SelectVehicle( string tag)
    {
        if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
        {
            FindObjectOfType<AudioManager>().Play("ButtonSound");
        }

        if( PlayerPrefs.GetInt(tag + "IsBought" , 0) == 1 || tag == "Tank")
        {
            vehicleSelectedText.transform.SetParent(GameObject.FindGameObjectWithTag(tag).transform);
            PlayerPrefs.SetString("selectedVehicle", tag);
            vehicleSelectedText.transform.position = GameObject.FindGameObjectWithTag(tag).transform.TransformPoint(Vector3.up * 60);
        }
    }

    public void SelectCircle( string tag)
    {
        if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
        {
            FindObjectOfType<AudioManager>().Play("ButtonSound");
        }

        circleSelectedText.transform.SetParent(GameObject.FindGameObjectWithTag(tag).transform);
        PlayerPrefs.SetString("selectedCircle", tag);
        circleSelectedText.transform.position = GameObject.FindGameObjectWithTag(tag).transform.TransformPoint(Vector3.up * 60);
    }

}
