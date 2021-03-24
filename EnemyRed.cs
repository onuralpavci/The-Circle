using UnityEngine;

public class EnemyRed : MonoBehaviour
{

    public Transform player;
    public float speed;

    Rigidbody2D rb;
    public float rotationSpeed;

    float enemyRotationInDegrees;
    float enemyRotationInRadians;

    Vector2 enemyDirection;
    Vector2 fromEnemyToPlayerDirection;

    float angle;
    bool speedDecreased;
    public GameObject crushClosingAnimation;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, speed);

    }



    void FixedUpdate()
    {
        enemyDirection = new Vector2(rb.velocity.x, rb.velocity.y);
        fromEnemyToPlayerDirection = player.position - transform.position;
        angle = Vector2.Angle(enemyDirection, fromEnemyToPlayerDirection);
        RotatePlayer();

        enemyRotationInDegrees = transform.eulerAngles.z;
        enemyRotationInRadians = enemyRotationInDegrees / Mathf.Rad2Deg;
        rb.velocity = new Vector2(-speed * Mathf.Sin(enemyRotationInRadians), speed * Mathf.Cos(enemyRotationInRadians));

    }

    private void RotatePlayer()
    {
        if (angle < 10 && !speedDecreased)
        {
            rotationSpeed = rotationSpeed / 3;
            speedDecreased = true;
        }

        if (angle >= 10 && speedDecreased)
        {
            rotationSpeed = rotationSpeed * 3;
            speedDecreased = false;
        }

        if (angle > 1)
        {
            //Both Facing Right
            if (enemyDirection.x > 0 && fromEnemyToPlayerDirection.x > 0)
            {
                //Clockwise
                if (Vector2.Angle(enemyDirection, Vector2.up) < Vector2.Angle(fromEnemyToPlayerDirection, Vector2.up))
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, enemyRotationInDegrees - rotationSpeed * Time.deltaTime));
                }
                //Anti Clokcwise
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, enemyRotationInDegrees + rotationSpeed * Time.deltaTime));
                }
            }
            //Player faces right, joystick faces left
            else if (enemyDirection.x > 0 && fromEnemyToPlayerDirection.x < 0)
            {
                //Clockwise
                if (Vector2.Angle(fromEnemyToPlayerDirection, Vector2.up) > Vector2.Angle(-enemyDirection, Vector2.up))
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, enemyRotationInDegrees - rotationSpeed * Time.deltaTime));
                }
                //Anti Clockwise
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, enemyRotationInDegrees + rotationSpeed * Time.deltaTime));
                }
            }
            //Both Faces Left
            else if (enemyDirection.x < 0 && fromEnemyToPlayerDirection.x < 0)
            {
                //Clockwsie
                if (Vector2.Angle(enemyDirection, Vector2.up) > Vector2.Angle(fromEnemyToPlayerDirection, Vector2.up))
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, enemyRotationInDegrees - rotationSpeed * Time.deltaTime));
                }
                //Anti Clockwise
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, enemyRotationInDegrees + rotationSpeed * Time.deltaTime));
                }
            }
            //Player faces left, joystick faces right
            else
            {
                //Clockwise
                if (Vector2.Angle(fromEnemyToPlayerDirection, Vector2.up) < Vector2.Angle(-enemyDirection, Vector2.up))
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, enemyRotationInDegrees - rotationSpeed * Time.deltaTime));
                }
                //Anti Clockwise
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, enemyRotationInDegrees + rotationSpeed * Time.deltaTime));
                }

            }
        }
    }

}
