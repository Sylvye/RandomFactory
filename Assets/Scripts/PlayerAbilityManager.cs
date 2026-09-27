using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityManager : MonoBehaviour
{
    
    [SerializeField] private float reach = 1;
    [SerializeField] private Vector2 reachHitbox = new Vector2(1, 1);
    [SerializeField] private float strength = 30;
    private bool _dragging;
    private IDraggable _draggedObject;
    private KeyboardInputManager _kim;
    private PlayerController _pc;
    private Rigidbody2D _rb;
    private Collider2D _col;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _dragging = false;
        _draggedObject = null;
        _kim = KeyboardInputManager.Main;
        _pc = PlayerController.Main;
        _rb = _pc.GetRB();
        _col = _pc.GetCol();
    }

    // Update is called once per frame
    void Update()
    {
        var spaceState = _kim.GetKeyState("space");
        ExecuteDrag(spaceState);
        
        var eState = _kim.GetKeyState("e");
        ExecuteInteract(eState);
        
        var dState = _kim.GetKeyState("d");
        if (dState.IsKeyDown()) DebugManager.Debug = !DebugManager.Debug;

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
                    difference = difference.normalized;
                }
                var center = (Vector2)transform.position + difference * reach;
                _draggedObject.OnDrag(center, strength * _rb.mass);
            }
            else
            {
                var hits = ReachOverlapBox();

                IDraggable draggable = null;
                var closestDistance = float.PositiveInfinity;
                foreach (var hit in hits)
                {
                    if (hit.attachedRigidbody == _rb || hit.transform.IsChildOf(transform)) continue;
                    var candidate = hit.GetComponentInParent<IDraggable>();
                    if (candidate is null) continue;
                    var distance = (_rb.position - hit.ClosestPoint(_rb.position)).sqrMagnitude;
                    if (distance >= closestDistance) continue;
                    closestDistance = distance;
                    draggable = candidate;
                }

                if (draggable is not null)
                {
                    draggable.OnStartDrag();
                    draggable.OnDrag(transform.position + transform.up * (reach * 1.2f), strength * _rb.mass);
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

    private void ExecuteInteract(KeyState state)
    {
        if (state.IsKeyDown())
        {
            var hits = ReachOverlapBox();

            IInteractable interactable = null;
            var closestDistance = float.PositiveInfinity;
            foreach (var hit in hits)
            {
                if (hit.attachedRigidbody == _rb || hit.transform.IsChildOf(transform)) continue;
                var candidate = hit.GetComponentInParent<IInteractable>();
                if (candidate == null) continue;
                var distance = (_rb.position - hit.ClosestPoint(_rb.position)).sqrMagnitude;
                if (distance >= closestDistance) continue;
                closestDistance = distance;
                interactable = candidate;
            }

            if (interactable is not null)
            {
                interactable.OnInteract();
            }
        }
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
