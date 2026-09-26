using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointerInteractor : MonoBehaviour
{
    public static PointerInteractor main;
    private Camera mainCamera;
    private Mouse mouse;
    private Vector2 mouseWorldPos;
    private bool isDragging = false;
    private IDraggable draggedObject;

    private Vector2 lastWorldPos;
    private Vector2 delta;
    
    // Start is called before the first frame update
    void Start()
    {
        main = this;
        mainCamera = Camera.main;
        mouse = Mouse.current;
        isDragging = false;
        draggedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        lastWorldPos = mouseWorldPos;
        mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        delta = mouseWorldPos - lastWorldPos;
        
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);
            if (hit.gameObject.TryGetComponent(out IDraggable draggable))
            {
                isDragging = true;
                draggedObject = draggable;
                draggedObject.OnStartDrag(this);
            }
        }

        if (mouse.leftButton.isPressed)
        {
            draggedObject.OnDrag(this);
        }
        
        if (mouse.leftButton.wasReleasedThisFrame)
        {
            isDragging = false;
            draggedObject.OnStopDrag(this);
            draggedObject = null;
        }
    }

    public Vector2 GetMouseWorldPos()
    {
        return mouseWorldPos;
    }
}
