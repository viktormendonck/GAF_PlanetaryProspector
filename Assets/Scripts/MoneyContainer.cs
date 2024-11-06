using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyContainer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float money;
    [SerializeField] private TextMeshProUGUI text;

    void Start()
    {
    }
    public float GetMoney()
    {
        return money;
    }
    public void AddMoney(float amount)
    {
        money += amount;
        text.text = "$" + (int)money + "k";
    }
}
