using UnityEngine;
using UnityEngine.UI;

public class GameOpeningAnimation : MonoBehaviour
{

    Color lightGreenAnimationColor = new Color(0.1568f, .7725f, .0901f, 1);
    Color darkGreenAnimationColor = new Color(0.0980f, 0.4352f, 0.0549f, 1);

    private void Start()
    {
        if (PlayerPrefs.GetString("selectedMode", "Light") == "Dark")
            GetComponent<Image>().color = darkGreenAnimationColor;
        else
            GetComponent<Image>().color = lightGreenAnimationColor;
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
