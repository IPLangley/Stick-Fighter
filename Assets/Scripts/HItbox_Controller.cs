using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HItbox_Controller : MonoBehaviour
{
    
    // Start is called before the first frame update
    public characterTemplate player;
    public Rigidbody2D playerRigidBody;
    private void Awake()
    {
        player = FindObjectOfType<characterTemplate>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Foo");
            collision.gameObject.GetComponent<characterTemplate>().damage(player.attackPower);

        }*/
        if (collision.gameObject.CompareTag("Destructable"))
        {
            Debug.Log("Bar");
            collision.gameObject.GetComponent<Punching_Bag>().damage(player.attackPower);
        }
    }
    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Foo");
            collision.gameObject.GetComponent<characterTemplate>().damage(player.attackPower);
        }
        if (collision.gameObject.CompareTag("Destructable"))
        {
            Debug.Log("Bar");
            collision.gameObject.GetComponent<Punching_Bag>().damage(player.attackPower);
        }
    }*/
}
