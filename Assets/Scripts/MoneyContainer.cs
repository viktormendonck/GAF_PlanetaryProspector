using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;

public class MoneyContainer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int money;
    [SerializeField] private TextMeshProUGUI text;

    void Start()
    {
    }
    public int GetMoney()
    {
        return money;
    }
    public void AddMoney(int amount)
    {
        money += amount;
        text.text = "$" + money;
    }
}
