using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    
    [SerializeField] Texture2D cursor;

    [SerializeField] Texture2D cursorClicked;

    private CursorControls control;


    private void Awake()
    {
        control = new CursorControls();
        ChangeCursor(cursor);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start()
    {
        control.Mouse.Click.started += _ => StartedClick();
        control.Mouse.Click.performed += _ => EndClick();
    }

    private void StartedClick()
    {
        ChangeCursor(cursorClicked);
    }

    private void EndClick()
    {
        ChangeCursor(cursor);
    }
    private void OnEnable()
    {
        control.Enable();
    }

    private void OnDisable()
    {
        control.Disable();
    }
    
    private void ChangeCursor(Texture2D cursorType)
    {
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }
}
