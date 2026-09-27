using System.Collections.Generic;
using UnityEngine;

public class MaterialSpawner : MonoBehaviour
{
    [SerializeField] private Lootpool<MaterialObjectBlueprint> blueprintLootpool;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float maxNearby;
    private float _lastSpawn;
    private Rigidbody2D _rb;
    private Collider2D _col;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _lastSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= _lastSpawn + spawnDelay)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(_rb.position, spawnRadius);
            Physics2DQueryVisualizer.DrawCircle(_rb.position, spawnRadius, Color.cyan);
            List<MaterialObject> matObjs = new();

            foreach (Collider2D hit in hits)
            {
                MaterialObject matObj = hit.gameObject.GetComponentInParent<MaterialObject>();
                if (matObj is not null)
                {
                    matObjs.Add(matObj);
                }
            }

            if (matObjs.Count <= maxNearby)
            {
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        float angle = Random.value * 360;
        float distance = Random.value * spawnRadius;
        Vector3 spawnPos = transform.position + (Vector3)AngleHelper.DegreesToVector(angle);
        GameObject spawned = Instantiate(blueprintLootpool.Select().prefab, spawnPos, Quaternion.identity);
    }
}
