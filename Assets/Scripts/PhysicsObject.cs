using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PhysicsObject : MonoBehaviour
{
    protected Rigidbody2D Rb;
    protected Collider2D Col;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Col = GetComponent<Collider2D>();
    }

    public virtual void ApplyForce(Vector2 force)
    {
        Rb.AddForce(force);
    }
    
    public Rigidbody2D GetRB()
    {
        return Rb;
    }
    
    public Collider2D GetCol()
    {
        return Col;
    }

    public float GetMass()
    {
        return Rb.mass;
    }

    public Vector2 GetPos()
    {
        return Rb.position;
    }
}
