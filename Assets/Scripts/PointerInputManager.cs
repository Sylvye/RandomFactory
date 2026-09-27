using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointerInputManager : MonoBehaviour
{
    public static PointerInputManager Main;
    private Camera _mainCamera;
    private Mouse _mouse;
    private Vector2 _mouseWorldPos;
    private bool _isDragging = false;
    private IDraggable _draggedObject;

    private Vector2 _lastWorldPos;
    private Vector2 _delta;

    void Awake()
    {
        Main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _mouse = Mouse.current;
        _isDragging = false;
        _draggedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        _lastWorldPos = _mouseWorldPos;
        _mouseWorldPos = _mainCamera.ScreenToWorldPoint(_mouse.position.ReadValue());
        _delta = _mouseWorldPos - _lastWorldPos;
        
        if (_mouse.leftButton.wasPressedThisFrame)
        {
            var hit = Physics2D.OverlapPoint(_mouseWorldPos);
            if (hit && hit.gameObject.TryGetComponent(out IDraggable draggable))
            {
                _isDragging = true;
                _draggedObject = draggable;
                _draggedObject.OnStartDrag();
            }
        }

        if (_mouse.leftButton.isPressed && _isDragging)
        {
            _draggedObject.OnDrag(_mouseWorldPos, 10);
        }
        
        if (_mouse.leftButton.wasReleasedThisFrame && _isDragging)
        {
            _isDragging = false;
            _draggedObject.OnStopDrag();
            _draggedObject = null;
        }
    }

    public Vector2 GetMouseWorldPos()
    {
        return _mouseWorldPos;
    }
}
