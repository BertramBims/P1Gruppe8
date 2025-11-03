using UnityEngine;

public class ConstructionPlot : MonoBehaviour
{
    public BuildingInstance currentBuilding;

    public void OnMouseDown()
    {
        if (currentBuilding == null)
        {
            BuildManager.Instance.TryBuildAt(this);
        } else
        {
            // later open up UI
        }
    }
}
