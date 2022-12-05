using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public KeyCode punchKey;
    public KeyCode blockKey;
    public KeyCode kickKey;
    public int PlayerSlot;
    public GameManager gm;
 
    public float maxHealth;
    public float health;

    bool blocking;
    bool attacking;
    public float attackDmg;
    float attackTime;
    public float attackCooldown;
    public float blockReduction;

    public Image hpBar;
    public GameObject punchBox;
    public GameObject kickBox;
    public Vector2 colliderOffset;
    public Collider2D punchCollider;
    public Collider2D kickCollider;

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
        if (Input.GetKeyDown(punchKey) && attackTime <= 0)
        {
            //activate hitbox and start animation
            StartPunch();
            Debug.Log("Punched!");
            Invoke(nameof(StopPunch), 0.7f);

        }
        if (Input.GetKeyDown(kickKey) && attackTime <= 0)
        {
            StartKick();
            Debug.Log("Kicked!");
            Invoke(nameof(StopKick), 0.7f);
        }
        if (Input.GetKeyDown(blockKey))
        {
            Debug.Log("Blocked!");
            StartBlock();
        }
        if (Input.GetKeyUp(blockKey))
        {
            Debug.Log("KeyReleased");
            StopBlock();
        }

        if (attackTime > 0)
        {
            attackTime -= Time.deltaTime;
        }
        hpBar.fillAmount = health / maxHealth;
    }

    //damage function
    public void Damage(float amnt)
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

    public void HpUpdate()
    {
        hpBar.fillAmount = health / maxHealth;
    }

    public void StartPunch()
    {
        punchBox.SetActive(true);
        punchCollider.offset += colliderOffset;
        anim.SetBool("Attack_Basic", true);
    }

    public void StopPunch()
    {
        punchBox.SetActive(false);
        punchCollider.offset -= colliderOffset;
        anim.SetBool("Attack_Basic", false);
        attackTime = attackCooldown;
    }

    public void StartKick()
    {
        kickBox.SetActive(true);
        kickCollider.offset += colliderOffset;
        AnimateKick();
    }

    public void StopKick()
    {
        kickBox.SetActive(false);
        kickCollider.offset -= colliderOffset;
        anim.SetBool("Kicking", false);
        attackTime = attackCooldown;
    }

    public void StartBlock()
    {
        anim.SetBool("Blocking", true);
        blocking = true;
    }

    public void StopBlock()
    {
        anim.SetBool("Blocking", false);
        blocking = false;
    }

    public void AnimateKick()
    {
        if (!anim.GetBool("Kicking"))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("running", false);
            anim.SetBool("Blocking", false);
            anim.SetBool("Attack_Basic", false);
            anim.SetBool("Jumping", false);
            anim.SetBool("Kicking", true);
        }
    }
}


