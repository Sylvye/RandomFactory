using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MaterialObject : MonoBehaviour, IDraggable
{
    public string name;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerInputManager pointer)
    {
        Vector2 difference = pointer.GetMouseWorldPos() - rb.position;
        Vector2 posDif = new Vector2(Math.Abs(difference.x), Math.Abs(difference.y));
        rb.AddForce(difference*posDif);
    }

    public void OnStartDrag(PointerInputManager pointer)
    {
        
    }

    public void OnStopDrag(PointerInputManager pointer)
    {
        
    }
}
