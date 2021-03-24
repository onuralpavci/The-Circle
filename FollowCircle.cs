using UnityEngine;

public class FollowCircle : MonoBehaviour
{
    Transform circle;

    public Vector3 position;

    private void Start()
    {
        circle = GameObject.FindGameObjectWithTag("Circle").transform;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = circle.TransformPoint(position);
        //adjusting the position of camera
        //by using cosine and sine of EulerAngle.z

        //adjusting the rotation accoring to player rotation
        //transform.rotation = Quaternion.Euler( player.eulerAngles);
    }
}
