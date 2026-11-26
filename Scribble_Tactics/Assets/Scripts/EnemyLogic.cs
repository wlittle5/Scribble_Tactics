using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Input = UnityEngine.Input;
using UnityEngine.Windows;
using System;
using UnityEngine.Events;
using static ClickLogic;

public class EnemyLogic : MonoBehaviour
{ 
    [SerializeField] GameObject range;

    private bool isSelected = false;
    private bool isMoving = false;

    private void Start()
    {
        EventManager.Instance.OnMouseClicked += EventManager_OnMouseClicked;
    }

    private void EventManager_OnMouseClicked(object sender, EventManager.OnMouseClickedEventArgs e)
    {
        if (e.isEnemy == true && e.objectClicked.collider.gameObject == this.gameObject && !isSelected)
        {
            ShowRange();
        }

        if (e.isEnemy != true && isSelected == true)
        {
            HideRange();
        }
    }

    private void ShowRange()
    {
        range.gameObject.SetActive(true);
        isSelected = true;

        EventManager.Instance.EnemySelected(new EventManager.OnEnemySelectArgs
        {
            enemy = this,
            isSelected = isSelected
        });
    }

    private void HideRange()
    {
        range.gameObject.SetActive(false);
        isSelected = false;

        EventManager.Instance.EnemySelected(new EventManager.OnEnemySelectArgs
        {
            enemy = this,
            isSelected = isSelected
        });
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
