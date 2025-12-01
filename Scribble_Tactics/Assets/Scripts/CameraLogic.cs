using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class Camera_Follow : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    //Only for WASD movement speed
    [SerializeField] private float moveSpeed = 6f;

    //Edge scrolling variables
    [SerializeField] private float edgeSpeed = 20f;
    [SerializeField] private int edgeScrollSize = 20;
    [SerializeField] private bool edgeScroll = true;

    //Zoom variables
    [SerializeField] private float fieldOfViewMin = 5f;
    [SerializeField] private float fieldOfViewMax = 50f;
    [SerializeField] private float zoomSpeed = 10f;

    private float targetFieldOfView = 50;

    //Drag pan variables
    [SerializeField] private float dragPanSpeed = 2f;
    [SerializeField] private bool dragPanMoveActive;
    private Vector2 lastMousePosition;

    void Update()
    {
        HandleCameraMovement();
        HandleCameraEdgeMovement();
        HandleCameraZoom_FieldOfView();
        HandleCameraDragPan();
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
        }
        if (Input.GetMouseButtonUp(1))
        {
            dragPanMoveActive = false;
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
}
