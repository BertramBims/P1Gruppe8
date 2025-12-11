using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

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
    public float cumulativeSpawnChance = 0f;

    public GameObject radarUI;
    private bool disasterWaitBool;

    [Header("For Disaster Visuals")]
    [SerializeField] private Volume baseVolume, cycloneVolume;
    private bool currentlyDisasterVolume = true;

    public GameObject cycloneDisasterVisual;

    [Header("Basin Stuff...")]
    public Sprite emptyBasin;
    public Sprite fullBasin;

    [Header("Library")]
    public GameObject TyphoonInformation;
    public GameObject TyphoonNewspaper;
    private TimeManager PauseScript;


    private void Awake()
    {
        Instance = this;
        timeManager = GetComponent<TimeManager>();
        PauseScript = GameObject.Find("GameManager").GetComponent<TimeManager>();

    }

    private void Update()
    {
        //time pause
        if (timeManager.isTimePaused)
            return;

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

        if (disasterWaitBool == true)
            return;

        //Don't start a new disaster if one is active
        if (activeDisaster != null) return;

        foreach (var disaster in possibleDisasters)
        {
            float rolledChance = Random.Range(0f, 100f);
            Debug.Log(rolledChance);
            if (rolledChance <= disaster.spawnChance + cumulativeSpawnChance)
            {
                cumulativeSpawnChance = 0;
                Debug.Log("1");
                TriggerDisaster(disaster);
                return; //only one disaster per check
            } else
            {
                Debug.Log("2");
                cumulativeSpawnChance += disaster.spawnChance;
                disasterWaitBool = true;
                StartCoroutine(ReenableDisasterSpawning());
                return;
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
        TriggerDisasterVisual(activeDisaster);
        SwitchVolumes(.1f, false);

        foreach (var building in buildings)
        {
            float distance = Vector3.Distance(building.transform.position, pos);
            if (distance <= disaster.effectRadius)
            {
                Debug.Log($"Should apply disastereffect to {building.name} now");

                foreach (var effect in disaster.effects)
                {
                    if (effect.effectName == "Flooding")
                    {
                        for (int i = 0; buildings.Length > i; i++)
                        {
                            if (buildings[i].data.buildingName == "Basin")
                            {
                                if (buildings[i].GetComponentInChildren<SpriteRenderer>().sprite != fullBasin)
                                {
                                    buildings[i].GetComponentInChildren<SpriteRenderer>().sprite = fullBasin;
                                    Debug.Log($"{buildings[i].name} stopped {building} from getting flooded");
                                }
                            }
                        }
                    }

                    /*bool buildingAlreadyAffected = false;
                    for (int i = 0; activeDisaster.affectedBuildings.Count > i; i++)
                    {
                        if (activeDisaster.affectedBuildings[i] == building)
                        {
                            buildingAlreadyAffected = true;
                        }
                    }
                    if (buildingAlreadyAffected == false)
                    {
                        building.AddEffect(effect);
                        activeDisaster.affectedBuildings.Add(building);
                    }*/
                    building.AddEffect(effect);
                }

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
            StopDisasterVisual(activeDisaster);
            SwitchVolumes(.1f, true);
            activeDisaster = null;
        }
    }

    private void TriggerDisasterVisual(DisasterInstance activeDisaster)
    {
        if(activeDisaster.disaster.disasterName == "Tropical Cyclone")
        {
            cycloneDisasterVisual.SetActive(true);
            StartCoroutine(ShowDisasterInfoAfterDelay(2f));
        }
    }

    private void StopDisasterVisual(DisasterInstance activeDisaster)
    {
        if (activeDisaster.disaster.disasterName == "Tropical Cyclone")
        {
            cycloneDisasterVisual.SetActive(false);
            Debug.Log("Stops Cyclone");

            var buildings = FindObjectsByType<BuildingInstance>(FindObjectsSortMode.None);
            for (int i = 0; i < buildings.Length; i++)
            {
                if (buildings[i].data.buildingName == "Basin" && buildings[i].GetComponentInChildren<SpriteRenderer>().sprite == fullBasin)
                    buildings[i].GetComponentInChildren<SpriteRenderer>().sprite = emptyBasin;
            }
        }
    }

    public void SwitchVolumes(float speed, bool switchFromDisasterVolume)
    {
        if (!currentlyDisasterVolume) return;

        StartCoroutine(SwitchVolumes_(speed, switchFromDisasterVolume));
        currentlyDisasterVolume = false;
    }

    private IEnumerator SwitchVolumes_(float speed, bool switchFromDisasterVolume)
    {
        Volume selectedVol = switchFromDisasterVolume ? baseVolume : cycloneVolume;
        Volume notSelectedVol = !switchFromDisasterVolume ? baseVolume : cycloneVolume;

        while (selectedVol.weight < 1)
        {
            selectedVol.weight += Time.deltaTime * speed;
            notSelectedVol.weight -= Time.deltaTime * speed;
            yield return null;
        }

        selectedVol.weight = 1;
        notSelectedVol.weight = 0;

        currentlyDisasterVolume = true;
    }

    public IEnumerator ShowDisasterInfoAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        TyphoonNewspaper.SetActive(true);
        TyphoonInformation.SetActive(true);
        PauseScript.PauseTime(); 
    }

    /*private void TickBuildingEffects(float daysPassed)
    {
        var buildings = Object.FindObjectsByType<BuildingInstance>(FindObjectsSortMode.None);
        foreach (var b in buildings)
        {
            b.TickDay();
        }
    }*/

    public IEnumerator ReenableDisasterSpawning()
    {
        yield return new WaitForSeconds(2f);
        disasterWaitBool = false;
    }
}
