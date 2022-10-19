using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Punching_Bag : MonoBehaviour
{
    [SerializeField]
    float health;
    [SerializeField]
    float maxHealth = 999;

    public Image hpBar;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth; 
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.fillAmount = health / maxHealth;
    }


    public void damage(float amnt)
    {
        health -= amnt;
        Debug.Log("The Bag Took Damage");
    }
}
