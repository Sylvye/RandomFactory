using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    private Dictionary<Vector2Int, HexCell> _cells;
    
    [SerializeField] private int worldRadius;
    [SerializeField] private float noiseScale;
    [SerializeField] private int perlinOctaves = 5;
    [SerializeField] private float perlinPersistence = 0.5f;
    [SerializeField] private float perlinLacunarity = 2f;
    [SerializeField] private float liquidHeight;
    [SerializeField] private float resourceHeight;
    [SerializeField] private int seed;
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
        if (seed == 0)
        {
            seed = Random.Range(0, int.MaxValue);
        }
        Debug.Log("Seed: " + seed);
        Random.InitState(seed);
        _seedOffsetX = Random.Range(-10000, 10000);
        _seedOffsetY = Random.Range(-10000, 10000);

        Debug.Log(_seedOffsetX + ", " + _seedOffsetY);
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
    
    private float SampleFractalPerlin(int x, int y, int octaves = 5, float persistence = 0.5f, float lacunarity = 2f)
    {
        float total = 0f;
        float frequency = 1f;
        float amplitude = 1f;
        float maxAmplitude = 0f;

        for (int i = 0; i < octaves; i++)
        {
            float sampleX =
                (x + _seedOffsetX) * noiseScale * frequency;

            float sampleY =
                (y + _seedOffsetY) * noiseScale * frequency;

            float noise = Mathf.PerlinNoise(sampleX, sampleY);

            total += noise * amplitude;
            maxAmplitude += amplitude;

            amplitude *= persistence;
            frequency *= lacunarity;
        }

        return total / maxAmplitude;
    }

    private TerrainType HeightToTerrainType(float height)
    {
        if (height < liquidHeight)
        {
            return TerrainType.Liquid;
        }
        else if (height < resourceHeight)
        {
            return TerrainType.Air;
        }
        else
        {
            return TerrainType.Resource;
        }
    }

    private HexCell GenerateCell(int x, int y)
    {
        float noise = SampleFractalPerlin(x, y, perlinOctaves, perlinPersistence, perlinLacunarity);
        var cell = new HexCell(new Vector2Int(x, y), HeightToTerrainType(noise), noise);
        return cell;
    }
}
