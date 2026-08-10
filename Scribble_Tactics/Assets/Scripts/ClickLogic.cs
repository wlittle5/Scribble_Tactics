using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Analytics.IAnalytic;
using UnityEngine.Events;
using System;

public class ClickLogic : MonoBehaviour
{
//Custom Event to shout what was clicked when mouse is pressed
    public event EventHandler<OnMouseClickedEventArgs> OnMouseClicked;
    public class OnMouseClickedEventArgs : EventArgs
    {
        public RaycastHit objectClicked;
        public bool isPlayer;
        public bool isWithinRange;
        public bool isWithinBounds;
        public bool isEnemy;
    }

//Singleton pattern
    public static ClickLogic Instance { get; private set; }

//Variables for getting the layers
    const string PLAYER = "Player";
    const string RANGE = "Range";
    const string PAPER = "Paper";
    const string ENEMY = "Enemy";

    int playerLayer;
    int rangeLayer;
    int boundaryLayer;
    int enemyLayer;

    //Variables for converting the layers into layer masks
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
        GetLayers();
        ConvertLayersToLayerMasks();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RayCastLogic();
        }
    }

    private void GetLayers()
    {
        playerLayer = LayerMask.NameToLayer(PLAYER);
        boundaryLayer = LayerMask.NameToLayer(PAPER);
        enemyLayer = LayerMask.NameToLayer(ENEMY);
        rangeLayer = LayerMask.NameToLayer(RANGE);

    }

    private void ConvertLayersToLayerMasks()
    {
        playerLayerMask = (1 << playerLayer);
        rangeLayerMask = (1 << rangeLayer); 
        boundaryLayerMask = (1 << boundaryLayer);
        enemyLayerMask = (1 << enemyLayer);
    }

    private void RayCastLogic()
    {
        OnMouseClicked?.Invoke(this, new OnMouseClickedEventArgs 
        {
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
        bool isHit = Physics.Raycast(myRay, out RaycastHit rayCastHit, 1000f, layerMask);

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
