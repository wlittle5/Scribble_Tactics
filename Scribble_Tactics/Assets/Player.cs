using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Input = UnityEngine.Input;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{

    [SerializeField] Camera myCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit rayCastHit;

            Vector3 mousePos = Input.mousePosition;
            Physics.Raycast(myCamera.ScreenPointToRay(mousePos), out rayCastHit);
 
            Debug.Log(mousePos);
            Debug.Log(rayCastHit);
        }
        /*if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Clicked: " + hit.collider.gameObject.name);
                // Add your logic here (e.g., trigger an event, change color, etc.)
            }
        }
        if (isSelected == 3)
            if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to world position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z; // Maintain Z position

            // Move object to mouse click position
            transform.position = mousePosition;

            isSelected = 1;
        }
        */
    }
}
