using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Input = UnityEngine.Input;
using UnityEngine.Windows;
using System;
using UnityEngine.Events;

public class EnemyLogic : MonoBehaviour
{
    public event EventHandler EnemyOnSelected;
    public event EventHandler EnemyOnDeSelected;

    public static EnemyLogic Instance { get; private set; }
    
    [SerializeField] GameObject range;

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
    }

    private void GetLayer()
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(myRay, out rayCastHit);
        hitData = rayCastHit.transform.gameObject.layer;
    }

    private bool MoveCheck()
    {

        if ((hitData == 9) && !isSelected)
        {
            isSelected = true;
            ShowRange();
            return false;
        }

        if ((hitData == 7) && isSelected)
        {
            return true;
        }

        if (((hitData != 7) || (hitData != 9)) && isSelected)
        {
            EnemyOnDeSelected?.Invoke(this, EventArgs.Empty);
            isSelected = false;
            HideRange();
            return false;
        }

        else
        {
            return isSelected;
        }
    }

    private void ShowRange()
    {
        range.gameObject.SetActive(true);
        EnemyOnSelected?.Invoke(this, EventArgs.Empty);
    }

    private void HideRange()
    {
        range.gameObject.SetActive(false);
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
