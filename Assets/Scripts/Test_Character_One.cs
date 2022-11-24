using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Character_One : characterTemplate
{
    float strikePowerOne;

    // Start is called before the first frame update


    protected override void Start()
    {
        base.Start();
        maxHealth = 100;
        health = maxHealth;
        strikePowerOne = 5;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        attack(strikePowerOne);
    }








}
