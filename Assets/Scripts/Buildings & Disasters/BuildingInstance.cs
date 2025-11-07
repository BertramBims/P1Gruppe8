using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ActiveEffect
{
    public DisasterEffect effect;
    public float remainingDays;
}

public class BuildingInstance : MonoBehaviour
{
    public static List<BuildingInstance> AllBuildings = new List<BuildingInstance>();
    public BuildingType data;
    public int currentPopulation;
    public float currentMorale = 100f;
    public bool isActive = true;

    private float productionMultiplier = 1f;
    public List<ActiveEffect> activeEffects = new();

    public Dictionary<ResourceType, float> GetDailyResourceChange()
    {
        Dictionary<ResourceType, float> change = new Dictionary<ResourceType, float>();

        //subtract upkeep
        foreach(var upkeep in data.upkeepPerDay)
        {
            if (!change.ContainsKey(upkeep.type))
                change[upkeep.type] = 0;
            change[upkeep.type] -= upkeep.amount;
        }

        //add production
        foreach(var prod in data.productionPerDay)
        {
            if (!change.ContainsKey(prod.type))
                change[prod.type] += prod.amount;
        }

        //scale by morale
        float efficiency = currentMorale / 100f;
        foreach (var key in change.Keys.ToList())
        {
            change[key] = Mathf.RoundToInt(change[key] * efficiency);
        }

        return change;
    }

    private void Start()
    {
        if (data == null)
        {
            Debug.LogError($"{name} has no BuildingType assigned!");
            return;
        }

        currentPopulation = data.maxPopulation;
    }

    private void OnEnable()
    {
        AllBuildings.Add(this);
    }

    private void OnDisable()
    {
        AllBuildings.Remove(this);
    }

    public void TickMonth()
    {
        if (!isActive)
            return;
    }

    private void ApplyUpkeep()
    {
        bool upkeepPaid = ResourceManager.Instance.TrySpend(data.upkeepPerDay);
        if (!upkeepPaid)
        {
            //ModifyMorale(-10f);
        }
    }

    private void ProduceResources()
    {
        if (currentMorale <= 0f) return;

        float totalMultiplier = productionMultiplier;
        foreach (var active in activeEffects)
            totalMultiplier *= active.effect.productionMultiplier;

        foreach(var resource in data.productionPerDay)
        {
            int finalAmount = Mathf.RoundToInt(resource.amount * productionMultiplier * (currentMorale / 100f));
            ResourceManager.Instance.Add(resource.type, finalAmount);
        }
    }

    public void ModifyMorale(float change)
    {
        currentMorale = Mathf.Clamp(currentMorale + change, 0f, 100f);
    }

    public void AddEffect (DisasterEffect effect)
    {
        effect.ApplyImmediate(this);

        activeEffects.Add(new ActiveEffect
        {
            effect = effect,
            remainingDays = effect.durationDays
        });

        Debug.Log($"{data.buildingName} gained effect: {effect.effectName}");
    }

    public void TickDay()
    {
        ApplyUpkeep();
        ProduceResources();

        //Apply daily decay and update durations
        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            var active = activeEffects[i];

            ModifyMorale(-active.effect.dailyMoraleDecay);
            currentPopulation = Mathf.Max(0, currentPopulation - active.effect.dailyPopulationLoss);

            active.remainingDays -= 1f;

            if(active.remainingDays <= 0)
            {
                activeEffects.RemoveAt(i);
                Debug.Log($"{data.buildingName} recovered from {active.effect.effectName}");
            }
        }
    }

    public void Upgrade()
    {
        if (data.nextTier == null) return;
        if (ResourceManager.Instance.TrySpend(data.upgradeCost))
        {
            data = data.nextTier;
            Debug.Log($"{name} upgraded to {data.buildingName}");
        }
    }
}
