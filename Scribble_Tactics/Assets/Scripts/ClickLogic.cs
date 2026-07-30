using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Analytics.IAnalytic;

public class ClickLogic : MonoBehaviour
{
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
        CastTheRay();
       
    }

    private RaycastHit CastTheRay(int layerMask)
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(myRay, out RaycastHit rayCastHit, layerMask);
        int layerData = rayCastHit.transform.gameObject.layer;

        return rayCastHit;
    }

    private RaycastHit CastTheRay()
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(myRay, out RaycastHit rayCastHit);
        int layerData = rayCastHit.transform.gameObject.layer;

        return rayCastHit;
    }

    private int CastTheRayLayer(int layerMask)
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(myRay, out RaycastHit rayCastHit, layerMask);
        int layerData = rayCastHit.transform.gameObject.layer;

        Debug.Log(layerData);
        return layerData;
    }

    private int CastTheRayLayer()
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(myRay, out RaycastHit rayCastHit);
        int layerData = rayCastHit.transform.gameObject.layer;

        Debug.Log(layerData);
        return layerData;
    }
}
