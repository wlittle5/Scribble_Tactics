using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Input = UnityEngine.Input;
using UnityEngine.Windows;
using System;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public event EventHandler OnSelected;
    public event EventHandler OnDeSelected;

    public static Player Instance { get; private set; }
    
    [SerializeField] GameObject range;
    [SerializeField] float moveSpeed = 0.5f;


    private bool isSelected = false;
    private bool canMove = false;
    private bool isMoving = false;
    private int hitData;

    private RaycastHit rayCastHit;
    private Vector3 mousePos;

    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && isMoving != true)
        {
            GetLayer();
            canMove = MoveCheck();
        }

        if (canMove)
        {
            MoveCharacter();
        }
    }

    private void GetLayer()
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(myRay, out rayCastHit);
        hitData = rayCastHit.transform.gameObject.layer;
    }

    private bool MoveCheck()
    {

        if ((hitData == 6) && !isSelected)
        {
            isSelected = true;
            ShowRange();
            return false;
        }

        if ((hitData == 7) && isSelected)
        {
            return true;
        }

        if ((hitData != 7) || (hitData != 6) && isSelected)
        {
            isSelected = false;
            HideRange();
            return false;
        }

        else
        {
            if (!isSelected)
            {
                HideRange();
            } 

            return isSelected;
        }
    }

    private void MoveCharacter()
    {
        HideRange();
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

    private void ShowRange()
    {
        range.gameObject.SetActive(true);
        OnSelected?.Invoke(this, EventArgs.Empty);
    }

    private void HideRange()
    {
        range.gameObject.SetActive(false);
        OnDeSelected?.Invoke(this, EventArgs.Empty);
    }
    public bool IsSelected()
    {
        return isSelected;
    }

    public bool IsMoving() 
    { 
        return isMoving;
    }

}
