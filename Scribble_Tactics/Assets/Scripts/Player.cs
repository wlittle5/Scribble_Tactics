using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Input = UnityEngine.Input;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    [SerializeField] Camera myCamera;
    
    public bool isSelected = false;
    private int hitData;
    private RaycastHit rayCastHit;
    Vector3 mousePos;


    // Update is called once per frame
    void Update()
    {      
        if (Input.GetMouseButtonUp(0))
        {
            GetLayer();
            if (MoveCheck())
                MoveCharacter();
        }
        
    }

    private void GetLayer()
    {
        Ray myRay = myCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(myRay, out rayCastHit);
        hitData = rayCastHit.transform.gameObject.layer;
    }

    private bool MoveCheck()
    {
        Debug.Log(hitData);
        if ((hitData == 6) && !isSelected)
        {
            isSelected = true;
            return false;
        }

        if ((hitData == 8) && isSelected)
        {
            return true;
        }

        if ((hitData != 8) || (hitData != 6) && isSelected)
        {
            isSelected = false;
            return false;
        }

        else
        {
            return false;
        }
    }

    private void MoveCharacter()
    {
        mousePos = rayCastHit.point;
        mousePos.z = transform.position.z;
        transform.position = mousePos;
        isSelected = false;
    }
}
