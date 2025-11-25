using System.Collections.Generic;
using UnityEngine;

public class DisasterInstance
{
    public Disaster disaster;
    public Vector3 position;
    public float remainingDays;
    public List<BuildingInstance> affectedBuildings = new();

    public DisasterInstance (Disaster disaster, Vector3 position)
    {
        this.disaster = disaster;
        this.position = position;
        this.remainingDays = disaster.durationDays;
    }
}

public class DisasterManager : MonoBehaviour
{
    public static DisasterManager Instance { get; private set; }

    [Header("Settings")]
    public List<Disaster> possibleDisasters;
    public float checkIntervalDays = 30f; //check once a month
    public Vector2 worldBounds = new Vector2(1000f, 1000f);

    private TimeManager timeManager;
    private DisasterInstance activeDisaster;

    public bool debugCanTriggerDisasterBool;

    private void Awake()
    {
        Instance = this;
        timeManager = GetComponent<TimeManager>();
    }

    private void Update()
    {

        //For simplicity right now, one second is one day in simulation
        float daysPassed = Time.deltaTime;

        //Check for new disaster periodically
        if (timeManager.currentDay == checkIntervalDays)
        {
            TryTriggerDisaster();
        }

        if (activeDisaster != null)
        {
            TickActiveDisaster(daysPassed);
        }

        //TickBuildingEffects(daysPassed);
    }

    private void TryTriggerDisaster()
    {
        if (debugCanTriggerDisasterBool == false)
            return;

        //Don't start a new disaster if one is active
        if (activeDisaster != null) return;

        foreach (var disaster in possibleDisasters)
        {
            if (Random.value <= disaster.spawnChance)
            {
                TriggerDisaster(disaster);
                return; //only one disaster per check
            }
        }
    }

    public void TriggerDisaster(Disaster disaster)
    {
        //Choose a random world position
        Vector3 pos = new Vector3(
            Random.Range(-worldBounds.x, worldBounds.x),
            0f,
            Random.Range(-worldBounds.y, worldBounds.y)
            );

        activeDisaster = new DisasterInstance(disaster, pos);
        Debug.Log($"Disaster triggered: {disaster.disasterName} at {pos}");

        //Find all buildings within range (for now, all)
        var buildings = FindObjectsByType<BuildingInstance>(FindObjectsSortMode.None);
        Debug.Log(buildings.Length);

        foreach (var building in buildings)
        {
            float distance = Vector3.Distance(building.transform.position, pos);
            if (distance <= disaster.effectRadius)
            {
                Debug.Log($"Should apply disastereffect to {building.name} now");

                foreach (var effect in disaster.effects)
                    building.AddEffect(effect);

                activeDisaster.affectedBuildings.Add(building);
            }
        }
    }

    private void TickActiveDisaster(float daysPassed)
    {
        if (activeDisaster == null) return;

        activeDisaster.remainingDays -= daysPassed;

        if(activeDisaster.remainingDays <= 0)
        {
            Debug.Log($"Disaster ended: {activeDisaster.disaster.disasterName}");
            activeDisaster = null;
        }
    }

    /*private void TickBuildingEffects(float daysPassed)
    {
        var buildings = Object.FindObjectsByType<BuildingInstance>(FindObjectsSortMode.None);
        foreach (var b in buildings)
        {
            b.TickDay();
        }
    }*/
}
