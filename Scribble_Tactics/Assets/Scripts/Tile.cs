using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;
    public Color hoverColor = Color.yellow;
    public Color selectedColor = Color.green;

    private bool isSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    private void OnMouseEnter()
    {
        if (!isSelected)
        {
            rend.material.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        if (!isSelected)
        {
            rend.material.color = originalColor;
        }
    }

    private void OnMouseDown()
    {
        isSelected = !isSelected;
        rend.material.color = isSelected ? selectedColor : originalColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
