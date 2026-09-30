using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialObjectLootpool", menuName = "ScriptableObjects/Material Object Lootpool")]
public class MaterialObjectLootpool : ScriptableObject
{
    [Serializable]
    public class Entry
    {
        public MaterialObjectBlueprint blueprint;
        [Min(0f)] public float weight = 1f;
    }

    [SerializeField] private List<Entry> entries = new();

    public bool TrySelect(out MaterialObjectBlueprint blueprint)
    {
        blueprint = null;
        double totalWeight = 0d;

        foreach (Entry entry in entries)
        {
            if (entry != null && entry.blueprint != null && IsValidWeight(entry.weight))
                totalWeight += entry.weight;
        }

        if (totalWeight <= 0d)
            return false;

        double roll = UnityEngine.Random.value * totalWeight;
        double cumulative = 0d;
        MaterialObjectBlueprint lastValid = null;

        foreach (Entry entry in entries)
        {
            if (entry == null || entry.blueprint == null || !IsValidWeight(entry.weight))
                continue;

            cumulative += entry.weight;
            lastValid = entry.blueprint;
            if (roll < cumulative)
            {
                blueprint = entry.blueprint;
                return true;
            }
        }

        blueprint = lastValid;
        return blueprint != null;
    }

    private static bool IsValidWeight(float weight) => weight > 0f && !float.IsNaN(weight) && !float.IsInfinity(weight);

    private void OnValidate()
    {
        if (entries == null)
            return;

        foreach (Entry entry in entries)
        {
            if (entry != null && (float.IsNaN(entry.weight) || float.IsInfinity(entry.weight) || entry.weight < 0f))
                entry.weight = 0f;
        }
    }
}
