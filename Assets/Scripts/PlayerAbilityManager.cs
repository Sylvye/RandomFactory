using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    
    [SerializeField] private float reach = 1;
    [SerializeField] private Vector2 reachHitbox = new Vector2(1, 1);
    [SerializeField] private float strength = 30;
    private bool _dragging;
    private IDraggable _draggedObject;
    private Rigidbody2D _rb;
    private Collider2D _col;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _dragging = false;
        _draggedObject = null;
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var spaceState = KeyboardInputManager.Main.GetKeyState("space");
        if (spaceState.IsKeyDown())
        {
            if (_dragging)
            {
                _draggedObject.OnDrag(transform.position + transform.up * (reach * 1.2f), strength * _rb.mass);
                print("Dragging");
            }
            else
            {
                var center = transform.position + transform.up * reach;
                var angle = transform.eulerAngles.z;

                Collider2D[] hits = Physics2D.OverlapBoxAll(center, reachHitbox, angle);
                Physics2DQueryVisualizer.DrawBox(center, reachHitbox, angle, Color.green);

                IDraggable draggable = null;
                var closestDistance = float.PositiveInfinity;
                foreach (var hit in hits)
                {
                    if (hit.attachedRigidbody == _rb || hit.transform.IsChildOf(transform)) continue;
                    var candidate = hit.GetComponentInParent<IDraggable>();
                    if (candidate == null) continue;
                    var distance = (_rb.position - hit.ClosestPoint(_rb.position)).sqrMagnitude;
                    if (distance >= closestDistance) continue;
                    closestDistance = distance;
                    draggable = candidate;
                }

                if (draggable != null)
                {
                    draggable.OnStartDrag();
                    draggable.OnDrag(transform.position + transform.up * (reach * 1.2f), strength * _rb.mass);
                    _draggedObject = draggable;
                    _dragging = true;
                    print("Started Dragging");
                }
            }
        }
        else
        {
            if (_dragging)
            {
                _draggedObject.OnStopDrag();
                _draggedObject = null;
                _dragging = false;
                print("Stopped Dragging");
            }
        }
    }
}
