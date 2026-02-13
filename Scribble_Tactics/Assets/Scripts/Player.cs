using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Input = UnityEngine.Input;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] Camera myCamera;
    private bool isSelected = false;
    Vector3 mousePos;
    RaycastHit rayCastHit;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           
        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(myCamera.ScreenPointToRay(Input.mousePosition), out rayCastHit, Mathf.Infinity, collisionMask))
            { 
                if (!isSelected)
                {
                    isSelected = true;
                    Debug.Log(isSelected);
                }
                
                /*else if (isSelected == true)
                {
                    mousePos.z = transform.position.z;
                    worldPos = myCamera.ScreenToWorldPoint(mousePos);
                    transform.position = worldPos;
                    isSelected = false;
                }
                */
            }

            else if (!Physics.Raycast(myCamera.ScreenPointToRay(Input.mousePosition), out rayCastHit, Mathf.Infinity, collisionMask))
            {
                if (!isSelected)
                {
                    //Do nothing
                }

                else if (isSelected)
                {
                    mousePos = myCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = transform.position.z;
                    transform.position = mousePos;
                    Debug.Log(mousePos);
                    isSelected = false;
                }
                
            }

        }
        
    }
}
