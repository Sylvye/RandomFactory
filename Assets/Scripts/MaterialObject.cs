using System;
using UnityEngine;

public class MaterialObject : MonoBehaviour, IDraggable
{
    public string name;
    private Rigidbody2D _rb;
    
    // Start is called before the first frame update
    public void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void OnDrag(Vector2 position, float strength)
    {
        var difference = position - _rb.position;
        if (difference.magnitude > 1)
        {
            difference.Normalize();
        }
        _rb.AddForce(difference * strength / _rb.mass);
    }

    public void OnStartDrag()
    {
        
    }

    public void OnStopDrag()
    {
        
    }
}
