using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 3f;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector3(horizontalInput * movementSpeed, rb.linearVelocity.y, verticalInput * movementSpeed);

        bool moving = horizontalInput != 0 || verticalInput != 0;
        anim.SetBool("isRunning", moving && IsGrounded());

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
            anim.SetTrigger("Jump");
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
        }
    }
    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }

}