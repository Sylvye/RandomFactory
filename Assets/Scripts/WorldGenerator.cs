using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    private Dictionary<Vector2Int, HexCell> _cells;
    
    [SerializeField] private int worldRadius;
    [SerializeField] private float noiseScale;
    [SerializeField] private TileBase resourceTile;
    [SerializeField] private TileBase liquidTile;
    private float _seedOffsetX;
    private float _seedOffsetY;

    private Tilemap _solidTilemap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cells = new();
        _solidTilemap = GameObject.FindWithTag("Terrain").GetComponent<Tilemap>();
        Generate();
    }

    private void Generate()
    {
        for (var x = -worldRadius; x <= worldRadius; x++)
        {
            for (var y = -worldRadius; y <= worldRadius; y++)
            {
                Vector3Int cellPos = new(x, y, 0);

                HexCell cell = GenerateCell(x, y);

                _cells[new Vector2Int(x, y)] = cell;

                TileBase tile = GetTerrainTile(cell.terrain);
                _solidTilemap.SetTile(cellPos, tile);
            }
        }
    }

    private TileBase GetTerrainTile(TerrainType terrainType)
    {
        return terrainType switch {
            TerrainType.Liquid => liquidTile,
            TerrainType.Resource => resourceTile,
            _ => null
        };
    }
    
    private float SamplePerlin(int x, int y)
    {
        return Mathf.PerlinNoise((x + _seedOffsetX) * noiseScale, (y + _seedOffsetY) * noiseScale);
    }

    private static TerrainType HeightToTerrainType(float height)
    {
        return height switch
        {
            < 0.2f => TerrainType.Liquid,
            < 0.8f => TerrainType.Air,
            _ => TerrainType.Resource
        };
    }

    private HexCell GenerateCell(int x, int y)
    {
        float noise = SamplePerlin(x, y);
        var cell = new HexCell(new Vector2Int(x, y), HeightToTerrainType(noise), noise);
        return cell;
    }
}
