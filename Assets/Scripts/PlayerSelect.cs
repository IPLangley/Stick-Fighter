using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
    // [SerializeField] Sprite[] Players;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject panel;

    int temp;

    public void CharSelect(Sprite character)
    {
        // if(temp > 1) panel.SetActive(false);

        if(temp == 0) 
            player1.GetComponent<SpriteRenderer>().sprite = character;
        else
            player2.GetComponent<SpriteRenderer>().sprite = character;
        temp++;

        if(temp > 1) panel.SetActive(false);
        // Players[temp] = character;
        // Debug.Log(Players[temp].name);
        // temp++;
        // if(temp >= 2) temp = 0; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
