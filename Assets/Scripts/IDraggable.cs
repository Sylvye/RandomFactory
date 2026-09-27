using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDraggable
{
    void OnDrag(PointerInputManager pointer);
    void OnStartDrag(PointerInputManager pointer);
    void OnStopDrag(PointerInputManager pointer);
}
