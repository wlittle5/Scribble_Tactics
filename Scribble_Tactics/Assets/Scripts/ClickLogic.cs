using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Analytics.IAnalytic;
using UnityEngine.Events;
using System;

public class ClickLogic : MonoBehaviour
{
    public event EventHandler<OnMouseClickedEventArgs> OnMouseClicked;
    public class OnMouseClickedEventArgs : EventArgs
    {
        public RaycastHit objectClicked;
        public bool isPlayer;
        public bool isWithinRange;
        public bool isWithinBounds;
        public bool isEnemy;
    }

    public static ClickLogic Instance { get; private set; }


    const string PLAYER = "Player";
    const string RANGE = "Range";
    const string PAPER = "Paper";
    const string ENEMY = "Enemy";

    int playerLayerMask;
    int rangeLayerMask;
    int boundaryLayerMask;
    int enemyLayerMask;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GetLayerMasks();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RayCastLogic();
        }

    }

    private void GetLayerMasks()
    {
        playerLayerMask = LayerMask.GetMask(PLAYER);
        boundaryLayerMask = LayerMask.GetMask(PAPER);
        enemyLayerMask = LayerMask.GetMask(ENEMY);
        rangeLayerMask = LayerMask.GetMask(RANGE);
    }

    private void RayCastLogic()
    {
        OnMouseClicked?.Invoke(this, new OnMouseClickedEventArgs {
            objectClicked = CastTheRay(),
            isPlayer = IsPlayer(),
            isWithinRange = IsWithinRange(),
            isWithinBounds = IsWithinBoundary(),
            isEnemy = IsEnemy()
        });
    }

    private bool CastTheRay(int layerMask)
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isHit = Physics.Raycast(myRay, layerMask);

        return isHit;
    }

    private RaycastHit CastTheRay()
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(myRay, out RaycastHit rayCastHit);

        return rayCastHit;
    }

    private bool IsWithinRange()
    {
        return CastTheRay(rangeLayerMask);
    }

    private bool IsWithinBoundary()
    {
        return CastTheRay(boundaryLayerMask);
    }

    private bool IsEnemy()
    {
        return CastTheRay(enemyLayerMask);
    }

    private bool IsPlayer()
    {
        return CastTheRay(playerLayerMask);
    }
}
