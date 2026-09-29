using UnityEngine;

public class HexCellTile : MonoBehaviour
{
    protected HexCell backingCell;
    protected SpriteRenderer sr;
    private Color baseColor;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        baseColor = sr.color;
    }

    public void SetBackingCell(HexCell cell)
    {
        backingCell = cell;
        float brightness = Mathf.Max(0f, 2f * cell.elevation - 0.5f);
        sr.color = new Color(
            baseColor.r * brightness,
            baseColor.g * brightness,
            baseColor.b * brightness,
            baseColor.a);
    }
}
