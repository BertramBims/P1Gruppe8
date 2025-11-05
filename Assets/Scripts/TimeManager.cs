using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float dayCounter = 0f;
    public int currentDay;
    public int currentMonth;

    private void Update()
    {
        float daysPassed = Time.deltaTime;
        dayCounter += daysPassed;

        //ticks month
        if (currentDay > 30)
        {
            dayCounter = 0;
            currentDay = 0;
            currentMonth++;
            TickMonth();
            Debug.Log("MonthTick");
        }

        //ticks day
        if (dayCounter > currentDay + 1)
        {
            currentDay++;
            TickDay();
            Debug.Log("DayTick");
        }
    }

    private void TickDay()
    {
        var buildings = GameObject.FindObjectsByType<BuildingInstance>(FindObjectsSortMode.None);
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i].TickDay();
        }
    }

    private void TickMonth()
    {
        var buildings = GameObject.FindObjectsByType<BuildingInstance>(FindObjectsSortMode.None);
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i].TickMonth();
        }
    }
}
