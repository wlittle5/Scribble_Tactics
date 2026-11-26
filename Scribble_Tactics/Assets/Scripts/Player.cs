using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Input = UnityEngine.Input;
using UnityEngine.Windows;
using System;
using UnityEngine.Events;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : MonoBehaviour
{
    
    [SerializeField] GameObject range;
    [SerializeField] float moveSpeed = 1.0f;

    private bool isSelected = false;
    private bool canMove = false;
    private bool isMoving = false;

    private Vector3 mousePos;
    private RaycastHit targetObject; 

    private void Start()
    {
        EventManager.Instance.OnMouseClicked += EventManager_OnMouseClicked;
    }

    private void Update()
    {
        if (canMove)
            MoveCharacter(targetObject);
    }

    private void EventManager_OnMouseClicked(object sender, EventManager.OnMouseClickedEventArgs e)
    {
        if (isMoving != true)
        {
            canMove = MoveCheck(e.objectClicked, e.isPlayer, e.isWithinRange, e.isWithinBounds, e.isEnemy);
            targetObject = (e.objectClicked);
        }   
    }

    private bool MoveCheck(RaycastHit objectClicked, bool isPlayer, bool isWithinRange, bool isWithinBoundary, bool isEnemy)
    {
        if (isPlayer && objectClicked.collider.gameObject == this.gameObject && !isSelected )
        {
            ShowRange();
            EventManager.Instance.PlayerSelected(new EventManager.OnPlayerSelectArgs { player = this, isSelected = isSelected });

            return false;
        }

        if (isWithinRange && isWithinBoundary && isSelected && !isEnemy)
        {
            return true;
        }

        if (isWithinRange && isWithinBoundary && isEnemy && isSelected)
        {
            CanBattle();
        }

        if ((!isPlayer || !isWithinRange) & isSelected)
        {
            isSelected = false;
            HideRange();
            EventManager.Instance.PlayerSelected(new EventManager.OnPlayerSelectArgs { player = this, isSelected = isSelected });

            return false;
        }

        else
        {
            return isSelected;
        }
    }

    private void MoveCharacter(RaycastHit objectClicked)
    {
        isMoving = true;
        HideRange();

        float step = moveSpeed * Time.deltaTime;
        mousePos = objectClicked.point;
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
        isSelected = true;

        range.gameObject.SetActive(true);
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
    
    private void CanBattle()
    {
        EventManager.Instance.InitiateBattle(new EventManager.OnBattleInitiateArgs { player = this });
    }
}
