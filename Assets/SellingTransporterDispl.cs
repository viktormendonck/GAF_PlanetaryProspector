using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SellingTransporterDispl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TransportHandler handler;

    // Update is called once per frame
    void Update()
    {
        text.text = handler.SellingTransporterAmount.ToString();
    }
}
