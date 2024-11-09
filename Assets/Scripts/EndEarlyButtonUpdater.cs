using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndEarlyButtonUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    private TimeManager timeManager;
    [SerializeField] private int MoneyPerDayLeft;
    [SerializeField] private TextMeshProUGUI text;
    private string textPrefix = "End Early (";
    private string textSuffix = "K$)";

    void Start()
    {
        timeManager = GameObject.FindGameObjectWithTag("Time").GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = textPrefix + MoneyPerDayLeft*(timeManager.GetMaxDay() - timeManager.GetCurrentDay()) + textSuffix;
    }

    public void ButtonPressed()
    {
        GameData.earlyEndScore = MoneyPerDayLeft * (timeManager.GetMaxDay() - timeManager.GetCurrentDay());
        timeManager.EndEarly();
    }
}
