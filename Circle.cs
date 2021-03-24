using UnityEngine;

public class Circle : MonoBehaviour
{
    public Transform player;
    public Vector3 lastPoint;
    public Vector3 oneBeforeLastPoint;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        lastPoint = player.GetComponent<LineRenderer>().GetPosition(player.GetComponent<LineRenderer>().positionCount - 1);
        oneBeforeLastPoint = player.GetComponent<LineRenderer>().GetPosition(player.GetComponent<LineRenderer>().positionCount - 2);
        transform.position = lastPoint;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(lastPoint.x - oneBeforeLastPoint.x, oneBeforeLastPoint.y - lastPoint.y)) * Mathf.Rad2Deg);
    }
}
