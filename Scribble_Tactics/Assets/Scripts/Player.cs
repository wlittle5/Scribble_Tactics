using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Input = UnityEngine.Input;
using UnityEngine.Windows;
using Cinemachine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject range;
    [SerializeField] GameObject freeCameraFollow;
    [SerializeField] Transform cameraFollow;

    [SerializeField] CinemachineVirtualCamera freeLookCamera;
    [SerializeField] CinemachineVirtualCamera selectedCamera;

    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] float cameraCenterSpeed = 3f;

    private bool isSelected = false;
    private bool canMove = false;
    private bool isMoving = false;
    private int hitData;
    private float elapsedTime;


    private RaycastHit rayCastHit;
    private Vector3 mousePos;

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && isMoving != true)
        {
            GetLayer();
            canMove = MoveCheck();
        }

        if (canMove)
        {
            MoveCharacter();
        }
    }

    private void GetLayer()
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(myRay, out rayCastHit);
        hitData = rayCastHit.transform.gameObject.layer;
    }

    private bool MoveCheck()
    {

        if ((hitData == 6) && !isSelected)
        {
            isSelected = true;
            //ShowSelectedCamera();
            //CenterCamera();
            ShowRange();
            return false;
        }

        if ((hitData == 7) && isSelected)
        {
            return true;
        }

        if ((hitData != 7) || (hitData != 6) && isSelected)
        {
            isSelected = false;
            HideRange();
            //HideSelectedCamera();
            return false;
        }

        else
        {
            if (!isSelected)
            {
                HideRange();
               // HideSelectedCamera();
            }
                

            return isSelected;
        }
    }

    private void MoveCharacter()
    {
        HideRange();
        isMoving = true;
        float step = moveSpeed * Time.deltaTime;
        mousePos = rayCastHit.point;
        mousePos.z = transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, mousePos, step);

        if (transform.position == mousePos)
        {
            isSelected = false;
            canMove = false;
            isMoving = false;
 
            //freeCameraFollow.transform.position = transform.position;
            //HideSelectedCamera();
        }

    }

    private void ShowRange()
    {
        range.gameObject.SetActive(true);
    }

    private void HideRange()
    {
        range.gameObject.SetActive(false);
    }

    private void ShowSelectedCamera()
    {
        freeLookCamera.enabled = false;
        selectedCamera.enabled = true;
    }

    private void HideSelectedCamera()
    {
        selectedCamera.enabled = false;
        freeLookCamera.enabled = true;
    }

    private void CenterCamera()
    {
        Vector3 startPosition = cameraFollow.transform.position;
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / cameraCenterSpeed;

        Vector3 position = transform.position;
        position.z = cameraFollow.transform.position.z;
        cameraFollow.transform.position = Vector3.Lerp(startPosition, position, percentageComplete);
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
