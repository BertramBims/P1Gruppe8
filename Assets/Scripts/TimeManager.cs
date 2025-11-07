using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
    public float dayCounter = 0f;
    public int currentDay;
    public int currentMonth;
    public bool isTimePaused = true;
    public GameObject pausedIndicator;
    public List<ConstructionPlot> plotsOngoingConstruction;

    private void Update()
    {
        //time pause
        if (isTimePaused)
        {
            return;
        }

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
        if (plotsOngoingConstruction != null)
        {
            //do shit
            for (int i = 0; i < plotsOngoingConstruction.Count; i++)
            {
                plotsOngoingConstruction[i].daysToFinishConstruction--;

                if (plotsOngoingConstruction[i].daysToFinishConstruction < 0)
                {
                    plotsOngoingConstruction[i].FinishConstruction();
                    plotsOngoingConstruction.RemoveAt(i);
                }
            }
        }

        var buildings = GameObject.FindObjectsByType<BuildingInstance>(FindObjectsSortMode.None);
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i].TickDay();
        }

        ResourceManager.Instance.RecalculateDailyIncome();
    }

    private void TickMonth()
    {
        var buildings = GameObject.FindObjectsByType<BuildingInstance>(FindObjectsSortMode.None);
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i].TickMonth();
        }
    }

    public void PauseTime()
    {
        if (isTimePaused)
        {
            isTimePaused = false;
            pausedIndicator.SetActive(false);
        } else
        {
            isTimePaused = true;
            pausedIndicator.SetActive(true);
        }
    }
}
