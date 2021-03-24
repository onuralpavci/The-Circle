using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    GameObject player;

    public Vector3 position;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    // Update is called once per frame
    void Update()
    {
        if( player.GetComponent<SpriteRenderer>().enabled == true)
        {
            transform.position = player.transform.TransformPoint(position);
            if(gameObject.CompareTag("MainCamera"))
                gameObject.GetComponent<Camera>().orthographicSize = 12;
        }
            
        else if( gameObject.CompareTag("MainCamera"))
            gameObject.GetComponent<Camera>().orthographicSize = gameObject.GetComponent<Camera>().orthographicSize + 0.5f;

        //adjusting the position of camera
        //by using cosine and sine of EulerAngle.z

        //adjusting the rotation accoring to player rotation
        //transform.rotation = Quaternion.Euler( player.eulerAngles);
    }
}
