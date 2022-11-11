using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sprite;

    public Vector3 boxSize; // size of ground check box
    public float maxDist; // The max distance of the ground check
    public LayerMask layerMask; // The layer that ground check takes place on
    public float jumpForce; // Impulse force for jumping
    public float moveAcceleration; // Acceleration force for horizontal movement
    public float velocityLimit;

    private float VEL_LIM_SQR; // the square of the velocity limit


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        VEL_LIM_SQR = velocityLimit * velocityLimit;
    }

    // FixedUpdate is called once per physics tick
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
#pragma warning disable CS0642 // Possible mistaken empty statement
            ; // Do nothing due to conflicting inputs
#pragma warning restore CS0642 // Possible mistaken empty statement

        // Restore drag to player when no horizontal input is given and is on the ground
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && isGrounded())
            rb.drag = velocityLimit * 0.5f;

        // Limit the overall velocity
        if (rb.velocity.sqrMagnitude > VEL_LIM_SQR)
            rb.velocity = rb.velocity.normalized * velocityLimit;
    }

    private void OnDrawGizmos() // For visualizing the boxcast for ground check
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up*maxDist, boxSize);
    }
    private bool isGrounded() // Checks if the attached object (player) is touching the ground.
    {
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDist, layerMask))
        {
            return true;
        }
        else
        {
            rb.drag = 0f;
            return false;
        }
    }
    private void Jump()
    {
        rb.drag = 0f;
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    private void MoveLeft()
    {
        if (transform.localScale.x > 0) // Flips the player to face other direction
            transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
        rb.drag = 0f; // Temporarily remove drag for better control
        if (!isGrounded())
            rb.AddForce(-transform.right * moveAcceleration * 0.5f, ForceMode2D.Force);
        else
            rb.AddForce(-transform.right * moveAcceleration, ForceMode2D.Force);
    }
    private void MoveRight()
    {
        if (transform.localScale.x < 0) // Flips the player to face other direction
            transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
        rb.drag = 0f; // Temporarily remove drag for better control
        if (!isGrounded())
            rb.AddForce(transform.right * moveAcceleration * 0.5f, ForceMode2D.Force);
        else
            rb.AddForce(transform.right * moveAcceleration, ForceMode2D.Force);
    }
}
