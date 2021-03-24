using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 6;

    Transform pivot;
    Transform pivotHook;

    HingeJoint2D hingeJoint2D;
    JointAngleLimits2D jointLimit2D;

    public float playerRotationInDegrees;
    public float playerRotationInRadians;
    public float anchorY = 0.3f;
    public float uncertainty = 1f;

    Vector2 anchorPosition;
    Vector2 vectorFromPivotToPlayer;

    Rigidbody2D playerRigidBody2D;
    Rigidbody2D pivotHookRigidBody2D;
    public bool reachedFullSpeed = true;

    private void Start()
    {
        pivot = GameObject.FindGameObjectWithTag("Pivot").transform;

        playerRigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        playerRigidBody2D.velocity = new Vector2(0, speed);

        pivotHook = pivot.GetChild(0);
        pivotHookRigidBody2D = pivot.GetChild(0).GetComponent<Rigidbody2D>();
    }

    private void Update()
	{
        if(!reachedFullSpeed)
        {
            playerRotationInDegrees = transform.eulerAngles.z;
            playerRotationInRadians = playerRotationInDegrees / Mathf.Rad2Deg;
            playerRigidBody2D.velocity = new Vector2(-speed * Mathf.Sin(playerRotationInRadians), speed * Mathf.Cos(playerRotationInRadians));
        }
            
        if (Input.GetMouseButtonDown(0))
        {
            hingeJoint2D = gameObject.AddComponent<HingeJoint2D>();
            playerRotationInDegrees = transform.eulerAngles.z;
            playerRotationInRadians = playerRotationInDegrees / Mathf.Rad2Deg;
            pivot.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

            pivot.GetComponent<SpriteRenderer>().enabled = true;
            hingeJoint2D.autoConfigureConnectedAnchor = false;
            hingeJoint2D.anchor = new Vector2(0, anchorY);


            anchorPosition = transform.TransformPoint(Vector2.up * anchorY);
            hingeJoint2D.connectedAnchor = pivot.transform.InverseTransformPoint(anchorPosition);

            vectorFromPivotToPlayer = new Vector2(hingeJoint2D.connectedAnchor.x, hingeJoint2D.connectedAnchor.y);
            //Left top quadrant
            if (playerRotationInDegrees >= 0 && playerRotationInDegrees < 90)
            {
                //clockwise
                if (Vector2.Angle(vectorFromPivotToPlayer, Vector2.up) > playerRotationInDegrees && hingeJoint2D.connectedAnchor.x < 0)
                {
                    if (hingeJoint2D.connectedAnchor.y > 0)
                    {
                        jointLimit2D.max = Mathf.Atan2(hingeJoint2D.connectedAnchor.y, -hingeJoint2D.connectedAnchor.x) * Mathf.Rad2Deg;
                        jointLimit2D.min = -playerRotationInDegrees - uncertainty ;
                    }
                    else
                    {
                        jointLimit2D.max = Mathf.Atan2(hingeJoint2D.connectedAnchor.y, -hingeJoint2D.connectedAnchor.x) * Mathf.Rad2Deg;
                        jointLimit2D.min = -playerRotationInDegrees - uncertainty;
                    }
                }
                //anti-clockwise
                else
                {
                    jointLimit2D.max = -Mathf.Atan2(hingeJoint2D.connectedAnchor.y, hingeJoint2D.connectedAnchor.x) * Mathf.Rad2Deg;
                    jointLimit2D.min = -playerRotationInDegrees + uncertainty;
                }
            }
            //Left bottom quadrant
            else if (playerRotationInDegrees >= 90 && playerRotationInDegrees < 180)
            {
                //anticlockwise
                if (Vector2.Angle(vectorFromPivotToPlayer, Vector2.up) < playerRotationInDegrees && hingeJoint2D.connectedAnchor.x < 0)
                {
                    jointLimit2D.max = -Mathf.Atan2(-hingeJoint2D.connectedAnchor.y, -hingeJoint2D.connectedAnchor.x) * Mathf.Rad2Deg - 180;
                    jointLimit2D.min = -playerRotationInDegrees + uncertainty;
                }
                //clockwise
                else
                {
                    jointLimit2D.max = Mathf.Atan2(-hingeJoint2D.connectedAnchor.y, hingeJoint2D.connectedAnchor.x) * Mathf.Rad2Deg - 180;
                    jointLimit2D.min = -playerRotationInDegrees - uncertainty;
                }
            }
            //Right bottom quadrant
            else if (playerRotationInDegrees >= 180 && playerRotationInDegrees < 270)
            {
                //clockwise
                if (Vector2.Angle(vectorFromPivotToPlayer, Vector2.up) < 360 - playerRotationInDegrees && hingeJoint2D.connectedAnchor.x > 0)
                {
                    jointLimit2D.max = -Mathf.Atan2(hingeJoint2D.connectedAnchor.y, hingeJoint2D.connectedAnchor.x) * Mathf.Rad2Deg + 180;
                    jointLimit2D.min = 360 - playerRotationInDegrees - uncertainty;
                }
                //anti clockwise
                else
                {
                    jointLimit2D.max = -Mathf.Atan2(-hingeJoint2D.connectedAnchor.y, -hingeJoint2D.connectedAnchor.x) * Mathf.Rad2Deg + 180;
                    jointLimit2D.min = 360 - playerRotationInDegrees + uncertainty;
                }
            }
            //Right top quadrant
            else
            {
                //anti clockwise
                if (Vector2.Angle(vectorFromPivotToPlayer, Vector2.up) > 360 - playerRotationInDegrees && hingeJoint2D.connectedAnchor.x > 0)
                {
                    if (hingeJoint2D.connectedAnchor.y > 0)
                    {
                        jointLimit2D.max = -Mathf.Atan2(hingeJoint2D.connectedAnchor.y, hingeJoint2D.connectedAnchor.x) * Mathf.Rad2Deg;
                        jointLimit2D.min = 360 - playerRotationInDegrees + uncertainty;
                    }
                    else
                    {
                        jointLimit2D.max = Mathf.Atan2(-hingeJoint2D.connectedAnchor.y, hingeJoint2D.connectedAnchor.x) * Mathf.Rad2Deg;
                        jointLimit2D.min = 360 - playerRotationInDegrees + uncertainty;
                    }
                }
                //clockwise
                else
                {
                    jointLimit2D.max = -Mathf.Atan2(hingeJoint2D.connectedAnchor.y, hingeJoint2D.connectedAnchor.x) * Mathf.Rad2Deg + 180;
                    jointLimit2D.min = 360 - playerRotationInDegrees - uncertainty;
                }
            }

            hingeJoint2D.useLimits = true;
            hingeJoint2D.limits = jointLimit2D;
            hingeJoint2D.connectedBody = pivot.GetChild(0).GetComponent<Rigidbody2D>();
            hingeJoint2D.enabled = true;
            reachedFullSpeed = false;


        }

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(hingeJoint2D);
            playerRigidBody2D.freezeRotation = true;
            playerRigidBody2D.freezeRotation = false;
            reachedFullSpeed = true;
            pivotHookRigidBody2D.freezeRotation = true;
            pivotHook.rotation = Quaternion.Euler(0, 0 ,0);
            pivotHookRigidBody2D.freezeRotation = false;
            pivot.GetComponent<SpriteRenderer>().enabled = false;
            transform.Rotate(new Vector3(0, 0, -360));
        }
    }

}
