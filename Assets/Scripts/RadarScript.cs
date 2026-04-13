using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RadarScript : MonoBehaviour
{
    //this script is very manual
    public TMP_Text radarText;
    public Slider radarSlider;
    private float typhoonRisk;
    public Disaster typhoonDisaster;

    public void UpdateRadar()
    {
        typhoonRisk = DisasterManager.Instance.cumulativeSpawnChance + typhoonDisaster.spawnChance;

        radarText.text = $"Typhoon Risk: {typhoonRisk}%";
        radarSlider.value = typhoonRisk;
    }
}
