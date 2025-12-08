using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public float dayCounter = 0f;
    public int currentDay;
    public int currentMonth = 1;
    public bool isTimePaused = true;
    public GameObject pausedIndicator;
    private string monthString = "JANUARY";
    public TMP_Text dayText;
    public TMP_Text monthText;
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
            if (currentMonth == 13)
                currentMonth = 1;
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
                plotsOngoingConstruction[i].UpdateConstructionSliderProgress();

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
            buildings[i].CalculateMoraleModifier();
        }

        dayText.text = $"DAY {currentDay}";
        monthText.text = $"{monthString}";
        IslandHappinessManager.Instance.RecalculateHappiness();
        ResourceManager.Instance.RecalculateDailyIncome();
    }

    private void TickMonth()
    {
        DecideCurrentMonth();

        var buildings = GameObject.FindObjectsByType<BuildingInstance>(FindObjectsSortMode.None);
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i].TickMonth();
        }
        dayText.text = $"DAY {currentDay}";
        monthText.text = $"{monthString}";
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

    public void DecideCurrentMonth()
    {
        if (currentMonth == 1)
            monthString = "JANUARY";
        if (currentMonth == 2)
            monthString = "FEBRUARY";
        if (currentMonth == 3)
            monthString = "MARCH";
        if (currentMonth == 4)
            monthString = "APRIL";
        if (currentMonth == 5)
            monthString = "MAY";
        if (currentMonth == 6)
            monthString = "JUNE";
        if (currentMonth == 7)
            monthString = "JULY";
        if (currentMonth == 8)
            monthString = "AUGUST";
        if (currentMonth == 9)
            monthString = "SEPTEMBER";
        if (currentMonth == 10)
            monthString = "OCTOBER";
        if (currentMonth == 11)
            monthString = "NOVEMBER";
        if (currentMonth == 12)
            monthString = "DECEMBER";

    }
}
