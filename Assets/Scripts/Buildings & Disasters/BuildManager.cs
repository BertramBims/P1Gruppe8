using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    private BuildingType selectedBuildingType;

    private void Awake()
    {
        Instance = this;
    }

    public void ClickOnMap()
    {
        var allMenues = GameObject.FindGameObjectsWithTag("BuildMenu");

        if (EventSystem.current != null && !EventSystem.current.IsPointerOverGameObject())
        {
            for (int i = 0; i < allMenues.Length; i++)
            {
                if (allMenues[i].GetComponentInParent<ConstructionPlot>().immuneUI == true)
                {
                    allMenues[i].GetComponentInParent<ConstructionPlot>().immuneUI = false;
                    continue;
                }
                allMenues[i].gameObject.SetActive(false);
            }
            return;
        }
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

        //Construction time:
        plot.daysToFinishConstruction = 30;
        plot.UnderConstruction();
        GameObject.Find("GameManager").GetComponent<TimeManager>().plotsOngoingConstruction.Add(plot);

        //Build it:
        plot.selectedBuildingType = selectedBuildingType;
    }

    public void CloseMenues()
    {
        var allMenues = GameObject.FindGameObjectsWithTag("BuildMenu");

        for (int i = 0; i < allMenues.Length; i++)
        {
            allMenues[i].gameObject.SetActive(false);
        }
    }
}
