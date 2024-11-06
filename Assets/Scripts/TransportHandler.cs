using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class TransportHandler : MonoBehaviour
{
    public List<GameObject> Transporters;
    public List<GameObject> SiloNodes;
    public List<GameObject> MinerNodes;
    [SerializeField] public List<GameObject> marketNodes;
    public int SellingTransporterAmount = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSellingTransporter()
    {
        if (SellingTransporterAmount < Transporters.Count)
        {
            SellingTransporterAmount++;
            foreach (GameObject transporter in Transporters)
            {
                if (transporter.GetComponent<TransporterController>().isSelling == false)
                {
                    transporter.GetComponent<TransporterController>().isSelling = true;
                    transporter.GetComponent<Animator>().SetTrigger("StartSelling");
                    break;
                }
            }
        }

    }

    public void RemoveSellingTransporter()
    {
        if (SellingTransporterAmount > 0)
        {
            SellingTransporterAmount--;
            foreach (GameObject transporter in Transporters)
            {
                if (transporter.GetComponent<TransporterController>().isSelling == true)
                {
                    transporter.GetComponent<TransporterController>().isSelling = false;
                    transporter.GetComponent<Animator>().SetTrigger("StartStoring");
                    break;
                }
            }
        }
    }
}
