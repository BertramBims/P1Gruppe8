using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    [Header("Resource Type")]
    public ResourceType type;

    [Header("UI Element")]
    public TMP_Text amountText;

    private void Start()
    {
        //Subscribe to changes
        ResourceManager.Instance.OnResourceChanged += UpdateDisplay;

        //Initialize on start
        UpdateDisplay(type, ResourceManager.Instance.Get(type));
    }

    private void OnDestroy()
    {
        //Unsubscribe to prevent memory leaks
        if (ResourceManager.Instance != null)
            ResourceManager.Instance.OnResourceChanged -= UpdateDisplay;
    }

    private void UpdateDisplay(ResourceType changedType, float newAmount)
    {
        if (changedType != type)
            return;

        float income = ResourceManager.Instance.GetDailyIncome(type);
        string incomeSign = income >= 0 ? "+" : "";
        string color = income >= 0 ? "green" : "red";
        amountText.text = $"{newAmount} (<color={color}>{incomeSign}{income}/day</color>)";
    }
}
