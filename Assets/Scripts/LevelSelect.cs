using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    Sprite Level;
    [SerializeField]
    GameObject bg;

    public void PickLevel(Sprite lvl){
        bg.GetComponent<SpriteRenderer>().sprite = lvl;
        // Debug.Log(Level.name);
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
