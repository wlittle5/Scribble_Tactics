using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cursor : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    //Only for WASD movement speed
    [SerializeField] private float moveSpeed = 6f;

    //Edge scrolling variables
    [SerializeField] private float edgeSpeed = 20f;
    [SerializeField] private int edgeScrollSize = 20;
    [SerializeField] private bool edgeScroll = true;

    //Zoom variables
    [SerializeField] private float followOffsetMin = 5f;
    [SerializeField] private float followOffsetMax = 50f;
    [SerializeField] private float zoomSpeed = 6f;

    private Vector3 followOffset;

    private void Awake()
    {
        followOffset = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }
    void Update()
    {
        HandleCameraMovement();
        HandleCameraEdgeMovement();
        HandleCameraZoom();
    }

    private void HandleCameraMovement()
    {
        Vector2 inputVector = new Vector2(0, 0);

        //WASD camera movement logic
        if (Input.GetKey(KeyCode.W)) inputVector.y = +1;
        if (Input.GetKey(KeyCode.A)) inputVector.x = -1;
        if (Input.GetKey(KeyCode.S)) inputVector.y = -1;
        if (Input.GetKey(KeyCode.D)) inputVector.x = +1;

        inputVector = inputVector.normalized;

        transform.position += (Vector3)inputVector * moveSpeed * Time.deltaTime;
    }

    private void HandleCameraEdgeMovement()
    {
        Vector2 edgeInputVector = new Vector2(0, 0);

        //Edge Scrolling logic
        if (Input.mousePosition.x < edgeScrollSize && edgeScroll) edgeInputVector.x = -1f;
        if (Input.mousePosition.y < edgeScrollSize && edgeScroll) edgeInputVector.y = -1f;
        if (Input.mousePosition.x > Screen.width - edgeScrollSize && edgeScroll) edgeInputVector.x = +1f;
        if (Input.mousePosition.y > Screen.height - edgeScrollSize && edgeScroll) edgeInputVector.y = +1f;


        edgeInputVector = edgeInputVector.normalized;
        transform.position += (Vector3)edgeInputVector * edgeSpeed * Time.deltaTime;
    }

    private void HandleCameraZoom()
    {
        Vector3 zoomDir = followOffset.normalized;

        float zoomAmount = 3f;

        if (Input.mouseScrollDelta.y > 0)
            followOffset += zoomDir * zoomAmount;
        

        if (Input.mouseScrollDelta.y < 0)
            followOffset -= zoomDir * zoomAmount;
        

        /*if (followOffset.magnitude < followOffsetMin)        
            followOffset = zoomDir * followOffsetMin;
        
        
        if (followOffset.magnitude > followOffsetMax)        
            followOffset = zoomDir * followOffsetMax;*/
        

        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, followOffset, Time.deltaTime);
        
    }
}
