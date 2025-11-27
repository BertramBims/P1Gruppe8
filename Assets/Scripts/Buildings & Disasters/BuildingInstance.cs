using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

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
    public ConstructionPlot constructionPlot; //plot that this building was built on;
    public TMP_Text buildingText;

    private float productionMultiplier = 1f;
    public List<ActiveEffect> activeEffects = new();

    [Header("Disaster Effect States & UI Elements...")]
    public GameObject floodedState;
    public GameObject floodedUIButton;
    public GameObject brokenWindowsState;
    public GameObject brokenWindowsUIButton;

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
                change[prod.type] = 0;
            change[prod.type] += prod.amount;
        }

        //scale by morale
        /*float efficiency = currentMorale / 100f;
        foreach (var key in change.Keys.ToList())
        {
            change[key] = Mathf.RoundToInt(change[key] * efficiency);
        }*/

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

        string buildText = BuildBuildingText(data);
        buildingText.text = buildText;
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
            float finalAmount = Mathf.Round(resource.amount * productionMultiplier * (currentMorale / 100f));
            ResourceManager.Instance.Add(resource.type, finalAmount);
        }
    }

    public void ModifyMorale(float change)
    {
        currentMorale = Mathf.Clamp(currentMorale + change, 0f, 100f);
    }

    public void AddEffect (DisasterEffect effect)
    {
        bool canEffectThisBuilding = false;
        for (int i = 0; i < effect.buildingsItCanEffect.Length; i++)
        {
            if (effect.buildingsItCanEffect[i] == data)
                canEffectThisBuilding = true;
        }
        if (!canEffectThisBuilding)
            return;

        effect.ApplyImmediate(this);

        if (effect.effectName == "Flooding")
        {
            floodedState.SetActive(true);
            floodedUIButton.SetActive(true);
        }
        if (effect.effectName == "Broken Windows")
        {
            brokenWindowsState.SetActive(true);
            brokenWindowsUIButton.SetActive(true);
        }

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
                if (active.effect.effectName == "Flooding")
                    floodedState.SetActive(false);
                if (active.effect.effectName == "Broken Windows")
                    brokenWindowsState.SetActive(false);

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

    public void Downgrade()
    {
        if (data.previousTier == null) return;
        data = data.nextTier;
        Debug.Log($"{name} downgraded to {data.buildingName}");
    }

    public void DestroyBuilding()
    {
        constructionPlot.gameObject.SetActive(true);
        Destroy(gameObject);
    }

    public void DoTrade(ResourceAmount[] cost, ResourceAmount[] purchase)
    {
        if (ResourceManager.Instance.TrySpend(cost))
        {
            for (int i = 0; i < purchase.Length; i++)
            {
                ResourceManager.Instance.Add(purchase[i].type, purchase[i].amount);
            }
        }
    }

    private string BuildBuildingText(BuildingType data)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.AppendLine(data.description);
        sb.AppendLine("");

        if (data.upkeepPerDay.Length > 0)
        {
            sb.AppendLine("<b>Upkeep / Day:</b>");
            foreach (var up in data.upkeepPerDay)
                sb.AppendLine($"- {up.type}: {up.amount}");
        }

        if (data.productionPerDay.Length > 0)
        {
            sb.AppendLine("");
            sb.AppendLine("<b>Production / Day:</b>");
            foreach (var prod in data.productionPerDay)
                sb.AppendLine($"+ {prod.type}: {prod.amount}");
        }

        return sb.ToString();
    }

    //deal with disastereffect
    public void DealWithDisasterEffect(DisasterEffect effect)
    {
        if (!ResourceManager.Instance.TrySpend(effect.costToRemoveEffect))
        {
            Debug.Log("Not enough resources!");
            return;
        }

        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            if (activeEffects[i].effect.effectName == effect.effectName)
            {
                activeEffects.RemoveAt(i);
                UpdateDisasterEffects(activeEffects[i].effect);
                return;
            }
        }
    }

    //update ui for disastereffects
    public void UpdateDisasterEffects(DisasterEffect effect)
    {
        if (effect.effectName == "Flooding")
        {
            floodedState.SetActive(false);
            floodedUIButton.SetActive(false);
        }
        if (effect.effectName == "Broken Windows")
        {
            brokenWindowsState.SetActive(false);
            brokenWindowsUIButton.SetActive(false);
        }
    }
}
