using System;
using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    public static PlayerAbilityManager Main;
    [SerializeField] private float reach = 1;
    [SerializeField] private Vector2 reachHitbox = new Vector2(1, 1);
    [SerializeField] private float strength = 30;
    private bool _dragging;
    private IDraggable _draggedObject;
    private KeyboardInputManager _kim;
    private PlayerController _pc;

    private void Awake()
    {
        Main = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _dragging = false;
        _draggedObject = null;
        _kim = KeyboardInputManager.Main;
        _pc = PlayerController.Main;
    }

    // Update is called once per frame
    void Update()
    {
        var dState = _kim.GetKeyState("d");
        if (dState.IsKeyDown()) DebugManager.Debug = !DebugManager.Debug;
    }

    private void FixedUpdate()
    {
        var spaceState = _kim.GetKeyState("space");
        ExecuteDrag(spaceState);
    }

    public void ApplyForceToDragged(Vector2 force)
    {
        if (_dragging)
        {
            _draggedObject.GetPhysics().ApplyForce(force);
        }
    }

    private void ExecuteDrag(KeyState state)
    {
        if (state.IsKeyDown())
        {
            if (_dragging)
            {
                var difference = PointerInputManager.Main.GetMouseWorldPos() - (Vector2)transform.position;
                if (difference.magnitude > reach)
                {
                    difference = difference.normalized * reach;
                }
                var center = (Vector2)transform.position + difference;
                _draggedObject.OnDrag(center, strength * _pc.GetMass());
            }
            else
            {
                var hits = ReachOverlapBox();

                IDraggable draggable = null;
                var closestDistance = float.PositiveInfinity;
                foreach (var hit in hits)
                {
                    if (hit.attachedRigidbody == _pc.GetRB() || hit.transform.IsChildOf(transform)) continue;
                    var candidate = hit.GetComponentInParent<IDraggable>();
                    if (candidate is null) continue;
                    var pos = _pc.GetPos();
                    var distance = (pos - hit.ClosestPoint(pos)).sqrMagnitude;
                    if (distance >= closestDistance) continue;
                    closestDistance = distance;
                    draggable = candidate;
                }

                if (draggable is not null)
                {
                    draggable.OnStartDrag();
                    draggable.OnDrag(transform.position + transform.up * (reach * 1.2f), strength * _pc.GetMass());
                    _draggedObject = draggable;
                    _dragging = true;
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
            }
        }
    }

    public bool IsWithinReach(Vector2 worldPosition)
    {
        return Vector2.Distance(transform.position, worldPosition) <= reach;
    }

    private Collider2D[] ReachOverlapBox()
    {
        var difference = Vector2.Normalize(PointerInputManager.Main.GetMouseWorldPos() - (Vector2)transform.position);
        var angle = AngleHelper.VectorToDegrees(difference);
        var center = (Vector2)transform.position + difference * reach;

        Collider2D[] hits = Physics2D.OverlapBoxAll(center, reachHitbox, angle);
        if (DebugManager.Debug)
            Physics2DQueryVisualizer.DrawBox(center, reachHitbox, angle, Color.green);
        return hits;
    }
}
