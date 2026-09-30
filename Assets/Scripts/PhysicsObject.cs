using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class PhysicsObject : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Collider2D col;

    protected virtual void Awake()
    {
        
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        
    }

    public virtual void ApplyForce(Vector2 force)
    {
        rb.AddForce(force);
    }

    public Vector2 GetVelocity()
    {
        return rb.linearVelocity;
    }
    
    public Rigidbody2D GetRB()
    {
        return rb;
    }
    
    public Collider2D GetCol()
    {
        return col;
    }

    public float GetMass()
    {
        return rb.mass;
    }

    public Vector2 GetPos()
    {
        return rb.position;
    }
}
