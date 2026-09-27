using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    
    [SerializeField] private float reach = 1;
    [SerializeField] private Vector2 reachHitbox = new Vector2(1, 1);
    [SerializeField] private float strength = 1;
    private bool _dragging;
    private IDraggable _draggedObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _dragging = false;
        _draggedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        var spaceState = KeyboardInputManager.Main.GetKeyState("space");
        if (spaceState.IsKeyDown())
        {
            if (_dragging)
            {
                _draggedObject.OnDrag(transform.position + transform.up * (reach * 1.2f), strength);
                print("Dragging");
            }
            else
            {
                var center = transform.position + transform.up * reach;
                var angle = transform.eulerAngles.z;

                Collider2D hit = Physics2D.OverlapBox(center, reachHitbox, angle);
                Physics2DQueryVisualizer.DrawBox(center, reachHitbox, angle, Color.green);

                if (hit && hit.gameObject.TryGetComponent(out IDraggable draggable))
                {
                    draggable.OnStartDrag();
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
