using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    //Set Key for Attack Button
    public KeyCode attack;
    //Set Key for Block Button
    public KeyCode block;
    //Int to show player slot
    public int PlayerSlot;
    public GameManager gm;
 
    public float maxHealth;
    public float health;

    bool blocking;
    bool attacking;
    public float attackDmg;
    float attackTime;
    public float attackCooldown;
    float blockReduction;

    public Image hpBar;
    public GameObject atkBox;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        //Sets the Keys for the player side
        //setKeys(PlayerSlot);
        attackTime = 0;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(attack) && attackTime <= 0)
        {
            //activate hitbox and start animation
            startAttack();
            Debug.Log("Attacked!");
            Invoke("stopAttack", 0.7f);

        }

        if (Input.GetKeyDown(block))
        {
            Debug.Log("Blocked!");
            startBlock();
        }
        if (Input.GetKey(block))
        {
            blocking = true;
        }
        if (Input.GetKeyUp(block))
        {
            Debug.Log("KeyReleased");
            stopBlock();
        }

        if (attackTime > 0)
        {
            attackTime -= Time.deltaTime;
        }
        hpBar.fillAmount = health / maxHealth;

    }

    //
    /*void setKeys(int slot)
    {
        switch(slot)
        {
            case 1:
                attack = KeyCode.E;
                block = KeyCode.Q;
                break;
            case 2:
                attack = KeyCode.U;
                block = KeyCode.O;
                break;
        }
    }*/


    //damage function
    public void damage(float amnt)
    {
        if (blocking)
        {
            amnt *= blockReduction;
        }
        health -= amnt;
        if(health <= 0)
        {
            gm.GameOver(PlayerSlot);
        }
    }

    public void hpUpdate()
    {
        hpBar.fillAmount = health / maxHealth;
    }


    public void startAttack()
    {
        atkBox.SetActive(true);
        anim.SetBool("Attack_Basic", true);
    }

    public void stopAttack()
    {
        atkBox.SetActive(false);
        anim.SetBool("Attack_Basic", false);
        attackTime = attackCooldown;
    }

    public void startBlock()
    {
        anim.SetBool("Blocking", true);
    }

    public void stopBlock()
    {
        anim.SetBool("Blocking", false);
        blocking = false;
    }
}


