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
    [SerializeField] float moveSpeed = 0.5f;

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
   
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ClickLogic.Instance.OnMouseClicked += ClickLogic_OnMouseClicked;

        GetLayers();
    }

    private void ClickLogic_OnMouseClicked(object sender, ClickLogic.OnMouseClickedEventArgs e)
    {
     

        if (isMoving != true)
            canMove = MoveCheck(e.objectClicked.transform.gameObject.layer);
            
        if (canMove)
            MoveCharacter(e.objectClicked);
       
    }

    private bool MoveCheck(int layerHit)
    {
        if ((layerHit == 6) && !isSelected)
        {
            isSelected = true;
            ShowRange();
            return false;
        }

        if ((layerHit == 7) && isSelected)
        {
            return true;
        }

        if (((layerHit != 7) || (layerHit != 6)) && isSelected)
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

    private void MoveCharacter(RaycastHit rayCastHit)
    {
        isMoving = true;
        HideRange();

        Debug.Log(rayCastHit.transform.position);
        
        float step = moveSpeed * Time.deltaTime;
        Vector3 mousePos = rayCastHit.point;
        //mousePos.z = transform.position.z;
        
        transform.position = Vector3.MoveTowards(transform.position, mousePos, step);

        /*if (transform.position == mousePos)
        {
            isSelected = false;
            canMove = false;
            isMoving = false;
        }*/

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
