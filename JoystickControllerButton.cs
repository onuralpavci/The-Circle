using UnityEngine;
using UnityEngine.UI;

public class JoystickControllerButton : MonoBehaviour
{
    public GameObject arrowControllerButton;

    public void ChangeColor()
    {
        PlayerPrefs.SetString("SelectedController", "joystick");
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        arrowControllerButton.GetComponent<Image>().color = new Color32(200, 200, 200, 128);
    }
}
