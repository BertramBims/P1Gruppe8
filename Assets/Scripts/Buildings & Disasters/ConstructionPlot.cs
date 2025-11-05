using UnityEngine;

public class ConstructionPlot : MonoBehaviour
{
    public GameObject buildUIPrefab;

    public void OnMouseDown()
    {
        bool wasOpen = buildUIPrefab.activeSelf;

        var allMenues = GameObject.FindGameObjectsWithTag("BuildMenu");

        for (int i = 0; i < allMenues.Length; i++)
        {
            allMenues[i].gameObject.SetActive(false);
        }

        if(!wasOpen)
        {
            Debug.Log("set true");
            buildUIPrefab.SetActive(true);
        } else
        {
            Debug.Log("set false");
            buildUIPrefab.SetActive(false);
        }
    }

    public void SelectBuilding(BuildingType type)
    {
        BuildManager.Instance.SelectBuildingType(type);
    }

    public void TryBuild()
    {
        BuildManager.Instance.TryBuildAt(this);
    }

    public void CloseMenu()
    {
        buildUIPrefab.SetActive(false);
    }
}
