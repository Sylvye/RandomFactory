using UnityEngine;

public class MaterialObject : MonoBehaviour, IDraggable
{
    public string displayName;
    private Rigidbody2D _rb;
    [SerializeField] private float springStiffness = 60f;
    [SerializeField] private float springDamping = 12f;
    private Vector2 _target;
    private float _strength;
    private bool _dragging;
    
    // Start is called before the first frame update
    public void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!_dragging) return;

        // Strength is a force limit. Mass affects acceleration through the Rigidbody2D.
        var force = (_target - _rb.position) * springStiffness - _rb.linearVelocity * springDamping;
        _rb.AddForce(Vector2.ClampMagnitude(force, _strength));
    }

    public void OnDrag(Vector2 position, float strength)
    {
        _target = position;
        _strength = Mathf.Max(0f, strength);
    }

    public void OnStartDrag()
    {
        _dragging = true;
        _target = _rb.position;
    }

    public void OnStopDrag()
    {
        _dragging = false;
    }
}
