using UnityEngine;

public class MyTime : MonoBehaviour
{
    public float timeLimit;
    public static float timeLeft;
    public bool gameStoped = false;
    public GameObject player;
    public GameObject timeClosingAnimation;
    public GameObject circle;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        circle = GameObject.FindGameObjectWithTag("Circle");
        timeLeft = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft < .5 && player.GetComponent<SpriteRenderer>().enabled == true)
        {
            if (!gameStoped)
            {
                Instantiate(Resources.Load("particles/Crush Particle"), player.transform.position, Quaternion.identity);
                player.GetComponent<SpriteRenderer>().enabled = false;
                player.GetComponent<Collider2D>().enabled = false;
                circle.GetComponent<Collider2D>().enabled = false;
                timeClosingAnimation.SetActive(true);

                if (PlayerPrefs.GetInt("SoundEnabled") == 1)
                    FindObjectOfType<AudioManager>().Play("CrushSound");

                if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
                {
                    FindObjectOfType<AudioManager>().PauseWithFadeOut("GameMusic2", 2f);
                    FindObjectOfType<AudioManager>().PlayWithFadeIn("GameMusic", 5f);
                }

                gameStoped = true;
            }
        }
    }

    public void setTimeLeft( float timeLimit)
    {
        timeLeft = timeLimit;
    }
}
