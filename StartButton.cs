using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{

    public GameObject infitineModeButton;
    public GameObject challangeModeButton;

    public void ShowGameModes()
    {
        infitineModeButton.GetComponent<Button>().interactable = true;
        challangeModeButton.GetComponent<Button>().interactable = true;
        gameObject.SetActive(false);
    }
}
