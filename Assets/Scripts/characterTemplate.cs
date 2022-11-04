using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterTemplate : MonoBehaviour
{
    //Reference to the player's rigid body component
    Rigidbody2D playerRigidBody;
    SpriteRenderer playerRenderer;

    //Describes which player is playing  IE (Player 1 vs Player 2);
    public int playerSlot;
    //Describes player speed
    [SerializeField]
    public float movementSpeed;
    //Describes player jumpHeight
    public float jumpHeight;
    //Describes player maximum health
    [SerializeField]
    public float maxHealth;
    //Describes player's current health
    [SerializeField]
    public float health;
    //horizontal player input
    [SerializeField]
    float input_x;
    //Reading if player is touching ground
    public bool isGrounded;
    //Blocking check
    public bool isBlocking;
    //Block Damage Reduction represented as a percentage of the damage mitigated
    public float blockAmount;
    //Knock Back Resistance represented as a percentage of the knockback mitigated
    public float knockbackResist;
    //Strength of current attack
    public float attackPower;
    //CurrentHitbox
    public GameObject atkBox;
    //Animator
    public Animator anim;
    //MoveCheck
    public bool canmove;


    //Ray cast for grounding
    //Ray Length
    float groundedCheckLength;
    //Information on what components are considered part of the "floor"
    public LayerMask floor;

    //Refence to HP bar such that it can update whilst player health decreases
    public Image hpBar;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //set all starting values to their initial
        //we should have all values mapped to an initial value which can change for a per character system
        //IE character 1 will have a movepseed of 5, while character 2 has a movemspeed of 6
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        health = maxHealth;
        movementSpeed = 5;
        jumpHeight = 5;
        canmove = true;

        //Should be set per character as well if we have different height characters
        groundedCheckLength = 1.3f;
        atkBox.gameObject.SetActive(false);

        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        hpUpdate();
        //Set the players horizontal movement
        input_x = Input.GetAxisRaw("Horizontal");
        //Move Player
        if (input_x != 0 && canmove)
        {
            anim.SetBool("Moving", true);
            //move the player
            move(input_x);
        }
        if (isGrounded && input_x != 0)
        {
            anim.SetBool("Jumping", false);
        }
        else if (isGrounded)
        {
            anim.SetBool("Jumping", false);
            anim.SetBool("Moving", false);
            //stop the player if they are touching the ground
            playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
        }
   
        //Check if player is grounded
        checkGrounding();
        //Allow the player to jump
        jump();
        block();
        //attack();


    }

    //Takes in horizontal input and moves player
    protected virtual void move(float input)
    {
        //Flip sprite if moving to the right
        playerRenderer.flipX = (input < 0);
        if(input <= 0)
        {
            atkBox.GetComponent<CircleCollider2D>().offset = new Vector2(-0.09f, 0.02f);
        }
        else
        {
            atkBox.GetComponent<CircleCollider2D>().offset = new Vector2(0.09f, 0.02f);
        }
        //Move player respective to input by movement speed
        playerRigidBody.velocity = new Vector2(input * movementSpeed, playerRigidBody.velocity.y);
        
    }

    //Makes player jump
    protected virtual void jump()
    {
        //read in if player is trying to jump
        if (Input.GetKeyDown(KeyCode.Space))
 
        {
            //see if player can jump
            if (isGrounded == true)
            {
                anim.SetBool("Jumping", true);
                //add velocity to player based on their jumpheight
                playerRigidBody.velocity = playerRigidBody.velocity + Vector2.up * jumpHeight;
                //set player unable to jump
                isGrounded = false;
            }
        }


    }

    protected virtual void checkGrounding()
    {
        //Check for grounding
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundedCheckLength, floor);
        
        //setting grounding to true or false
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    public virtual void attack(float amnt)
    {
     
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            canmove = false;
            attackPower = amnt;
            startAttack();
            //attack
            Debug.Log("ATTACK");
            Invoke("resetAttack", 0.4f);

        }
        //attackPower = 0;
    }

    public virtual void block()
    {
        //See if the player is inputing
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //Set blocking to true
            isBlocking = true;
            //block
            Debug.Log("BLOCK");
        }
        //Set blocking to false at the end of block
        isBlocking = false;
    }

    public void damage(float amnt)
    {
        if (isBlocking)
        {
            amnt *= blockAmount;
        }
        health -= amnt;
    }

    public void knockBack(float amnt)
    {
        if (isBlocking)
        {
            amnt *= knockbackResist;
        }
        playerRigidBody.velocity = new Vector2(amnt, 0);
    }

    public void hpUpdate()
    {
        hpBar.fillAmount = health / maxHealth;
    }

    public void resetAttack()
    {
        atkBox.SetActive(false);
        anim.SetBool("Attack_Basic", false);
        canmove = true;
    }

    public void startAttack()
    {
        atkBox.SetActive(true);
        anim.SetBool("Attack_Basic", true);
    }

}




