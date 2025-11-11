using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConstructionPlot : MonoBehaviour
{
    public GameObject buildUIPrefab;

    public GameObject PlotOutline;
    public Sprite underConstructionSprite;

    [Header("Ignore...")]
    public bool immuneUI;
    public int daysToFinishConstruction = 0;
    public BuildingType selectedBuildingType;

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
            buildUIPrefab.SetActive(true);
        } else
        {
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

    public void UnderConstruction()
    {
        //change sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = underConstructionSprite;
        PlotOutline.SetActive(false);
    }

    public void FinishConstruction()
    {
        GameObject obj = Instantiate(selectedBuildingType.prefab, this.transform.position, Quaternion.identity);
        BuildingInstance instance = obj.GetComponent<BuildingInstance>();
        instance.data = selectedBuildingType;

        //plot.currentBuilding = instance;
        obj.GetComponent<BuildingInstance>().constructionPlot = this;
        this.gameObject.SetActive(false);

        Debug.Log($"Built {selectedBuildingType.buildingName} at {this.name}");
    }
}
