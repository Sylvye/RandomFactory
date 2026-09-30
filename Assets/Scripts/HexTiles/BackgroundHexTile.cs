using HexTiles;
using UnityEngine;

public class BackgroundHexTile : HexTile
{
    private Color baseColor;

    protected override void Awake()
    {
        base.Awake();
        baseColor = sr.color;
    }
    
    public override void SetBackingCell(HexCell cell)
    {
        base.SetBackingCell(cell);
        float brightness = Mathf.Max(0f, 2f * cell.elevation - 0.5f);
        sr.color = new Color(
            baseColor.r * brightness,
            baseColor.g * brightness,
            baseColor.b * brightness,
            baseColor.a);
    }
}
