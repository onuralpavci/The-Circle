using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{

    public void ChangeColor()
    {

        if(PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
        {
            gameObject.GetComponent<Image>().color = new Color32(200, 200, 200, 128);
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }
}
