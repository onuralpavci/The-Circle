using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float rotationSpeed;

    public float playerRotationInDegrees;
    public float playerRotationInRadians;

    Vector2 playerDirection;
    Vector2 joystickDirection;

    public float angle;

    public Joystick joystick;

    bool speedDecreased;

    public GameObject circle;

    // Start is called before the first frame update
    void Start()
    {
        circle = GameObject.FindGameObjectWithTag("Circle");
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, speed);
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
    }


    void FixedUpdate()
    {
        if( PlayerPrefs.GetString("SelectedController" , "joystick") == "arrows")
        {
            joystick.enabled = false;

            playerRotationInDegrees = transform.eulerAngles.z;
            playerRotationInRadians = playerRotationInDegrees / Mathf.Rad2Deg;
            rb.velocity = new Vector2(-speed * Mathf.Sin(playerRotationInRadians), speed * Mathf.Cos(playerRotationInRadians));
            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x > Screen.width * 0.5f)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, playerRotationInDegrees - rotationSpeed * Time.deltaTime));
                }
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, playerRotationInDegrees + rotationSpeed * Time.deltaTime));
                }
            }



        }
        else
        {
            playerDirection = new Vector2(rb.velocity.x, rb.velocity.y);
            joystickDirection = new Vector2(joystick.Horizontal, joystick.Vertical);
            angle = Vector2.Angle(playerDirection, joystickDirection);
            RotatePlayer();

            playerRotationInDegrees = transform.eulerAngles.z;
            playerRotationInRadians = playerRotationInDegrees / Mathf.Rad2Deg;
            rb.velocity = new Vector2(-speed * Mathf.Sin(playerRotationInRadians), speed * Mathf.Cos(playerRotationInRadians));
        }
        

    }

    private void RotatePlayer()
    {
        if( angle < 10 && !speedDecreased)
        {
            rotationSpeed = rotationSpeed / 3;
            speedDecreased = true;
        }

        if( angle >= 10 && speedDecreased)
        {
            rotationSpeed = rotationSpeed * 3;
            speedDecreased = false;
        }

        if (angle > 1)
        {
            //Both Facing Right
            if (playerDirection.x > 0 && joystickDirection.x > 0)
            {
                //Clockwise
                if (Vector2.Angle(playerDirection, Vector2.up) < Vector2.Angle(joystickDirection, Vector2.up))
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, playerRotationInDegrees - rotationSpeed * Time.deltaTime));
                }
                //Anti Clokcwise
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, playerRotationInDegrees + rotationSpeed * Time.deltaTime));
                }
            }
            //Player faces right, joystick faces left
            else if (playerDirection.x > 0 && joystickDirection.x < 0)
            {
                //Clockwise
                if (Vector2.Angle(joystickDirection, Vector2.up) > Vector2.Angle(-playerDirection, Vector2.up))
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, playerRotationInDegrees - rotationSpeed * Time.deltaTime));
                }
                //Anti Clockwise
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, playerRotationInDegrees + rotationSpeed * Time.deltaTime));
                }
            }
            //Both Faces Left
            else if (playerDirection.x < 0 && joystickDirection.x < 0)
            {
                //Clockwsie
                if (Vector2.Angle(playerDirection, Vector2.up) > Vector2.Angle(joystickDirection, Vector2.up))
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, playerRotationInDegrees - rotationSpeed * Time.deltaTime));
                }
                //Anti Clockwise
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, playerRotationInDegrees + rotationSpeed * Time.deltaTime));
                }
            }
            //Player faces left, joystick faces right
            else
            {
                //Clockwise
                if (Vector2.Angle(joystickDirection, Vector2.up) < Vector2.Angle(-playerDirection, Vector2.up))
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, playerRotationInDegrees - rotationSpeed * Time.deltaTime));
                }
                //Anti Clockwise
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, playerRotationInDegrees + rotationSpeed * Time.deltaTime));
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GameOver();

            if ( PlayerPrefs.GetInt("isChallange") == 0)
            {
                if (PlayerPrefs.GetInt("highestScore", 0) < EnemyWaveController.highScore)
                    PlayerPrefs.SetInt("highestScore", EnemyWaveController.highScore);
            }

            Instantiate(Resources.Load("particles/Crush Particle"), transform.position, Quaternion.identity);
            GetComponent<SpriteRenderer>().enabled = false;
            foreach (var collider in GetComponents<Collider2D>())
            {
                collider.enabled = false;
            }
            circle.GetComponent<Collider2D>().enabled = false;

            if (PlayerPrefs.GetInt("SoundEnabled") == 1)
            {
                FindObjectOfType<AudioManager>().Play("CrushSound");
            }
                

            if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
            {
                FindObjectOfType<AudioManager>().PauseWithFadeOut("GameMusic2", 2f);
                FindObjectOfType<AudioManager>().PlayWithFadeIn("GameMusic", 5f);
            }

        }

        else if (collision.gameObject.CompareTag("EnemyRed"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GameOver();

            if (PlayerPrefs.GetInt("highestScore", 0) < EnemyWaveController.highScore)
                PlayerPrefs.SetInt("highestScore", EnemyWaveController.highScore);

            Instantiate(Resources.Load("particles/Crush Particle Red"), transform.position, Quaternion.identity);
            GetComponent<SpriteRenderer>().enabled = false;
            foreach (var collider in GetComponents<Collider2D>())
            {
                collider.enabled = false;
            }   

            circle.GetComponent<Collider2D>().enabled = false;

            if (PlayerPrefs.GetInt("SoundEnabled") == 1)
            {
                FindObjectOfType<AudioManager>().Play("CrushSound");
            }
                

            if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
            {
                FindObjectOfType<AudioManager>().PauseWithFadeOut("GameMusic2", 2f);
                FindObjectOfType<AudioManager>().PlayWithFadeIn("GameMusic", 5f);
            }

        }
    }

}
