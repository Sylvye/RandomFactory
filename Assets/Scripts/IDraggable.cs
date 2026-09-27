using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDraggable
{
    void OnDrag(Vector2 position, float strength);
    void OnStartDrag();
    void OnStopDrag();
    PhysicsObject GetPhysics();
}
