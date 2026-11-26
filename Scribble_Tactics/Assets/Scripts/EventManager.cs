using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //------ Singleton ------
    public static EventManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    //------ Event for Checking What Was Clicked ------
    public event EventHandler<OnMouseClickedEventArgs> OnMouseClicked;
    public class OnMouseClickedEventArgs : EventArgs
    {
        public RaycastHit objectClicked;
        public bool isPlayer;
        public bool isWithinRange;
        public bool isWithinBounds;
        public bool isEnemy;
    }

    //------ Player and Enemy Events ------
    public event EventHandler<OnPlayerSelectArgs> OnPlayerSelect;
    public class OnPlayerSelectArgs : EventArgs
    {
        public Player player;
        public bool isSelected;
    }

    public event EventHandler<OnBattleInitiateArgs> OnBattleInitiate;
    public class OnBattleInitiateArgs : EventArgs
    {
        public Player player;
    }

    public event EventHandler<OnEnemySelectArgs> OnEnemySelect;
    public class OnEnemySelectArgs : EventArgs
    {
        public EnemyLogic enemy;
        public bool isSelected;
    }

    //------ Functions for other scripts to cause events ------
    public void InitiateBattle(OnBattleInitiateArgs args)
    {
        OnBattleInitiate?.Invoke(this, args);
    }

    public void PlayerSelected(OnPlayerSelectArgs args)
    {
        OnPlayerSelect?.Invoke(this, args);
    }

    public void EnemySelected(OnEnemySelectArgs args)
    {
        OnEnemySelect?.Invoke(this, args);
    }

    public void MouseClicked(OnMouseClickedEventArgs args)
    {
        OnMouseClicked?.Invoke(this, args);
    }
}
