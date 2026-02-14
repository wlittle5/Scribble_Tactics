using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Input = UnityEngine.Input;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    [SerializeField] Camera myCamera;
    [SerializeField] float moveSpeed = 0.5f;
    
    private bool isSelected = false;
    private bool canMove = false;
    private bool isMoving = false;
    private int hitData;

    private RaycastHit rayCastHit;
    private Vector3 mousePos;

    void Update()
    {  
        if (Input.GetMouseButtonUp(0) && isMoving != true)
        {       
            GetLayer();
            canMove = MoveCheck();    
        }

        if(canMove)
            MoveCharacter();
    }

    private void GetLayer()
    {
        Ray myRay = myCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(myRay, out rayCastHit);
        hitData = rayCastHit.transform.gameObject.layer;
    }

    private bool MoveCheck()
    {
        if ((hitData == 6) && !isSelected)
        {
            isSelected = true;
            return false;
        }

        if ((hitData == 8) && isSelected)
        {
            return true;
        }

        if ((hitData != 8) || (hitData != 6) && isSelected)
        {
            isSelected = false;
            return false;
        }

        else
        {
            return isSelected;
        }
    }

    private void MoveCharacter()
    {
        isMoving = true;
        float step = moveSpeed * Time.deltaTime;
        mousePos = rayCastHit.point;
        mousePos.z = transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, mousePos, step);

        if (transform.position == mousePos)
        {
            isSelected = false;
            canMove = false;
            isMoving = false;   
        }
    }
}
