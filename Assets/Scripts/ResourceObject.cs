using UnityEngine;

public class ResourceObject : PhysicsObject, IDraggable
{
    public string displayName;
    [SerializeField] private float springStiffness = 60f;
    [SerializeField] private float springDamping = 12f;
    private Vector2 _target;
    private float _strength;
    private bool _dragging;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!_dragging) return;

        // Strength is a force limit. Mass affects acceleration through the Rigidbody2D.
        var force = (_target - rb.position) * springStiffness - rb.linearVelocity * springDamping;
        rb.AddForce(Vector2.ClampMagnitude(force, _strength));
    }

    public void OnDrag(Vector2 position, float strength)
    {
        _target = position;
        _strength = Mathf.Max(0f, strength);
    }

    public void OnStartDrag()
    {
        _dragging = true;
        _target = rb.position;
    }

    public void OnStopDrag()
    {
        _dragging = false;
    }

    public PhysicsObject GetPhysics()
    {
        return this;
    }
}
