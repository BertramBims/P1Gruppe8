using UnityEngine;

[CreateAssetMenu(menuName = "Island Game/Disaster")]
public class Disaster : ScriptableObject
{
    public string disasterName;
    public Sprite icon;
    public float durationDays;
    public float spawnChance = 0.1f; //10% per check
    public float effectRadius = 9999f;

    [Header("Effects")]
    public DisasterEffect[] effects;
}
