using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Analytics.IAnalytic;
using UnityEngine.Events;

public class ClickLogic : MonoBehaviour
{
    public UnityEvent<RaycastHit, int> OnObjectClicked;
    public UnityEvent<bool> OnClickedObjectWithinRange;

    const string PLAYER = "Player";
    const string RANGE = "Range";
    const string PAPER = "Paper";
    const string ENEMY = "Enemy";

    int playerLayerMask;
    int rangeLayerMask;
    int boundaryLayerMask;
    int enemyLayerMask;

    void Start()
    {
        GetLayerMasks();
    }

    void Update()
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
        RaycastHit objectHit = CastTheRay();
        
        int objectHitLayer = objectHit.transform.gameObject.layer;

        OnObjectClicked.Invoke(objectHit, objectHitLayer);

        if (objectHitLayer == rangeLayerMask)
        {
            if (CastTheRay(boundaryLayerMask))
            {
                OnClickedObjectWithinRange.Invoke(true);
            }
        }
       
    }

    private bool CastTheRay(int layerMask)
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isHit = Physics.Raycast(myRay, out RaycastHit rayCastHit, layerMask);

        return isHit;
    }

    private RaycastHit CastTheRay()
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(myRay, out RaycastHit rayCastHit);

        return rayCastHit;
    }

}
