using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class Lootpool<T>
{
    private List<T> _items;
    private List<float> _weights;
    private int _length;
    private float _totalWeight;

    public Lootpool()
    {
        _items = new List<T>();
        _weights = new List<float>();
        _length = 0;
        _totalWeight = 0;
    }

    public Lootpool(List<T> items, List<float> weights)
    {
        if (items.Count != weights.Count)
        {
            throw new ArgumentException("Items and Weights lists are of differing lengths!");
        }
        _items = items;
        _weights = weights;
        _length = items.Count;
        _totalWeight = weights.Sum();
    }

    public void Add(T item, float weight)
    {
        _items.Add(item);
        _weights.Add(weight);
        _length++;
        _totalWeight += weight;
    }

    public void Remove(T item)
    {
        var index = _items.IndexOf(item);
        var weight = _weights[index];
        _items.Remove(item);
        _weights.RemoveAt(index);
        _length--;
        _totalWeight -= weight;
    }
    
    private int ChooseIndex()
    {
        if (_totalWeight <= 0f)
            throw new InvalidOperationException("Loot pool has no positive weights.");

        var roll = Random.value * _totalWeight;
        var cumulative = 0f;

        for (int i = 0; i < _items.Count; i++)
        {
            cumulative += _weights[i];
            if (roll < cumulative)
                return i;
        }

        for (int i = _weights.Count - 1; i >= 0; i--)
            if (_weights[i] > 0f)
                return i;

        throw new InvalidOperationException("Loot pool has no positive weights.");
    }

    public T Pull()
    {
        var index = ChooseIndex();
        var item = _items[index];
        var weight = _weights[index];
        _items.RemoveAt(index);
        _weights.RemoveAt(index);
        _length--;
        _totalWeight -= weight;
        return item;
    }

    public T Select()
    {
        var index = ChooseIndex();
        var item = _items[index];
        return item;
    }

    public int GetLength()
    {
        return _length;
    }
}
