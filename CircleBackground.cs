using UnityEngine;

public class CircleBackground : MonoBehaviour
{

    public Sprite circleBackgroundLight;
    public Sprite circleBackgroundDark;

    Color lightColor = new Color(0.0980f, .7803f, .0588f, 1);
    Color darkColor = new Color(0.0549f, 0.4392f, 0.0313f, 1);
    // Start is called before the first frame update
    void Start()
    {
        if( PlayerPrefs.GetString("selectedMode" , "Light") == "Light")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = circleBackgroundLight;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().backgroundColor = lightColor;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = circleBackgroundDark;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().backgroundColor = darkColor;
        }
    }

}
