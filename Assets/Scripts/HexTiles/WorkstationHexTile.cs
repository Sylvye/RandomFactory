using UnityEngine;

// grid-locked stations placed/crafted/found by the player
namespace HexTiles
{
    public abstract class WorkstationHexTile : HexTile, IInteractable
    {
        public bool CanInteract(PlayerController player)
        {
            return true;
        }

        public void OnInteract(PlayerController player)
        {
            Debug.Log("Interacted with: " + name);
        }
    }
}
