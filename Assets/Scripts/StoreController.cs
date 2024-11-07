using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private MoneyContainer moneyContainer;
    private OreContainer oreContainer;
    [SerializeField] private float SalePrice = 10;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TimeManager time;
    [SerializeField] private float volatility = 0.1f;
    private float PreviousOreAmount = 0;
    private int prevDay = 0;
    private System.Random rand;
    void Start()
    {
        if (moneyContainer == null)
        {
            Debug.LogError("no money container assigned");
        }
        if (TryGetComponent(out OreContainer ore))
        {
            oreContainer = ore;
        }
        else
        {
            Debug.LogError("no ore container found in object");
        }
        if (text == null)
        {
            Debug.LogError("no textMeshPro assigned");
        }
        rand = new System.Random();
        SalePrice = 10+((float)rand.NextDouble()*4-2);

        text.text = "$" + SalePrice.ToString("0.00");
    }

    // Update is called once per frame
    void Update()
    {
        float difference = oreContainer.GetCurrentOreAmount() - PreviousOreAmount;
        if (difference > 0)
        {
            moneyContainer.AddMoney(difference * SalePrice);
            PreviousOreAmount = oreContainer.GetCurrentOreAmount();
        }
        UpdateSalePrice();
    }

    private void UpdateSalePrice()
    {
        if(prevDay != time.GetCurrentDay())
        {
            prevDay = time.GetCurrentDay();
            float dailyChange = (float)(rand.NextDouble() * 2 * volatility - volatility);
            SalePrice += SalePrice * dailyChange;
            text.text = "$" + SalePrice.ToString("0.00");
        }
    }
}
