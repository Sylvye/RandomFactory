using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;

public class KeyboardInputManager : MonoBehaviour
{
    public static KeyboardInputManager Main;
    private List<string> _pressedKeys;
    private Keyboard _keyboard;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Main = this;
        _pressedKeys = new List<string>();
        _keyboard = Keyboard.current;
    }

    public KeyState GetKeyState(string keyName)
    {
        KeyControl key = _keyboard[keyName] as KeyControl;

        if (key is null)
        {
            // Debug.Log("Invalid key: " + keyName);
            return KeyState.Up;
        }
        
        if (key.wasPressedThisFrame)
            return KeyState.Pressed;

        if (key.wasReleasedThisFrame)
            return KeyState.Released;

        return key.isPressed ? KeyState.Held : KeyState.Up;
    }

    public Vector2 GetMovementVector()
    {
        var vec = Vector2.zero;
        vec.x += _keyboard.dKey.isPressed ? 1 : 0;
        vec.x += _keyboard.aKey.isPressed ? -1 : 0;
        vec.y += _keyboard.wKey.isPressed ? 1 : 0;
        vec.y += _keyboard.sKey.isPressed ? -1 : 0;
        return vec;
    }
}
