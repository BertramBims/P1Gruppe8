using UnityEngine;

[CreateAssetMenu(menuName = "Island Game/Building Type")]
public class BuildingType : ScriptableObject
{
    [Header("Identity")]
    public string buildingName;
    public string description;
    public Sprite sprite;
    public GameObject prefab;

    [Header("Economy")]
    public ResourceAmount[] buildCost;
    public ResourceAmount[] upkeepPerDay;
    public ResourceAmount[] productionPerDay;

    [Header("Population & Morale")]
    public int maxPopulation;
    public float baseMoraleEffect; //how much the morale contributes
    public float minimumMoraleEffect; //how much is the building affected by disastereffects

    [Header("Upgrades")]
    public BuildingType nextTier; //optional: link to higher tier
    public BuildingType previousTier; //optional: link to previous tier
    public ResourceAmount[] upgradeCost;

    [Header("UI / Interaction")]
    public bool isInteractable; 
}
