using UnityEngine;

public class EnemyController : MonoBehaviour
{
    int numberOfEnemies;

    Vector2 enemyPosition;
    public GameObject enemyPrefab;
    public static float enemiesKilled;
    public GameObject player;
    public GameObject crushParticles;
    public GameObject levelWonClosingAnimation;

    public EnemyCreaterInCircle[] circleAreas;  
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        enemiesKilled = 0;
        for( int i = 0 ; i < circleAreas.Length ; i++)
        {
            numberOfEnemies += circleAreas[i].numberOfEnemies;
        }

        for (int i = 0; i < circleAreas.Length; i++)
        {
            circleAreas[i].CreatePositions();
            for(int index = 0; index < circleAreas[i].numberOfEnemies; index++)
            {
                enemyPosition = circleAreas[i].enemyPositions[index];
                GameObject enemy = (GameObject)Instantiate(enemyPrefab, enemyPosition, Quaternion.Euler(0, 0, Random.Range(0, 120)), gameObject.transform);
                enemy.GetComponent<Enemy>().circleAreaPosition = circleAreas[i].circleAreaPosition;
                enemy.GetComponent<Enemy>().radius = circleAreas[i].radius;
            }
            
        }

        PlayerPrefs.SetInt("levelCoinGiven", 0);
    }

    private void Update()
    {
        if (numberOfEnemies == enemiesKilled)
        {

            if ( PlayerPrefs.GetInt("levelCoinGiven") != 1)
            {
                PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 100) + 500);
                PlayerPrefs.SetInt("levelCoinGiven", 1);
            }

            if (player.GetComponent<SpriteRenderer>().enabled == true)
            {
                Instantiate(crushParticles, player.transform.position, Quaternion.identity);
                player.GetComponent<SpriteRenderer>().enabled = false;
                foreach (var collider in player.GetComponents<Collider2D>())
                {
                    collider.enabled = false;
                }

                if (PlayerPrefs.GetInt("SoundEnabled") == 1)
                    FindObjectOfType<AudioManager>().Play("LevelWonSound");

                if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
                {
                    FindObjectOfType<AudioManager>().PauseWithFadeOut("GameMusic2", 2f);
                    FindObjectOfType<AudioManager>().PlayWithFadeIn("GameMusic", 5f);
                }

                levelWonClosingAnimation.SetActive(true);

                if( PlayerPrefs.GetInt( "level" , 1) != 40)
                    PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level", 1) + 1);

            }

        }
    }

    public void ContinueMainLevel()
    {

    }

}
