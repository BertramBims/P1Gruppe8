using UnityEngine;
using UnityEngine.EventSystems;

public class ConstructionPlot : MonoBehaviour
{
    public GameObject buildUIPrefab;
    public bool immuneUI;

    public void OnMouseDown()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //buildUIPrefab.SetActive(true);
        immuneUI = true;

        bool wasOpen = buildUIPrefab.activeSelf;

        var allMenues = GameObject.FindGameObjectsWithTag("BuildMenu");

        /*if (EventSystem.current != null && !EventSystem.current.IsPointerOverGameObject() && buildUIPrefab.activeSelf)
        {
            Debug.Log("Hej!");
            for (int i = 0; i < allMenues.Length; i++)
            {
                allMenues[i].gameObject.SetActive(false);
            }
            return;
        }*/

        for (int i = 0; i < allMenues.Length; i++)
        {
            allMenues[i].gameObject.SetActive(false);
        }

        if (!wasOpen)
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
