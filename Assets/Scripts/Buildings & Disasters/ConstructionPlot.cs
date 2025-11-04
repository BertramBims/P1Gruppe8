using UnityEngine;

public class ConstructionPlot : MonoBehaviour
{
    public GameObject buildUIPrefab;

    public void OnMouseDown()
    {
        //open ui here
        if (buildUIPrefab.activeInHierarchy == true)
            buildUIPrefab.SetActive(false);
        if (buildUIPrefab.activeInHierarchy == false)
            buildUIPrefab.SetActive(true);
    }

    public void SelectBuilding(BuildingType type)
    {
        BuildManager.Instance.SelectBuildingType(type);
    }

    public void TryBuild()
    {
        BuildManager.Instance.TryBuildAt(this);
    }
}
