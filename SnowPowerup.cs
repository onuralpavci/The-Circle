using UnityEngine;

public class SnowPowerup : MonoBehaviour
{
    Color darkBlueColor = new Color(0.0221f, 0.5158f, 0.6698f, 1);
    Color lightBlueColor = new Color(0.0378f, 0.8290f, 0.9811f, 1);
    private void Start()
    {
        PlayerPrefs.SetInt("SnowPowerupExists", 1);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
                FindObjectOfType<AudioManager>().PlayWithPitch("ButtonSound", 2f);

            foreach (var enemy in GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyWaveController>().redEnemies)
            {
                enemy.GetComponent<EnemyRed>().speed = 0;
                if (PlayerPrefs.GetString("selectedMode", "Light") == "Light")
                    enemy.GetComponent<SpriteRenderer>().color = darkBlueColor;
                else
                    enemy.GetComponent<SpriteRenderer>().color = lightBlueColor;
            }

            PlayerPrefs.SetInt("SnowPowerupExists", 0);
            Destroy(this.gameObject);
        }

    }

}
