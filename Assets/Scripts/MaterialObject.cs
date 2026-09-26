using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MaterialObject : MonoBehaviour, IDraggable
{
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

    public void OnDrag(PointerInteractor pointer)
    {
        Vector2 difference = pointer.GetMouseWorldPos() - rb.position;
        rb.AddForce(difference);
    }

    public void OnStartDrag(PointerInteractor pointer)
    {
        
    }

    public void OnStopDrag(PointerInteractor pointer)
    {
        
    }
}
