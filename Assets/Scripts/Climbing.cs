using JetBrains.Annotations;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Rigidbody rb;
    public PlayerMovement pm;
    public LayerMask whatIsWall;

    [Header("Climbing")]
    public float climbSpeed;
    public float ledgeBoost = 5f;
    public float maxClimbTime;
    private float climbTimer;

    private bool climbing;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;

    private Transform lastwall;
    private Vector3 lastWallNormal;
    public float minWallNormalAngleChange;

    private void Update()
    {
        WallCheck();
        StateMachine();

        if (climbing) ClimbingMovement();
    }

    private void StateMachine()
    {
        if (wallFront && Input.GetAxisRaw("Vertical") > 0 && wallLookAngle < maxWallLookAngle)
        {
            if (!climbing && climbTimer > 0) StartClimbing();

            if (climbTimer > 0) climbTimer -= Time.deltaTime;
            if (climbTimer < 0) StopClimbing();
        }

        else
        {
            if (climbing)
            {
                Debug.Log ("Climb ended | wallFront: " + wallFront + " | angle: " + wallLookAngle + " | timer: " + climbTimer);

                if (!wallFront && Input.GetAxisRaw("Vertical") > 0)
                {
                    Debug.Log("Ledge Boost Fired");
                    rb.linearVelocity = new Vector3(rb.linearVelocity.x, ledgeBoost, rb.linearVelocity.z);
                }

                StopClimbing();
            }
        }
    }

    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);

        bool newWall = frontWallHit.transform != lastwall || Mathf.Abs(Vector3.Angle(frontWallHit.normal, lastWallNormal)) > minWallNormalAngleChange;

        if ((wallFront && newWall) || pm.IsGrounded())
        {
            climbTimer = maxClimbTime;
        }
    }

    private void StartClimbing()
    {
        climbing = true;

        lastwall = frontWallHit.transform;
        lastWallNormal = frontWallHit.normal;
    }

    private void ClimbingMovement()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, climbSpeed, rb.linearVelocity.z);
    }

    private void StopClimbing()
    {
        climbing = false;
    }
}
