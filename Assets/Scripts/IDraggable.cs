using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDraggable
{
    void OnDrag(PointerInteractor pointer);
    void OnStartDrag(PointerInteractor pointer);
    void OnStopDrag(PointerInteractor pointer);
}
