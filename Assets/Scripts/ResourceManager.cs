using System.Collections.Generic;
using System;
using System.Linq;
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
    private Dictionary<ResourceType, float> dailyIncome = new Dictionary<ResourceType, float>();

    public event Action<ResourceType, float> OnResourceChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        resources[ResourceType.Pesos] = 75;
        resources[ResourceType.Food] = 75;
        resources[ResourceType.Lumber] = 75;
        resources[ResourceType.Stone] = 75;
    }

    public float Get(ResourceType type) => resources.TryGetValue(type, out float value) ? value : 0;
    public float GetDailyIncome(ResourceType type) => dailyIncome.TryGetValue(type, out float value) ? value : 0;

    /*public float Get(ResourceType type)
    {
        return resources[type];
    }*/

    public void RecalculateDailyIncome()
    {
        dailyIncome.Clear();

        foreach(var building in BuildingInstance.AllBuildings)
        {
            var changes = building.GetDailyResourceChange();

            foreach (var kvp in changes)
            {
                if (!dailyIncome.ContainsKey(kvp.Key))
                    dailyIncome[kvp.Key] = 0;

                dailyIncome[kvp.Key] += kvp.Value;
            }
        }

        //Notify UI listeners
        foreach (var type in dailyIncome.Keys)
        {
            OnResourceChanged?.Invoke(type, Get(type));
        }
    }

    public void Add (ResourceType type, float amount)
    {
        if (!resources.ContainsKey(type))
            resources[type] = 0;

        resources[type] += amount;
        //Debug.Log($"{amount} {type} added. Total: {resources[type]}");
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
