using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Grabbed components
    Rigidbody2D rb;
    Animator anim;

    // Movement-related
    public Vector3 boxSize; // size of ground check box
    public float maxDist; // The max distance of the ground check
    public LayerMask layerMask; // The layer that ground check takes place on
    public float jumpForce; // Impulse force for jumping
    public float moveAcceleration; // Acceleration force for horizontal movement
    public float velocityLimit;

    // Input-related
    public int playerSlot;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode upKey;

    private float VEL_LIM_SQR; // the square of the velocity limit


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        VEL_LIM_SQR = velocityLimit * velocityLimit;
        anim = GetComponent<Animator>();
    }

    // FixedUpdate is called once per physics tick
    void FixedUpdate()
    {
        if (Input.GetKey(upKey) && isGrounded())
        {
            
            Jump();
        }
        if (Input.GetKey(rightKey))
        {
            AnimateRun();
            MoveRight();
        }
        if (Input.GetKey(leftKey))
        {
            AnimateRun();
            MoveLeft();
        }
        if (Input.GetKey(leftKey) && Input.GetKey(rightKey))
        {
            AnimateIdle(); // Set to idle animation due to conflicting inputs
        }

        // Restore drag to player when no horizontal input is given and is on the ground
        if (!Input.GetKey(leftKey) && !Input.GetKey(rightKey) && isGrounded())
        {
            AnimateIdle();
            rb.drag = velocityLimit * 0.5f;
        } else if (!isGrounded() && !anim.GetBool("Kicking") && !anim.GetBool("Attack_Basic"))
        {
            AnimateJump();
        }

        // Limit the overall velocity
        if (rb.velocity.sqrMagnitude > VEL_LIM_SQR)
            rb.velocity = rb.velocity.normalized * velocityLimit;
    }

    private void OnDrawGizmos() // For visualizing the boxcast for ground check
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up*maxDist, boxSize);
    }
    private bool isGrounded() // Checks if the attached player is touching the ground. Also plays jump animation if false.
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
        if (!isGrounded()) // If in the air, reduce movement acceleration
            rb.AddForce(-transform.right * moveAcceleration * 0.5f, ForceMode2D.Force);
        else
            rb.AddForce(-transform.right * moveAcceleration, ForceMode2D.Force);
    }
    private void MoveRight()
    {
        if (transform.localScale.x < 0) // Flips the player to face other direction
            transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
        rb.drag = 0f; // Temporarily remove drag for better control
        if (!isGrounded()) // If in the air, reduce movement acceleration
            rb.AddForce(transform.right * moveAcceleration * 0.5f, ForceMode2D.Force);
        else
            rb.AddForce(transform.right * moveAcceleration, ForceMode2D.Force);
    }
    private void AnimateJump()
    {
        if (!anim.GetBool("Jumping"))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("running", false);
            anim.SetBool("Blocking", false);
            anim.SetBool("Attack_Basic", false);
            anim.SetBool("Jumping", true);
            anim.SetBool("Kicking", false);
        }
    }
    private void AnimateIdle()
    {
        if (!anim.GetBool("Idle"))
        {
            anim.SetBool("running", false);
            anim.SetBool("Jumping", false);
            anim.SetBool("Blocking", false);
            anim.SetBool("Attack_Basic", false);
            anim.SetBool("Idle", true);
            anim.SetBool("Kicking", false);
        }
    }
    private void AnimateRun()
    {
        if (!anim.GetBool("running"))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Jumping", false);
            anim.SetBool("Blocking", false);
            anim.SetBool("Attack_Basic", false);
            anim.SetBool("running", true);
            anim.SetBool("Kicking", false);
        }
    }

}
