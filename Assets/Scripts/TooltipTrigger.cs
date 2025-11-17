using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea] public string manualText;
    public BuildingType buildingInfo;

    public void OnPointerEnter(PointerEventData eventData)
    {
        string tooltip = "";

        if (buildingInfo != null)
            tooltip = BuildBuildingTooltip(buildingInfo);
        else
            tooltip = manualText;

        TooltipManager.Instance.ShowTooltip(tooltip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.HideTooltip();
    }

    private string BuildBuildingTooltip(BuildingType data)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.AppendLine($"<b>{data.buildingName}</b>");
        sb.AppendLine(data.description);
        sb.AppendLine("");

        if(data.upkeepPerDay.Length > 0)
        {
            sb.AppendLine("<b>Upkeep / Day:</b>");
            foreach (var up in data.upkeepPerDay)
                sb.AppendLine($"- {up.type}: {up.amount}");
        }

        if(data.productionPerDay.Length > 0)
        {
            sb.AppendLine("");
            sb.AppendLine("<b>Production / Day:</b>");
            foreach (var prod in data.productionPerDay)
                sb.AppendLine($"+ {prod.type}: {prod.amount}");
        }

        return sb.ToString();
    }
}
