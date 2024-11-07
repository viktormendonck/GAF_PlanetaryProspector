using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI BonusDisplay;
    [SerializeField] private TextMeshProUGUI MoneyDisplay;




    void Start()
    {
        if (GameData.clearedBoard)
        {
            BonusDisplay.text = "500";
            GameData.score += 500;
        }
        else
        {
            BonusDisplay.text = "0";
        }

        MoneyDisplay.text = GameData.money.ToString("0.00");
        GameData.score += GameData.money;
        scoreDisplay.text = GameData.score.ToString("0.00");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
