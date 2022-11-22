using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HItbox_Controller : MonoBehaviour
{
    
    // Start is called before the first frame update
    public PlayerCombat player;
    public Rigidbody2D playerRigidBody;
    float power;
    private void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Foo");
            collision.gameObject.GetComponent<characterTemplate>().damage(player.attackPower);

        }*/
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<PlayerCombat>();
            executeAttack();
        }
    }


    private void executeAttack()
    {
        power = player.attackDmg;
        player.damage(power);
        Debug.Log("damaged");
    }



}



