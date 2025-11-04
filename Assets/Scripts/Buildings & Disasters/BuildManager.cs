using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    private BuildingType selectedBuildingType;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectBuildingType(BuildingType type)
    {
        selectedBuildingType = type;
    }

    public void TryBuildAt(ConstructionPlot plot)
    {
        if (selectedBuildingType == null) return;

        if(!ResourceManager.Instance.TrySpend(selectedBuildingType.buildCost))
        {
            Debug.Log("Not enough resources!");
            return;
        }

        //Build it:
        GameObject obj = Instantiate(selectedBuildingType.prefab, plot.transform.position, Quaternion.identity);
        BuildingInstance instance = obj.GetComponent<BuildingInstance>();
        instance.data = selectedBuildingType;

        //plot.currentBuilding = instance;
        plot.gameObject.SetActive(false);

        Debug.Log($"Built {selectedBuildingType.buildingName} at {plot.name}");
    }
}
