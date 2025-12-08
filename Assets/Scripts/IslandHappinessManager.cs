using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IslandHappinessManager : MonoBehaviour
{
    public static IslandHappinessManager Instance;

    [Header("UI References")]
    [SerializeField] private Slider happinessSlider;
    [SerializeField] private TMP_Text happinessText;

    //private List<BuildingInstance> buildings = new List<BuildingInstance>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    //call this when building is constructed
    /*public void RegisterBuilding(BuildingInstance building)
    {
        if (!buildings.Contains(building))
        {
            buildings.Add(building);
            RecalculateHappiness();
        }
    }*/

    //Call this when a building is destroyed or removed
    /*public void UnregisterBuilding(BuildingInstance building)
    {
        if (buildings.Contains(building))
        {
            buildings.Remove(building);
            RecalculateHappiness();
        }
    }*/

    //Calculates overall happiness morale and updates UI;
    public void RecalculateHappiness()
    {
        if(BuildingInstance.AllBuildings.Count == 0)
        {
            happinessSlider.value = 50;
            happinessText.text = "50%";
            return;
        }

        float total = 0;

        foreach (var building in BuildingInstance.AllBuildings)
            total += building.currentMorale;

        float average = total / BuildingInstance.AllBuildings.Count;

        //UI Update
        happinessSlider.value = average;
        happinessText.text = Mathf.RoundToInt(average) + "%";
    }
}
