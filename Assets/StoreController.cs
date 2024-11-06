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
    private float PreviousOreAmount = 0;
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

    }
}
