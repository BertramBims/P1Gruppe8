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

    private void UpdateDisplay(ResourceType changedType, int newAmount)
    {
        if (changedType == type)
            amountText.text = $"{newAmount}";
    }
}
