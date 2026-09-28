using UnityEngine;

public class HexCell
{
    public Vector2Int coordinate;

    public TerrainType terrain;
    public float elevation;

    // public ResourceType resource;
    // public float resourceAmount;

    public HexCell(Vector2Int coordinate, TerrainType terrain, float elevation)
    {
        this.coordinate = coordinate;
        this.terrain = terrain;
        this.elevation = elevation;
    }
}