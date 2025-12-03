using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;

    [SerializeField] private GameObject tooltipObject;
    [SerializeField] private TMP_Text tooltipText;

    private RectTransform rectTransform;

    private void Awake()
    {
        Instance = this;
        rectTransform = tooltipObject.GetComponent<RectTransform>();
        tooltipObject.SetActive(false);
    }

    private void Update()
    {
        if (tooltipObject.activeSelf)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform as RectTransform,
                Input.mousePosition,
                null,
                out pos
                );
            rectTransform.anchoredPosition = pos + new Vector2(125, 165);
        }
    }

    public void ShowTooltip(string text)
    {
        tooltipText.text = text;
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        tooltipObject.SetActive(true);

    }

    public void HideTooltip()
    {
        tooltipObject.SetActive(false);
    }
}
