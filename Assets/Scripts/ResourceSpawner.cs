using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private MaterialObjectLootpool blueprintLootpool;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float maxNearby;
    private float _lastSpawn;
    
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
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, spawnRadius);
            if (DebugManager.Debug)
                Physics2DQueryVisualizer.DrawCircle(transform.position, spawnRadius, Color.cyan);
            List<ResourceObject> matObjs = new();

            foreach (Collider2D hit in hits)
            {
                ResourceObject matObj = hit.gameObject.GetComponentInParent<ResourceObject>();
                if (matObj is not null)
                {
                    matObjs.Add(matObj);
                }
            }

            if (matObjs.Count <= maxNearby)
            {
                Spawn();
            }
            _lastSpawn = Time.time;
        }
    }

    private void Spawn()
    {
        if (blueprintLootpool is null || !blueprintLootpool.TrySelect(out MaterialObjectBlueprint blueprint) || blueprint.prefab is null)
            return;

        float angle = Random.value * 360f;
        float distance = Random.value * spawnRadius;
        Vector3 spawnPos = transform.position + (Vector3)AngleHelper.DegreesToVector(angle) * distance;
        Instantiate(blueprint.prefab, spawnPos, Quaternion.identity);
    }
}
