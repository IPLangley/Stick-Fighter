using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;

public class ColorChange : MonoBehaviour
{
    private Renderer rend;
    // private Image image;

    [SerializeField]
    private Color colorToTurnTo = Color.white;

    private void Update()
    {
        rend = GetComponent<Renderer>();
        // GetComponent<Image>().color = colorToTurnTo;
        rend.material.color = colorToTurnTo;
    }
}
