using System;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType { Milk, Egg, Flour }

public class Inventory : MonoBehaviour
{
    private static Inventory instance;
    public static Inventory Instance
    {
        get
        {
            if (instance == null)
            {
                var go = new GameObject("Inventory");
                instance = go.AddComponent<Inventory>();
                DontDestroyOnLoad(go);
                Debug.Log("[Inventory] Created");
            }
            return instance;
        }
    }

    private readonly Dictionary<IngredientType, int> items = new Dictionary<IngredientType, int>();
    public event Action<IngredientType> Changed;

    // задаёт стартовое количество, но только если этот продукт ещё не был записан
    public void EnsureInitialized(IngredientType type, int startCount)
    {
        if (items.ContainsKey(type)) return;

        items[type] = startCount;
        Debug.Log($"[Inventory] INIT {type} = {startCount}");
    }

    public int Get(IngredientType type)
    {
        return items.TryGetValue(type, out int count) ? count : 0;
    }

    public void Add(IngredientType type, int amount = 1)
    {
        items[type] = Get(type) + amount;
        Debug.Log($"[Inventory] ADD {type} +{amount} -> now {items[type]}");
        Changed?.Invoke(type);
    }

    public bool TryConsume(IngredientType type)
    {
        int count = Get(type);
        if (count <= 0)
        {
            Debug.LogWarning($"[Inventory] CONSUME FAILED: no {type} left");
            return false;
        }

        items[type] = count - 1;
        Debug.Log($"[Inventory] CONSUME {type} -> now {items[type]}");
        Changed?.Invoke(type);
        return true;
    }
}