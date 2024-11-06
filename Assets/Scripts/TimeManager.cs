using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float timePerDay = 6;
    private int currentDay = 0;
    readonly private string textPrefix = "sol ";
    readonly private string textSuffix = "/100";
    private float currentTimeOfDay = 0;
    // Update is called once per frame
    void Update()
    {
        currentTimeOfDay += Time.deltaTime;
        if (currentTimeOfDay > timePerDay)
        {
            currentTimeOfDay = 0;
            currentDay++;
            text.text = textPrefix + currentDay + textSuffix;
        }
    }
    public int GetCurrentDay()
    {
        return currentDay;
    }
}
