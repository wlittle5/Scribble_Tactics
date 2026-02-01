using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class Camera_Follow : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    //Boundary variables for camera
    [SerializeField] private int xBoundaryNegative = -25;
    [SerializeField] private int yBoundaryNegative = -25;
    [SerializeField] private int xBoundaryPositive = 25;
    [SerializeField] private int yBoundaryPositive = 25;

    //Only for WASD movement speed
    [SerializeField] private float moveSpeed = 6f;

    //Edge scrolling variables
    [SerializeField] private float edgeSpeed = 20f;
    [SerializeField] private int edgeScrollSize = 20;
    [SerializeField] private bool useEdgeScroll = true;

    //Zoom variables
    [SerializeField] private float fieldOfViewMin = 5f;
    [SerializeField] private float fieldOfViewMax = 50f;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private bool useCameraZoom = true;

    private float targetFieldOfView = 50;

    //Drag pan variables
    [SerializeField] private float dragPanSpeed = 2f;
    [SerializeField] private bool useDragMove = true;
    private Vector2 lastMousePosition;
    private bool dragPanMoveActive;

    void Update()
    {
        HandleCameraMovement();

        if (useDragMove)
            HandleCameraDragPan();

        if (useEdgeScroll)
            HandleCameraEdgeMovement();

        if(useCameraZoom)
            HandleCameraZoom_FieldOfView();
        
        CheckBoundaries();
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
        if (Input.mousePosition.x < edgeScrollSize) edgeInputVector.x = -1f;
        if (Input.mousePosition.y < edgeScrollSize) edgeInputVector.y = -1f;
        if (Input.mousePosition.x > Screen.width - edgeScrollSize) edgeInputVector.x = +1f;
        if (Input.mousePosition.y > Screen.height - edgeScrollSize) edgeInputVector.y = +1f;

       
        edgeInputVector = edgeInputVector.normalized;
        transform.position += (Vector3)edgeInputVector * edgeSpeed * Time.deltaTime;
    }

    private void HandleCameraZoom_FieldOfView()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFieldOfView -= 5;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFieldOfView += 5;
        }

        targetFieldOfView = Mathf.Clamp(targetFieldOfView, fieldOfViewMin, fieldOfViewMax);

        cinemachineVirtualCamera.m_Lens.FieldOfView =
            Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView, targetFieldOfView, Time.deltaTime * zoomSpeed);
    }

    private void HandleCameraDragPan()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetMouseButtonDown(1))
        {
            dragPanMoveActive = true;
            lastMousePosition = Input.mousePosition;
            useEdgeScroll = false;
        }
        if (Input.GetMouseButtonUp(1))
        {
            dragPanMoveActive = false;
            useEdgeScroll = true;
        }

        if (dragPanMoveActive)
        {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;

            inputDir.x = -mouseMovementDelta.x * dragPanSpeed;
            inputDir.y = -mouseMovementDelta.y * dragPanSpeed;

            lastMousePosition = (Vector2)Input.mousePosition;

            transform.position += inputDir * moveSpeed * Time.deltaTime;
        }
    }

    private void CheckBoundaries()
    {
        if (transform.position.x < xBoundaryNegative)
            transform.position = new Vector3(xBoundaryNegative, transform.position.y, transform.position.z);

        if (transform.position.x > xBoundaryPositive)
            transform.position = new Vector3(xBoundaryPositive, transform.position.y, transform.position.z);

        if (transform.position.y < yBoundaryNegative)
            transform.position = new Vector3(transform.position.x, yBoundaryNegative, transform.position.z);

        if (transform.position.y > yBoundaryPositive)
            transform.position = new Vector3(transform.position.x, yBoundaryPositive, transform.position.z);
    }
}
