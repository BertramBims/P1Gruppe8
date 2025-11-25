using UnityEngine;

[CreateAssetMenu(menuName = "Island Game/Disaster Effect")]
public class DisasterEffect : ScriptableObject
{
    public string effectName;
    public float moraleChange;
    public float productionMultiplier = 1f;
    public int populationLoss;
    public float durationDays = 3f;
    public BuildingType[] buildingsItCanEffect;

    //Optional ongoing damage each day
    public float dailyMoraleDecay;
    public int dailyPopulationLoss;

    public void ApplyImmediate(BuildingInstance building)
    {
        building.ModifyMorale(moraleChange);
        building.currentPopulation = Mathf.Max(0, building.currentPopulation - populationLoss);

        Debug.Log($"{building.data.buildingName} affected by {effectName}");
    }
}
