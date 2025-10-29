using UnityEngine;

[CreateAssetMenu(menuName = "Island Game/Building Type")]
public class BuildingType : ScriptableObject
{
    [Header("Identity")]
    public string buildingName;
    public Sprite icon;
    public GameObject prefab;

    [Header("Economy")]
    public ResourceAmount[] buildCost;
    public ResourceAmount[] upkeepPerMonth;
    public ResourceAmount[] productionPerMonth;

    [Header("Population & Morale")]
    public int maxPopulation;
    public float baseMoraleEffect; //how much the morale contributes
    public float moraleSensitivity; //how much is the building affected by disastereffects

    [Header("Upgrades")]
    public BuildingType nextTier; //optional: link to higher tier
    public ResourceAmount[] upgradeCost;

    [Header("UI / Interaction")]
    public bool isInteractable; 
}
