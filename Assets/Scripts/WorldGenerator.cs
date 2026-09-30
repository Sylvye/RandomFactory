using System.Collections.Generic;
using HexTiles;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    public static WorldGenerator Main;
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
    [SerializeField] private TileBase backgroundTile;
    [SerializeField] private Color backgroundColor = new(0.294f, 0.717f, 0.489f, 1f);
    private float _seedOffsetX;
    private float _seedOffsetY;

    private Tilemap _solidTilemap;
    private Tilemap _backgroundTilemap;

    public Tilemap SolidTilemap => _solidTilemap;
    public Tilemap BackgroundTilemap => _backgroundTilemap;

    void Awake()
    {
        Main = this;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cells = new Dictionary<Vector2Int, HexCell>();
        _solidTilemap = GameObject.FindWithTag("Terrain").GetComponent<Tilemap>();
        _backgroundTilemap = GameObject.FindWithTag("Background").GetComponent<Tilemap>();
        if (seed == 0)
        {
            seed = Random.Range(int.MinValue, int.MaxValue);
        }
        Debug.Log("Seed: " + seed);
        Random.InitState(seed);
        _seedOffsetX = Random.Range(-10000, 10000);
        _seedOffsetY = Random.Range(-10000, 10000);

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

                _cells[cell.coordinate] = cell;

                TileBase tile = GetTerrainTile(cell.terrain);
                _solidTilemap.SetTile(cellPos, tile);
                _backgroundTilemap.SetTile(cellPos, backgroundTile);
                float brightness = Mathf.Lerp(0.6f, 1.2f, cell.elevation);
                _backgroundTilemap.SetColor(cellPos, new Color(
                    backgroundColor.r * brightness,
                    backgroundColor.g * brightness,
                    backgroundColor.b * brightness,
                    backgroundColor.a));

                GameObject solidTileObject = _solidTilemap.GetInstantiatedObject(cellPos);
                if (solidTileObject is not null && solidTileObject.TryGetComponent(out HexTile solidHexTile))
                {
                    solidHexTile.SetBackingCell(cell);
                }

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

    public bool TryGetCell(Vector3Int cellPosition, out HexCell cell)
    {
        if (_cells is null)
        {
            cell = null;
            return false;
        }

        return _cells.TryGetValue(new Vector2Int(cellPosition.x, cellPosition.y), out cell);
    }

    public Vector3 GetCellCenterWorld(Vector3Int cellPosition)
    {
        return _solidTilemap.GetCellCenterWorld(cellPosition);
    }

    public GameObject GetTileObject(Vector3Int cellPosition)
    {
        return _solidTilemap.GetInstantiatedObject(cellPosition);
    }

    public bool CanPlaceTile(TileBase tile, Vector3Int cellPosition)
    {
        if (tile is null || !TryGetCell(cellPosition, out var cell)) return false;
        if (cell.terrain != TerrainType.Air || _solidTilemap.HasTile(cellPosition)) return false;

        var center = (Vector2)GetCellCenterWorld(cellPosition);
        var cellSize = (Vector2)_solidTilemap.layoutGrid.cellSize;
        var hits = Physics2D.OverlapBoxAll(center, cellSize * 0.5f, 0f);
        foreach (var hit in hits)
        {
            if (hit.gameObject == _solidTilemap.gameObject ||
                hit.gameObject == _backgroundTilemap.gameObject) continue;
            return false;
        }

        return true;
    }

    public bool TryPlaceTile(TileBase tile, Vector3Int cellPosition)
    {
        if (!CanPlaceTile(tile, cellPosition)) return false;

        _solidTilemap.SetTile(cellPosition, tile);
        return true;
    }
}
