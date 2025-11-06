using NUnit.Framework;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum ResourceType
{
    Pesos,
    Food,
    Lumber,
    Stone
}

[System.Serializable]
public struct ResourceAmount
{
    public ResourceType type;
    public float amount;
}

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    private Dictionary<ResourceType, float> resources = new Dictionary<ResourceType, float>();

    public event Action<ResourceType, float> OnResourceChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            resources[type] = 500;
        }
    }

    public float Get(ResourceType type)
    {
        return resources[type];
    }

    public void Add (ResourceType type, int amount)
    {
        resources[type] += amount;
        Debug.Log($"{amount} {type} added. Total: {resources[type]}");
        OnResourceChanged?.Invoke(type, resources[type]); //Notifies listeners
    }

    public bool TrySpend(params ResourceAmount[] costs)
    {
        //Check if we can afford all costs
        foreach (var cost in costs)
        {
            if (resources[cost.type] < cost.amount)
                return false;
        }

        foreach (var cost in costs)
        {
            resources[cost.type] -= cost.amount;
            OnResourceChanged?.Invoke(cost.type, resources[cost.type]); //Notifies listeners
        }

        Debug.Log("Resources spent successfully!");
        return true;
    }
}
