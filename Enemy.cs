using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool inTouchWithObstacle = false;

    public Vector2 circleAreaPosition;
    public float radius;
    public float randomAngle;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Circle"))
        {
            EnemyController.enemiesKilled += 1;
            EnemyWaveController.enemiesKilled += 1;
            EnemyWaveController.highScore += 1;

            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + 3);
            GameObject explosionParticle = (GameObject) Instantiate(Resources.Load("particles/Explosion Particle"), transform.position, Quaternion.identity);
            if (PlayerPrefs.GetInt("SoundEnabled" , 1) == 0)
                explosionParticle.GetComponent<AudioSource>().volume = 0;

                /**
                  if( PlayerPrefs.GetInt("SoundEnabled") == 1)
                    FindObjectOfType<AudioManager>().Play("ExplosionSound" + PlayerPrefs.GetInt("currentExplosionSound" , 1) );

                if (PlayerPrefs.GetInt("currentExplosionSound", 1) == 3)
                    PlayerPrefs.SetInt("currentExplosionSound", 1);
                else
                    PlayerPrefs.SetInt("currentExplosionSound", PlayerPrefs.GetInt("currentExplosionSound", 1) + 1);
                */

                Destroy(this.gameObject);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            inTouchWithObstacle = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            inTouchWithObstacle = false;
        }
    }

    private void Update()
    {
        if(inTouchWithObstacle)
        {
            randomAngle = Random.Range(0f, 1f) * 2 * Mathf.PI;
            transform.position = new Vector2(radius * Mathf.Sqrt(Random.Range(0f, 1f)) * Mathf.Cos(randomAngle), radius * Mathf.Sqrt(Random.Range(0f, 1f) * Mathf.Sin(randomAngle))) + circleAreaPosition;
        }
    }
}
