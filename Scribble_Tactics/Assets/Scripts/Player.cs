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
    public static event EventHandler<OnPlayerSelectArgs> OnPlayerSelect;
    public class OnPlayerSelectArgs : EventArgs
    {
        public Player player;
        public bool isSelected;
    }

    public static event EventHandler<OnBattleInitiateArgs> OnBattleInitiate;
    public class OnBattleInitiateArgs : EventArgs
    {
        public Player player;
    }
    
    [SerializeField] GameObject range;
    [SerializeField] float moveSpeed = 1.0f;

    private bool isSelected = false;
    private bool canMove = false;
    private bool isMoving = false;

    private Vector3 mousePos;
    private RaycastHit targetObject; 

    private void Start()
    {
        ClickLogic.Instance.OnMouseClicked += ClickLogic_OnMouseClicked;
    }

    private void Update()
    {
        if (canMove)
            MoveCharacter(targetObject);
    }

    private void ClickLogic_OnMouseClicked(object sender, ClickLogic.OnMouseClickedEventArgs e)
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
            OnPlayerSelect?.Invoke(this, new OnPlayerSelectArgs { player = this, isSelected = isSelected });

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
            OnPlayerSelect?.Invoke(this, new OnPlayerSelectArgs { player = this, isSelected = isSelected });

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
        OnBattleInitiate?.Invoke(this, new OnBattleInitiateArgs { player = this });
    }
}
