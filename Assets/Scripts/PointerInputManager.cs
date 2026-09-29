using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
public class PointerInputManager : MonoBehaviour
{
    public static PointerInputManager Main;
    private Camera _mainCamera;
    private Mouse _mouse;
    private Vector2 _mouseWorldPos;
    void Awake()
    {
        Main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _mouse = Mouse.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mainCamera is null || _mouse is null) return;

        _mouseWorldPos = _mainCamera.ScreenToWorldPoint(_mouse.position.ReadValue());
    }

    public Vector2 GetMouseWorldPos()
    {
        return _mouseWorldPos;
    }

    public bool WasLeftButtonPressedThisFrame()
    {
        return _mouse?.leftButton.wasPressedThisFrame ?? false;
    }
}
