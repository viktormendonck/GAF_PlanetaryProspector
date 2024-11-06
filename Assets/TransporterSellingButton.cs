using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterSellingButton : MonoBehaviour
{
    [SerializeField] private int delta = 0;
    [SerializeField] private TransportHandler handler;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (delta > 0)
            {
                handler.AddSellingTransporter();
            }
            else
            {
                handler.RemoveSellingTransporter();
            }
        }
    }
}
