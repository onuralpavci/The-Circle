using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Vector3 playerStartPosition;
    public bool increaseSpeedOfPlayer;
    public GameObject crushClosingAnim;
    public GameObject timeClosingAnim;
    public GameObject timeText;
    public GameObject player;
    public GameObject circle;
    // Start is called before the first frame update
    void Awake()
    {
        player = (GameObject) Resources.Load("player/" + PlayerPrefs.GetString("selectedVehicle", "Tank"));
        player.GetComponent<Rope>().segmentLength = 14 + PlayerPrefs.GetInt("circleSize", 10) / 20;
        player.transform.position = playerStartPosition;

        /**
        if (increaseSpeedOfPlayer)
        {
            player.GetComponent<PlayerMovement>().speed += 1;
            player.GetComponent<PlayerMovement>().rotationSpeed += 10;
        }
        */
            
        Instantiate(player);

        circle = (GameObject) Resources.Load("circle/" + PlayerPrefs.GetString("selectedCircle", "Orange") );
        circle.transform.localScale = new Vector3(PlayerPrefs.GetInt("circleSize", 10) / 40 + 2f, PlayerPrefs.GetInt("circleSize", 10) / 40 + 2f, 1);
        Instantiate(circle);
    }


    public void GameOver()
    {
        crushClosingAnim.GetComponent<CrushClosingAnimation>().ShowScore();
        crushClosingAnim.SetActive(true);
    }

    public void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex);

        if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1 )
        {
            FindObjectOfType<AudioManager>().Pause("GameMusic");
            FindObjectOfType<AudioManager>().Play("GameMusic2");
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
        {
            FindObjectOfType<AudioManager>().PauseWithFadeOut("GameMusic" , 0.5f);
            FindObjectOfType<AudioManager>().PlayWithFadeIn("GameMusic2" , 1.5f);
        }
    }

    public void ContinueMainLevel()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        circle = GameObject.FindGameObjectWithTag("Circle");

        player.transform.position = playerStartPosition;
        circle.transform.position = playerStartPosition - new Vector3(0, 5, 0);
        player.transform.up = Vector2.up;
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<Collider2D>().enabled = true;
        circle.GetComponent<Collider2D>().enabled = true;

        if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1 )
        {
            FindObjectOfType<AudioManager>().Play("GameMusic2");
        }

        timeClosingAnim = GameObject.FindGameObjectWithTag("TimeClosingAnim");
        if (timeClosingAnim != null) { timeClosingAnim.SetActive(false); };

        if (crushClosingAnim != null) { crushClosingAnim.SetActive(false); };

        timeText = GameObject.FindGameObjectWithTag("TimeText");
        if( timeText != null) 
        {
            timeText.GetComponent<MyTime>().setTimeLeft(timeText.GetComponent<MyTime>().timeLimit);
            timeText.GetComponent<MyTime>().gameStoped = false;
        };
    }
}

