using UnityEngine;

public interface IInteractable
{
    bool CanInteract(PlayerController player);
    void OnInteract(PlayerController player);
}
