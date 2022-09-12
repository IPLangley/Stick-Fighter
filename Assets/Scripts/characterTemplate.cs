using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterTemplate : MonoBehaviour
{

    
    //Reference to the player's rigid body component
    Rigidbody2D playerRigidBody;
    SpriteRenderer playerRenderer;

    //Descirbes which player is playing  IE (Player 1 vs Player 2);
    int playerSlot;
    //Describes player speed
    [SerializeField]
    float movementSpeed;
    //Describes player jumpHeight
    float jumpHeight;
    //Describes player maximum health
    [SerializeField]
    float maxHealth;
    //Describes player's current health
    float health;
    //horizontal player input
    [SerializeField]
    float input_x;
    //Reading if player is touching ground
    bool grounded;


    //Ray cast for grounding
    //Ray Length
    float groundedCheckLength;
    //Information on what components are considered part of the "floor"
    public LayerMask floor;







    // Start is called before the first frame update
    void Start()
    {
        //set all starting values to their initial
        //we should have all values mapped to an initial value which can change for a per character system
        //IE character 1 will have a movepseed of 5, while character 2 has a movemspeed of 6
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;
        movementSpeed = 5;
        jumpHeight = 5;

        //Should be set per character as well if we have different height characters
        groundedCheckLength = 1.3f;

        
    }

    // Update is called once per frame
    void Update()
    {

        //Set the players horizontal movement
        input_x = Input.GetAxisRaw("Horizontal");
        //Move Player
        if (input_x != 0)
        {
            //move the player
            move(input_x);
        }
        else if(grounded)
        {
            //stop the player if they are touching the ground
            playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
        }
   
        //Check if player is grounded
        checkGrounding();
        //Allow the player to jump
        jump();



    }


    //Takes in horizontal input and moves player
    public void move(float input)
    {
        //Flip sprite if moving to the right
        playerRenderer.flipX = (input < 0);
        //Move player respective to input by movement speed
        playerRigidBody.velocity = new Vector2(input * movementSpeed, playerRigidBody.velocity.y);
    }


    //Makes player jump
    public void jump()
    {
        //read in if player is trying to jump
        if (Input.GetKeyDown(KeyCode.Space))
 
        {
            //see if player can jump
            if (grounded == true)
            {
                //add velocity to player based on their jumpheight
                playerRigidBody.velocity = playerRigidBody.velocity + Vector2.up * jumpHeight;
                //set player unable to jump
                grounded = false;
            }
        }


    }

    public void checkGrounding()
    {
        //Check for grounding
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundedCheckLength, floor);
        
        //setting grounding to true or false
        if (hit.collider != null)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
    

    public void attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //attack
            Debug.Log("ATTACK");
        }
    }

    public void block()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //block
            Debug.Log("BLOCK");
        }
    }

}




