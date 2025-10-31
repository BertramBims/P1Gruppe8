using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActiveEffect
{
    public DisasterEffect effect;
    public float remainingDays;
}

public class BuildingInstance : MonoBehaviour
{
    public BuildingType data;
    public int currentPopulation;
    public float currentMorale = 100f;
    public bool isActive = true;

    private float productionMultiplier = 1f;
    public List<ActiveEffect> activeEffects = new();

    private void Start()
    {
        if (data == null)
        {
            Debug.LogError($"{name} has no BuildingType assigned!");
            return;
        }

        currentPopulation = data.maxPopulation;
    }

    public void TickMonth()
    {
        if (!isActive)
            return;

        ApplyUpkeep();
        ProduceResources();
    }

    private void ApplyUpkeep()
    {
        bool upkeepPaid = ResourceManager.Instance.TrySpend(data.upkeepPerMonth);
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

        foreach(var resource in data.productionPerMonth)
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
