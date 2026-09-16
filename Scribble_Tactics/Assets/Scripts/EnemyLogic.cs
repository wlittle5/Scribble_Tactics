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
    public static event EventHandler<OnEnemySelectArgs> OnEnemySelect;
    public class OnEnemySelectArgs : EventArgs
    {
        public EnemyLogic enemy;
        public bool isSelected;
    }
    
    [SerializeField] GameObject range;

    private bool isSelected = false;
    private bool isMoving = false;

    private void Start()
    {
        ClickLogic.Instance.OnMouseClicked += ClickLogic_OnMouseClicked;

    }

    private void ClickLogic_OnMouseClicked(object sender, OnMouseClickedEventArgs e)
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

        OnEnemySelect?.Invoke(this, new OnEnemySelectArgs
        {
            enemy = this,
            isSelected = isSelected
        });
    }

    private void HideRange()
    {
        range.gameObject.SetActive(false);
        isSelected = false;

        OnEnemySelect?.Invoke(this, new OnEnemySelectArgs
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
