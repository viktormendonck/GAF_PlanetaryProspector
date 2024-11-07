using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private GameObject EndScreen;
    [SerializeField] private GameObject GameRoot;
    [SerializeField] private Transform EndScreenParent;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float timePerDay = 6;
    private int currentDay = 0;
    [SerializeField] private int maxDay = 100;
    readonly private string textPrefix = "sol ";
    private float currentTimeOfDay = 0;
    private bool hasEnded = false;
    // Update is called once per frame
    void Start()
    {
        text.text = textPrefix + currentDay + "/" + maxDay.ToString();
    }
    void Update()
    {
        currentTimeOfDay += Time.deltaTime;
        if (currentTimeOfDay > timePerDay)
        {
            currentTimeOfDay = 0;
            currentDay++;
            text.text = textPrefix + currentDay + "/" + maxDay.ToString();
        }

        if (currentDay == maxDay+1 && !hasEnded)
        {
            hasEnded = true;
            GameObject go = Instantiate(EndScreen,EndScreenParent);
            EndScript end =go.GetComponent<EndScript>();
            end.Money = GameObject.FindGameObjectWithTag("Money").GetComponent<MoneyContainer>().GetMoney();
            bool temp = false;
            foreach (GameObject ore in GameObject.FindGameObjectsWithTag("Ore"))
            {
                temp |= !ore.GetComponent<VeinInfo>().IsDepleted();
            }
            end.ClearedField = !temp;
            Destroy(GameRoot);

        }
    }
    public int GetCurrentDay()
    {
        return currentDay;
    }
}
