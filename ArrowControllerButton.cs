using UnityEngine;
using UnityEngine.UI;

public class ArrowControllerButton : MonoBehaviour
{
    public GameObject joystickControllerButton;

    public void ChangeColor()
    {
        PlayerPrefs.SetString("SelectedController", "arrows");
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        joystickControllerButton.GetComponent<Image>().color = new Color32(200, 200, 200, 128);
    }

}
