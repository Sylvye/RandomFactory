using UnityEngine;

public class HexCellTile : MonoBehaviour
{
    protected HexCell backingCell;
    protected SpriteRenderer sr;
    protected Color color;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (backingCell is not null)
        {
            sr.color = color * (2 * backingCell.elevation - 0.5f);
        }
    }

    public void SetBackingCell(HexCell cell)
    {
        backingCell = cell;
    }
}
