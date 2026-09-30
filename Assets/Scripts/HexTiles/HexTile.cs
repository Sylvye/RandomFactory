using UnityEngine;

namespace HexTiles
{
    public abstract class HexTile : MonoBehaviour
    {
        protected HexCell backingCell;
        protected SpriteRenderer sr;

        protected virtual void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        protected virtual void Start()
        {
            
        }

        public virtual void SetBackingCell(HexCell cell)
        {
            backingCell = cell;
        }
    }
}
