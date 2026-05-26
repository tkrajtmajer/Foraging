using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    [Header("Duration & Time Settings")]
    [Tooltip("Minute duration in total seconds")]
    [SerializeField] float minuteDuration;
    [Tooltip("Avoid starting on an hour in which the stage of the day changes")]
    [SerializeField] int startHour;

    private int minutes;
    public int Minutes { get { return minutes; } set { minutes = value; OnMinutesChange(value); } }

    private int hours;
    public int Hours { get { return hours; } set { hours = value; OnHoursChange(value); } }

    private int days;
    public int Days { get { return days; } set { days = value; } }

    [Tooltip("Duration in in-game hours")]
    [RangeAttribute(1, 3)]
    [SerializeField] int sunriseToMorningDuration = 3;
    [Tooltip("Duration in in-game hours")]
    [RangeAttribute(1, 6)]
    [SerializeField] int morningToAfternoonDuration = 5;
    [Tooltip("Duration in in-game hours")]
    [RangeAttribute(1, 5)]
    [SerializeField] int afternoonToNightDuration = 4;
    [Tooltip("Duration in in-game hours")]
    [RangeAttribute(1, 2)]
    [SerializeField] int nightToSunriseDuration = 2;

    [SerializeField] private float modSeconds;

    public static UnityAction<Gradient, float> DayPeriodChange;

    [Header("Light Settings")]
    [SerializeField] Gradient sunriseToMorningGradient;
    [SerializeField] Gradient morningToAfternoonGradient;
    [SerializeField] Gradient afternoonToNightGradient;
    [SerializeField] Gradient nightToSunriseGradient;

    [SerializeField] Light globalLight;

    public static event Action OnDayEnded;

    public static TimeManager Instance {get; private set;}

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        Hours = startHour;
    }

    private void Update()
    {
        modSeconds += Time.deltaTime;

        if (modSeconds >= minuteDuration) // FINISH FIXING DAYLENGTH VARIABLE (DAY NOT LASTING DAYLENGTH SECONDS LONG)
        {
            ++Minutes;
            modSeconds = 0;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(days);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log(hours);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log(minutes);
        }
    }

    public int GetDayTimeMinutes()
    {
        return minutes + hours * 60;
    }

    public int GetDayTimeSeconds()
    {
        return (int)(GetDayTimeMinutes() * minuteDuration);
    }

    private void OnMinutesChange(int min)
    {
        if (min >= 60)
        {
            ++Hours;
            minutes = 0;
            //Debug.Log("hour passed" + hours);
        }

        if (hours >= 24)
        {
            ++Days;
            Hours = 0;
        }
    }

    private void OnHoursChange(int hour)
    {
        if (hour == 6)
        {
            StartCoroutine(LerpLight(sunriseToMorningGradient, sunriseToMorningDuration * 60 * minuteDuration));
            //DayPeriodChange.Invoke(sunriseToMorningGradient, sunriseToMorningDuration * 60 * minuteDuration);
        }
        else if (hour == 12)
        {
            StartCoroutine(LerpLight(morningToAfternoonGradient, morningToAfternoonDuration * 60 * minuteDuration));
            //DayPeriodChange.Invoke(morningToAfternoonGradient, morningToAfternoonDuration * 60 * minuteDuration);
        }
        else if (hour == 18)
        {
            StartCoroutine(LerpLight(afternoonToNightGradient, afternoonToNightDuration * 60 * minuteDuration));
            //DayPeriodChange.Invoke(afternoonToNightGradient, afternoonToNightDuration * 60 * minuteDuration);
        }
        else if (hour == 4)
        {
            StartCoroutine(LerpLight(nightToSunriseGradient, nightToSunriseDuration * 60 * minuteDuration));
            //DayPeriodChange.Invoke(nightToSunriseGradient, nightToSunriseDuration * 60 * minuteDuration);

            OnDayEnded?.Invoke();
        }
    }

    private IEnumerator LerpLight(Gradient lightGradient, float duration)
    {
        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            globalLight.color = lightGradient.Evaluate(i / duration);
            //Debug.Log("relative time: " + i + " / " + duration);
            yield return null;
        }
    }
}
