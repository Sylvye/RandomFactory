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

        public virtual void SetBackingCell(HexCell cell)
        {
            backingCell = cell;
        }
    }
}
