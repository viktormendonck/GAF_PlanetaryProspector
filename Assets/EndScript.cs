using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public bool ClearedField = false;
    public float Money = 0;

    private float score = 0;
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI BonusDisplay;
    [SerializeField] private TextMeshProUGUI MoneyDisplay;




    void Start()
    {
        if (ClearedField)
        {
            BonusDisplay.text = "500";
            score += 500;
        }
        else
        {
            BonusDisplay.text = "0";
        }

        MoneyDisplay.text = Money.ToString("0.00");
        score += Money;
        scoreDisplay.text = score.ToString("0.00");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
