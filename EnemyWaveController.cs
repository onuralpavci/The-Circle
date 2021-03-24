using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
    public int waveNumber;
    int numberOfEnemies;
    public int lastNumberOfEnemies;
    public float startRadius;
    public float increaseRate;
    public float increaseSpeed;

    Vector2 enemyPosition;
    public GameObject enemyPrefab;
    public GameObject redEnemyPrefab;
    public static int enemiesKilled;
    public static int highScore;

    public GameObject player;
    public GameObject crushParticles;
    public GameObject circleBackground;
    float newCircleBackgrounSize;
    public bool sizeIncreased = false;
    public GameObject[] redEnemies;

    public float angle;

    public GameObject enlargePowerupPrefab;
    public GameObject snowPowerupPrefab;

    public int maximumNumberOfEnemies;


    public EnemyCreaterInCircle circleArea;
    // Start is called before the first frame update
    void Start()
    {
        highScore = 0;
        waveNumber = 1;
        enemiesKilled = 0;
        newCircleBackgrounSize = 1;
        maximumNumberOfEnemies = 500;

        circleArea.radius = startRadius;
        circleArea.numberOfEnemies = lastNumberOfEnemies;
        circleArea.circleAreaPosition = new Vector2(0, 1);
        circleArea.CreatePositions();

        numberOfEnemies = lastNumberOfEnemies;

        for (int index = 0; index < circleArea.numberOfEnemies; index++)
        {
            enemyPosition = circleArea.enemyPositions[index];
            GameObject enemy = (GameObject)Instantiate(enemyPrefab, enemyPosition, Quaternion.Euler(0, 0, Random.Range(0, 120)), gameObject.transform);
            enemy.GetComponent<Enemy>().circleAreaPosition = circleArea.circleAreaPosition;
            enemy.GetComponent<Enemy>().radius = circleArea.radius;
        }

        redEnemies = new GameObject[waveNumber];
        angle = Random.Range(0f, 1f) * Mathf.PI * 2;
        GameObject redEnemy = (GameObject)Instantiate(redEnemyPrefab, new Vector2(Mathf.Cos(angle) * (circleArea.radius + Random.Range(5, 10)), Mathf.Sin(angle) * (circleArea.radius + Random.Range(5, 10) )), Quaternion.Euler(0, 0, Random.Range(0, 120)) );
        redEnemies[0] = redEnemy;

        PlayerPrefs.SetInt("EnlargePowerupExists", 0);
        PlayerPrefs.SetInt("SnowPowerupExists", 0);

    }

    private void Update()
    {
        if (numberOfEnemies == enemiesKilled)
        {

            if(!sizeIncreased)
            {
                for (int i = 0; i < redEnemies.Length; i++)
                    Destroy(redEnemies[i]);

                newCircleBackgrounSize += increaseRate;
                if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
                    FindObjectOfType<AudioManager>().PlayWithPitch("LevelWonSound" , 1.5f);
                sizeIncreased = true;
            }
            
            IncreaseSize();

        }

    }

    public void IncreaseSize()
    {
        if (circleBackground.transform.localScale.x < newCircleBackgrounSize)
        {
            circleBackground.transform.localScale += new Vector3(Time.deltaTime * increaseSpeed, Time.deltaTime * increaseSpeed, 0);
        }
        else
        {
            waveNumber += 1;

            circleArea.radius = startRadius * newCircleBackgrounSize;
            if (lastNumberOfEnemies < maximumNumberOfEnemies)
                circleArea.numberOfEnemies = (int)(lastNumberOfEnemies * 1.45f);
            else
                circleArea.numberOfEnemies = maximumNumberOfEnemies;
            lastNumberOfEnemies = circleArea.numberOfEnemies;
            circleArea.circleAreaPosition = new Vector2(0, 1);
            circleArea.CreatePositions();

            numberOfEnemies = lastNumberOfEnemies;

            for (int index = 0; index < circleArea.numberOfEnemies; index++)
            {
                enemyPosition = circleArea.enemyPositions[index];
                GameObject enemy = (GameObject)Instantiate(enemyPrefab, enemyPosition, Quaternion.Euler(0, 0, Random.Range(0, 120)), gameObject.transform);
                enemy.GetComponent<Enemy>().circleAreaPosition = circleArea.circleAreaPosition;
                enemy.GetComponent<Enemy>().radius = circleArea.radius;
            }

            enemiesKilled = 0;
            sizeIncreased = false;

            redEnemies = new GameObject[waveNumber]; 
            for( int i = 0; i < waveNumber; i++)
            {
                angle = Random.Range(0f, 1f) * Mathf.PI * 2;
                GameObject redEnemy = (GameObject)Instantiate(redEnemyPrefab, new Vector2( Mathf.Cos(angle) * (circleArea.radius + Random.Range( 5, 10)) , Mathf.Sin(angle) * (circleArea.radius + Random.Range(5, 10) ) ), Quaternion.identity);
                redEnemy.GetComponent<EnemyRed>().speed = Random.Range( 2f, waveNumber / 2f + 4f);
                redEnemy.GetComponent<EnemyRed>().rotationSpeed = Random.Range(20, 80);
                redEnemies[i] = redEnemy;
            }

            if( Random.Range(0, 10) < (8 - PlayerPrefs.GetInt("circleSize", 10) / 100) && PlayerPrefs.GetInt( "EnlargePowerupExists" , 0) == 0)
            {
                Instantiate(enlargePowerupPrefab, circleArea.enemyPositions[0] + Vector2.up * 1.5f, Quaternion.Euler(0, 0, Random.Range(0, 120)), gameObject.transform);
            }

            if (Random.Range(0, 10) < 4 - waveNumber / 4f && PlayerPrefs.GetInt("SnowPowerupExists", 0) == 0)
            {
                Instantiate(snowPowerupPrefab, enemyPosition + Vector2.up * 1.5f, Quaternion.Euler(0, 0, Random.Range(0, 120)), gameObject.transform);
            }
        }
    }

    public void ContinueMainLevel()
    {
        circleArea.numberOfEnemies = enemiesKilled;
        circleArea.CreatePositions();

        for (int index = 0; index < enemiesKilled; index++)
        {
            enemyPosition = circleArea.enemyPositions[index];
            GameObject enemy = (GameObject)Instantiate(enemyPrefab, enemyPosition, Quaternion.Euler(0, 0, Random.Range(0, 120)), gameObject.transform);
            enemy.GetComponent<Enemy>().circleAreaPosition = circleArea.circleAreaPosition;
            enemy.GetComponent<Enemy>().radius = circleArea.radius;
        }

        for (int i = 0; i < redEnemies.Length; i++)
            Destroy(redEnemies[i]);

        redEnemies = new GameObject[waveNumber];
        for (int i = 0; i < waveNumber; i++)
        {
            angle = Random.Range(0f, 1f) * Mathf.PI * 2;
            GameObject redEnemy = (GameObject)Instantiate(redEnemyPrefab, new Vector2(Mathf.Cos(angle) * (circleArea.radius + Random.Range(5, 10)), Mathf.Sin(angle) * (circleArea.radius + Random.Range(5, 10))), Quaternion.identity);
            redEnemy.GetComponent<EnemyRed>().speed = Random.Range( 2f, waveNumber / 2f + 4f);
            redEnemy.GetComponent<EnemyRed>().rotationSpeed = Random.Range(20, 80);
            redEnemies[i] = redEnemy;
        }

        enemiesKilled = 0;
    }
}
