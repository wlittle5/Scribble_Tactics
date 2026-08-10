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
    public event EventHandler OnSelected;
    public event EventHandler OnDeSelected;

    public static Player Instance { get; private set; }
    
    [SerializeField] GameObject range;
    [SerializeField] float moveSpeed = 1.0f;

    const string PLAYER = "Player";
    const string RANGE = "Range";
    const string PAPER = "Paper";
    const string ENEMY = "Enemy";

    int playerLayer;
    int rangeLayer;
    int boundaryLayer;
    int enemyLayer;

    private bool isSelected = false;
    private bool canMove = false;
    private bool isMoving = false;

    private Vector3 mousePos;
    private RaycastHit targetObject; 

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ClickLogic.Instance.OnMouseClicked += ClickLogic_OnMouseClicked;

        GetLayers();
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
            canMove = MoveCheck(e.isPlayer, e.isWithinRange, e.isWithinBounds, e.isEnemy);
            targetObject = (e.objectClicked);
        }   
    }

    private bool MoveCheck(bool isPlayer, bool isWithinRange, bool isWithinBoundary, bool isEnemy)
    {
        if (isPlayer && !isSelected)
        {
            isSelected = true;
            ShowRange();
            return false;
        }

        if (isWithinRange && isWithinBoundary && isSelected && !isEnemy)
        {
            return true;
        }

        if (isWithinRange && isWithinBoundary && isEnemy && isSelected)
        {
            Debug.Log("It's time to d-d-d-d-d-duel!!!");
        }

        if ((!isPlayer || !isWithinRange) & isSelected)
        {
            OnDeSelected?.Invoke(this, EventArgs.Empty);
            isSelected = false;
            HideRange();
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
        range.gameObject.SetActive(true);
        OnSelected?.Invoke(this, EventArgs.Empty);
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

    private void GetLayers()
    {
        playerLayer = LayerMask.NameToLayer(PLAYER);
        boundaryLayer = LayerMask.NameToLayer(PAPER);
        enemyLayer = LayerMask.NameToLayer(ENEMY);
        rangeLayer = LayerMask.NameToLayer(RANGE);
    }
    /*public bool CanBattle()
    {

    }*/
}
