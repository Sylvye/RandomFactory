using UnityEngine;

// grid-locked stations placed/crafted/found by the player
namespace HexTiles
{
    public abstract class WorkstationHexTile : HexTile, IInteractable
    {
        [SerializeField] protected Sprite icon;
        [SerializeField] protected ItemContainer inventory;
        
        protected override void Start()
        {
            base.Start();
            SpawnIcon();
        }
        
        public bool CanInteract(PlayerController player)
        {
            return true;
        }

        public void OnInteract(PlayerController player)
        {
            Debug.Log("Interacted with: " + name);
        }
        
        protected void SpawnIcon() {
            var obj = new GameObject("SpawnIcon")
            {
                transform = { position = transform.position + Vector3.back, parent = transform }
            };
            obj.AddComponent<SpriteRenderer>().sprite = icon;
        }
    }
}
