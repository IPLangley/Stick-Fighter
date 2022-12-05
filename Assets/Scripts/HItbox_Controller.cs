using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HItbox_Controller : MonoBehaviour
{
    
    // Start is called before the first frame update
    public PlayerCombat player;
    public Rigidbody2D playerRigidBody;
    public GameObject hitMark;
    public float hitmarkDecay;
    private PlayerCombat otherPlayer;
    float power;

    private void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            GameObject lastHitMark;
            Vector3 collisionPoint = otherCollider.ClosestPoint(transform.position);
            otherPlayer = otherCollider.gameObject.GetComponent<PlayerCombat>();
            executeAttack();
            lastHitMark = Instantiate(hitMark, collisionPoint, new Quaternion());
            Destroy(lastHitMark, hitmarkDecay);
        }
    }

    private void executeAttack()
    {
        power = player.attackDmg;
        otherPlayer.Damage(power);
        Debug.Log("damaged");
    }

    public void DeleteFeedback()
    {

    }


}



