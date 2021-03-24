using UnityEngine;

public class EnlargePowerup : MonoBehaviour
{
    GameObject circle;
    float startTime;
    public float increaseRate;
    public float stayTime;
    public float increaseSpeed;

    float targetSize;
    float originalSize;

    private void Start()
    {
        startTime = 0;
        PlayerPrefs.SetInt("EnlargePowerupExists", 1);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
                FindObjectOfType<AudioManager>().PlayWithPitch("ButtonSound" , 2f);

            circle = GameObject.FindGameObjectWithTag("Circle");
            originalSize = circle.transform.localScale.x;
            targetSize = originalSize * (increaseRate + 1 - (PlayerPrefs.GetInt("circleSize", 10) / 400f) );
            startTime = Time.time;

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }

    }

    private void Update()
    {
        if( originalSize < targetSize)
        {
            circle.transform.localScale = circle.transform.localScale + new Vector3(Time.deltaTime * increaseSpeed, Time.deltaTime * increaseSpeed, 0);
            originalSize = circle.transform.localScale.x;
        }

        if( startTime > 0 && Time.time - startTime > stayTime)
        {
            circle.transform.localScale = circle.transform.localScale / (increaseRate + 1 - (PlayerPrefs.GetInt("circleSize", 10) / 400f)); ;
            PlayerPrefs.SetInt("EnlargePowerupExists", 0);
            Destroy(this.gameObject);
        }
    }
}
